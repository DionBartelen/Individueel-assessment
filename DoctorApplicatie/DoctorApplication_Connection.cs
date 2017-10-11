using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoctorApplicatie
{
    public class DoctorApplication_Connection
    {
        private bool _SSL = false;
        readonly SslStream _sslStream;
        readonly NetworkStream _stream;
        int port = 1234;
        TcpClient client;
        IPAddress localhost;
        Boolean isConnected;
        DoctorApplication_Session doctorApplication_Session;
        DoctorAplicatie application;
        public DoctorApplication_Connection(string username, string password, DoctorAplicatie application)
        {
            this.application = application;
            bool ipIsOk = IPAddress.TryParse("127.0.0.1", out localhost);
            if (!ipIsOk) { Console.WriteLine("ip adres kan niet geparsed worden."); Environment.Exit(1); }
            client = new TcpClient(localhost.ToString(), port);
            _stream = client.GetStream();
            if (_SSL)
            {
                _sslStream = new SslStream(_stream, false, new RemoteCertificateValidationCallback(ValidateCert));
                _sslStream.AuthenticateAsClient("Healthcare", null, System.Security.Authentication.SslProtocols.Tls12,
                    false);
            }
            isConnected = true;
            Thread read = new Thread(Read);
            read.Start();
            sendLogin(username, password);
        }

        public void Read()
        {
            while (isConnected)
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
                    System.Diagnostics.Debug.WriteLine("Received: \r\n" + toReturn);
                    ProcessAnswer(toReturn);

                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.StackTrace);
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
            }
        }
        public void ProcessAnswer(string information)
        {
            
            dynamic jsonData = JsonConvert.DeserializeObject(information);
            if (jsonData.id == "doctor/login")
            {
                if (jsonData.data.status == "ok")
                {
                    doctorApplication_Session = new DoctorApplication_Session(this);
                    application.RunSessionForm(doctorApplication_Session);
                }
                else
                {
                    new Thread(() => { MessageBox.Show("Wrong username and password"); }).Start();
                }
            }
            else if (jsonData.id == "doctor/sessions")
            {
                List<String> connected_Sessions = new List<string>();
                foreach (dynamic d in jsonData.data.sessions)
                {
                    connected_Sessions.Add((string)d);
                }
                doctorApplication_Session.UpdateComboBox(connected_Sessions);
            }
            else if (jsonData.id == "doctor/sessions/users")
            {
                List<string> users = new List<string>();
                foreach (dynamic e in jsonData.data.users)
                {
                    if (e != null)
                    {
                        users.Add((string)e);
                    }
                }
                doctorApplication_Session.UpdateOlderDataComboBox(users);
            }
            else if (jsonData.id == "session/data/historic")
            {
                HandleHistoricData(jsonData);
            }
            else if(jsonData.id == "data")
            {

                string session = (string)jsonData.sessionId;
                int power = jsonData.data.data.Requested_Power;
                double speed = jsonData.data.data.Speed;
                int time = jsonData.data.data.Time;
                int rpm = jsonData.data.data.RPM;
                double distance = jsonData.data.data.Distance;
                int pulse = jsonData.data.data.Pulse;
                ErgometerData data = new ErgometerData(pulse, rpm, speed, distance, time, 0, 0, power);
                checkData(session, data);

            }
            else if(jsonData.id == "doctor/UnfollowPatient")
            {
                
            }

        }

        public void Send(string message)
        {
            System.Diagnostics.Debug.WriteLine("Send: \r\n" + message);
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

        public void sendLogin(string username, string password)
        {
            dynamic sendLogin = new
            {
                id = "doctor/login",
                data = new
                {
                    username = username,
                    password = password
                }

            };

            Send(JsonConvert.SerializeObject(sendLogin));
        }

        public void startTraining(String patientID)
        {
            dynamic startTraining = new
            {
                id = "doctor/training/start",
                data = new
                {
                    patientId = patientID
                }

            };

            Send(JsonConvert.SerializeObject(startTraining));

        }

        public void stopTraining(String patientID)
        {
            dynamic stopTraining = new
            {
                id = "doctor/training/stop",
                data = new
                {
                    patientId = patientID

                }


            };
            Send(JsonConvert.SerializeObject(stopTraining));


        }

        public void sendMessageToClient(String message, String patientID)
        {
            dynamic sendMessageToClient = new
            {
                id = "doctor/message/toClient",
                data = new
                {
                    messageId = message,
                    patientiD = patientID


                }

            };
            Send(JsonConvert.SerializeObject(sendMessageToClient));


        }

        public void sendMessagetoAllClients(String message)
        {
            dynamic sendMessageToAllClients = new
            {
                id = "doctor/message/toAll",
                data = new
                {
                    messageId = message,

                }

            };
            Send(JsonConvert.SerializeObject(sendMessageToAllClients));

        }

        public void setPower(string power, String patientID)
        {
            dynamic setPower = new
            {
                id = "doctor/setPower",
                data = new
                {
                    power = power,
                    patientID = patientID
                }

            };

            Send(JsonConvert.SerializeObject(setPower));

        }
        public void getSessions()
        {
            dynamic getSessions = new
            {
                id = "doctor/sessions"

            };
            Send(JsonConvert.SerializeObject(getSessions));
        }

        public void GetUsers()
        {
            dynamic request = new
            {
                id = "doctor/sessions/users"
            };
            Send(JsonConvert.SerializeObject(request));
        }

        public void getOlderData(string username)
        {
            dynamic request = new
            {
                id = "session/data/historic",
                data = new
                {
                    patientID = username
                }
            };
            Send(JsonConvert.SerializeObject(request));
        }

        public void HandleHistoricData(dynamic jsonObject)
        {
            List<TrainSession> trainsessions = new List<TrainSession>();
            foreach (dynamic tSession in jsonObject.data)
            {
                TrainSession trainsession = new TrainSession();
                foreach (dynamic EData in tSession.data)
                {
                    trainsession.AddData(new ErgometerData((int)EData.Pulse, (int)EData.RPM, (double)EData.Speed, (double)EData.Distance, (int)EData.Time, (int)EData.Energy, (int)EData.Actual_Power, (int)EData.Requested_Power));
                }
                trainsessions.Add(trainsession);
            }
            DoctorApplication_Trainsessions TrainsessionsForm = new DoctorApplication_Trainsessions();
            TrainsessionsForm.SetAllSessions(trainsessions);
            doctorApplication_Session.RunTrainSessionForm(TrainsessionsForm);

        }
            
        public void FollowPatient(string SessionId)
        {
            dynamic followPatient = new
            {
                id = "doctor/FollowPatient",
                data = new
                {
                    username = SessionId
                }

            };

            Send(JsonConvert.SerializeObject(followPatient));

        }

        public void UnFollowPatient(string SessionId)
        {
            dynamic unFollowPatient = new
            {
                id = "doctor/UnfollowPatient",
                data = new 
                {
                    username = SessionId
                }

            };

            Send(JsonConvert.SerializeObject(unFollowPatient));

        }

        public void close()
        {
            //stream.Close();
            _sslStream.Close();
            client.Close();
        }


        public static bool ValidateCert(object sender, X509Certificate certificate,
              X509Chain chain, SslPolicyErrors sslPolicyErrors) => sslPolicyErrors == SslPolicyErrors.None;

        public void checkData(string dataSessionId, ErgometerData data)
        { 
            
            foreach (DoctorApplication_SessionClient s in doctorApplication_Session.followed_sessions)
            {
                if(s.sessionID == dataSessionId)
                {
                    s.currentData.Add(data);
                    s.updateChart();
                }
            }
        }

    }

}

