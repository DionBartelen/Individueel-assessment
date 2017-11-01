using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Healthcare_test;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;

namespace WindowsFormsApp1
{
    public class Astrand
    {
        private int age;
        private string sex;
        private double weight;

        private Boolean MaxHFReached;
        private Boolean SteadyHF;
        private Boolean ReadyState130;
        private List<int> Pulse;
        public int currentRPM = 0;
        private List<ErgometerData> measurements;

        private ChatPanel chatPanel;
        private Boolean started;
        private Boolean canConfirm;
        private Boolean confirmed;
        private Client client;

        private readonly int speed = 10;

        public Astrand(ChatPanel chatPanel, Client client)
        {
            this.chatPanel = chatPanel;
            this.client = client;
            started = false;
            confirmed = false;
            ReadyState130 = false;
            SteadyHF = true;
            MaxHFReached = false;
            canConfirm = false;
            Pulse = new List<int>();
            measurements = new List<ErgometerData>();
        }

        public void Start()
        {
            try
            {
                client.ResetErgo();
                Thread.Sleep(1000);
                client.CommandMode();
                Application.Run(new GebruikerGegevensAstrandForm(this));
                chatPanel.astrand = this;
                started = true;
                chatPanel.UpdateText(
                    "De astrand test gaat beginnen. Doe de hartslagband om en neem plaats op de fiets. Zorg ook dat het zadel van de fiets op de juiste hoogte is ingesteld. \r\nDruk op begrepen om door te gaan.");
                WachtOpConfirm();
                chatPanel.UpdateText(
                    "U gaat fietsen met een snelheid van ongeveer 60 omwentelingen per minuut. U krijgt eerst een korte warming-up van 2 minuten. De test duurt 4 minuten met daarna een cooling down van 1 miuut. \r\nDruk op begrepen om door te gaan.");
                WachtOpConfirm();
                chatPanel.UpdateText("De test begint wanneer u start met fietsen");
                while (Math.Abs(client.GetErgoData().Speed) < 1)
                {
                    Thread.Sleep(10);
                }
                client.simulation?.s.startSession();
                WarmingUp();
                chatPanel.UpdateText("De warming up is nu klaar, de echte test gaat nu beginnen probeer tussen de 50 en 60 rpm te fietsen");
                RealTest();
                chatPanel.UpdateText("De test is voorbij, je begin nu aan de cooling down");
                CoolingDown();
                chatPanel.UpdateText("De test is klaar. U kunt nu van de fiets afstappen");
                if (SteadyHF)
                {
                    if (ReadyState130)
                    {
                        StopAstrandSucces();
                    }
                    else
                    {
                        ErrorEndAstrand("Een hartslag van 130 is niet behaald. De test is dus niet geldig en zal niet worden opgeslagen.");
                    }
                }
                else
                {
                    ErrorEndAstrand("Uw hartslag is niet geleidelijk verhoorgd. De test is dus niet geldig en zal niet worden opgeslagen.");
                }
                WachtOpConfirm();
                client.close();
                chatPanel.End();
            }
            catch (Exception e)
            {
                ErrorEndAstrand(e.Message);
                client.close();
                chatPanel.End();
            }
        }

        //Confirm meganisme
        #region
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
        #endregion

        //Warming up
        #region
        public void WarmingUp()
        {
            chatPanel.UpdateText("Power word naar 50 gezet");
            client.SetPower(50);
            //Measurements 1200 is 2 minuten warming up
            int measurements = 1200;
            int currentMeasurement = 0;
            while (currentMeasurement <= measurements)
            {
                long beginTime = DateTime.Now.Ticks;
                ErgometerData data = client.GetErgoData();
                this.measurements.Add(data);
                if (currentMeasurement % 10 == 0)
                {
                    chatPanel.UpdatePreviousText("Warming up nog: " + (measurements - currentMeasurement) / 10 + " seconden");
                    SendData(data);
                }
                if (HFAboveMaximum(data.Pulse))
                {
                    MaxHFReached = true;
                }
                currentMeasurement++;
                Wait100Ms(beginTime);
            }
        }
        #endregion

        public void RealTest()
        {
            RealTestPhase1();
            RealTestPhase2();
            if (GetAvgPulse() < 130)
            {
                chatPanel.UpdateText("De test is niet goed afgesloten, phase 2 word opnieuw uitgevoerd");
                RealTestPhase2();
            }
        }

