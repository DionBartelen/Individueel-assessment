namespace WindowsFormsApp1
{
    partial class GebruikerGegevensAstrandForm
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
            this.SexComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.AgeTextBox = new System.Windows.Forms.TextBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SexComboBox
            // 
            this.SexComboBox.FormattingEnabled = true;
            this.SexComboBox.Items.AddRange(new object[] {
            "Male",
            "Female"});
            this.SexComboBox.Location = new System.Drawing.Point(102, 47);
            this.SexComboBox.Name = "SexComboBox";
            this.SexComboBox.Size = new System.Drawing.Size(121, 24);
            this.SexComboBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sex:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Age:";
            // 
            // AgeTextBox
            // 
            this.AgeTextBox.Location = new System.Drawing.Point(102, 88);
            this.AgeTextBox.Name = "AgeTextBox";
            this.AgeTextBox.Size = new System.Drawing.Size(121, 22);
            this.AgeTextBox.TabIndex = 3;
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(27, 143);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(196, 23);
            this.OkButton.TabIndex = 4;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // GebruikerGegevensAstrandForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 290);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.AgeTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SexComboBox);
            this.Name = "GebruikerGegevensAstrandForm";
            this.Text = "GebruikerGegevensAstrandForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox SexComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox AgeTextBox;
        private System.Windows.Forms.Button OkButton;
    }
}