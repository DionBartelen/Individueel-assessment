using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class ChatPanel : Form
    {
        public Astrand astrand { get; set; }

        public ChatPanel()
        {
            InitializeComponent();
        }

        public void ShowPanel()
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                this.Show();
            }));
        }

        public void UpdateText(string newText)
        {
            string totalText = newText + "\r\n\r\n" + chatbox.Text;
            chatbox.Invoke(new Action(() => { chatbox.Text = totalText; }));
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            if (astrand != null)
            {
                astrand.Confirm();
            }
        }

        public void End()
        {
            this.BeginInvoke(new MethodInvoker(() => { this.Close(); }));
        }
    }
}
