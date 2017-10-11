namespace DoctorApplicatie
{
    partial class DoctorApplication_Session
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
            this.ConectedSessionsListCombo = new System.Windows.Forms.ComboBox();
            this.ConnectedSessionsListLbl = new System.Windows.Forms.Label();
            this.getPastSessions = new System.Windows.Forms.Label();
            this.OlderDataComboBox = new System.Windows.Forms.ComboBox();
            this.getPastDataBtn = new System.Windows.Forms.Button();
            this.RefreshConnectedButton = new System.Windows.Forms.Button();
            this.RefreshHistoricUsers = new System.Windows.Forms.Button();
            this.followBtn = new System.Windows.Forms.Button();
            this.informationBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ConectedSessionsListCombo
            // 
            this.ConectedSessionsListCombo.FormattingEnabled = true;
            this.ConectedSessionsListCombo.Location = new System.Drawing.Point(173, 31);
            this.ConectedSessionsListCombo.Name = "ConectedSessionsListCombo";
            this.ConectedSessionsListCombo.Size = new System.Drawing.Size(162, 24);
            this.ConectedSessionsListCombo.TabIndex = 0;
            // 
            // ConnectedSessionsListLbl
            // 
            this.ConnectedSessionsListLbl.AutoSize = true;
            this.ConnectedSessionsListLbl.Location = new System.Drawing.Point(13, 37);
            this.ConnectedSessionsListLbl.Name = "ConnectedSessionsListLbl";
            this.ConnectedSessionsListLbl.Size = new System.Drawing.Size(137, 17);
            this.ConnectedSessionsListLbl.TabIndex = 1;
            this.ConnectedSessionsListLbl.Text = "Connected Sessions";
            // 
            // getPastSessions
            // 
            this.getPastSessions.AutoSize = true;
            this.getPastSessions.Location = new System.Drawing.Point(17, 117);
            this.getPastSessions.Name = "getPastSessions";
            this.getPastSessions.Size = new System.Drawing.Size(101, 17);
            this.getPastSessions.TabIndex = 12;
            this.getPastSessions.Text = "Past Sessions:";
            // 
            // OlderDataComboBox
            // 
            this.OlderDataComboBox.FormattingEnabled = true;
            this.OlderDataComboBox.Location = new System.Drawing.Point(16, 137);
            this.OlderDataComboBox.Name = "OlderDataComboBox";
            this.OlderDataComboBox.Size = new System.Drawing.Size(123, 24);
            this.OlderDataComboBox.TabIndex = 13;
            // 
            // getPastDataBtn
            // 
            this.getPastDataBtn.BackColor = System.Drawing.SystemColors.Control;
            this.getPastDataBtn.Location = new System.Drawing.Point(264, 137);
            this.getPastDataBtn.Name = "getPastDataBtn";
            this.getPastDataBtn.Size = new System.Drawing.Size(75, 27);
            this.getPastDataBtn.TabIndex = 14;
            this.getPastDataBtn.Text = "getData";
            this.getPastDataBtn.UseVisualStyleBackColor = false;
            this.getPastDataBtn.Click += new System.EventHandler(this.getPastDataBtn_Click);
            // 
            // RefreshConnectedButton
            // 
            this.RefreshConnectedButton.Location = new System.Drawing.Point(352, 31);
            this.RefreshConnectedButton.Name = "RefreshConnectedButton";
            this.RefreshConnectedButton.Size = new System.Drawing.Size(75, 23);
            this.RefreshConnectedButton.TabIndex = 15;
            this.RefreshConnectedButton.Text = "Refresh";
            this.RefreshConnectedButton.UseVisualStyleBackColor = true;
            this.RefreshConnectedButton.Click += new System.EventHandler(this.RefreshConnectedButton_Click);
            // 
            // RefreshHistoricUsers
            // 
            this.RefreshHistoricUsers.Location = new System.Drawing.Point(177, 137);
            this.RefreshHistoricUsers.Name = "RefreshHistoricUsers";
            this.RefreshHistoricUsers.Size = new System.Drawing.Size(75, 23);
            this.RefreshHistoricUsers.TabIndex = 16;
            this.RefreshHistoricUsers.Text = "Refresh";
            this.RefreshHistoricUsers.UseVisualStyleBackColor = true;
            this.RefreshHistoricUsers.Click += new System.EventHandler(this.RefreshHistoricUsers_Click);
            // 
            // followBtn
            // 
            this.followBtn.Location = new System.Drawing.Point(433, 31);
            this.followBtn.Name = "followBtn";
            this.followBtn.Size = new System.Drawing.Size(75, 23);
            this.followBtn.TabIndex = 17;
            this.followBtn.Text = "Follow";
            this.followBtn.UseVisualStyleBackColor = true;
            this.followBtn.Click += new System.EventHandler(this.followBtn_Click);
            // 
            // informationBtn
            // 
            this.informationBtn.Location = new System.Drawing.Point(13, 13);
            this.informationBtn.Name = "informationBtn";
            this.informationBtn.Size = new System.Drawing.Size(75, 23);
            this.informationBtn.TabIndex = 18;
            this.informationBtn.Text = "Info";
            this.informationBtn.UseVisualStyleBackColor = true;
            this.informationBtn.Click += new System.EventHandler(this.informationBtn_Click);
            // 
            // DoctorApplication_Session
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(511, 479);
            this.Controls.Add(this.informationBtn);
            this.Controls.Add(this.followBtn);
            this.Controls.Add(this.RefreshHistoricUsers);
            this.Controls.Add(this.RefreshConnectedButton);
            this.Controls.Add(this.getPastDataBtn);
            this.Controls.Add(this.OlderDataComboBox);
            this.Controls.Add(this.getPastSessions);
            this.Controls.Add(this.ConnectedSessionsListLbl);
            this.Controls.Add(this.ConectedSessionsListCombo);
            this.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.Name = "DoctorApplication_Session";
            this.Text = "DoctorApplication_Session";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ConectedSessionsListCombo;
        private System.Windows.Forms.Label ConnectedSessionsListLbl;
        private System.Windows.Forms.Label getPastSessions;
        private System.Windows.Forms.ComboBox OlderDataComboBox;
        private System.Windows.Forms.Button getPastDataBtn;
        private System.Windows.Forms.Button RefreshConnectedButton;
        private System.Windows.Forms.Button RefreshHistoricUsers;
        private System.Windows.Forms.Button followBtn;
        private System.Windows.Forms.Button informationBtn;
    }
}