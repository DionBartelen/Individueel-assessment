using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Healthcare_test;
using Newtonsoft.Json;

namespace WindowsFormsApp1
{
    public class VRConnector2
    {
        private List<ClientInfo> clients;
        ClientInfo currentClient;
        Session2 session;
        public string tunnel { get; set; }
        Thread Reading;
        bool TunnelIsMade;
        private string messageDoctor;

        public VRConnector2()
        {
            TunnelIsMade = false;
            session = new Session2("145.48.6.10", 6666, this);
            Reading = new Thread(session.Read);
            Reading.Start();
            GetClientList();
            while (clients == null)
            {
                Thread.Sleep(10);
            }
            while (!TunnelIsMade)
            {
                connectToEngine();
                Thread.Sleep(100);
            }
            Commands.AdjustPaths(currentClient.Folder);
            System.Diagnostics.Debug.WriteLine(currentClient.Folder);
            session.folder = currentClient.Folder;
            SetupTerrain();
        }

        //Get client list from VREngine
        #region
        public void GetClientList()
        {
            if (session != null)
            {
                session.Send(JsonConvert.SerializeObject(Commands.SessionList()));
            }
        }
        #endregion

        //Creates a tunnel between VREngine and this pc
        #region
        public void CreateTunnel()
        {
            session.Send(JsonConvert.SerializeObject(Commands.CreateTunnel(currentClient.ID)));
        }
        #endregion

        //Overwrites client list with new one
        #region
        public void AddOptions(List<ClientInfo> clients)
        {
            this.clients = clients;
        }
        #endregion