        //Real test Phase 1
        #region
        public void RealTestPhase1()
        {
            //Measurements 1200 is 2 minuten voor de test
            int measurements = 1200;
            int currentMeasurement = 0;
            while (currentMeasurement <= measurements)
            {
                long beginTime = DateTime.Now.Ticks;
                ErgometerData data = client.GetErgoData();
                this.measurements.Add(data);
                currentRPM = data.RPM;
                if (currentMeasurement % 10 == 0)
                {
                    chatPanel.UpdatePreviousText("Huidige power: " + (data.Actual_Power) + " Huidige RPM: " + data.RPM + " Huidige hartslag: " + data.Pulse + "\r\n\r\n" + "Testphase 1: " + (measurements - currentMeasurement) / 10 + " seconden\r\n");
                    SendData(data);
                }
                if (currentMeasurement % 30 == 0 && data.Pulse < 130 && currentRPM >= 50 && currentRPM <= 60)
                {
                    client.SetPower(data.Actual_Power + 5);
                }
                if (HFAboveMaximum(data.Pulse))
                {
                    MaxHFReached = true;
                }
                if (data.Pulse >= 130 && !ReadyState130)
                {
                    ReadyState130 = true;
                    chatPanel.UpdateText("Ready state bereikt");
                }
                currentMeasurement++;
                chatPanel.Invalidate();
                Wait100Ms(beginTime);
            }
        }
        #endregion

        //Real test phase 2
        #region
        public void RealTestPhase2()
        {
            //Measurements 1200 is 2 minuten voor de test
            int measurements = 1200;
            int currentMeasurement = 0;
            while (currentMeasurement <= measurements)
            {
                long beginTime = DateTime.Now.Ticks;
                ErgometerData data = client.GetErgoData();
                this.measurements.Add(data);
                currentRPM = data.RPM;
                if (currentMeasurement % 10 == 0)
                {
                    chatPanel.UpdatePreviousText("Huidige power: " + (data.Actual_Power) + " Huidige RPM: " + data.RPM + " Huidige hartslag: " + data.Pulse + "\r\n\r\n" + "Testphase 2: " + (measurements - currentMeasurement) / 10 + " seconden\r\n");
                    SendData(data);
                }
                if (currentMeasurement % 30 == 0 && data.Pulse < 130 && currentRPM >= 50 && currentRPM <= 60)
                {
                    client.SetPower(data.Actual_Power + 5);
                }
                if (HFAboveMaximum(data.Pulse))
                {
                    MaxHFReached = true;
                }
                if (currentMeasurement >= 1200)
                {
                    if (currentMeasurement % 150 == 0)
                    {
                        AddPulseForAvg(data.Pulse);
                    }
                }
                if (data.Pulse >= 130 && !ReadyState130)
                {
                    ReadyState130 = true;
                    chatPanel.UpdateText("Ready state bereikt");
                }
                currentMeasurement++;
                chatPanel.Invalidate();
                Wait100Ms(beginTime);
            }
        }
        #endregion

        //Cooling down
        #region
        public void CoolingDown()
        {
            currentRPM = 0;
            chatPanel.Invalidate();
            chatPanel.UpdateText("Power word naar 50 gezet");
            client.SetPower(50);
            //Measurements 600 is 1 minuten voor de cooling down
            int measurements = 600;
            int currentMeasurement = 0;
            while (currentMeasurement <= measurements)
            {
                long beginTime = DateTime.Now.Ticks;
                ErgometerData data = client.GetErgoData();
                this.measurements.Add(data);
                if (currentMeasurement % 10 == 0)
                {
                    chatPanel.UpdatePreviousText("Cooling down: " + (measurements - currentMeasurement) / 10 + " seconden");
                    SendData(data);
                }
                if (HFAboveMaximum(data.Pulse))
                {
                    MaxHFReached = true;
                }
                currentMeasurement++;
                Wait100Ms(beginTime);
            }
        }
        #endregion

