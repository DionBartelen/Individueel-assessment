using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorApplicatie
{
    public class ErgometerData
    {
        public int RPM { get; set; }
        public double Speed { get; set; }
        public double Distance { get; set; }
        public int Pulse { get; set; }
        public int Time { get; set; }
        public int Energy { get; set; }
        public int Actual_Power { get; set; }
        public int Requested_Power { get; set; }

        public ErgometerData(int pulse, int rpm, double speed, double distance, int time, int energy, int actual_Power, int requested_Power)
        {
            this.RPM = rpm;
            this.Speed = speed;
            this.Distance = distance;
            this.Pulse = pulse;
            this.Time = time;
            this.Energy = energy;
            this.Actual_Power = actual_Power;
            this.Requested_Power = requested_Power;

        }

        override
        public String ToString()
        {
            return $"RPM: {RPM}\r\nSpeed: {Speed}\r\nDistance: {Distance:F2}\r\nPulse: {Pulse}\r\nTime: {Time}\r\nEnergy: {Energy}\r\nActual_Power: {Actual_Power}\r\nRequested_Power: {Requested_Power}\r\n";
        }
    }
}