        //Check for correct host name in clients
        #region
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
                    TunnelIsMade = true;
                    CreateTunnel();
                }
            }
        }
        #endregion

        //Setup up terrain with commands in VREngine
        #region
        public void SetupTerrain()
        {
            while (tunnel == null)
            {
                Thread.Sleep(1000);
            }
            session.Send(JsonConvert.SerializeObject(Commands.pause(tunnel)));
            while (!session.terrain.pauze)
            {
                Thread.Sleep(1000);
            }
            session.Send(JsonConvert.SerializeObject(Commands.GetNodeByName(tunnel, "GroundPlane")));
            while (session.terrain.UuidGroundPlane == null)
            {
                Thread.Sleep(1000);
            }
            session.Send(JsonConvert.SerializeObject(Commands.DeleteNode(tunnel, session.terrain.UuidGroundPlane)));
            session.Send(JsonConvert.SerializeObject(Commands.CreateGroundTerrainWithHeights(tunnel)));
            while (!session.terrain.terainAdded)
            {
                Thread.Sleep(1000);
            }
            session.Send(JsonConvert.SerializeObject(Commands.AddObject(tunnel, -105, -4, -128, 0, 0, 0, "terrain", true, false)));
            while (session.terrain.UuidTerrainNode == null)
            {
                Thread.Sleep(1000);
            }
            session.Send(JsonConvert.SerializeObject(Commands.addTextureTerrain(tunnel, session.terrain.UuidTerrainNode, session.folder, "water.jpg", 0, 1, 1)));
            session.Send(JsonConvert.SerializeObject(Commands.addTextureTerrain(tunnel, session.terrain.UuidTerrainNode, session.folder, "grass_ground2y_d.jpg", 1, 5, 0)));
            session.Send(JsonConvert.SerializeObject(Commands.addTextureTerrain(tunnel, session.terrain.UuidTerrainNode, session.folder, "snow_mud_d.jpg", 5, 15, 0)));
            session.Send(JsonConvert.SerializeObject(Commands.addTextureTerrain(tunnel, session.terrain.UuidTerrainNode, session.folder, "mntn_x2_d.jpg", 15, 28, 0)));
            session.Send(JsonConvert.SerializeObject(Commands.addTextureTerrain(tunnel, session.terrain.UuidTerrainNode, session.folder, "snow_rough_s.jpg", 28, 40, 0)));
            session.Send(JsonConvert.SerializeObject(Commands.addTextureTerrain(tunnel, session.terrain.UuidTerrainNode, session.folder, "snow2ice_d.jpg", 40, 100, 0)));
            while (session.terrain.UuidCamera == null)
            {
                session.Send(JsonConvert.SerializeObject(Commands.GetNodeByName(tunnel, "Camera")));
                Thread.Sleep(1000);
            }
            session.Send(JsonConvert.SerializeObject(Commands.AddObject(tunnel, 0, 0, 0, 0, 0, 0, "MainBike", false, false)));
            while (session.terrain.UuidMainBike == null)
            {
                session.Send(JsonConvert.SerializeObject(Commands.GetNodeByName(tunnel, "MainBike")));
                Thread.Sleep(1000);
            }
            session.Send(JsonConvert.SerializeObject(Commands.UpdateNodeWithParent(tunnel, session.terrain.UuidMainBike, session.terrain.UuidCamera)));
            session.Send(JsonConvert.SerializeObject(Commands.UpdateNode(tunnel, session.terrain.UuidMainBike, 0, 0, 0, 270, 0)));
            session.Send(JsonConvert.SerializeObject(Commands.AddObject(tunnel, 10, 0, 0, 0, 0.5, 0.4, "BikePanel", false, true)));

            while (session.terrain.UuidStatsPanel == null)
            {
                session.Send(JsonConvert.SerializeObject(Commands.GetNodeByName(tunnel, "BikePanel")));
                Thread.Sleep(1000);
            }
            session.Send(JsonConvert.SerializeObject(Commands.UpdateNodeWithParent(tunnel, session.terrain.UuidStatsPanel, session.terrain.UuidCamera)));
            session.Send(JsonConvert.SerializeObject(Commands.UpdateNode(tunnel, session.terrain.UuidStatsPanel, 0, 1, -0.75, 0, -30)));
            session.Send(JsonConvert.SerializeObject(Commands.AddObject(tunnel, 10, 0, 0, 0, 1, 0.5, "MessagePanel", false, true)));
            while (session.terrain.UuidMessagePanel == null)
            {
                session.Send(JsonConvert.SerializeObject(Commands.GetNodeByName(tunnel, "MessagePanel")));
                Thread.Sleep(1000);
            }
            session.Send(JsonConvert.SerializeObject(Commands.UpdateNodeWithParent(tunnel, session.terrain.UuidMessagePanel, session.terrain.UuidCamera)));
            session.Send(JsonConvert.SerializeObject(Commands.UpdateNode(tunnel, session.terrain.UuidMessagePanel, 0.9, 1.5, -1.4, -30, 0)));
            session.Send(JsonConvert.SerializeObject(Commands.addSkyBox(tunnel)));
            session.Send(JsonConvert.SerializeObject(Commands.AddRoute(tunnel)));
            while (session.terrain.UuidRoute == null)
            {
                Thread.Sleep(1000);
            }
            session.Send(JsonConvert.SerializeObject(Commands.AddRoad(tunnel, session.terrain.UuidRoute)));
            while (session.terrain.UuidRoadNode == null)
            {
                session.Send(JsonConvert.SerializeObject(Commands.GetNodeByName(tunnel, "Road")));
                Thread.Sleep(1000);
            }
            session.Send(JsonConvert.SerializeObject(Commands.UpdateNode(tunnel, session.terrain.UuidRoadNode, 0, -3.99, 0, 0, 0)));
            session.Send(JsonConvert.SerializeObject(Commands.MoveObject(tunnel, session.terrain.UuidCamera, session.terrain.UuidRoute)));
            session.Send(JsonConvert.SerializeObject(Commands.UpdateSpeed(tunnel, session.terrain.UuidCamera, 0)));
            session.Send(JsonConvert.SerializeObject(Commands.SwapPanel(tunnel, session.terrain.UuidStatsPanel)));
            session.Send(JsonConvert.SerializeObject(Commands.SwapPanel(tunnel, session.terrain.UuidMessagePanel)));
            session.Send(JsonConvert.SerializeObject(Commands.play(tunnel)));
        }
        #endregion

        //Setup up terrain with json file in VREngine NOT WORKING!!!!!!!!!!!!
        #region
        public void SetupTerainWithJson()
        {
            while (tunnel == null)
            {
                Thread.Sleep(10);
            }
            session.Send(JsonConvert.SerializeObject(Commands.LoadTerrain(tunnel)));
        }
        #endregion

        //Handle message from doctor
        #region
        public void HandeMessageFromDoctor(string message)
        {
            messageDoctor = message + "\\n" + messageDoctor;
            session.Send(JsonConvert.SerializeObject(Commands.clearPanel(tunnel, session.terrain.UuidMessagePanel)));
            session.Send(JsonConvert.SerializeObject(Commands.addTextPanel(tunnel, session.terrain.UuidMessagePanel, messageDoctor)));
            session.Send(JsonConvert.SerializeObject(Commands.SwapPanel(tunnel, session.terrain.UuidMessagePanel)));
        }
        #endregion

        //Update bike panel
        #region
        public void UpdateBikePanelInVR(ErgometerData ed)
        {
            if (ed.Actual_Power != null && ed.Speed != null && ed.Time != null && ed.RPM != null && ed.Distance != null && ed.Distance != null)
            {
                string text = "Power: " + ed.Actual_Power + "\\n" + "Speed: " + ed.Speed + "\\n" + "Time: " + ed.Time +
                              "\\n" + "RPM: " + ed.RPM + "\\n" + "Distance: " + Math.Round(ed.Distance, 2) + "\\n" +
                              "Pulse: " + ed.Pulse;

                session.Send(JsonConvert.SerializeObject(Commands.UpdateSpeed(tunnel, session.terrain.UuidCamera,
                    (int)ed.Speed / 2)));
                session.Send(JsonConvert.SerializeObject(Commands.clearPanel(tunnel, session.terrain.UuidStatsPanel)));
                session.Send(
                    JsonConvert.SerializeObject(Commands.addTextPanel(tunnel, session.terrain.UuidStatsPanel, text)));
                session.Send(JsonConvert.SerializeObject(Commands.SwapPanel(tunnel, session.terrain.UuidStatsPanel)));
            }
        }
        #endregion
    }
}
