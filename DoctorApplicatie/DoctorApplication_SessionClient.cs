using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoctorApplicatie
{
    public partial class DoctorApplication_SessionClient : Form
    {
        DoctorApplication_Connection connection;
        public String sessionID;
        public List<ErgometerData> currentData;
        public Boolean sessionStarted;

        public DoctorApplication_SessionClient(DoctorApplication_Connection connection, String sessionID)
        {
            InitializeComponent();
            this.connection = connection;
            this.sessionID = sessionID;
            this.Text = sessionID;
            sessionStarted = false;
            Distancelbl.Text = "0.00";
            currentData = new List<ErgometerData>();
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            if (sessionID != null)
            {
                connection.startTraining(sessionID);
                sessionStarted = true;
                startBtn.Enabled = false;
                StopBtn.Enabled = true;

            }
        }


        private void StopBtn_Click(object sender, EventArgs e)
        {
            connection.stopTraining(sessionID);
            sessionStarted = false;
        }

        private void sendToClientBtn_Click(object sender, EventArgs e)
        {
            if (messageTxt.Text != null)
            {
                connection.sendMessageToClient(messageTxt.Text, sessionID);
            }
        }

        private void toAllBtn_Click(object sender, EventArgs e)
        {
            if (messageTxt.Text != null)
            {
                connection.sendMessagetoAllClients(messageTxt.Text);
            }
        }

        private void setPowerBtn_Click(object sender, EventArgs e)
        {
            if (setPowerTxt.Text != null && sessionID != null)
            {
                bool isNumber = int.TryParse(setPowerTxt.Text, out int Power);
                if (isNumber)
                {
                    if (Power >= 25 && Power <= 400)
                        connection.setPower(setPowerTxt.Text, sessionID);
                    else
                    {
                        MessageBox.Show("the input was either too high or too low, please insert a number between 25 and 400");
                    }
                }
                else
                {
                    MessageBox.Show("the input has not been reconised as a number, please put in a number");
                }
            }
        }

        private void unFollowBtn_Click(object sender, EventArgs e)
        {
            connection.UnFollowPatient(sessionID);
            Close();
        }

        public void updateChart()
        {

          
            this.BeginInvoke(new MethodInvoker(delegate
            {
                chart1.Series.Clear();
                chart2.Series.Clear();
                chart1.ChartAreas[0].AxisX.Minimum = 0;
                chart2.ChartAreas[0].AxisX.Minimum = 0;
                chart1.Series.Add(@"Speed in KM/h");
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart1.Series.Add(@"RPM");
                chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart2.Series.Add(@"Power in Watt");
                chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart2.Series.Add(@"heart pulse");
                chart2.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                Distancelbl.Text = Math.Round(currentData.Last().Distance/3600, 3) + "";
                foreach (ErgometerData data in currentData)
                {
                    chart1.Series[0].Points.AddXY(data.Time, data.Speed);
                    chart1.Series[1].Points.AddXY(data.Time, data.RPM);
                    
                    chart2.Series[0].Points.AddXY(data.Time, data.Requested_Power);
                    chart2.Series[1].Points.AddXY(data.Time, data.Pulse);
                }
            }));
            
        }

        private void DoctorApplication_SessionClient_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void InformationBtn_Click(object sender, EventArgs e)
        {
            
            MessageBox.Show("Usage of the buttons: \r\n" +
                "\r\n" + "\r\n" + 
                "Start: this button will start the session of the client, after this button is pressed, charts will be presented. \r\n" +
                "\r\n" +
                "Stop: this button will stop the session of the client. \r\n" +
                "\r\n" +
                "To client: this button wil send a message to the selected client,this message will be presented to a panel which is shown in the VR. \r\n" +
                "\r\n" +
                "All clients: this button will send a message to all clients the server is connected to, this will also be presented to a panel which is presented in the VR \r\n" +
                "\r\n" +
                "Set Power: this button will send a requested power to the client, this will change the requested power on the bike \r\n" +
                "\r\n" +
                "Unfollow: this button will close this form, be advised the the session of the patiënt is still running"
                );

        }
    }
}
