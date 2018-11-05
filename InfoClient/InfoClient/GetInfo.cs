using System;
using System.Timers;
using System.Net;
using System.Net.Sockets;
using System.Management;
using InfoClient.Model;
using System.Net.Http;
using System.Text;
using Microsoft.Win32;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Linq;
using Microsoft.Owin.Hosting;

namespace ClientInformation
{
    public class GetInfo
    {
        private Timer _timer;
        private const string url = "http://localhost:51928/api/Machines/PostMachines";


        public GetInfo()
        {
            _timer = new Timer(10000);
            _timer.Elapsed += timer_Elapsed;
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var machine = PrepareMachineInfo();
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(machine);
                    var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = client.PostAsync(url, httpContent).Result;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(ex);
                Console.ResetColor();
            }
        }

        public void Start()
        {
            _timer.Start();
            Connect();
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

        public List<HardDrive> GetHardDriveSerial()
        {
            var hdCollection = new List<HardDrive>();
            ManagementObjectSearcher searcher = new
            ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                HardDrive hd = new HardDrive();
                hd.Model = wmi_HD["Model"].ToString();
                hd.Type = wmi_HD["InterfaceType"].ToString();
                hdCollection.Add(hd);
            }
            searcher = new
            ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");

            int i = 0;
            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                // get the hard drive from collection
                // using index
                HardDrive hd = (HardDrive)hdCollection[i];

                // get the hardware serial no.
                if (wmi_HD["SerialNumber"] == null)
                    hd.SerialNo = "None";
                else
                    hd.SerialNo = wmi_HD["SerialNumber"].ToString();

                ++i;
            }
            return hdCollection;
        }

        public Machine PrepareMachineInfo()
            => new Machine(
                    GetLocalIPAddress(),
                    Environment.MachineName, 
                    Environment.UserName,
                    GetOsVersion(),
                    GetHardDriveSerial(),
                    GetMacAdress()
                );

        public string GetMacAdress()
            => NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .Select(nic => nic.GetPhysicalAddress().ToString())
                .FirstOrDefault();

        public string GetOsVersion()
            => Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ProductName", "").ToString();

        public void Connect()
        {
            string url = "http://" +  GetLocalIPAddress() + ":6969/" + GetMacAdress();
            //var options = new StartOptions("http://" + GetLocalIPAddress() + ":6969")
            //{
            //    ServerFactory = "Microsoft.Owin.Host.HttpListener"
            //};
            WebApp.Start(url);
            Console.WriteLine("Server running at {0}\n", url);
            
        }
    }
}
