using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Healthcare_test;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class Client
    {
        //VR_Connector vrc;
        private VRConnector2 vrc;
        private readonly bool _SSL = false;
        private readonly SslStream _sslStream;
        private readonly NetworkStream _stream;
        int port = 1234;
        TcpClient client;
        IPAddress localhost;
        SerialPort serialPort;
        Healthcare_test.ErgometerCOM ergometerCOM;
        string sessionID;
        Thread read;
        Thread getData;
        public Boolean isConnected;
        ErgometerSimulatie simulation;
        int measurement = 0;


        public Client(ClientData clientdata, ErgometerSimulatie simulation, string comport)
        {
            this.simulation = simulation;
            bool ipIsOk = IPAddress.TryParse("127.0.0.1", out localhost);
            if (!ipIsOk)
            {
                Console.WriteLine("ip adres kan niet geparsed worden."); Environment.Exit(1);
            }

            client = new TcpClient(localhost.ToString(), port);
            _stream = client.GetStream();
            if (_SSL)
            {
                _sslStream = new SslStream(_stream, false, new RemoteCertificateValidationCallback(ValidateCert));
                _sslStream.AuthenticateAsClient("Healthcare", null, System.Security.Authentication.SslProtocols.Tls12, false);
            }
            isConnected = true;
            if (comport != null)
            {
                ergometerCOM = new Healthcare_test.ErgometerCOM(comport, "9600");
            }

            read = new Thread(Read);
            read.Start();
            sendlogin(clientdata.username, clientdata.password);
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
                    System.Diagnostics.Debug.WriteLine("Received client: \r\n" + toReturn);
                    ProcessAnswer(toReturn);

                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
            }
        }
        public void ProcessAnswer(string information)
        {
            dynamic jsonData = JsonConvert.DeserializeObject(information);
            if (jsonData.id == "session/start")
            {
                sessionID = (String)jsonData.data.sessionID;
                System.Diagnostics.Debug.WriteLine("sessionID: " + sessionID);
                getData = new Thread(GetData);
                getData.Start();
                Thread.Sleep(2000);
                simulation?.s.startSession();
            }
            if (jsonData.id == "session/end")
            {
                System.Diagnostics.Debug.WriteLine("Closing...");
                isConnected = false;
                close();
            }
            if (jsonData.id == "log in")
            {
                if (jsonData.data.status != "ok")
                {
                    new Thread(() => { MessageBox.Show("Username or password is incorrect"); }).Start();
                    close();
                }
                else
                {
                    vrc = new VRConnector2();
                    new Thread(() => { MessageBox.Show("You are now connected, please put on VR glasses on now"); }).Start();
                }

            }
            if (jsonData.id == "client/message")
            {
                HandleNewMessageFromDoctor((string)jsonData.data.message);
            }
            if (jsonData.id == "client/SetPower")
            {
                ergometerCOM?.SetPower((int)jsonData.data.power);
                simulation?.SetPower((int)jsonData.data.power);
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

        public void GetData()
        {
            while (sessionID == null)
            {
                Thread.Sleep(100);
            }
            while (isConnected)
            {
                measurement++;
                if (simulation == null && ergometerCOM != null)
                {
                    System.Diagnostics.Debug.WriteLine(isConnected);
                    Healthcare_test.ErgometerData ergometerData = ergometerCOM.GetData();
                    vrc.UpdateBikePanelInVR(ergometerData);
                    dynamic ergometerdata = new
                    {
                        id = "data",
                        session = sessionID,
                        data = new
                        {
                            power = ergometerData.Actual_Power,
                            speed = ergometerData.Speed,
                            time = ergometerData.Time,
                            RPM = ergometerData.RPM,
                            distance = ergometerData.Distance,
                            pulse = ergometerData.Pulse
                        }

                    };
                    if (measurement >= 10)
                    {
                        Send(JsonConvert.SerializeObject(ergometerdata));
                        measurement = 0;
                    }
                }
                else if (simulation != null)
                {
                    System.Diagnostics.Debug.WriteLine(isConnected);
                    Healthcare_test.ErgometerData ergometerData2 = simulation.GetData();
                    vrc.UpdateBikePanelInVR(ergometerData2);
                    dynamic ergometerdata2 = new
                    {
                        id = "data",
                        session = sessionID,
                        data = new
                        {
                            power = ergometerData2.Actual_Power,
                            speed = ergometerData2.Speed,
                            time = ergometerData2.Time,
                            RPM = ergometerData2.RPM,
                            distance = ergometerData2.Distance,
                            pulse = ergometerData2.Pulse
                        }

                    };
                    if (measurement >= 10)
                    {
                        Send(JsonConvert.SerializeObject(ergometerdata2));
                        measurement = 0;
                    }
                }
                Thread.Sleep(100);
            }


        }

        public void sendlogin(String username, String password)
        {
            dynamic sendlogin = new
            {
                id = "log in",
                data = new
                {
                    username = username,
                    password = password
                }
            };
            Send(JsonConvert.SerializeObject(sendlogin));
        }

        public void HandleNewMessageFromDoctor(string message)
        {
            vrc.HandeMessageFromDoctor(message);
        }

        public void close()
        {
            if (simulation != null)
            {
                simulation.Close();
            }
            if (ergometerCOM != null)
            {
                ergometerCOM.Close();
            }
            try
            {
                read.Abort();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("error: " + e.Message);
            }
            try
            {
                getData.Abort();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("error: " + e.Message);
            }
            try
            {
                if (_SSL)
                {
                    _sslStream.Close();
                }
                else
                {
                    _stream.Close();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("error: " + e.Message);
            }
            try
            {
                client.Close();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("error: " + e.Message);
            }



        }

        public static bool ValidateCert(object sender, X509Certificate certificate,
              X509Chain chain, SslPolicyErrors sslPolicyErrors) => sslPolicyErrors == SslPolicyErrors.None;
    }
}

