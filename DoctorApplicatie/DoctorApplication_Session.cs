using Healthcare_test.VR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace DoctorApplicatie
{
    public partial class DoctorApplication_Session : Form
    {
        DoctorApplication_Connection connection;
        List<String> connected_clients;
       public  List<DoctorApplication_SessionClient> followed_sessions;

        public DoctorApplication_Session(DoctorApplication_Connection connection)
        {
            InitializeComponent();
            this.connection = connection;
            connected_clients = new List<string>();
            followed_sessions = new List<DoctorApplication_SessionClient>();
        }

        private void getPastDataBtn_Click(object sender, EventArgs e)
        {
            if (connection != null && OlderDataComboBox.Text != null)
            {
                connection.getOlderData(OlderDataComboBox.Text);
            }
        }

        public void UpdateComboBox(List<String> new_Connected_Sessions)
        {
            this.BeginInvoke(new MethodInvoker(delegate
            {
                ConectedSessionsListCombo.Items.Clear();
            }));

            foreach (string c in new_Connected_Sessions)
            {
                if (c != null)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        ConectedSessionsListCombo.Items.Add(c);
                    }));
                }                
            }
        }

        public void UpdateOlderDataComboBox(List<string> users)
        {
            this.BeginInvoke(new MethodInvoker(delegate
            {
                OlderDataComboBox.Items.Clear();
            }));

            foreach (string c in users)
            {
                if (c != null)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        OlderDataComboBox.Items.Add(c);
                    }));
                }
            }
        }

        private void RefreshConnectedButton_Click(object sender, EventArgs e)
        {
            if (connection != null)
            {
                ConectedSessionsListCombo.Text = "";
                connection.getSessions();
            }
        }

        private void RefreshHistoricUsers_Click(object sender, EventArgs e)
        {
            if (connection != null)
            {
                connection.GetUsers();
            }
        }

        public void RunTrainSessionForm(DoctorApplication_Trainsessions session)
        {
            this.BeginInvoke(new MethodInvoker(delegate
            {
                session.Show();
            }));
        }

        private void followBtn_Click(object sender, EventArgs e)
        {
            if (ConectedSessionsListCombo.SelectedItem != null)
            {
                connection.FollowPatient(ConectedSessionsListCombo.SelectedItem.ToString());
                this.BeginInvoke(new MethodInvoker(delegate
                {
                    DoctorApplication_SessionClient session = new DoctorApplication_SessionClient(connection, ConectedSessionsListCombo.SelectedItem.ToString());
                    followed_sessions.Add(session);
                    session.Show();
                }
                ));
            }
            else
            {
                MessageBox.Show("no patiënt has been found, please selected a patiënt from the combo box connected sessions list");
            }
            
        }

        private void informationBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("usage of buttons: \r\n" +
                "\r\n" + "\r\n" + 
                "Refresh session list: this button wil refresh the combo box connected sessions, the refresh method will check if new patiënts have connected with the server \r\n in case they have they will be added to this combo box. \r\n" +
                "\r\n" +
                "Follow: this button will open a new form with the selected patiënt from the combo connected sessions, the form that has opend will have all the methods that the doctor that he/she can use to communicate with the patiënt or to change some settings more information in that form \r\n" +
                "\r\n" +
                "Past sessions: this combobox can be used for patiënts that have done more then 1 test you can select a patiënt that you want to check older data from \r\n" +
                "\r\n" +
                "Refresh old data list: this button refreshes the list, mostly used for patiënts who just finished a session it refreshes the list of sessions that have a sessions saved in the database \r\n" +
                "\r\n" +
                "getData: this button opens a new form, in this form you can select a training you want to get recent data from more info in the form");



        }
    }
}
