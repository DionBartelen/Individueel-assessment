﻿using System;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace Server
{
    class Session
    {
        private bool _SSL = false;
        private readonly NetworkStream _stream;
        readonly SslStream _sslStream;
        public Boolean IsDoctor;
        public string Username;
        public List<Session> DoctorsToSendDataTo;

        public Session(TcpClient client)
        {
            _stream = client.GetStream();
            if (_SSL)
            {
                var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadOnly);
                X509Certificate2 certificate = store.Certificates.Find(X509FindType.FindByThumbprint, "52F29B382FD556BE6B75B9F026470A1609186C64", false)[0];
                store.Close();
                _sslStream = new SslStream(_stream, false);
                _sslStream.AuthenticateAsServer(certificate, false, SslProtocols.Tls12, true);
            }
            DoctorsToSendDataTo = new List<Session>();
        }

        //Send to networkstream
        #region
        public void Send(string message)
        {
            System.Diagnostics.Debug.WriteLine("Send from server: \r\n" + message);
            byte[] prefixArray = BitConverter.GetBytes(message.Length);
            byte[] requestArray = Encoding.Default.GetBytes(message);
            byte[] buffer = new Byte[prefixArray.Length + message.Length];
            prefixArray.CopyTo(buffer, 0);
            requestArray.CopyTo(buffer, prefixArray.Length);
            if (_SSL)
            {
                _sslStream.Write(buffer, 0, buffer.Length);
            }
            else
            {
                _stream.Write(buffer, 0, buffer.Length);
            }
        }
        #endregion

        //Read from networkstream
        #region
        public void Read()
        {
            while (true)
            {
                try
                {
                    StringBuilder response = new StringBuilder();
                    int totalBytesreceived = 0;
                    int lengthMessage = -1;
                    byte[] receiveBuffer = new byte[1024];
                    bool messagereceived = false;

                    do
                    {
                        int numberOfBytesRead = _SSL ? _sslStream.Read(receiveBuffer, 0, receiveBuffer.Length) : _stream.Read(receiveBuffer, 0, receiveBuffer.Length);
                        totalBytesreceived += numberOfBytesRead;
                        string received = Encoding.ASCII.GetString(receiveBuffer, 0, numberOfBytesRead);
                        response.AppendFormat("{0}", received);
                        if (lengthMessage == -1)
                        {
                            if (receiveBuffer.Length >= 4)
                            {
                                Byte[] lengthMessageArray = new Byte[4];
                                Array.Copy(receiveBuffer, 0, lengthMessageArray, 0, 3);
                                lengthMessage = BitConverter.ToInt32(lengthMessageArray, 0);
                                if ((totalBytesreceived - 4) == lengthMessage)
                                {
                                    messagereceived = true;
                                }
                            }
                        }
                        else if ((totalBytesreceived - 4) == lengthMessage)
                        {
                            messagereceived = true;
                        }
                    }
                    while (!messagereceived);
                    if (_SSL)
                    {
                        _sslStream.Flush();
                    }
                    else
                    {
                        _stream.Flush();
                    }

                    string toReturn = response.ToString().Substring(4);
                    System.Diagnostics.Debug.WriteLine("Received at server: \r\n" + toReturn);
                    ProcesAnswer(toReturn);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
            }
        }
        #endregion

        //Process answer
        #region
        public void ProcesAnswer(dynamic answer)
        {
            dynamic jsonObject = JsonConvert.DeserializeObject(answer);
            try
            {
                if (jsonObject.id == "log in")
                {
                    CheckCredentials((string)jsonObject.data.username, (string)jsonObject.data.password);
                }
                else if (jsonObject.id == "session/start")
                {
                    NoPermission("session/start");
                }
                else if (jsonObject.id == "data")
                {
                    DataRecieved(jsonObject);
                }
                else if (jsonObject.id == "start")
                {

                }
                else if (jsonObject.id == "pauze")
                {

                }
                else if (jsonObject.id == "session/end")
                {
                    NoPermission("session/end");
                }
                else if (jsonObject.id == "StopAstrand")
                {
                    if (jsonObject.data.status == "ok")
                    {
                        string patient = (string) jsonObject.data.patientId;
                        int age = (int) jsonObject.data.data.age;
                        string sex = (string) jsonObject.data.data.sex;
                        double weight = (double) jsonObject.data.data.weight;
                        double vo2 = (double) jsonObject.data.data.vo2Max;
                        double avgPulse = (double) jsonObject.data.data.avgPulse;
                        if (CloseSession(patient, age, sex, vo2, avgPulse, weight))
                        {
                            Database.Close();
                        }
                    } else if (jsonObject.data.status == "error")
                    {
                        foreach (Session doctor in DoctorsToSendDataTo)
                        {
                            doctor.Send(JsonConvert.SerializeObject(jsonObject));
                        }
                        Session s = Program.GetSessionWithUsername((string)jsonObject.data.patientId);
                        if (s != null)
                        {
                            Program.ErrorWithSession(s);
                        }
                    }
                    else
                    {
                        Session s = Program.GetSessionWithUsername((string) jsonObject.data.patientId);
                        if (s != null)
                        {
                            Program.ErrorWithSession(s);
                        }
                    }
                }
                else if (jsonObject.id == "doctor/login")
                {
                    CheckDoctorCredentials((string)jsonObject.data.username, (string)jsonObject.data.password);
                }
                else if (jsonObject.id == "doctor/sessions")
                {
                    SessionList();
                }
                else if (jsonObject.id == "session/data/historic")
                {
                    GetDataFromUser(jsonObject);
                }
                else if (jsonObject.id == "doctor/training/start")
                {
                    string patient = jsonObject.data.patientId;
                    if (CreateNewSession(patient, true))
                    {
                        dynamic response = new
                        {
                            id = "doctor/training/start",
                            data = new
                            {
                                status = "ok"
                            }
                        };
                        Send(JsonConvert.SerializeObject(response));
                    }
                }
                else if (jsonObject.id == "doctor/training/stop")
                {
                    string patient = jsonObject.data.patientId;
                    if (CloseSession(patient, -1, "Unknown", -1, -1, -1))
                    {
                        dynamic response = new
                        {
                            id = "doctor/training/stop",
                            data = new
                            {
                                status = "ok"
                            }
                        };
                        Send(JsonConvert.SerializeObject(response));
                        Database.Close();
                    }
                }
                else if (jsonObject.id == "doctor/message/toClient")
                {
                    string patientId = (string)jsonObject.data.patientiD;
                    string message = (string)jsonObject.data.messageId;
                    SendMessageToSingleClient(patientId, message);
                }
                else if (jsonObject.id == "doctor/message/toAll")
                {
                    SendMessageToAllClients(jsonObject);
                }
                else if (jsonObject.id == "doctor/setPower")
                {
                    if (SetPowerFromClient(jsonObject))
                    {
                        dynamic response = new
                        {
                            id = "doctor/setPower",
                            data = new
                            {
                                status = "ok",
                            }
                        };
                        Send(response);
                    }
                }
                else if (jsonObject.id == "doctor/sessions/users")
                {
                    GetUsernamesInDb();
                }
                else if (jsonObject.id == "doctor/FollowPatient")
                {
                    if (IsDoctor)
                    {
                        FolowAPatientSession((string)jsonObject.data.username);
                    }
                    else
                    {
                        NoPermission("doctor/FollowPatient");
                    }
                }
                else if (jsonObject.id == "doctor/UnfollowPatient")
                {
                    if (IsDoctor)
                    {
                        UnFollowAPatientSession((string)jsonObject.data.username);
                    }
                    else
                    {
                        NoPermission("doctor/FollowPatient");
                    }
                }
                else if (jsonObject.id == "doctor/StartAstrand")
                {
                    if (IsDoctor)
                    {
                        string client = (string)jsonObject.data.client;
                        CreateNewSession(client, false);
                        StartAstrandFromPatient(client);
                    }
                    else
                    {
                        NoPermission("doctor/StartAstrand");
                    }
                }
            }
            catch (Exception e)
            {
                dynamic error = new
                {
                    id = "command",
                    status = "Error",
                    error = e.Message
                };
                Send(JsonConvert.SerializeObject(error));
            }
        }
        #endregion

        //Login normal and doctor.
        #region
        public void CheckCredentials(string username, string password)
        {
            if (Database.CheckCredentials(username, password))
            {
                Username = username;
                Send(JsonConvert.SerializeObject(Commands.LoginResponse("ok")));
            }
            else
            {
                Send(JsonConvert.SerializeObject(Commands.LoginResponse("error")));
            }
        }

        public void CheckDoctorCredentials(string username, string password)
        {
            if (Database.CheckDoctorCredentials(username, password))
            {
                IsDoctor = true;
                Username = username;
                Send(JsonConvert.SerializeObject(Commands.DoctorLoginResponse("ok")));
            }
            else
            {
                Send(JsonConvert.SerializeObject(Commands.DoctorLoginResponse("error")));
            }
        }
        #endregion

        //Create new session.
        #region
        public Boolean CreateNewSession(string username, Boolean responseStartSession)
        {
            if (!IsDoctor)
            {
                NoPermission("doctor/training/start");
                return false;
            }
            else
            {
                Session client = Program.GetSessionWithUsername(username);
                if (client == null)
                {
                    Send(JsonConvert.SerializeObject(Commands.DoctorTrainingStartError("No client active with given username.")));
                    return false;
                }
                try
                {
                    if (Database.AddActiveSession(username))
                    {
                        dynamic answer = new
                        {
                            id = "session/start",
                            data = new
                            {
                                status = "OK",
                                sessionID = username
                            }
                        };
                        if (responseStartSession)
                        {
                            client.Send(JsonConvert.SerializeObject(answer));
                        }
                        return true;
                    }
                    else
                    {
                        Send(JsonConvert.SerializeObject(Commands.DoctorTrainingStartError("Username already active. Other session has to be stopped first before starting a new one.")));
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Send(JsonConvert.SerializeObject(Commands.DoctorTrainingStartError(e.Message)));
                    return false;
                }
            }
        }
        #endregion

        //Return list with active sessions to doctor
        #region
        public void SessionList()
        {
            if (!IsDoctor)
            {
                NoPermission("doctor/sessions");
            }
            else
            {
                List<Session> sessions = Program.GetAllPatients();
                List<string> sessionNames = new List<string>();
                foreach (Session s in sessions)
                {
                    sessionNames.Add(s.Username);
                }
                dynamic response = new
                {
                    id = "doctor/sessions",
                    data = new
                    {
                        sessions = sessionNames.ToArray()
                    }
                };
                Console.WriteLine(JsonConvert.SerializeObject(response));
                Send(JsonConvert.SerializeObject(response));
            }
        }
        #endregion

        //Recieved data from patient
        #region
        public void DataRecieved(dynamic jsonObject)
        {
            try
            {
                string session = (string)jsonObject.session;
                int power = jsonObject.data.power;
                double speed = jsonObject.data.speed;
                int time = jsonObject.data.time;
                int rpm = jsonObject.data.RPM;
                double distance = jsonObject.data.distance;
                int pulse = jsonObject.data.pulse;
                ErgometerData data = new ErgometerData(pulse, rpm, speed, distance, time, 0, 0, power);
                Boolean added = Database.AddErgometerDataToSession(session, data);
                if (added)
                {
                    dynamic answer = new
                    {
                        id = "data",
                        data = new
                        {
                            status = "OK"
                        }
                    };
                    Send(JsonConvert.SerializeObject(answer));
                    dynamic answerToDoctor = new
                    {
                        id = "data",
                        sessionId = session,
                        data = new
                        {
                            data = data
                        }
                    };
                    foreach (Session s in DoctorsToSendDataTo)
                    {
                        s.Send(JsonConvert.SerializeObject(answerToDoctor));
                    }
                }
                else
                {
                    dynamic answer = new
                    {
                        id = "data",
                        data = new
                        {
                            status = "ERROR",
                            error = "Geen sessie voor username bekend"
                        }
                    };
                    Send(JsonConvert.SerializeObject(answer));
                }

            }
            catch (Exception e)
            {
                dynamic answer = new
                {
                    id = "data",
                    status = "Error",
                    error = e.Message
                };
                Send(JsonConvert.SerializeObject(answer));
            }
        }
        #endregion

        //Close session
        #region
        public Boolean CloseSession(string sessionId, int age, string sex, double vo2, double avgPulse, double weight)
        {
            Session client = Program.GetSessionWithUsername(sessionId);
            if (client == null)
            {
                Send(JsonConvert.SerializeObject(Commands.DoctorTrianingStopError("No client active with given username.")));
                return false;
            }
            try
            {

                Database.CloseActiveSession(sessionId, age, sex, vo2, avgPulse, weight);
                dynamic answer = new
                {
                    id = "session/end",
                    data = new
                    {
                        status = "OK"
                    }
                };
                client.Send(JsonConvert.SerializeObject(answer));
                Program.sessions.Remove(client);
                return true;
            }
            catch (Exception e)
            {
                Send(JsonConvert.SerializeObject(Commands.DoctorTrianingStopError(e.Message)));
                return false;
            }
        }
        #endregion

        //Historic usernames
        #region
        public void GetUsernamesInDb()
        {
            string[] allUsers = Database.GetUsers();
            dynamic response = new
            {
                id = "doctor/sessions/users",
                data = new
                {
                    users = allUsers
                }
            };
            Send(JsonConvert.SerializeObject(response));
        }
        #endregion

        //Historic data
        #region
        public void GetDataFromUser(dynamic jsonObject)
        {
            if (IsDoctor)
            {
                TrainSession[] dataFromUser = Database.GetDataFromUser((string)jsonObject.data.patientID);
                dynamic response = new
                {
                    id = "session/data/historic",
                    data = dataFromUser
                };
                Send(JsonConvert.SerializeObject(response));
            }
            else
            {
                NoPermission("session/data/historic");
            }
        }
        #endregion

        //Send message to all clients
        #region
        public void SendMessageToAllClients(dynamic jsonObject)
        {
            List<Session> sessions = Program.GetAllPatients();
            foreach (Session s in sessions)
            {
                SendMessageToSingleClient(s.Username, (string)jsonObject.data.messageId);
            }
        }
        #endregion

        //Send message to single client
        #region
        public void SendMessageToSingleClient(string user, string messageFromClient)
        {
            List<Session> sessions = Program.GetAllPatients();
            foreach (Session s in sessions)
            {
                if (s.Username == user)
                {
                    dynamic answer = new
                    {
                        id = "client/message",
                        data = new
                        {
                            message = messageFromClient
                        }
                    };
                    s.Send(JsonConvert.SerializeObject(answer));
                }
            }
        }
        #endregion

        //Set power from client
        #region
        public Boolean SetPowerFromClient(dynamic jsonObject)
        {
            if (!IsDoctor)
            {
                NoPermission("doctor/setPower");
                return false;
            }
            else
            {
                string patientId = jsonObject.data.patientID;
                Session client = Program.GetSessionWithUsername(patientId);
                if (client == null)
                {
                    Send(JsonConvert.SerializeObject(Commands.SetPowerError("No client active with given username.")));
                    return false;
                }
                else
                {
                    dynamic power = new
                    {
                        id = "client/SetPower",
                        data = new
                        {
                            power = jsonObject.data.power
                        }
                    };
                    client.Send(JsonConvert.SerializeObject(power));
                    return true;
                }
            }
        }
        #endregion

        //Follow and unfollow a patient session
        #region
        public void FolowAPatientSession(string username)
        {
            try
            {
                Session clientToListenTo = Program.GetSessionWithUsername(username);
                if (clientToListenTo != null)
                {
                    clientToListenTo.DoctorsToSendDataTo.Add(this);
                    dynamic answer = new
                    {
                        id = "doctor/FollowPatient",
                        data = new
                        {
                            status = "ok"
                        }
                    };
                    Send(JsonConvert.SerializeObject(answer));
                }
                else
                {
                    Send(JsonConvert.SerializeObject(Commands.FollowPatientError("Patient not active")));
                }
            }
            catch (Exception e)
            {
                Send(JsonConvert.SerializeObject(Commands.FollowPatientError(e.Message)));
            }
        }

        public void UnFollowAPatientSession(string username)
        {
            try
            {
                Session clientToListenTo = Program.GetSessionWithUsername(username);
                if (clientToListenTo != null)
                {
                    if (clientToListenTo.DoctorsToSendDataTo.Contains(this))
                    {
                        clientToListenTo.DoctorsToSendDataTo.Remove(this);
                        dynamic answer = new
                        {
                            id = "doctor/UnfollowPatient",
                            data = new
                            {
                                status = "ok"
                            }
                        };
                        Send(JsonConvert.SerializeObject(answer));
                    }
                }
                else
                {
                    Send(JsonConvert.SerializeObject(Commands.UnFollowPatientError("Patient not found")));
                }
            }
            catch (Exception e)
            {
                Send(JsonConvert.SerializeObject(Commands.FollowPatientError(e.Message)));
            }
        }
        #endregion

        //StartAstrand from patient
        #region
        public void StartAstrandFromPatient(string patientId)
        {
            try
            {

                Session clientToStart = Program.GetSessionWithUsername(patientId);
                if (clientToStart != null)
                {
                    dynamic answer = new
                    {
                        id = "StartAstrand",
                        data = new
                        {
                            sessionId = patientId
                        }
                    };
                    clientToStart.Send(JsonConvert.SerializeObject(answer));
                    dynamic answerToDocter = new
                    {
                        id = "Doctor/StartAstrand",
                        data = new
                        {
                            status = "ok"
                        }
                    };
                    Send(JsonConvert.SerializeObject(answerToDocter));
                    Database.AddActiveSession(patientId);
                }
                else
                {
                    Send(JsonConvert.SerializeObject(Commands.StartAstrandError("Patient not found")));
                }
            }
            catch (Exception e)
            {
                Send(JsonConvert.SerializeObject(Commands.StartAstrandError(e.Message)));
            }
        }
        #endregion

        //NoPermission
        #region
        public void NoPermission(string idAnswer)
        {
            dynamic answer = new
            {
                id = idAnswer,
                status = "Error",
                error = "You do not have permission for this action"
            };
            Send(JsonConvert.SerializeObject(answer));
        }
        #endregion
    }
}
