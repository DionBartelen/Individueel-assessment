using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoctorApplicatie
{
    public partial class dataGUI : Form
    {
        TrainSession session;

        public dataGUI()
        {
            InitializeComponent();
        }

        public void SetTrainSession(TrainSession session)
        {
            this.session = session;
            SetChartData();
        }

        public void SetChartData()
        {
            Chart.Series.Clear();
            Chart2.Series.Clear();
            Chart.Series.Add(@"Speed in KM/h");
            Chart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            Chart.Series.Add(@"RPM");
            Chart.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            Chart.Series.Add(@"Distance in KM");
            Chart.Series[2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            Chart.Series.Add(@"Power in Watt");
            Chart.Series[3].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            Chart2.Series.Add(@"Pulse in BPM");
            Chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            foreach (ErgometerData data in session.data)
            {
                Chart.Series[0].Points.AddXY(data.Time, data.Speed);
                Chart.Series[1].Points.AddXY(data.Time, data.RPM);
                Chart.Series[2].Points.AddXY(data.Time, data.Distance);
                Chart.Series[3].Points.AddXY(data.Time, data.Requested_Power);
                Chart2.Series[0].Points.AddXY(data.Time, data.Pulse);
            }
        }
    }
}
