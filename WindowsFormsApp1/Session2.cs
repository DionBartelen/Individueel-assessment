using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WindowsFormsApp1
{
    public class Session2
    {
        private TcpClient client;
        private NetworkStream stream;
        public Terrain terrain;
        private VRConnector2 vrc2;
        public string folder = "";

        public Session2(string ip, int port, VRConnector2 vrc2)
        {
            terrain = new Terrain(new int[256], new int[256]);
            client = new TcpClient();
            client.Connect(ip, port);
            stream = client.GetStream();
            this.vrc2 = vrc2;
        }

        //Send to VREngine
        #region
        public void Send(string message)
        {
            System.Diagnostics.Debug.WriteLine("Send: \r\n" + message);
            byte[] prefixArray = BitConverter.GetBytes(message.Length);
            byte[] requestArray = Encoding.Default.GetBytes(message);
            byte[] buffer = new Byte[prefixArray.Length + message.Length];
            prefixArray.CopyTo(buffer, 0);
            requestArray.CopyTo(buffer, prefixArray.Length);
            stream.Write(buffer, 0, buffer.Length);
            Save(message);
        }
        #endregion

        //Save Response from VREngine
        #region
        public void Save(string message)
        {
            dynamic jsonData = JsonConvert.DeserializeObject(message);
            Console.WriteLine(message);
            if (jsonData.id == "tunnel/send")
            {
                if (jsonData.data.data.id == "scene/node/add")
                {
                    int[] aPosition = new int[3];
                    int i = 0;

                    int[] aRotation = new int[3];
                    int j = 0;

                    foreach (var item in jsonData.data.data.data.components.transform.position)
                    {
                        aPosition[i] = jsonData.data.data.data.components.transform.position[i];
                        i++;
                    }

                    foreach (var item in jsonData.data.data.data.components.transform.rotation)
                    {
                        aRotation[j] = jsonData.data.data.data.components.transform.rotation[j];
                        j++;
                    }
                    terrain.nodes.Add(new Node((string)jsonData.data.data.data.name,
                        null,
                        aPosition,
                        (int)jsonData.data.data.data.components.transform.scale,
                        aRotation));
                }
                else if (jsonData.data.data.id == "scene/road/add")
                {
                    terrain.road.Add(new Road((string)jsonData.data.data.data.route,
                        (float)jsonData.data.data.data.heightoffset));
                }
                else if (jsonData.data.data.id == "route/add")
                {
                    List<Node> nodes = new List<Node>();
                    foreach (dynamic item in jsonData.data.data.data.nodes)
                    {
                        int[] aPosition = new int[3];
                        int i = 0;

                        int[] aRotation = new int[3];
                        int j = 0;
                        foreach (dynamic coordinate in item.pos)
                        {
                            aPosition[i] = coordinate;
                            i++;
                        }

                        foreach (var rot in item.dir)
                        {
                            aRotation[j] = rot;
                            j++;
                        }

                        nodes.Add(new Node("",
                        null,
                        aPosition,
                        0,
                        aRotation));
                    }
                    terrain.route.Add(new Route("", nodes));
                }
                else if (jsonData.data.data.id == "route/follow")
                {

                }
            }
        }
        #endregion

        //Read from VREngine
        #region
//        public void Read()
//        {
//            while (true)
//            {
//                try
//                {
//                    StringBuilder response = new StringBuilder();
//                    int totalBytesreceived = 0;
//                    int lengthMessage = -1;
//                    byte[] receiveBuffer = new byte[1024];
//                    bool messagereceived = false;
//
//                    do
//                    {
//                        int numberOfBytesRead = stream.Read(receiveBuffer, 0, receiveBuffer.Length);
//                        totalBytesreceived += numberOfBytesRead;
//                        string received = Encoding.ASCII.GetString(receiveBuffer, 0, numberOfBytesRead);
//                        response.AppendFormat("{0}", received);
//                        System.Diagnostics.Debug.WriteLine("Totalbytes received: " + totalBytesreceived + "   lengthmessage: " + lengthMessage + "   received: " + messagereceived);
//                        if (lengthMessage == -1)
//                        {
//                            if (receiveBuffer.Length >= 4)
//                            {
//                                Byte[] lengthMessageArray = new Byte[4];
//                                Array.Copy(receiveBuffer, 0, lengthMessageArray, 0, 3);
//                                lengthMessage = BitConverter.ToInt32(lengthMessageArray, 0);
//                            }
//                        }
//                        if ((totalBytesreceived - 4) >= lengthMessage)
//                        {
//                            messagereceived = true;
//                        }
//                    } while (!messagereceived);
//                    stream.Flush();
//                    string toReturn = response.ToString().Substring(4);
//                    System.Diagnostics.Debug.WriteLine("Received: \r\n" + toReturn);
//                    ProcessAnswer(toReturn);
//                }
//                catch (Exception e)
//                {
//                    System.Diagnostics.Debug.WriteLine("Error while reading from VREngine: " + e.Message + "\r\n" + e.StackTrace);
//                    stream.Flush();
//                }
//            }
//        }
        #endregion

        public void Read()
        {
            while (true)
            {
                try
                {
                    GetNextMsg();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Error while reading from VREnginge: " + e.StackTrace);
                }
            }
        }

        private void GetNextMsg()
        {
            byte[] prefixArray = new byte[4];
            stream.Read(prefixArray, 0, 4);
            int msgLength = BitConverter.ToInt32(prefixArray, 0);
            //if (verbose) Console.WriteLine($"Received a {msgLength} bit long message.");
            byte[] responseArray = new byte[msgLength];
            int bytesRead = 0;
            while (bytesRead < msgLength)
            {
                bytesRead += stream.Read(responseArray, bytesRead, responseArray.Length - bytesRead);
            }
            string response = Encoding.Default.GetString(responseArray);
            System.Diagnostics.Debug.WriteLine("Received: \r\n"+ response);
            ProcessAnswer(response);
        }

        //Process answer from the VREngine, Hier moeten nog send's uit !!!!!!!!!!!!
        #region
        public void ProcessAnswer(string answer)
        {
            dynamic jsonData = JsonConvert.DeserializeObject(answer);
            if (jsonData.id == "session/list")
            {
                ProcessSessionList(jsonData.data);
            }
            else if (jsonData.id == "tunnel/create")
            {
                ProcessTunnelCreate(jsonData.data);
            }
            else if (jsonData.id == "tunnel/send")
            {
                if (jsonData.data.data.id == "route/add")
                {
                    //terrain.RouteNameReceived((string)jsonData.data.data.data.uuid);
                    terrain.UuidRoute = (string)jsonData.data.data.data.uuid;
                }
                else if (jsonData.data.data.id == "pause")
                {
                    if (jsonData.data.data.status == "ok")
                    {
                        terrain.pauze = true;
                    }
                }
                else if (jsonData.data.data.id == "scene/node/add" && jsonData.data.data.status == "ok")
                {
                    //terrain.NodeNameReceived((string)jsonData.data.data.data.uuid);
                    if (jsonData.data.data.data.name == "terrain")
                    {
                        terrain.UuidTerrainNode = (string)jsonData.data.data.data.uuid;
                    }
                }
                else if (jsonData.id == "packetid")
                {
                }
                else if (jsonData.id == "route/add")
                {
                }
                else if (jsonData.data.data.id == "scene/terrain/add" && jsonData.data.data.status == "ok")
                {
                    terrain.terainAdded = true;
                }
                else if (jsonData.data.data.id == "scene/node/find")
                {
                    try
                    {
                        string uuid = jsonData.data.data.data[0].uuid;
                        if (jsonData.data.data.data[0].name == "GroundPlane")
                        {
                            terrain.UuidGroundPlane = uuid;
                        }
                        else if (jsonData.data.data.data[0].name == "terrain")
                        {
                            //niet de bedoeling
                            //SetupTexturesTerrain(uuid);
                            terrain.UuidTerrainNode = uuid;
                        }
                        else if (jsonData.data.data.data[0].name == "Road")
                        {
                            terrain.UuidRoadNode = uuid;
                        }
                        else if (jsonData.data.data.data[0].name == "MainBike")
                        {
                            terrain.UuidMainBike = uuid;
                        }
                        else if (jsonData.data.data.data[0].name == "Head")
                        {
                            terrain.UuidHead = uuid;
                        }
                        else if (jsonData.data.data.data[0].name == "Camera")
                        {
                            terrain.UuidCamera = uuid;
                        }
                        else if (jsonData.data.data.data[0].name == "BikePanel")
                        {
                            terrain.UuidStatsPanel = uuid;
                        }
                        else if (jsonData.data.data.data[0].name == "MessagePanel")
                        {
                            terrain.UuidMessagePanel = uuid;
                        }
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
        }

        #endregion

        //setup Textures in Terrain moet eigenlijk weg uit deze klasse !!!!!!!!!!!!
        #region
        public void SetupTexturesTerrain(string uuid)
        {
            Send(JsonConvert.SerializeObject(Commands.addTextureTerrain(vrc2.tunnel, uuid, folder, "water.jpg", 0, 1, 1)));
            Send(JsonConvert.SerializeObject(Commands.addTextureTerrain(vrc2.tunnel, uuid, folder, "grass_ground2y_d.jpg", 1, 5, 0)));
            Send(JsonConvert.SerializeObject(Commands.addTextureTerrain(vrc2.tunnel, uuid, folder, "snow_mud_d.jpg", 5, 15, 0)));
            Send(JsonConvert.SerializeObject(Commands.addTextureTerrain(vrc2.tunnel, uuid, folder, "mntn_x2_d.jpg", 15, 28, 0)));
            Send(JsonConvert.SerializeObject(Commands.addTextureTerrain(vrc2.tunnel, uuid, folder, "snow_rough_s.jpg", 28, 40, 0)));
            Send(JsonConvert.SerializeObject(Commands.addTextureTerrain(vrc2.tunnel, uuid, folder, "snow2ice_d.jpg", 40, 100, 0)));
            terrain.textureLoaded = true;
        }
        #endregion

        //Process list with sessions
        #region
        public void ProcessSessionList(dynamic information)
        {
            dynamic sessions = information;
            List<ClientInfo> clientinfoList = new List<ClientInfo>();
            foreach (dynamic d in sessions)
            {
                clientinfoList.Add(new ClientInfo((string)d.clientinfo.host, (string)d.id, (string)d.clientinfo.file));
            }
            vrc2.AddOptions(clientinfoList);
        }
        #endregion

        //Processes when tunnel needs to be created
        #region
        public void ProcessTunnelCreate(dynamic information)
        {
            vrc2.tunnel = information.id;
        }
        #endregion

        //Close session
        #region
        public void Close()
        {
            stream.Close();
            client.Close();
        }
        #endregion
    }
}
