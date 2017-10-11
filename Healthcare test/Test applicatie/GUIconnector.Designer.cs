using System.IO.Ports;

namespace Healthcare_test
{
    partial class GUIconnector
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
            this.CommandInput = new System.Windows.Forms.TextBox();
            this.SendInput = new System.Windows.Forms.Button();
            this.replyBoxText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ConnectSerial = new System.Windows.Forms.Button();
            this.ComPortText = new System.Windows.Forms.ComboBox();
            this.Data_Collector = new System.Windows.Forms.Button();
            this.BaudRateText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // CommandInput
            // 
            this.CommandInput.Location = new System.Drawing.Point(34, 285);
            this.CommandInput.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CommandInput.Name = "CommandInput";
            this.CommandInput.Size = new System.Drawing.Size(366, 20);
            this.CommandInput.TabIndex = 0;
            // 
            // SendInput
            // 
            this.SendInput.ForeColor = System.Drawing.Color.DarkBlue;
            this.SendInput.Location = new System.Drawing.Point(424, 285);
            this.SendInput.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SendInput.Name = "SendInput";
            this.SendInput.Size = new System.Drawing.Size(56, 19);
            this.SendInput.TabIndex = 1;
            this.SendInput.Text = "Send";
            this.SendInput.UseVisualStyleBackColor = true;
            this.SendInput.Click += new System.EventHandler(this.SendInput_Click);
            // 
            // replyBoxText
            // 
            this.replyBoxText.Location = new System.Drawing.Point(34, 67);
            this.replyBoxText.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.replyBoxText.Multiline = true;
            this.replyBoxText.Name = "replyBoxText";
            this.replyBoxText.ReadOnly = true;
            this.replyBoxText.Size = new System.Drawing.Size(366, 194);
            this.replyBoxText.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(32, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "COM:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(179, 16);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "BAUD:";
            // 
            // ConnectSerial
            // 
            this.ConnectSerial.ForeColor = System.Drawing.Color.DarkBlue;
            this.ConnectSerial.Location = new System.Drawing.Point(331, 13);
            this.ConnectSerial.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ConnectSerial.Name = "ConnectSerial";
            this.ConnectSerial.Size = new System.Drawing.Size(68, 25);
            this.ConnectSerial.TabIndex = 7;
            this.ConnectSerial.Text = "CONNECT";
            this.ConnectSerial.UseVisualStyleBackColor = true;
            this.ConnectSerial.Click += new System.EventHandler(this.ConnectSerial_Click);
            // 
            // ComPortText
            // 
            this.ComPortText.FormattingEnabled = true;
            this.ComPortText.Items.AddRange(new object[] {
            "Simulator"});
            this.ComPortText.Location = new System.Drawing.Point(68, 14);
            this.ComPortText.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ComPortText.Name = "ComPortText";
            this.ComPortText.Size = new System.Drawing.Size(92, 21);
            this.ComPortText.TabIndex = 8;
            this.ComPortText.SelectedIndexChanged += new System.EventHandler(this.ComPortText_SelectedIndexChanged);
            // 
            // Data_Collector
            // 
            this.Data_Collector.Enabled = false;
            this.Data_Collector.ForeColor = System.Drawing.Color.DarkBlue;
            this.Data_Collector.Location = new System.Drawing.Point(424, 67);
            this.Data_Collector.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Data_Collector.Name = "Data_Collector";
            this.Data_Collector.Size = new System.Drawing.Size(70, 23);
            this.Data_Collector.TabIndex = 9;
            this.Data_Collector.Text = "current data ";
            this.Data_Collector.UseVisualStyleBackColor = true;
            this.Data_Collector.Click += new System.EventHandler(this.Data_Collector_Click);
            // 
            // BaudRateText
            // 
            this.BaudRateText.Location = new System.Drawing.Point(221, 16);
            this.BaudRateText.Margin = new System.Windows.Forms.Padding(2);
            this.BaudRateText.Name = "BaudRateText";
            this.BaudRateText.Size = new System.Drawing.Size(76, 20);
            this.BaudRateText.TabIndex = 6;
            this.BaudRateText.Text = "9600";
            // 
            // GUIconnector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(514, 332);
            this.Controls.Add(this.Data_Collector);
            this.Controls.Add(this.ComPortText);
            this.Controls.Add(this.ConnectSerial);
            this.Controls.Add(this.BaudRateText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.replyBoxText);
            this.Controls.Add(this.SendInput);
            this.Controls.Add(this.CommandInput);
            this.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "GUIconnector";
            this.Text = "Remote Healthcare";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.GUIconnector_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox CommandInput;
        private System.Windows.Forms.Button SendInput;
        private System.Windows.Forms.TextBox replyBoxText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ConnectSerial;
        private System.Windows.Forms.ComboBox ComPortText;
        private System.Windows.Forms.Button Data_Collector;
        private System.Windows.Forms.TextBox BaudRateText;
    }
}

