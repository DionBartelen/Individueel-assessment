namespace DoctorApplicatie
{
    partial class DoctorAplicatie
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
            this.usernameLabel = new System.Windows.Forms.Label();
            this.usernameTxt = new System.Windows.Forms.TextBox();
            this.passwordLbl = new System.Windows.Forms.Label();
            this.passwordTxt = new System.Windows.Forms.TextBox();
            this.sign_in_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // usernameLabel
            // 
            this.usernameLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.usernameLabel.Location = new System.Drawing.Point(55, 102);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(99, 25);
            this.usernameLabel.TabIndex = 0;
            this.usernameLabel.Text = "username";
            // 
            // usernameTxt
            // 
            this.usernameTxt.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.usernameTxt.Location = new System.Drawing.Point(183, 102);
            this.usernameTxt.Name = "usernameTxt";
            this.usernameTxt.Size = new System.Drawing.Size(220, 22);
            this.usernameTxt.TabIndex = 1;
            this.usernameTxt.Text = "Dokter van de Steen";
            // 
            // passwordLbl
            // 
            this.passwordLbl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.passwordLbl.AutoSize = true;
            this.passwordLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordLbl.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.passwordLbl.Location = new System.Drawing.Point(55, 141);
            this.passwordLbl.Name = "passwordLbl";
            this.passwordLbl.Size = new System.Drawing.Size(96, 25);
            this.passwordLbl.TabIndex = 2;
            this.passwordLbl.Text = "password";
            // 
            // passwordTxt
            // 
            this.passwordTxt.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.passwordTxt.Location = new System.Drawing.Point(183, 141);
            this.passwordTxt.Name = "passwordTxt";
            this.passwordTxt.PasswordChar = '*';
            this.passwordTxt.Size = new System.Drawing.Size(220, 22);
            this.passwordTxt.TabIndex = 3;
            this.passwordTxt.Text = "Doktervds123";
            // 
            // sign_in_btn
            // 
            this.sign_in_btn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.sign_in_btn.BackColor = System.Drawing.SystemColors.Control;
            this.sign_in_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sign_in_btn.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.sign_in_btn.Location = new System.Drawing.Point(183, 179);
            this.sign_in_btn.Name = "sign_in_btn";
            this.sign_in_btn.Size = new System.Drawing.Size(220, 38);
            this.sign_in_btn.TabIndex = 4;
            this.sign_in_btn.Text = "sign in ";
            this.sign_in_btn.UseVisualStyleBackColor = false;
            this.sign_in_btn.Click += new System.EventHandler(this.sign_in_btn_Click);
            // 
            // DoctorAplicatie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(492, 329);
            this.Controls.Add(this.sign_in_btn);
            this.Controls.Add(this.passwordTxt);
            this.Controls.Add(this.passwordLbl);
            this.Controls.Add(this.usernameTxt);
            this.Controls.Add(this.usernameLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DoctorAplicatie";
            this.Text = "doctor aplicatie ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.TextBox usernameTxt;
        private System.Windows.Forms.Label passwordLbl;
        private System.Windows.Forms.TextBox passwordTxt;
        private System.Windows.Forms.Button sign_in_btn;
    }
}

