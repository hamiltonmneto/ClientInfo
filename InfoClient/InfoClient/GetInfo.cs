using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace ClientInformation
{
    public class GetInfo
    {
        private Timer _timer;

        public GetInfo()
        {
            _timer = new Timer(10000);
            _timer.Elapsed += timer_Elapsed;
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            string msg = $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") } Teste";

            Console.WriteLine(msg);
            StringBuilder sb = new StringBuilder();
            var ipAddress = "IP ADRESS: " + GetLocalIPAddress();
            sb.AppendLine(ipAddress);
            var machineName = "MACHINE'S NAME: " + Environment.MachineName;
            sb.AppendLine(machineName);
            var userName = "LOGGED USER: " + Environment.UserName;
            sb.AppendLine(userName);
            using (StreamWriter arquivo =
                new StreamWriter(@"C:\tmp\InfoClient\Info.txt", true))
            {
                arquivo.WriteLine(sb);
                arquivo.Close();
            }
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
