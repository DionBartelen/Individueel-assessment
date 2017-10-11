using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        public static List<Session> sessions { get; set; }
        static int port = 1234;

        static void Main(string[] args)
        {
            _handler += new EventHandler(Handler);
            SetConsoleCtrlHandler(_handler, true);
            sessions = new List<Session>();
            IPAddress localhost;
            if (IPAddress.TryParse("127.0.0.1", out localhost))
            {
                TcpListener listener = new TcpListener(localhost, port);
                listener.Start();
                Console.WriteLine("Client server klaar voor verbindingen...");
                Database.ReadSavedData();
                Database.ReadSavedCredentials();
                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    Thread thread = new Thread(() => { HandleNewClient(client); });
                    thread.Start();
                    Console.WriteLine("Verbonden met client: " + client.Client.AddressFamily.ToString());
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Error:\r\nKan het IP-adres niet parsen.");
            }

        }

        public static void HandleNewClient(TcpClient client)
        {
            Session session = new Session(client);
            sessions.Add(session);
            session.Read();
        }

        public static List<Session> GetAllPatients()
        {
            List<Session> ToReturn = new List<Session>();
            foreach (Session s in sessions)
            {
                if (!s.IsDoctor)
                {
                    ToReturn.Add(s);
                }
            }
            return ToReturn;
        }

        public static Session GetSessionWithUsername(string username)
        {
            foreach (Session s in sessions)
            {
                if (s.Username == username)
                {
                    return s;
                }
            }
            return null;
        }



        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);

        private delegate bool EventHandler(CtrlType sig);
        static EventHandler _handler;

        enum CtrlType
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6
        }

        private static bool Handler(CtrlType sig)
        {
            switch (sig)
            {
                case CtrlType.CTRL_C_EVENT:
                case CtrlType.CTRL_LOGOFF_EVENT:
                case CtrlType.CTRL_SHUTDOWN_EVENT:
                case CtrlType.CTRL_CLOSE_EVENT:
                default:
                    {
                        Database.Close();
                        return false;
                    }
            }
        }

    }
}
