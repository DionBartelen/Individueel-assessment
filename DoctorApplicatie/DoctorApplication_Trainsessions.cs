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
    public partial class DoctorApplication_Trainsessions : Form
    {
        List<TrainSession> AllSessions;

        public DoctorApplication_Trainsessions()
        {
            InitializeComponent();
            AllSessions = new List<TrainSession>();
        }

        public void SetAllSessions(List<TrainSession> sessions)
        {
            AllSessions = sessions;
            ShowComboBox.Items.Clear();
            foreach(TrainSession s in AllSessions)
            {
                ShowComboBox.Items.Add(s);
            }
        }

        private void ShowTrainSessionButton_Click(object sender, EventArgs e)
        {
            dataGUI dataGui = new dataGUI();
            dataGui.SetTrainSession((TrainSession)ShowComboBox.SelectedItem);
            RunDataGUI(dataGui);
        }

        public void RunDataGUI(dataGUI dataGUI)
        {
            this.BeginInvoke(new MethodInvoker(delegate
            {
                dataGUI.Show();
            }));
        }

        private void infoBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("usage buttons and form:" + "\r\n" + "\r\n" +
                "this form is used for getting data from past sessions with one certain patiënt \r\n" +
                "\r\n" +
                "the combo box is used to select one training session from before. \r\n" +
                "\r\n" +
                "the button will open a new form, in this form a few charts will be shown" +
                "this form will only show these charts, there are no additional functions, so there will be no other functions screen \r\n");
        }
    }
}
