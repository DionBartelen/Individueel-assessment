using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using Healthcare_test;

namespace WindowsFormsApp1
{
    public class Astrand
    {
        private ChatPanel chatPanel;
        private Boolean started;
        private Boolean canConfirm;
        private Boolean confirmed;
        private Client client;
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
            chatPanel.UpdateText("De astrand test gaat beginnen. Doe de hartslagband om en neem plaats op de fiets. Zorg ook dat het zadel van de fiets op de juiste hoogte is ingesteld");
            WachtOpConfirm();
            chatPanel.UpdateText("U gaat fietsen met een snelheid van ongeveer 60 omwentelingen per minuut. U krijgt eerst een korte warming-up van 2 minuten. Hierna wordt (eventueel in korte stappen) de beoogde testbelasting ingesteld. De test duurt 4 minuten met daarna een cooling down.");
            WachtOpConfirm();
            chatPanel.UpdateText("De test begint wanneer u start met fietsen");
            while (Math.Abs(client.GetErgoData().Speed) < 1)
            {
                Thread.Sleep(10);
            }
            WarmingUp();
            chatPanel.UpdateText("De warmin up is nu klaar, de echte test gaat nu beginnen");
            RealTest();
            chatPanel.UpdateText("De test is voorbij, je begin nu aan de cooling down");
            CoolingDown();
            chatPanel.UpdateText("De test is klaar. U kunt nu van de fiets afstappen");
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
            //Measurements 120 is 2 minuten warming up
            int measurements = 12;
            int currentMeasurement = 0;
            while (currentMeasurement <= measurements)
            {
                this.measurements.Add(client.GetErgoData());
                currentMeasurement++;
                Thread.Sleep(1000);
            }
        }

        public void RealTest()
        {
            //Measurements 240 is 4 minuten voor de test
            int measurements = 24;
            int currentMeasurement = 0;
            while (currentMeasurement <= measurements)
            {
                this.measurements.Add(client.GetErgoData());
                currentMeasurement++;
                Thread.Sleep(1000);
            }
        }

        public void CoolingDown()
        {
            //Measurements 60 is 1 minuten voor de cooling down
            int measurements = 6;
            int currentMeasurement = 0;
            while (currentMeasurement <= measurements)
            {
                this.measurements.Add(client.GetErgoData());
                currentMeasurement++;
                Thread.Sleep(1000);
            }
        }
    }
}
