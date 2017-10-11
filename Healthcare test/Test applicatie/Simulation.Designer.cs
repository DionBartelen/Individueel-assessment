namespace Healthcare_test.test_applicatie
{
    partial class Simulation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PowerTrackbar = new System.Windows.Forms.TrackBar();
            this.SpeedTrackbar = new System.Windows.Forms.TrackBar();
            this.RPMtext = new System.Windows.Forms.Label();
            this.PowerText = new System.Windows.Forms.Label();
            this.SpeedText = new System.Windows.Forms.Label();
            this.DistanceText = new System.Windows.Forms.Label();
            this.RpmLabel = new System.Windows.Forms.Label();
            this.DistanceLabel = new System.Windows.Forms.Label();
            this.ResetButton = new System.Windows.Forms.Button();
            this.PulseText = new System.Windows.Forms.Label();
            this.PulseLabel = new System.Windows.Forms.Label();
            this.TimeText = new System.Windows.Forms.Label();
            this.TimeLabel = new System.Windows.Forms.Label();
            this.PowerLabel = new System.Windows.Forms.Label();
            this.SpeedLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.PauseButton = new System.Windows.Forms.Button();
            this.StartButton = new System.Windows.Forms.Button();
            this.stop = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PowerTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpeedTrackbar)).BeginInit();
            this.SuspendLayout();
            // 
            // PowerTrackbar
            // 
            this.PowerTrackbar.Location = new System.Drawing.Point(24, 57);
            this.PowerTrackbar.Maximum = 400;
            this.PowerTrackbar.Minimum = 25;
            this.PowerTrackbar.Name = "PowerTrackbar";
            this.PowerTrackbar.Size = new System.Drawing.Size(324, 56);
            this.PowerTrackbar.TabIndex = 0;
            this.PowerTrackbar.TickFrequency = 25;
            this.PowerTrackbar.Value = 25;
            this.PowerTrackbar.Scroll += new System.EventHandler(this.PowerTrackbar_Scroll);
            // 
            // SpeedTrackbar
            // 
            this.SpeedTrackbar.Location = new System.Drawing.Point(24, 146);
            this.SpeedTrackbar.Maximum = 80;
            this.SpeedTrackbar.Name = "SpeedTrackbar";
            this.SpeedTrackbar.Size = new System.Drawing.Size(324, 56);
            this.SpeedTrackbar.TabIndex = 1;
            this.SpeedTrackbar.TickFrequency = 5;
            this.SpeedTrackbar.Scroll += new System.EventHandler(this.TrackBar2_Scroll);
            // 
            // RPMtext
            // 
            this.RPMtext.AutoSize = true;
            this.RPMtext.Location = new System.Drawing.Point(40, 267);
            this.RPMtext.Name = "RPMtext";
            this.RPMtext.Size = new System.Drawing.Size(38, 17);
            this.RPMtext.TabIndex = 2;
            this.RPMtext.Text = "RPM";
            // 
            // PowerText
            // 
            this.PowerText.AutoSize = true;
            this.PowerText.Location = new System.Drawing.Point(40, 34);
            this.PowerText.Name = "PowerText";
            this.PowerText.Size = new System.Drawing.Size(60, 17);
            this.PowerText.TabIndex = 3;
            this.PowerText.Text = "POWER";
            // 
            // SpeedText
            // 
            this.SpeedText.AutoSize = true;
            this.SpeedText.Location = new System.Drawing.Point(40, 116);
            this.SpeedText.Name = "SpeedText";
            this.SpeedText.Size = new System.Drawing.Size(109, 17);
            this.SpeedText.TabIndex = 4;
            this.SpeedText.Text = "SPEED IN KM/H";
            // 
            // DistanceText
            // 
            this.DistanceText.AutoSize = true;
            this.DistanceText.Location = new System.Drawing.Point(40, 299);
            this.DistanceText.Name = "DistanceText";
            this.DistanceText.Size = new System.Drawing.Size(76, 17);
            this.DistanceText.TabIndex = 5;
            this.DistanceText.Text = "DISTANCE";
            // 
            // RpmLabel
            // 
            this.RpmLabel.AutoSize = true;
            this.RpmLabel.Location = new System.Drawing.Point(206, 267);
            this.RpmLabel.Name = "RpmLabel";
            this.RpmLabel.Size = new System.Drawing.Size(28, 17);
            this.RpmLabel.TabIndex = 6;
            this.RpmLabel.Text = "0.0";
            // 
            // DistanceLabel
            // 
            this.DistanceLabel.AutoSize = true;
            this.DistanceLabel.Location = new System.Drawing.Point(206, 299);
            this.DistanceLabel.Name = "DistanceLabel";
            this.DistanceLabel.Size = new System.Drawing.Size(28, 17);
            this.DistanceLabel.TabIndex = 7;
            this.DistanceLabel.Text = "0.0";
            // 
            // ResetButton
            // 
            this.ResetButton.ForeColor = System.Drawing.Color.Crimson;
            this.ResetButton.Location = new System.Drawing.Point(209, 394);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(124, 62);
            this.ResetButton.TabIndex = 9;
            this.ResetButton.Text = "RESET";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // PulseText
            // 
            this.PulseText.AutoSize = true;
            this.PulseText.Location = new System.Drawing.Point(40, 332);
            this.PulseText.Name = "PulseText";
            this.PulseText.Size = new System.Drawing.Size(53, 17);
            this.PulseText.TabIndex = 10;
            this.PulseText.Text = "PULSE";
            // 
            // PulseLabel
            // 
            this.PulseLabel.AutoSize = true;
            this.PulseLabel.Location = new System.Drawing.Point(206, 332);
            this.PulseLabel.Name = "PulseLabel";
            this.PulseLabel.Size = new System.Drawing.Size(28, 17);
            this.PulseLabel.TabIndex = 11;
            this.PulseLabel.Text = "0.0";
            // 
            // TimeText
            // 
            this.TimeText.AutoSize = true;
            this.TimeText.Location = new System.Drawing.Point(40, 235);
            this.TimeText.Name = "TimeText";
            this.TimeText.Size = new System.Drawing.Size(40, 17);
            this.TimeText.TabIndex = 12;
            this.TimeText.Text = "TIME";
            // 
            // TimeLabel
            // 
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Location = new System.Drawing.Point(206, 235);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(64, 17);
            this.TimeLabel.TabIndex = 13;
            this.TimeLabel.Text = "00:00:00";
            // 
            // PowerLabel
            // 
            this.PowerLabel.AutoSize = true;
            this.PowerLabel.Location = new System.Drawing.Point(305, 34);
            this.PowerLabel.Name = "PowerLabel";
            this.PowerLabel.Size = new System.Drawing.Size(16, 17);
            this.PowerLabel.TabIndex = 14;
            this.PowerLabel.Text = "0";
            // 
            // SpeedLabel
            // 
            this.SpeedLabel.Location = new System.Drawing.Point(293, 116);
            this.SpeedLabel.Name = "SpeedLabel";
            this.SpeedLabel.Size = new System.Drawing.Size(28, 17);
            this.SpeedLabel.TabIndex = 15;
            this.SpeedLabel.Text = "0";
            this.SpeedLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(305, 299);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 17);
            this.label6.TabIndex = 17;
            this.label6.Text = "KM";
            // 
            // PauseButton
            // 
            this.PauseButton.ForeColor = System.Drawing.Color.ForestGreen;
            this.PauseButton.Location = new System.Drawing.Point(43, 394);
            this.PauseButton.Name = "PauseButton";
            this.PauseButton.Size = new System.Drawing.Size(120, 28);
            this.PauseButton.TabIndex = 18;
            this.PauseButton.Text = "PAUSE";
            this.PauseButton.UseVisualStyleBackColor = true;
            this.PauseButton.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(41, 428);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(122, 28);
            this.StartButton.TabIndex = 19;
            this.StartButton.Text = "START";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // stop
            // 
            this.stop.Location = new System.Drawing.Point(153, 420);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(10, 10);
            this.stop.TabIndex = 20;
            this.stop.Text = "stop";
            this.stop.UseVisualStyleBackColor = true;
            this.stop.Visible = false;
            // 
            // Simulation
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(376, 483);
            this.Controls.Add(this.stop);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.PauseButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.SpeedLabel);
            this.Controls.Add(this.PowerLabel);
            this.Controls.Add(this.TimeLabel);
            this.Controls.Add(this.TimeText);
            this.Controls.Add(this.PulseLabel);
            this.Controls.Add(this.PulseText);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.DistanceLabel);
            this.Controls.Add(this.RpmLabel);
            this.Controls.Add(this.DistanceText);
            this.Controls.Add(this.SpeedText);
            this.Controls.Add(this.PowerText);
            this.Controls.Add(this.RPMtext);
            this.Controls.Add(this.SpeedTrackbar);
            this.Controls.Add(this.PowerTrackbar);
            this.ForeColor = System.Drawing.Color.DarkBlue;
            this.Name = "Simulation";
            this.Text = "Simulation";
            ((System.ComponentModel.ISupportInitialize)(this.PowerTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpeedTrackbar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar PowerTrackbar;
        private System.Windows.Forms.TrackBar SpeedTrackbar;
        private System.Windows.Forms.Label RPMtext;
        private System.Windows.Forms.Label PowerText;
        private System.Windows.Forms.Label SpeedText;
        private System.Windows.Forms.Label DistanceText;
        private System.Windows.Forms.Label RpmLabel;
        private System.Windows.Forms.Label DistanceLabel;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Label PulseText;
        private System.Windows.Forms.Label PulseLabel;
        private System.Windows.Forms.Label TimeText;
        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.Label PowerLabel;
        private System.Windows.Forms.Label SpeedLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button PauseButton;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button stop;
    }
}