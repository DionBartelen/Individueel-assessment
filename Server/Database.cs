using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Server
{
    class Database
    {
        static Dictionary<string, string> CredentialsDoctor = new Dictionary<string, string>();
        static Dictionary<string, string> CredentialsClient = new Dictionary<string, string>();
        static Dictionary<string, List<TrainSession>> TrainSessions = new Dictionary<string, List<TrainSession>>();
        static Dictionary<string, TrainSession> ActiveTrainSessions = new Dictionary<string, TrainSession>();

        public static void ReadSavedData()
        {
            try
            {
                string path = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, @"Healthcare\Server\Database\Database.txt");
                string AllText = File.ReadAllText(path);
                List<string> lines = AllText.Split('&').ToList();
                Dictionary<string, List<TrainSession>> dictionary = new Dictionary<string, List<TrainSession>>();
                foreach (string s in lines)
                {
                    dynamic jsonObject = JsonConvert.DeserializeObject(s);
                    if (jsonObject != null)
                    {
                        string username = jsonObject.username;
                        List<TrainSession> sessions = new List<TrainSession>();
                        foreach (dynamic training in jsonObject.data.Trainingsession)
                        {
                            List<ErgometerData> ergoData = new List<ErgometerData>();
                            foreach (dynamic ergometerData in training.data)
                            {
                                ergoData.Add(new ErgometerData((int)ergometerData.Pulse, (int)ergometerData.RPM, (double)ergometerData.Speed, (double)ergometerData.Distance, (int)ergometerData.Time, 0, 0, (int)ergometerData.Requested_Power));
                            }
                            TrainSession trainingsession = new TrainSession();
                            trainingsession.SetData(ergoData);
                            sessions.Add(trainingsession);
                        }
                        dictionary.Add(username, sessions);
                    }
                }
                TrainSessions = dictionary;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Error while reading:(\r\n" + e.Message);
            }
        }

        public static Boolean AddActiveSession(string username)
        {
            if (!ActiveTrainSessions.ContainsKey(username))
            {
                ActiveTrainSessions.Add(username, new TrainSession());
                return true;
            }
            return false;
        }

        public static Boolean AddErgometerDataToSession(string username, ErgometerData data)
        {
            if (ActiveTrainSessions.ContainsKey(username))
            {
                ((TrainSession)ActiveTrainSessions[username]).AddData(data);
                return true;
            }
            return false;
        }

        public static void CloseActiveSession(string username)
        {
            TrainSession sessionToClose = ActiveTrainSessions[username];
            ActiveTrainSessions.Remove(username);
            if (TrainSessions.ContainsKey(username))
            {
                ((List<TrainSession>)TrainSessions[username]).Add(sessionToClose);
            }
            else
            {
                List<TrainSession> trainList = new List<TrainSession>();
                trainList.Add(sessionToClose);
                TrainSessions.Add(username, trainList);
            }
        }

        public static void Close()
        {
            try
            {
                string path = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, @"Healthcare\Server\Database\Database.txt");
                string toWrite = "";
                foreach (KeyValuePair<string, List<TrainSession>> entry in TrainSessions)
                {

                    dynamic keyValuePair = new
                    {
                        username = entry.Key,
                        data = new
                        {
                            Trainingsession = entry.Value
                        }
                    };
                    toWrite += JsonConvert.SerializeObject(keyValuePair) + "&";
                }
                File.WriteAllText(path, toWrite);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Error while saving:(\r\n" + e.Message);
            }
        }

        public static TrainSession[] GetDataFromUser(string username)
        {
            if (TrainSessions.ContainsKey(username))
            {
                return TrainSessions[username].ToArray();
            }
            else
            {
                return null;
            }
        }

        public static string[] GetUsers()
        {
            Dictionary<string, List<TrainSession>>.KeyCollection keyColl = TrainSessions.Keys;
            List<string> keys = new List<string>();
            foreach(string s in keyColl)
            {
                keys.Add(s.ToString());
            }
            return keys.ToArray();
        }

        public static void ReadSavedCredentials()
        {
            string path = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, @"Healthcare\Server\Database\Login.txt");
            string AllText = File.ReadAllText(path);
            dynamic jsonObject = JsonConvert.DeserializeObject(AllText);
            foreach (dynamic combination in jsonObject.combinations)
            {
                CredentialsClient.Add((string)combination.username, (string)combination.password);
            }

            string path2 = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, @"Healthcare\Server\Database\LoginDoctor.txt");
            string AllText2 = File.ReadAllText(path2);
            dynamic jsonObject2 = JsonConvert.DeserializeObject(AllText2);
            foreach (dynamic combination2 in jsonObject2.combinations)
            {
                CredentialsDoctor.Add((string)combination2.username, (string)combination2.password);
            }
        }

        public static Boolean CheckCredentials(string username, string password)
        {
            if (CredentialsClient.ContainsKey(username))
            {
                return CredentialsClient[username] == password;
            }
            return false;
        }

        public static Boolean CheckDoctorCredentials(string username, string password)
        {
            if (CredentialsDoctor.ContainsKey(username))
            {
                return CredentialsDoctor[username] == password;
            }
            return false;
        }

            public static Boolean IsDoctor(string username)
        {
            return CredentialsDoctor.ContainsKey(username);
        }
    }
}
