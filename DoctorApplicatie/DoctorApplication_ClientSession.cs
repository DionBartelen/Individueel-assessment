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
    public partial class DoctorApplication_ClientSession : Form
    {
        DoctorApplication_Connection connection;
        String sessionID;
        public DoctorApplication_ClientSession(DoctorApplication_Connection connection, String sessionID)
        {
            InitializeComponent();
            this.connection = connection;
            this.sessionID = sessionID;
            this.Text = sessionID;
            
        }

        private void DoctorApplication_ClientSession_Load(object sender, EventArgs e)
        {

        }

        private void sendToClientBtn_Click(object sender, EventArgs e)
        {
            if (messageTxt.Text != null && sessionID != null)
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
                connection.setPower(setPowerTxt.Text, sessionID);
            }
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            connection.stopTraining(sessionID);
        }

      

        private void getPastDataBtn_Click(object sender, EventArgs e)
        {
            
        }
    }
}
