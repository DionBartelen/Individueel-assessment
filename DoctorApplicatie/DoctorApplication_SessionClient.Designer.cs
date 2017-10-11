namespace DoctorApplicatie
{
    partial class DoctorApplication_SessionClient
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.setPowerBtn = new System.Windows.Forms.Button();
            this.setPowerTxt = new System.Windows.Forms.TextBox();
            this.setPowerLbl = new System.Windows.Forms.Label();
            this.toAllBtn = new System.Windows.Forms.Button();
            this.sendToClientBtn = new System.Windows.Forms.Button();
            this.messageLbl = new System.Windows.Forms.Label();
            this.messageTxt = new System.Windows.Forms.TextBox();
            this.StopBtn = new System.Windows.Forms.Button();
            this.startBtn = new System.Windows.Forms.Button();
            this.TrainingLbl = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.unFollowBtn = new System.Windows.Forms.Button();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.Distancelbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.InformationBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.SuspendLayout();
            // 
            // setPowerBtn
            // 
            this.setPowerBtn.BackColor = System.Drawing.SystemColors.Control;
            this.setPowerBtn.Location = new System.Drawing.Point(168, 112);
            this.setPowerBtn.Name = "setPowerBtn";
            this.setPowerBtn.Size = new System.Drawing.Size(84, 29);
            this.setPowerBtn.TabIndex = 18;
            this.setPowerBtn.Text = "Set Power";
            this.setPowerBtn.UseVisualStyleBackColor = false;
            this.setPowerBtn.Click += new System.EventHandler(this.setPowerBtn_Click);
            // 
            // setPowerTxt
            // 
            this.setPowerTxt.Location = new System.Drawing.Point(8, 119);
            this.setPowerTxt.Name = "setPowerTxt";
            this.setPowerTxt.Size = new System.Drawing.Size(122, 22);
            this.setPowerTxt.TabIndex = 17;
            // 
            // setPowerLbl
            // 
            this.setPowerLbl.AutoSize = true;
            this.setPowerLbl.Location = new System.Drawing.Point(11, 101);
            this.setPowerLbl.Name = "setPowerLbl";
            this.setPowerLbl.Size = new System.Drawing.Size(76, 17);
            this.setPowerLbl.TabIndex = 16;
            this.setPowerLbl.Text = "Set Power:";
            // 
            // toAllBtn
            // 
            this.toAllBtn.BackColor = System.Drawing.SystemColors.Control;
            this.toAllBtn.Location = new System.Drawing.Point(255, 65);
            this.toAllBtn.Name = "toAllBtn";
            this.toAllBtn.Size = new System.Drawing.Size(108, 26);
            this.toAllBtn.TabIndex = 15;
            this.toAllBtn.Text = "All Clients";
            this.toAllBtn.UseVisualStyleBackColor = false;
            this.toAllBtn.Click += new System.EventHandler(this.toAllBtn_Click);
            // 
            // sendToClientBtn
            // 
            this.sendToClientBtn.BackColor = System.Drawing.SystemColors.Control;
            this.sendToClientBtn.Location = new System.Drawing.Point(168, 65);
            this.sendToClientBtn.Name = "sendToClientBtn";
            this.sendToClientBtn.Size = new System.Drawing.Size(75, 26);
            this.sendToClientBtn.TabIndex = 14;
            this.sendToClientBtn.Text = "To Client";
            this.sendToClientBtn.UseVisualStyleBackColor = false;
            this.sendToClientBtn.Click += new System.EventHandler(this.sendToClientBtn_Click);
            // 
            // messageLbl
            // 
            this.messageLbl.AutoSize = true;
            this.messageLbl.Location = new System.Drawing.Point(11, 48);
            this.messageLbl.Name = "messageLbl";
            this.messageLbl.Size = new System.Drawing.Size(69, 17);
            this.messageLbl.TabIndex = 13;
            this.messageLbl.Text = "Message:";
            // 
            // messageTxt
            // 
            this.messageTxt.Location = new System.Drawing.Point(11, 67);
            this.messageTxt.Name = "messageTxt";
            this.messageTxt.Size = new System.Drawing.Size(119, 22);
            this.messageTxt.TabIndex = 12;
            // 
            // StopBtn
            // 
            this.StopBtn.BackColor = System.Drawing.SystemColors.Control;
            this.StopBtn.Enabled = false;
            this.StopBtn.Location = new System.Drawing.Point(255, 13);
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.Size = new System.Drawing.Size(108, 28);
            this.StopBtn.TabIndex = 19;
            this.StopBtn.Text = "Stop";
            this.StopBtn.UseVisualStyleBackColor = false;
            this.StopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // startBtn
            // 
            this.startBtn.BackColor = System.Drawing.SystemColors.Control;
            this.startBtn.Location = new System.Drawing.Point(168, 13);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(75, 28);
            this.startBtn.TabIndex = 21;
            this.startBtn.Text = "Start";
            this.startBtn.UseVisualStyleBackColor = false;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // TrainingLbl
            // 
            this.TrainingLbl.AutoSize = true;
            this.TrainingLbl.Location = new System.Drawing.Point(11, 13);
            this.TrainingLbl.Name = "TrainingLbl";
            this.TrainingLbl.Size = new System.Drawing.Size(60, 17);
            this.TrainingLbl.TabIndex = 20;
            this.TrainingLbl.Text = "Training";
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 210);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(714, 642);
            this.chart1.TabIndex = 22;
            this.chart1.Text = "chart1";
            // 
            // unFollowBtn
            // 
            this.unFollowBtn.Location = new System.Drawing.Point(369, 13);
            this.unFollowBtn.Name = "unFollowBtn";
            this.unFollowBtn.Size = new System.Drawing.Size(89, 28);
            this.unFollowBtn.TabIndex = 23;
            this.unFollowBtn.Text = "Unfollow";
            this.unFollowBtn.UseVisualStyleBackColor = true;
            this.unFollowBtn.Click += new System.EventHandler(this.unFollowBtn_Click);
            // 
            // chart2
            // 
            chartArea2.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart2.Legends.Add(legend2);
            this.chart2.Location = new System.Drawing.Point(720, 210);
            this.chart2.Name = "chart2";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart2.Series.Add(series2);
            this.chart2.Size = new System.Drawing.Size(742, 650);
            this.chart2.TabIndex = 24;
            this.chart2.Text = "chart2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 163);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 17);
            this.label1.TabIndex = 25;
            this.label1.Text = "Distance: ";
            // 
            // Distancelbl
            // 
            this.Distancelbl.AutoSize = true;
            this.Distancelbl.Location = new System.Drawing.Point(100, 163);
            this.Distancelbl.Name = "Distancelbl";
            this.Distancelbl.Size = new System.Drawing.Size(0, 17);
            this.Distancelbl.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(262, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 17);
            this.label2.TabIndex = 27;
            this.label2.Text = "KM";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // InformationBtn
            // 
            this.InformationBtn.Location = new System.Drawing.Point(1396, 1);
            this.InformationBtn.Name = "InformationBtn";
            this.InformationBtn.Size = new System.Drawing.Size(66, 35);
            this.InformationBtn.TabIndex = 28;
            this.InformationBtn.Text = "Info";
            this.InformationBtn.UseVisualStyleBackColor = true;
            this.InformationBtn.Click += new System.EventHandler(this.InformationBtn_Click);
            // 
            // DoctorApplication_SessionClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1465, 872);
            this.Controls.Add(this.InformationBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Distancelbl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.unFollowBtn);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.TrainingLbl);
            this.Controls.Add(this.StopBtn);
            this.Controls.Add(this.setPowerBtn);
            this.Controls.Add(this.setPowerTxt);
            this.Controls.Add(this.setPowerLbl);
            this.Controls.Add(this.toAllBtn);
            this.Controls.Add(this.sendToClientBtn);
            this.Controls.Add(this.messageLbl);
            this.Controls.Add(this.messageTxt);
            this.Name = "DoctorApplication_SessionClient";
            this.Text = "DoctorApplication_SessionClient";
            this.Load += new System.EventHandler(this.DoctorApplication_SessionClient_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button setPowerBtn;
        private System.Windows.Forms.TextBox setPowerTxt;
        private System.Windows.Forms.Label setPowerLbl;
        private System.Windows.Forms.Button toAllBtn;
        private System.Windows.Forms.Button sendToClientBtn;
        private System.Windows.Forms.Label messageLbl;
        private System.Windows.Forms.TextBox messageTxt;
        private System.Windows.Forms.Button StopBtn;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Label TrainingLbl;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button unFollowBtn;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Distancelbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button InformationBtn;
    }
}