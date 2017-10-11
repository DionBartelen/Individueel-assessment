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

namespace Healthcare_test.test_applicatie
{
    public partial class Simulation : Form
    {
        public Time CurrentTime { get; set; }
        public float Distance { get; set; }
        public float Speed { get; set; }
        public int Power { get; set; }
        public int Pulse { get; set; }
        public double RPM { get; set; }
        public int Actual_Energy { get; set; }
        public int Requested_Energy { get; set; }
        public Thread CountThread;
        private Boolean ShouldCount = true;
        private Boolean IsRunning = true;

        public Simulation()
        {
            InitializeComponent();
            CurrentTime = new Time(0, 0, 0);
            Distance = 0;
            Speed = 0;
            Power = PowerTrackbar.Minimum;
            Pulse = 0;
            RPM = 0;
            Actual_Energy = 0;
            Requested_Energy = 0;
        }

        private void PowerTrackbar_Scroll(object sender, EventArgs e)
        {
            Power = ((TrackBar)sender).Value;
            PowerLabel.Text = Power + "";
        }

        private void TrackBar2_Scroll(object sender, EventArgs e)
        {
            Speed = ((TrackBar)sender).Value;
            SpeedLabel.Text = Speed + "";
        }

        public void Count()
        {
            while (ShouldCount)
            {
                try
                {
                    CurrentTime.Timer();
                    TimeLabel.Invoke(new Action(() => TimeLabel.Text = CurrentTime.ToString()));

                    Distance += (Speed / 60);
                    DistanceLabel.Invoke(new Action(() => DistanceLabel.Text = $"{Distance:f2}"));

                    RPM = Speed * 2.8;
                    RpmLabel.Invoke(new Action(() => RpmLabel.Text = RPM.ToString()));

                    Pulse = 90 + (int)((Power/6) * (Speed/20));
                    PulseLabel.Invoke(new Action(() => PulseLabel.Text = Pulse.ToString()));

                    
                    SpeedTrackbar.Invoke(new Action(() => SpeedTrackbar.Value = (int)Speed));
                    SpeedLabel.Invoke(new Action(() => SpeedLabel.Text = Speed.ToString()));
                    PowerTrackbar.Invoke(new Action(() => PowerTrackbar.Value = (int)Power));
                    PowerLabel.Invoke(new Action(() => PowerLabel.Text = Power.ToString()));
                }
                catch (Exception e)
                {
                    
                }

                Thread.Sleep(1000);
            }
        }

        public void Reset()
        {
            CurrentTime = new Time(0, 0, 0);
            Distance = 0;
            Speed = 0;
            Power = PowerTrackbar.Minimum;
            Pulse = 0;
            RPM = 0;
            Actual_Energy = 0;
            Requested_Energy = 0;
            SpeedTrackbar.Value = SpeedTrackbar.Minimum;
            PowerTrackbar.Value = PowerTrackbar.Minimum;
            SpeedLabel.Text = 0 + "";
            PowerLabel.Text = PowerTrackbar.Minimum + "";
        }

        public void ResetButton_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            if (IsRunning)
            {
                IsRunning = false;
                 CountThread.Suspend();
            }
            
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (!IsRunning)
            {
                IsRunning = true;
                CountThread.Resume();
            }
        }

        public void End()
        {
            this.BeginInvoke(new MethodInvoker(() =>
            {
                this.Close();
            }));
        }

        public void startSession()
        {
            CountThread = new Thread(new ThreadStart(Count));
            CountThread.Start();

        }
    }
}
