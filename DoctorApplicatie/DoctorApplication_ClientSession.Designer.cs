namespace DoctorApplicatie
{
    partial class DoctorApplication_ClientSession
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
            this.RefreshHistoricUsers = new System.Windows.Forms.Button();
            this.getPastDataBtn = new System.Windows.Forms.Button();
            this.OlderDataComboBox = new System.Windows.Forms.ComboBox();
            this.getPastSessions = new System.Windows.Forms.Label();
            this.setPowerBtn = new System.Windows.Forms.Button();
            this.setPowerTxt = new System.Windows.Forms.TextBox();
            this.setPowerLbl = new System.Windows.Forms.Label();
            this.toAllBtn = new System.Windows.Forms.Button();
            this.sendToClientBtn = new System.Windows.Forms.Button();
            this.messageLbl = new System.Windows.Forms.Label();
            this.messageTxt = new System.Windows.Forms.TextBox();
            this.StopBtn = new System.Windows.Forms.Button();
            this.GetDataCurrent = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RefreshHistoricUsers
            // 
            this.RefreshHistoricUsers.Location = new System.Drawing.Point(169, 275);
            this.RefreshHistoricUsers.Name = "RefreshHistoricUsers";
            this.RefreshHistoricUsers.Size = new System.Drawing.Size(84, 24);
            this.RefreshHistoricUsers.TabIndex = 27;
            this.RefreshHistoricUsers.Text = "Refresh";
            this.RefreshHistoricUsers.UseVisualStyleBackColor = true;
            // 
            // getPastDataBtn
            // 
            this.getPastDataBtn.BackColor = System.Drawing.SystemColors.Control;
            this.getPastDataBtn.Location = new System.Drawing.Point(265, 275);
            this.getPastDataBtn.Name = "getPastDataBtn";
            this.getPastDataBtn.Size = new System.Drawing.Size(75, 27);
            this.getPastDataBtn.TabIndex = 26;
            this.getPastDataBtn.Text = "getData";
            this.getPastDataBtn.UseVisualStyleBackColor = false;
            this.getPastDataBtn.Click += new System.EventHandler(this.getPastDataBtn_Click);
            // 
            // OlderDataComboBox
            // 
            this.OlderDataComboBox.FormattingEnabled = true;
            this.OlderDataComboBox.Location = new System.Drawing.Point(8, 279);
            this.OlderDataComboBox.Name = "OlderDataComboBox";
            this.OlderDataComboBox.Size = new System.Drawing.Size(123, 24);
            this.OlderDataComboBox.TabIndex = 25;
            // 
            // getPastSessions
            // 
            this.getPastSessions.AutoSize = true;
            this.getPastSessions.Location = new System.Drawing.Point(9, 259);
            this.getPastSessions.Name = "getPastSessions";
            this.getPastSessions.Size = new System.Drawing.Size(101, 17);
            this.getPastSessions.TabIndex = 24;
            this.getPastSessions.Text = "Past Sessions:";
            // 
            // setPowerBtn
            // 
            this.setPowerBtn.BackColor = System.Drawing.SystemColors.Control;
            this.setPowerBtn.Location = new System.Drawing.Point(169, 200);
            this.setPowerBtn.Name = "setPowerBtn";
            this.setPowerBtn.Size = new System.Drawing.Size(84, 24);
            this.setPowerBtn.TabIndex = 23;
            this.setPowerBtn.Text = "setPower";
            this.setPowerBtn.UseVisualStyleBackColor = false;
            this.setPowerBtn.Click += new System.EventHandler(this.setPowerBtn_Click);
            // 
            // setPowerTxt
            // 
            this.setPowerTxt.Location = new System.Drawing.Point(9, 200);
            this.setPowerTxt.Name = "setPowerTxt";
            this.setPowerTxt.Size = new System.Drawing.Size(122, 22);
            this.setPowerTxt.TabIndex = 22;
            // 
            // setPowerLbl
            // 
            this.setPowerLbl.AutoSize = true;
            this.setPowerLbl.Location = new System.Drawing.Point(12, 182);
            this.setPowerLbl.Name = "setPowerLbl";
            this.setPowerLbl.Size = new System.Drawing.Size(76, 17);
            this.setPowerLbl.TabIndex = 21;
            this.setPowerLbl.Text = "Set Power:";
            // 
            // toAllBtn
            // 
            this.toAllBtn.BackColor = System.Drawing.SystemColors.Control;
            this.toAllBtn.Location = new System.Drawing.Point(265, 126);
            this.toAllBtn.Name = "toAllBtn";
            this.toAllBtn.Size = new System.Drawing.Size(75, 23);
            this.toAllBtn.TabIndex = 20;
            this.toAllBtn.Text = "AllClients";
            this.toAllBtn.UseVisualStyleBackColor = false;
            this.toAllBtn.Click += new System.EventHandler(this.sendToClientBtn_Click);
            // 
            // sendToClientBtn
            // 
            this.sendToClientBtn.BackColor = System.Drawing.SystemColors.Control;
            this.sendToClientBtn.Location = new System.Drawing.Point(169, 124);
            this.sendToClientBtn.Name = "sendToClientBtn";
            this.sendToClientBtn.Size = new System.Drawing.Size(75, 25);
            this.sendToClientBtn.TabIndex = 19;
            this.sendToClientBtn.Text = "toClient";
            this.sendToClientBtn.UseVisualStyleBackColor = false;
            this.sendToClientBtn.Click += new System.EventHandler(this.sendToClientBtn_Click);
            // 
            // messageLbl
            // 
            this.messageLbl.AutoSize = true;
            this.messageLbl.Location = new System.Drawing.Point(12, 106);
            this.messageLbl.Name = "messageLbl";
            this.messageLbl.Size = new System.Drawing.Size(69, 17);
            this.messageLbl.TabIndex = 18;
            this.messageLbl.Text = "Message:";
            // 
            // messageTxt
            // 
            this.messageTxt.Location = new System.Drawing.Point(12, 125);
            this.messageTxt.Name = "messageTxt";
            this.messageTxt.Size = new System.Drawing.Size(119, 22);
            this.messageTxt.TabIndex = 17;
            // 
            // StopBtn
            // 
            this.StopBtn.BackColor = System.Drawing.SystemColors.Control;
            this.StopBtn.Location = new System.Drawing.Point(169, 47);
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.Size = new System.Drawing.Size(75, 23);
            this.StopBtn.TabIndex = 28;
            this.StopBtn.Text = "stop";
            this.StopBtn.UseVisualStyleBackColor = false;
            this.StopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // GetDataCurrent
            // 
            this.GetDataCurrent.Location = new System.Drawing.Point(360, 275);
            this.GetDataCurrent.Name = "GetDataCurrent";
            this.GetDataCurrent.Size = new System.Drawing.Size(81, 28);
            this.GetDataCurrent.TabIndex = 29;
            this.GetDataCurrent.Text = "current";
            this.GetDataCurrent.UseVisualStyleBackColor = true;
            // 
            // DoctorApplication_ClientSession
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 422);
            this.Controls.Add(this.GetDataCurrent);
            this.Controls.Add(this.StopBtn);
            this.Controls.Add(this.RefreshHistoricUsers);
            this.Controls.Add(this.getPastDataBtn);
            this.Controls.Add(this.OlderDataComboBox);
            this.Controls.Add(this.getPastSessions);
            this.Controls.Add(this.setPowerBtn);
            this.Controls.Add(this.setPowerTxt);
            this.Controls.Add(this.setPowerLbl);
            this.Controls.Add(this.toAllBtn);
            this.Controls.Add(this.sendToClientBtn);
            this.Controls.Add(this.messageLbl);
            this.Controls.Add(this.messageTxt);
            this.Name = "DoctorApplication_ClientSession";
            this.Text = "DoctorApplication_ClientSession";
            this.Load += new System.EventHandler(this.DoctorApplication_ClientSession_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RefreshHistoricUsers;
        private System.Windows.Forms.Button getPastDataBtn;
        private System.Windows.Forms.ComboBox OlderDataComboBox;
        private System.Windows.Forms.Label getPastSessions;
        private System.Windows.Forms.Button setPowerBtn;
        private System.Windows.Forms.TextBox setPowerTxt;
        private System.Windows.Forms.Label setPowerLbl;
        private System.Windows.Forms.Button toAllBtn;
        private System.Windows.Forms.Button sendToClientBtn;
        private System.Windows.Forms.Label messageLbl;
        private System.Windows.Forms.TextBox messageTxt;
        private System.Windows.Forms.Button StopBtn;
        private System.Windows.Forms.Button GetDataCurrent;
    }
}