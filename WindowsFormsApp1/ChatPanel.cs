using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class ChatPanel : Form
    {
        private int screenWidth;
        private int screenHeight;
        public Astrand astrand { get; set; }

        public ChatPanel()
        {
            InitializeComponent();
            this.Paint += new System.Windows.Forms.PaintEventHandler(PaintMethod);
            screenWidth = SystemInformation.VirtualScreen.Width;
            screenHeight = SystemInformation.VirtualScreen.Height;
            this.chatbox.Size = new System.Drawing.Size((screenWidth), (int)(screenHeight * 0.8));
            this.confirmButton.Location = new System.Drawing.Point(0, screenHeight - (screenHeight / 30) - 75);
            this.confirmButton.Size = new System.Drawing.Size((screenWidth), (screenHeight / 30));
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

        public void PaintMethod(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (astrand == null)
            {
                return;
            }
            Graphics g = this.CreateGraphics();
            Pen p;
            Pen textPen = new Pen(Color.Green, 7);
            string text = "";
            if (astrand.currentRPM == 0)
            {
                return;
            }
            if (astrand.currentRPM >= 50 && astrand.currentRPM <= 60)
            {
                p = new Pen(Color.Green);
            }
            else if (astrand.currentRPM <= 50)
            {
                p = new Pen(Color.Red, 7);
                text = "Fiets sneller";
            } else if (astrand.currentRPM >= 60)
            {
                p = new Pen(Color.Red, 7);
                text = "Fiets langzamer";
            }
            else
            {
                p = new Pen(Color.Blue, 7);
            }
            g.FillRectangle(p.Brush, new RectangleF(new PointF(0, (int)(screenHeight * 0.8)), new SizeF(screenWidth, screenHeight / 10)));
            g.DrawString(text, new Font("Arial", 16), textPen.Brush, (screenWidth / 2), (int)(screenHeight * 0.8) + (screenHeight / 60));
            p.Dispose();
            textPen.Dispose();
            g.Dispose();
        }
    }
}
