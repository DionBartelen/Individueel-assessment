using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class VRgui : Form
    {
        Session session;
        List<ClientInfo> clients;
        ClientInfo currentClient;
        public string tunnel { get; set; }
        Thread Reading;

        public VRgui()
        {
            InitializeComponent();
            //session = new Session("145.48.6.10", 6666, this);
            Reading = new Thread(session.Read);
            Reading.IsBackground = true;
            Reading.Start();
            GetClientList();
        }

        private void VRgui_Load(object sender, EventArgs e)
        {

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
            foreach (ClientInfo c in clients)
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    sessions.Items.Add(c.HostName);
                });
            }
        }

        public void UpdateTunnelStatus(string status)
        {
            this.Invoke((MethodInvoker)delegate ()
           {
               TunnelStatus.Text = status;
           });
        }

        private void VRgui_FormClosing(object sender, FormClosingEventArgs e)
        {
            Reading.Abort();
        }

        private void VRgui_FormClosed(object sender, FormClosedEventArgs e)
        {
            Reading.Abort();
        }

        private void connect_Button_Click(object sender, EventArgs e)
        {
            string selectedName = sessions.Text;
            foreach (ClientInfo c in clients)
            {
                if (c.HostName == selectedName)
                {
                    currentClient = c;
                    CreateTunnel();
                }
            }
        }

        private void ChangeTerain_Click(object sender, EventArgs e)
        {
            try
            {
                double receiving = Convert.ToDouble(((textBox1.Text)));
                session.Send(JsonConvert.SerializeObject(Commands.SetTime(tunnel, receiving)));
            }
            catch(Exception x)
            {
                System.Windows.Forms.MessageBox.Show("Not a valid number" + x.Message);
            }
        }

        private void RefreshClients_Click(object sender, EventArgs e)
        {
            sessions.Items.Clear();
            session.Send(JsonConvert.SerializeObject(Commands.SessionList()));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //    if(receiving > 24)
            //    {
            //        return;
            //    }
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }


        private void set_Ground_Terrain_256_256_Click(object sender, EventArgs e)
        {
            session.Send(JsonConvert.SerializeObject(Commands.CreateGroundTerrain(tunnel)));
        }

        private void remove_Terrain_Button_Click(object sender, EventArgs e)
        {
            session.Send(JsonConvert.SerializeObject(Commands.GetNodeByName(tunnel,"GroundPlane")));
        }

        private void Buttonobject_Click(object sender, EventArgs e)
        {
            //session.Send(JsonConvert.SerializeObject(Commands.AddObject(tunnel, -105, -4, -128,0,"terrain",true)));
            //

        }

        private void terrainWH_Click(object sender, EventArgs e)
        {
           // session.Send(JsonConvert.SerializeObject(Commands.GetNodeByName(tunnel, "GroundPlane")));
           // Task.Delay(100).Wait();
           // session.Send(JsonConvert.SerializeObject(Commands.CreateGroundTerrainWithHeights(tunnel)));
           // Task.Delay(100).Wait();
           // session.Send(JsonConvert.SerializeObject(Commands.AddObject(tunnel, -105, -4, -128,0,"terrain",true,false)));
           // Task.Delay(1000).Wait();
           // session.Send(JsonConvert.SerializeObject(Commands.GetNodeByName(tunnel, "terrain")));
           // Task.Delay(1000).Wait();
           // while(!session.terrain.textureLoaded)
           // {
           //     Thread.Sleep(100);
           // }
            
           // //Task.Delay(2000).Wait();
           // // session.Send(JsonConvert.SerializeObject(Commands.GetNodeByName(tunnel, "Head")));
           // Task.Delay(1000).Wait();
           // session.Send(JsonConvert.SerializeObject(Commands.GetNodeByName(tunnel, "Camera")));
           //Task.Delay(1000).Wait();
           // session.Send(JsonConvert.SerializeObject(Commands.AddObject(tunnel, 0, 0, 0, 0, "MainBike", false,false)));
           // Task.Delay(500).Wait();
           // session.Send(JsonConvert.SerializeObject(Commands.GetNodeByName(tunnel, "MainBike")));
           // Task.Delay(500).Wait();
           // session.Send(JsonConvert.SerializeObject(Commands.UpdateNodeWithParent(tunnel, session.terrain.UuidMainBike, session.terrain.UuidCamera)));
           // Task.Delay(500).Wait();
           // session.Send(JsonConvert.SerializeObject(Commands.UpdateNode(tunnel, session.terrain.UuidMainBike,0,0,0,270,0)));
           // Task.Delay(500).Wait();
           // session.Send(JsonConvert.SerializeObject(Commands.AddObject(tunnel, 10, 0, 0, 0, "BikePanel", false, true)));
           // Task.Delay(500).Wait();
           // session.Send(JsonConvert.SerializeObject(Commands.GetNodeByName(tunnel, "BikePanel")));
           // Task.Delay(500).Wait();
           // session.Send(JsonConvert.SerializeObject(Commands.UpdateNodeWithParent(tunnel, session.terrain.UuidStatsPanel, session.terrain.UuidCamera)));
           // Task.Delay(500).Wait();
           // session.Send(JsonConvert.SerializeObject(Commands.UpdateNode(tunnel, session.terrain.UuidStatsPanel,0, 1, -0.75, 0, -30)));
           // Task.Delay(500).Wait();
           // session.Send(JsonConvert.SerializeObject(Commands.addTextPanel(tunnel, session.terrain.UuidStatsPanel, "Speed: 0")));
           // Task.Delay(500).Wait();
           // session.Send(JsonConvert.SerializeObject(Commands.SwapPanel(tunnel, session.terrain.UuidStatsPanel)));
           // Task.Delay(500).Wait();
           // session.Send(JsonConvert.SerializeObject(Commands.addSkyBox(tunnel)));
           // Task.Delay(500).Wait();
           // session.Send(JsonConvert.SerializeObject(Commands.AddRoute(tunnel)));
           // Task.Delay(2000).Wait();
           // session.Send(JsonConvert.SerializeObject(Commands.AddRoad(tunnel, session.terrain.route.Last().id)));
           // Task.Delay(2000).Wait();
           // session.Send(JsonConvert.SerializeObject(Commands.GetNodeByName(tunnel, "Road")));
           // Task.Delay(3000).Wait();
           // session.Send(JsonConvert.SerializeObject(Commands.MoveObject(tunnel, session.terrain.UuidCamera, session.terrain.road.Last().id)));
           // Task.Delay(500).Wait();
           // session.Send(JsonConvert.SerializeObject(Commands.UpdateSpeed(tunnel, session.terrain.UuidCamera, 0)));




        }

        private void Routebutton_Click(object sender, EventArgs e)
        {
            session.Send(JsonConvert.SerializeObject(Commands.AddRoute(tunnel)));
        }

        private void Showroutebutton_Click(object sender, EventArgs e)
        {
            session.Send(JsonConvert.SerializeObject(Commands.AddRoad(tunnel, session.terrain.route.Last().id)));
            Task.Delay(1000);
            session.Send(JsonConvert.SerializeObject(Commands.GetNodeByName(tunnel, "Road")));
           



        }

        private void Moving_3D_Ojbect_Click(object sender, EventArgs e)
        {
            session.Send(JsonConvert.SerializeObject(Commands.MoveObject(tunnel, session.terrain.UuidCamera, session.terrain.road.Last().id)));
            Task.Delay(500);
            session.Send(JsonConvert.SerializeObject(Commands.UpdateSpeed(tunnel, session.terrain.UuidCamera,5)));
            // session.Send(JsonConvert.SerializeObject(Commands.MoveObject(tunnel, session.terrain.nodes.Last().uuid, session.terrain.road.Last().id)));
            //session.Send(JsonConvert.SerializeObject(Commands.UpdateNode(tunnel, session.terrain.UuidMainBike, 0, 90)));
        }

        private void GetTerrainButton_Click(object sender, EventArgs e)
        {
            session.Send(JsonConvert.SerializeObject(Commands.GetScene(tunnel)));
        }

        private void Texture_Click(object sender, EventArgs e)
        {
            session.Send(JsonConvert.SerializeObject(Commands.GetNodeByName(tunnel, "terrain")));
        }

        private void Resetbutton_Click(object sender, EventArgs e)
        {
            session.Send(JsonConvert.SerializeObject(Commands.ResetScene(tunnel)));
        }

        private void SpeedSlider_Scroll(object sender, EventArgs e)
        {
            session.Send(JsonConvert.SerializeObject(Commands.UpdateSpeed(tunnel, session.terrain.UuidCamera, SpeedSlider.Value)));
            session.Send(JsonConvert.SerializeObject(Commands.clearPanel(tunnel, session.terrain.UuidStatsPanel)));
            session.Send(JsonConvert.SerializeObject(Commands.addTextPanel(tunnel, session.terrain.UuidStatsPanel, "Speed: " + SpeedSlider.Value)));
            session.Send(JsonConvert.SerializeObject(Commands.SwapPanel(tunnel, session.terrain.UuidStatsPanel)));
        }
    }
}
