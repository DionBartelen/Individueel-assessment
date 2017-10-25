using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [Serializable]
    class TrainSession
    {
        public int age { get; set; }
        public string sex { get; set; }
        public double vo2Max { get; set; }
        public double avgPulse { get; set; }
        public double weight { get; set; }

        public List<ErgometerData> data;

        public TrainSession()
        {
            age = -1;
            sex = "Unknown";
            vo2Max = -1;
            data = new List<ErgometerData>();
        }

        public void SetData(List<ErgometerData> data)
        {
            this.data = data;
        }

        public void AddData(ErgometerData ergometerData)
        {
            data.Add(ergometerData);
        }
    }
}
