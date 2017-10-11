using Healthcare_test;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class VR_Connector
    {
        Session session;
        List<ClientInfo> clients;
        ClientInfo currentClient;
        public string tunnel { get; set; }
        Thread Reading;
        bool TunnelIsMade;
        private string messageDoctor;

        public VR_Connector()
        {
            TunnelIsMade = false;
            session = new Session("145.48.6.10", 6666, this);
            Reading = new Thread(session.Read);
           Reading.IsBackground = true;
            Reading.Start();
            GetClientList();
            while (clients == null)
            {
                Thread.Sleep(100);
            }
            while (!TunnelIsMade)
            {
                connectToEngine();
            }
            Commands.AdjustPaths(currentClient.Folder);
            System.Diagnostics.Debug.WriteLine(currentClient.Folder);
            session.folder = currentClient.Folder;
            SetupTerrain();

        }


        public void GetClientList()
        {
            if (session != null)
            {
                session.Send(JsonConvert.SerializeObject(Commands.SessionList()));
            }
        }

        public void CreateTunnel()
        {
            session.Send(JsonConvert.SerializeObject(Commands.CreateTunnel(currentClient.ID)));
        }

        public void AddOptions(List<ClientInfo> clients)
        {
            this.clients = clients;
        }

        public void UpdateBikePanelInVR(ErgometerData ed)
        {
            if (ed.Actual_Power != null && ed.Speed != null && ed.Time != null && ed.RPM != null && ed.Distance != null && ed.Distance != null)
            {
                string text = "Power: " + ed.Actual_Power + "\\n" + "Speed: " + ed.Speed + "\\n" + "Time: " + ed.Time +
                              "\\n" + "RPM: " + ed.RPM + "\\n" + "Distance: " + Math.Round(ed.Distance, 2) + "\\n" +
                              "Pulse: " + ed.Pulse;

                session.Send(JsonConvert.SerializeObject(Commands.UpdateSpeed(tunnel, session.terrain.UuidCamera,
                    (int) ed.Speed / 2)));
                session.Send(JsonConvert.SerializeObject(Commands.clearPanel(tunnel, session.terrain.UuidStatsPanel)));
                session.Send(
                    JsonConvert.SerializeObject(Commands.addTextPanel(tunnel, session.terrain.UuidStatsPanel, text)));
                session.Send(JsonConvert.SerializeObject(Commands.SwapPanel(tunnel, session.terrain.UuidStatsPanel)));
            }
        }


        private void connectToEngine()
        {
            string CurrentPcName = Environment.MachineName;
            foreach (ClientInfo c in clients)
            {
               
                if (c.HostName.ToLower() == CurrentPcName.ToLower())
                //if (c.HostName == "CAVE-Control")
                //if (c.HostName == "DESKTOP-M48E3PG")
                {
                    currentClient = c;
                    System.Diagnostics.Debug.WriteLine("current client = " + c.ID);
                    TunnelIsMade = true;
                    CreateTunnel();
                }
            }
        }

        private void SetupTerrainV2()
        {
            while (tunnel == null)
            {
                Thread.Sleep(10);
            }
            session.Send(JsonConvert.SerializeObject(Commands.pause(tunnel)));
            Task.Delay(1000).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.GetNodeByName(tunnel, "GroundPlane")));
            Task.Delay(100).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.CreateGroundTerrainWithHeights(tunnel)));
            Task.Delay(100).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.AddObject(tunnel, -105, -4, -128, 0,0,0, "terrain", true, false)));
            Task.Delay(100).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.GetNodeByName(tunnel, "terrain")));
            Task.Delay(100).Wait();

             Task.Delay(100).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.GetNodeByName(tunnel, "Camera")));
            Task.Delay(100).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.AddObject(tunnel, 0, 0, 0, 0, 0, 0, "MainBike", false, false)));
            Task.Delay(100).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.GetNodeByName(tunnel, "MainBike")));
             Task.Delay(100).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.UpdateNodeWithParent(tunnel, session.terrain.UuidMainBike, session.terrain.UuidCamera)));
            Task.Delay(100).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.UpdateNode(tunnel, session.terrain.UuidMainBike,0, 0, 0, 270, 0)));
             Task.Delay(100).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.AddObject(tunnel, 10, 0, 0, 0, 0.5, 0.4, "BikePanel", false, true)));
            Task.Delay(100).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.GetNodeByName(tunnel, "BikePanel")));
            Task.Delay(100).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.UpdateNodeWithParent(tunnel, session.terrain.UuidStatsPanel, session.terrain.UuidCamera)));
            Task.Delay(500).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.UpdateNode(tunnel, session.terrain.UuidStatsPanel, 0,1, -0.75, 0, -30)));
            Task.Delay(100).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.AddObject(tunnel, 10, 0, 0, 0, 1, 0.5, "MessagePanel", false, true)));
            Task.Delay(100).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.GetNodeByName(tunnel, "MessagePanel")));
             Task.Delay(100).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.UpdateNodeWithParent(tunnel, session.terrain.UuidMessagePanel, session.terrain.UuidCamera)));
             Task.Delay(100).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.UpdateNode(tunnel, session.terrain.UuidMessagePanel,0.9,1.5, -1.4, -30, 0)));
            Task.Delay(100).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.addSkyBox(tunnel)));
             Task.Delay(100).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.AddRoute(tunnel)));
            Task.Delay(100).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.AddRoad(tunnel, session.terrain.route.Last().id)));
            Task.Delay(100).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.GetNodeByName(tunnel, "Road")));
             Task.Delay(100).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.MoveObject(tunnel, session.terrain.UuidCamera, session.terrain.road.Last().id)));
            Task.Delay(100).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.UpdateSpeed(tunnel, session.terrain.UuidCamera, 0)));
            session.Send(JsonConvert.SerializeObject(Commands.SwapPanel(tunnel, session.terrain.UuidStatsPanel)));
            session.Send(JsonConvert.SerializeObject(Commands.SwapPanel(tunnel, session.terrain.UuidMessagePanel)));
            session.Send(JsonConvert.SerializeObject(Commands.play(tunnel)));
            session.Send(JsonConvert.SerializeObject(Commands.SaveTerrain(tunnel)));
        }

        private void SetupTerrainV3()
        {
            while (tunnel == null)
            {
                Thread.Sleep(10);
            }
           
            session.Send(JsonConvert.SerializeObject(Commands.LoadTerrain(tunnel)));
        }

        private void SetupTerrain()
        {
            while (tunnel == null)
            {
                Thread.Sleep(10);
            }
            session.Send(JsonConvert.SerializeObject(Commands.GetNodeByName(tunnel, "GroundPlane")));
            Task.Delay(100).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.CreateGroundTerrainWithHeights(tunnel)));
            Task.Delay(100).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.AddObject(tunnel, -105, -4, -128, 0, 0, 0, "terrain", true, false)));
            Task.Delay(1500).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.GetNodeByName(tunnel, "terrain")));
            Task.Delay(1000).Wait();
            while (!session.terrain.textureLoaded)
            {
                Thread.Sleep(100);
            }
            Task.Delay(1000).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.GetNodeByName(tunnel, "Camera")));
            Task.Delay(1000).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.AddObject(tunnel, 0, 0, 0, 0, 0, 0, "MainBike", false, false)));
            Task.Delay(500).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.GetNodeByName(tunnel, "MainBike")));
            Task.Delay(500).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.UpdateNodeWithParent(tunnel, session.terrain.UuidMainBike, session.terrain.UuidCamera)));
            Task.Delay(500).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.UpdateNode(tunnel, session.terrain.UuidMainBike, 0, 0, 0, 270, 0)));
            Task.Delay(500).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.AddObject(tunnel, 10, 0, 0, 0, 0.5, 0.4, "BikePanel", false, true)));
            Task.Delay(500).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.GetNodeByName(tunnel, "BikePanel")));
            Task.Delay(500).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.UpdateNodeWithParent(tunnel, session.terrain.UuidStatsPanel, session.terrain.UuidCamera)));
            Task.Delay(500).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.UpdateNode(tunnel, session.terrain.UuidStatsPanel, 0, 1, -0.75, 0, -30)));
            Task.Delay(500).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.AddObject(tunnel, 10, 0, 0, 0, 1, 0.5, "MessagePanel", false, true)));
            Task.Delay(500).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.GetNodeByName(tunnel, "MessagePanel")));
            Task.Delay(500).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.UpdateNodeWithParent(tunnel, session.terrain.UuidMessagePanel, session.terrain.UuidCamera)));
            Task.Delay(500).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.UpdateNode(tunnel, session.terrain.UuidMessagePanel, 0.9, 1.5, -1.4, -30, 0)));
            Task.Delay(500).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.addSkyBox(tunnel)));
            Task.Delay(500).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.AddRoute(tunnel)));
            Task.Delay(2000).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.AddRoad(tunnel, session.terrain.route.Last().id)));
            Task.Delay(2000).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.GetNodeByName(tunnel, "Road")));
            Task.Delay(3000).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.MoveObject(tunnel, session.terrain.UuidCamera, session.terrain.road.Last().id)));
            Task.Delay(500).Wait();
            session.Send(JsonConvert.SerializeObject(Commands.UpdateSpeed(tunnel, session.terrain.UuidCamera, 0)));
            session.Send(JsonConvert.SerializeObject(Commands.SwapPanel(tunnel, session.terrain.UuidStatsPanel)));
            session.Send(JsonConvert.SerializeObject(Commands.SwapPanel(tunnel, session.terrain.UuidMessagePanel)));
            session.Send(JsonConvert.SerializeObject(Commands.SaveTerrain(tunnel)));
        }

        public void HandeMessageFromDoctor(string message)
        {
            messageDoctor = message + "\\n" + messageDoctor;
            session.Send(JsonConvert.SerializeObject(Commands.clearPanel(tunnel, session.terrain.UuidMessagePanel)));
            session.Send(JsonConvert.SerializeObject(Commands.addTextPanel(tunnel, session.terrain.UuidMessagePanel, messageDoctor)));
            session.Send(JsonConvert.SerializeObject(Commands.SwapPanel(tunnel, session.terrain.UuidMessagePanel)));
        }
    }

}

