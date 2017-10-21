using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using Healthcare_test;
using Newtonsoft.Json;

namespace WindowsFormsApp1
{
    public class Astrand
    {
        private ChatPanel chatPanel;
        private Boolean started;
        private Boolean canConfirm;
        private Boolean confirmed;
        private Client client;
        public int currentRPM = 0;
        private List<ErgometerData> measurements;

        public Astrand(ChatPanel chatPanel, Client client)
        {
            this.chatPanel = chatPanel;
            this.client = client;
            started = false;
            confirmed = false;
            canConfirm = false;
            measurements = new List<ErgometerData>();
        }

        public void Start()
        {
            chatPanel.astrand = this;
            started = true;
            chatPanel.UpdateText("De astrand test gaat beginnen. Doe de hartslagband om en neem plaats op de fiets. Zorg ook dat het zadel van de fiets op de juiste hoogte is ingesteld. \r\nDruk op begrepen om door te gaan.");
            WachtOpConfirm();
            chatPanel.UpdateText("U gaat fietsen met een snelheid van ongeveer 60 omwentelingen per minuut. U krijgt eerst een korte warming-up van 2 minuten. De test duurt 4 minuten met daarna een cooling down van 1 miuut. \r\nDruk op begrepen om door te gaan.");
            WachtOpConfirm();
            chatPanel.UpdateText("De test begint wanneer u start met fietsen");
            while (Math.Abs(client.GetErgoData().Speed) < 1)
            {
                Thread.Sleep(10);
            }
            client.simulation?.s.startSession();
            WarmingUp();
            chatPanel.UpdateText("De warming up is nu klaar, de echte test gaat nu beginnen");
            RealTest();
            chatPanel.UpdateText("De test is voorbij, je begin nu aan de cooling down");
            CoolingDown();
            chatPanel.UpdateText("De test is klaar. U kunt nu van de fiets afstappen");
            StopAstrand();
            WachtOpConfirm();
            client.close();
            chatPanel.End();
        }

        public void WachtOpConfirm()
        {
            canConfirm = true;
            while (!confirmed)
            {
                Thread.Sleep(100);
            }
            confirmed = false;
            canConfirm = false;
        }

        public void Confirm()
        {
            if (canConfirm)
            {
                confirmed = true;
            }
        }

        public void WarmingUp()
        {
            //Measurements 1200 is 2 minuten warming up
            int measurements = 1200;
            int currentMeasurement = 0;
            while (currentMeasurement <= measurements)
            {
                ErgometerData data = client.GetErgoData();
                this.measurements.Add(data);
                currentRPM = data.RPM;
                if (currentMeasurement % 10 == 0)
                {
                    SendData(data);
                }
                currentMeasurement++;
                chatPanel.Invalidate();
                Thread.Sleep(100);
            }
        }

        public void RealTest()
        {
            //Measurements 2400 is 4 minuten voor de test
            int measurements = 240;
            int currentMeasurement = 0;
            while (currentMeasurement <= measurements)
            {
                ErgometerData data = client.GetErgoData();
                this.measurements.Add(data);
                currentRPM = data.RPM;
                if (currentMeasurement % 10 == 0)
                {
                    SendData(data);
                }
                currentMeasurement++;
                chatPanel.Invalidate();
                Thread.Sleep(100);
            }
        }

        public void CoolingDown()
        {
            //Measurements 600 is 1 minuten voor de cooling down
            int measurements = 60;
            int currentMeasurement = 0;
            while (currentMeasurement <= measurements)
            {
                ErgometerData data = client.GetErgoData();
                this.measurements.Add(data);
                currentRPM = data.RPM;
                if (currentMeasurement % 10 == 0)
                {
                    SendData(data);
                }
                currentMeasurement++;
                chatPanel.Invalidate();
                Thread.Sleep(100);
            }
        }

        public void SendData(ErgometerData data)
        {
            dynamic ergometerdata = new
            {
                id = "data",
                session = client.sessionID,
                data = new
                {
                    power = data.Actual_Power,
                    speed = data.Speed,
                    time = data.Time,
                    RPM = data.RPM,
                    distance = data.Distance,
                    pulse = data.Pulse
                }
            };
            client.Send(JsonConvert.SerializeObject(ergometerdata));
        }

        public void StopAstrand()
        {
            dynamic request = new
            {
                id = "StopAstrand",
                data = new
                {
                    patientId = client.sessionID
                }
            };
            client.Send(JsonConvert.SerializeObject(request));
        }
    }
}
