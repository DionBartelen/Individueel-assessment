namespace WindowsFormsApp1
{
    partial class ChatPanel
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
            this.chatbox = new System.Windows.Forms.TextBox();
            this.confirmButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chatbox
            // 
            this.chatbox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.chatbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chatbox.Location = new System.Drawing.Point(-1, 2);
            this.chatbox.Multiline = true;
            this.chatbox.Name = "chatbox";
            this.chatbox.ReadOnly = true;
            this.chatbox.Size = new System.Drawing.Size(855, 353);
            this.chatbox.TabIndex = 0;
            // 
            // confirmButton
            // 
            this.confirmButton.Location = new System.Drawing.Point(13, 361);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(828, 38);
            this.confirmButton.TabIndex = 1;
            this.confirmButton.Text = "Begrepen";
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
            // 
            // ChatPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 411);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.chatbox);
            this.Name = "ChatPanel";
            this.Text = "ChatPanel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox chatbox;
        private System.Windows.Forms.Button confirmButton;
    }
}