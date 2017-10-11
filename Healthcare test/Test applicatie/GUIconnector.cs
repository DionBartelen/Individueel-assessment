using Healthcare_test;
using Healthcare_test.test_applicatie;

using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace Healthcare_test
{
    public partial class GUIconnector : Form
    {
        public Ergometer ergometer;

        public GUIconnector()
        {
            InitializeComponent();
            string[] ports = SerialPort.GetPortNames();
            foreach (String s in ports)
            {
                ComPortText.Items.Add(s);
            }
            ComPortText.SelectedItem = ComPortText.Items[0];
        }

        private void ConnectSerial_Click(object sender, EventArgs e)
        {
            if(ergometer != null)
            {
                ergometer.Close();
            }
            if (ComPortText.Text == "Simulator")
            {
                ergometer = new ErgometerSimulatie();
                Data_Collector.Enabled = true;
            }
            else
            {
                string comPort = ComPortText.Text;
                string baudRate = BaudRateText.Text;
                ergometer = new ErgometerCOM(comPort, baudRate);
                if (ergometer.IsConnected())
                    Data_Collector.Enabled = true;
                else
                    Data_Collector.Enabled = false;

               
            }
        }

        private void SendInput_Click(object sender, EventArgs e)
        {
            string command = CommandInput.Text;
            ProcessCommand(command);
            CommandInput.Text = "";
        }

        private void ProcessCommand(string command)
        {
            if(command.Length < 2)
            {
                return;
            }
            if(command.Substring(0, 2) == "ST")
            {
                replyBoxText.Clear();
                ErgometerData ergometerData = ergometer.GetData();
                replyBoxText.Text = ergometerData.ToString();
            } else if(command.Substring(0, 2) == "PW")
            {
                ergometer.SetPower(Convert.ToInt32(command.Substring(2)));
            }
            else if (command.Substring(0, 2) == "PD")
            {
                ergometer.SetDistance(Convert.ToInt32(command.Substring(2)));
            }
            else if (command.Substring(0, 2) == "PT")
            {
                ergometer.SetTime(Convert.ToInt32(command.Substring(2)));
            }
            else if (command.Substring(0, 2) == "CM")
            {
                ergometer.ErgometerCommandMode();
            }
            else if(command.Substring(0,2) == "RS")
            {
                ergometer.Reset();
            }

        }

        private void Data_Collector_Click(object sender, EventArgs e)
        {
            if(ergometer.IsConnected())
            {
                replyBoxText.Clear();
                ErgometerData ergometerData = ergometer.GetData();
                if (ergometerData != null)
                {
                    replyBoxText.Text = ergometerData.ToString();
                }

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ergometer != null)
            {
                ergometer.Close();
            }
        }

        private void ComPortText_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void GUIconnector_Load(object sender, EventArgs e)
        {

        }
    }
}
