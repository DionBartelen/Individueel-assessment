using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Healthcare_test.VR
{
    public class Session
    {
        VRgui gui;
        TcpClient client;
        NetworkStream stream;
        public Terrain terrain;
        


        public Session(string ip, int port, VRgui gui)
        {
            //dit moet nog in de methode gezet worden
            terrain = new Terrain(new int[256], new int[256]);
            client = new TcpClient();
            client.ReceiveTimeout = 1000;
            client.SendTimeout = 1000;
            client.Connect(ip, port);
            stream = client.GetStream();
            this.gui = gui;
        }

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
                //System.Diagnostics.Debug.WriteLine(terrain.ToString());
            }
        }

        public void Read()
        {
            while (true)
            {
                try
                {
                    StringBuilder response = new StringBuilder();
                    int numberOfBytesRead = 0;
                    int totalBytesreceived = 0;
                    int lengthMessage = -1;
                    byte[] receiveBuffer = new byte[1024];
                    bool messagereceived = false;

                    do
                    {
                        numberOfBytesRead = stream.Read(receiveBuffer, 0, receiveBuffer.Length);
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
                            }
                        }
                        else if ((totalBytesreceived - 4) == lengthMessage)
                        {
                            messagereceived = true;
                        }
                    }
                    while (!messagereceived);
                    stream.Flush();
                    string toReturn = response.ToString().Substring(4);
                    System.Diagnostics.Debug.WriteLine("Received: \r\n" + toReturn);
                    ProcessAnswer(toReturn);
                }
                catch (Exception e)
                {
                   // System.Diagnostics.Debug.WriteLine(e.Message + "\r\n" + e.StackTrace);
                }
            }
        }

        public void ProcessAnswer(string information)
        {
            dynamic jsonData = JsonConvert.DeserializeObject(information);
            //System.Diagnostics.Debug.WriteLine("ID: " + (string)jsonData.id);



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
                    terrain.RouteNameReceived((string)jsonData.data.data.data.uuid);
                }
                //System.Diagnostics.Debug.WriteLine((string)jsonData.data.data.id);

                if (jsonData.data.data.id == "scene/node/add")
                {
                    terrain.NodeNameReceived((string)jsonData.data.data.data.uuid);
                }

                else if (jsonData.id == "packetid")
                {

                }
                else if (jsonData.id == "route/add")
                {

                }
                if (jsonData.data.data.id == "scene/node/find")
                {
                    //System.Diagnostics.Debug.WriteLine(information);
                    string uuid = jsonData.data.data.data[0].uuid;
                    //System.Diagnostics.Debug.WriteLine(uuid + " test");
                    //System.Diagnostics.Debug.WriteLine(information);
                    //System.Diagnostics.Debug.WriteLine("naam is " + (string)jsonData.data.data.data[0].name);
                    if (jsonData.data.data.data[0].name == "GroundPlane")
                    {
                        //System.Diagnostics.Debug.WriteLine("dit is de groundplane");
                        Send(JsonConvert.SerializeObject(Commands.DeleteNode(gui.tunnel, uuid)));
                    }
                    else if (jsonData.data.data.data[0].name == "terrain")
                    {

                        SetupTexturesTerrain(uuid);

                    }
                    else if (jsonData.data.data.data[0].name == "Road")
                    {

                        Send(JsonConvert.SerializeObject(Commands.UpdateNode(gui.tunnel, uuid, -3.99,0,0,0)));

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
                        terrain.UuidPanel = uuid;
                    }

                }
                else
                {
                     System.Diagnostics.Debug.WriteLine("Else: \r\n" + information);
                }
            }


        }

        private void SetupTexturesTerrain(string uuid)
        {
            //desert_wet_n.jpg = og water texture
            //Task.Delay(1000);
            Send(JsonConvert.SerializeObject(Commands.addTextureTerrain(gui.tunnel, uuid, "water.jpg",0,1,1 )));
            //Task.Delay(1000);
            Send(JsonConvert.SerializeObject(Commands.addTextureTerrain(gui.tunnel, uuid, "grass_ground2y_d.jpg", 1, 5, 0)));
            //Task.Delay(1000);
            Send(JsonConvert.SerializeObject(Commands.addTextureTerrain(gui.tunnel, uuid, "snow_mud_d.jpg", 5, 15, 0)));
            //Task.Delay(1000);
            Send(JsonConvert.SerializeObject(Commands.addTextureTerrain(gui.tunnel, uuid, "mntn_x2_d.jpg", 15, 28, 0)));
            //Task.Delay(1000);
            Send(JsonConvert.SerializeObject(Commands.addTextureTerrain(gui.tunnel, uuid, "snow_rough_s.jpg", 28, 40, 0)));
            //Task.Delay(1000);
            Send(JsonConvert.SerializeObject(Commands.addTextureTerrain(gui.tunnel, uuid, "snow2ice_d.jpg", 40, 100, 0)));
            //Task.Delay(1000);
            terrain.textureLoaded = true;
        }

        public void ProcessSessionList(dynamic information)
        {
            dynamic sessions = information;
            List<ClientInfo> clientinfoList = new List<ClientInfo>();
            foreach (dynamic d in sessions)
            {
                clientinfoList.Add(new ClientInfo((string)d.clientinfo.host, (string)d.id));
            }
            gui.AddOptions(clientinfoList);
        }

        public void ProcessTunnelCreate(dynamic information)
        {
            dynamic status = information.status;
            gui.UpdateTunnelStatus((string)status);
            gui.tunnel = information.id;
        }


        public void Close()
        {
            stream.Close();
            client.Close();
        }


    }
}
