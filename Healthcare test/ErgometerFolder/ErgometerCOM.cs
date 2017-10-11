  using Healthcare_test.test_applicatie;
using System;
using System.IO.Ports;

namespace Healthcare_test
{

    public class ErgometerCOM : Ergometer
    {
        public SerialPort serialPort;
   

        public ErgometerCOM(string comport, string baudRate)
        {
            try
            {
                serialPort = new SerialPort(comport)
                {
                    BaudRate = Convert.ToInt32(baudRate),
                    Parity = Parity.None,
                    StopBits = StopBits.One,
                    DataBits = 8,
                    Handshake = Handshake.None,
                    ReadTimeout = 2000,
                    WriteTimeout = 500
                };
                serialPort.Open();
                ErgometerCommandMode();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.StackTrace);
            }
        }

        override
        public ErgometerData GetData()
        {

            serialPort.WriteLine("ST");
            string response = "";
            try
            {
                response = serialPort.ReadLine();
                if (response.Length < 6)
                {
                    response = serialPort.ReadLine();
                }
            }


            catch (Exception e)
            {

            }
            String[] data = response.Split('\t');
            int time;
            // foreach (String datas in data)
            // {
            //     System.Diagnostics.Debug.WriteLine(datas);
            // }
            if (data.Length == 8)
            {
                Int32.TryParse(data[0], out int pulse);
                Int32.TryParse(data[1], out int rpm);
                Double.TryParse(data[2], out double speed);
                Double.TryParse(data[3], out double distance);
                Int32.TryParse(data[4], out int requested_Power);
                Int32.TryParse(data[5], out int energy);
                time = Convert.ToInt32(data[6].Substring(0, 2) + data[6].Substring(3, 2));
                Int32.TryParse(data[7], out int actual_Power);
                return new ErgometerData(pulse, rpm, speed / 10.0, distance / 10.00, time, energy, actual_Power, requested_Power);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show(response);
                return null;
            }
        }
            

        

        override
        public void SetDistance(double distance)
        {
            serialPort.WriteLine("PD " + distance);
            serialPort.ReadLine();
        }

        override
        public void SetTime(int time)
        {
            serialPort.WriteLine("PT " + time);
            serialPort.ReadLine();
        }

        override
        public void SetPower(int power)
        {
            serialPort.WriteLine("PW " + power);
            serialPort.ReadLine();
        }

        public override bool IsConnected()
        {
            return serialPort.IsOpen;
        }

        public override void ErgometerCommandMode()
        {
            serialPort.WriteLine("CM");
            serialPort.ReadLine();
        }

        public override void Close()
        {
            serialPort.Close();
        }

        public override void Reset()
        {
            serialPort.WriteLine("RS");
            serialPort.ReadLine();
        }
    }
}
