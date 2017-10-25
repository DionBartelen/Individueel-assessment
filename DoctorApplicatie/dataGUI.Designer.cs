namespace DoctorApplicatie
{
    partial class dataGUI
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.Chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.gemiddeldeHFLbl = new System.Windows.Forms.Label();
            this.gemiddeldeHFInputLbl = new System.Windows.Forms.Label();
            this.VO2MaxLbl = new System.Windows.Forms.Label();
            this.VO2MaxInputLbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chart2)).BeginInit();
            this.SuspendLayout();
            // 
            // Chart
            // 
            chartArea3.Name = "ChartArea1";
            this.Chart.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.Chart.Legends.Add(legend3);
            this.Chart.Location = new System.Drawing.Point(-1, 1);
            this.Chart.Name = "Chart";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series3.Legend = "Legend1";
            series3.MarkerBorderWidth = 2;
            series3.Name = "Series1";
            series3.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series3.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.Chart.Series.Add(series3);
            this.Chart.Size = new System.Drawing.Size(1327, 404);
            this.Chart.TabIndex = 0;
            this.Chart.Text = "+";
            // 
            // Chart2
            // 
            chartArea4.Name = "ChartArea1";
            this.Chart2.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.Chart2.Legends.Add(legend4);
            this.Chart2.Location = new System.Drawing.Point(-1, 402);
            this.Chart2.Name = "Chart2";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.IsXValueIndexed = true;
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            series4.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Single;
            series4.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.Chart2.Series.Add(series4);
            this.Chart2.Size = new System.Drawing.Size(1327, 404);
            this.Chart2.TabIndex = 0;
            this.Chart2.Text = "chart1";
            // 
            // gemiddeldeHFLbl
            // 
            this.gemiddeldeHFLbl.AutoSize = true;
            this.gemiddeldeHFLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gemiddeldeHFLbl.Location = new System.Drawing.Point(12, 809);
            this.gemiddeldeHFLbl.Name = "gemiddeldeHFLbl";
            this.gemiddeldeHFLbl.Size = new System.Drawing.Size(193, 29);
            this.gemiddeldeHFLbl.TabIndex = 1;
            this.gemiddeldeHFLbl.Text = "gemiddelde HF: ";
            // 
            // gemiddeldeHFInputLbl
            // 
            this.gemiddeldeHFInputLbl.AutoSize = true;
            this.gemiddeldeHFInputLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gemiddeldeHFInputLbl.Location = new System.Drawing.Point(17, 842);
            this.gemiddeldeHFInputLbl.Name = "gemiddeldeHFInputLbl";
            this.gemiddeldeHFInputLbl.Size = new System.Drawing.Size(26, 29);
            this.gemiddeldeHFInputLbl.TabIndex = 2;
            this.gemiddeldeHFInputLbl.Text = "0";
            // 
            // VO2MaxLbl
            // 
            this.VO2MaxLbl.AutoSize = true;
            this.VO2MaxLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VO2MaxLbl.Location = new System.Drawing.Point(285, 809);
            this.VO2MaxLbl.Name = "VO2MaxLbl";
            this.VO2MaxLbl.Size = new System.Drawing.Size(110, 29);
            this.VO2MaxLbl.TabIndex = 3;
            this.VO2MaxLbl.Text = "VO2Max:";
            // 
            // VO2MaxInputLbl
            // 
            this.VO2MaxInputLbl.AutoSize = true;
            this.VO2MaxInputLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VO2MaxInputLbl.Location = new System.Drawing.Point(287, 838);
            this.VO2MaxInputLbl.Name = "VO2MaxInputLbl";
            this.VO2MaxInputLbl.Size = new System.Drawing.Size(26, 29);
            this.VO2MaxInputLbl.TabIndex = 4;
            this.VO2MaxInputLbl.Text = "0";
            // 
            // dataGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1376, 887);
            this.Controls.Add(this.VO2MaxInputLbl);
            this.Controls.Add(this.VO2MaxLbl);
            this.Controls.Add(this.gemiddeldeHFInputLbl);
            this.Controls.Add(this.gemiddeldeHFLbl);
            this.Controls.Add(this.Chart2);
            this.Controls.Add(this.Chart);
            this.Name = "dataGUI";
            this.Text = "dataGUI";
            ((System.ComponentModel.ISupportInitialize)(this.Chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chart2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataVisualization.Charting.Chart Chart;
        private System.Windows.Forms.DataVisualization.Charting.Chart Chart2;
        private System.Windows.Forms.Label gemiddeldeHFLbl;
        private System.Windows.Forms.Label gemiddeldeHFInputLbl;
        private System.Windows.Forms.Label VO2MaxLbl;
        private System.Windows.Forms.Label VO2MaxInputLbl;
    }
}