        public void AddPulseForAvg(int pulse)
        {
            if (Pulse.Any())
            {
                if (Math.Abs(Pulse[Pulse.Count - 1] - pulse) > 5)
                {
                    SteadyHF = false;
                    System.Diagnostics.Debug.WriteLine("error bij steadyHF: " + Pulse[Pulse.Count - 1] + "   " + pulse);
                }
            }
            Pulse.Add(pulse);
        }

        //Send data to server
        #region
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
        #endregion

        //HF above maximum
        #region
        public Boolean HFAboveMaximum(int pulse)
        {
            if (age < 15)
            {
                return false;
            }
            int maxPulse = 0;
            if (age >= 15 && age < 25)
            {
                maxPulse = 200;
            }
            if (age >= 25 && age < 35)
            {
                maxPulse = 200;
            }
            else if (age >= 35 && age < 40)
            {
                maxPulse = 190;
            }
            else if (age >= 40 && age < 45)
            {
                maxPulse = 180;
            }
            else if (age >= 45 && age < 50)
            {
                maxPulse = 170;
            }
            else if (age >= 50 && age < 55)
            {
                maxPulse = 160;
            }
            else if (age >= 55 && age < 60)
            {
                maxPulse = 150;
            }
            return pulse > maxPulse;
        }
        #endregion

        //Get correctionfactor
        #region
        public double GetCorrectionFactor()
        {
            if (age >= 25 && age < 35)
            {
                return 1.00;
            }
            else if (age >= 35 && age < 40)
            {
                return 0.93;
            }
            else if (age >= 40 && age < 45)
            {
                return 0.83;
            }
            else if (age >= 45 && age < 50)
            {
                return 0.78;
            }
            else if (age >= 50 && age < 55)
            {
                return 0.75;
            }
            else if (age >= 55 && age < 60)
            {
                return 0.71;
            }
            else
            {
                return 0.65;
            }
        }
        #endregion

        public void StopAstrandSucces()
        {
            dynamic request = new
            {
                id = "StopAstrand",
                data = new
                {
                    patientId = client.sessionID,
                    status = "ok",
                    data = new
                    {
                        age = age,
                        sex = sex,
                        weight = weight,
                        vo2Max = CalculateVO2(),
                        avgPulse = GetAvgPulse()
                    }
                }
            };
            client.Send(JsonConvert.SerializeObject(request));
        }

        public void StopAstrandError(string status)
        {
            dynamic request = new
            {
                id = "StopAstrand",
                data = new
                {
                    patientId = client.sessionID,
                    status = "error",
                    data = new
                    {
                        status = status
                    }
                }
            };
            client.Send(JsonConvert.SerializeObject(request));
        }

        public int GetAvgPulse()
        {
            int TotalPulse = 0;
            foreach (int p in Pulse)
            {
                TotalPulse += p;
            }
            if (Pulse.Count > 0)
            {
                return TotalPulse / Pulse.Count;
            }
            return 0;
        }

        public void PrivateData(int age, string sex, double weight)
        {
            this.age = age;
            this.sex = sex;
            this.weight = weight;
        }

        //Calculate VO2max 
        #region
        public double CalculateVO2()
        {
            double maxWatage = 0;
            double VO2max = 0;
            measurements.ForEach(measurement =>
            {
                if (measurement.Actual_Power > maxWatage)
                    maxWatage = measurement.Actual_Power;
            }
            );
            maxWatage *= 6.11;
            if (sex != "male")
            {
                VO2max = (((0.00193 * maxWatage) + 0.326) / (0.769 * GetAvgPulse() - 56.1) * 100) * 1000 / weight;
            }
            else
            {    
                VO2max = (((0.00212 * maxWatage) + 0.299) / (0.769 * GetAvgPulse() - 48.5) * 100) * 1000 / weight;
            }

            if (age >= 30)
                VO2max *= GetCorrectionFactor();
            return VO2max;
        }
        #endregion

        public void ErrorEndAstrand(string error)
        {
            StopAstrandError(error);
            Task.Delay(1000).Wait();
            client.close();
            chatPanel.End();
            MessageBox.Show("Er is een error opgetreden:\r\n" + error);
        }

        public void Wait100Ms(long beginTicks)
        {
            long differencte = speed * TimeSpan.TicksPerMillisecond;
            while (DateTime.Now.Ticks < beginTicks + differencte)
            {
                Thread.Sleep(1);
            }
        }
    }
}
