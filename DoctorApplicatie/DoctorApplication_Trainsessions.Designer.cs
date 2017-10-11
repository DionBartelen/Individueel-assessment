namespace DoctorApplicatie
{
    partial class DoctorApplication_Trainsessions
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
            this.ShowComboBox = new System.Windows.Forms.ComboBox();
            this.ShowTrainSessionButton = new System.Windows.Forms.Button();
            this.infoBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ShowComboBox
            // 
            this.ShowComboBox.FormattingEnabled = true;
            this.ShowComboBox.Location = new System.Drawing.Point(28, 39);
            this.ShowComboBox.Name = "ShowComboBox";
            this.ShowComboBox.Size = new System.Drawing.Size(121, 24);
            this.ShowComboBox.TabIndex = 0;
            // 
            // ShowTrainSessionButton
            // 
            this.ShowTrainSessionButton.Location = new System.Drawing.Point(197, 39);
            this.ShowTrainSessionButton.Name = "ShowTrainSessionButton";
            this.ShowTrainSessionButton.Size = new System.Drawing.Size(75, 23);
            this.ShowTrainSessionButton.TabIndex = 1;
            this.ShowTrainSessionButton.Text = "Show";
            this.ShowTrainSessionButton.UseVisualStyleBackColor = true;
            this.ShowTrainSessionButton.Click += new System.EventHandler(this.ShowTrainSessionButton_Click);
            // 
            // infoBtn
            // 
            this.infoBtn.Location = new System.Drawing.Point(208, 0);
            this.infoBtn.Name = "infoBtn";
            this.infoBtn.Size = new System.Drawing.Size(75, 23);
            this.infoBtn.TabIndex = 2;
            this.infoBtn.Text = "info";
            this.infoBtn.UseVisualStyleBackColor = true;
            this.infoBtn.Click += new System.EventHandler(this.infoBtn_Click);
            // 
            // DoctorApplication_Trainsessions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 257);
            this.Controls.Add(this.infoBtn);
            this.Controls.Add(this.ShowTrainSessionButton);
            this.Controls.Add(this.ShowComboBox);
            this.Name = "DoctorApplication_Trainsessions";
            this.Text = "DoctorApplication_Trainsessions";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox ShowComboBox;
        private System.Windows.Forms.Button ShowTrainSessionButton;
        private System.Windows.Forms.Button infoBtn;
    }
}