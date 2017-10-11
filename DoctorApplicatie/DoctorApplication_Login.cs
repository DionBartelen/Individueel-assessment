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

    public partial class DoctorAplicatie : Form
    {
        string username_doctor;
        string password_doctor;

        public DoctorAplicatie()
        {
            InitializeComponent();
            
        }

        private void sign_in_btn_Click(object sender, EventArgs e)
        {
            username_doctor = usernameTxt.Text;
            password_doctor = passwordTxt.Text;
            new DoctorApplication_Connection(username_doctor, password_doctor, this);
        }

        public void RunSessionForm(DoctorApplication_Session session)
        {
            this.BeginInvoke(new MethodInvoker(delegate
            {
                session.Show();
            }));
        }

    }
}
