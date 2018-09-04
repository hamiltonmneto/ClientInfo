using System;
using System.Diagnostics;

namespace ServiceInstaller
{
    class Program
    {
        public static void Main(string[] args)
        {
            ProcessStartInfo procStartInfo =
                new ProcessStartInfo("cmd", "/c " + "InfoClient.exe install --autostart");

            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            procStartInfo.CreateNoWindow = true;
            Process proc = new Process();
            proc.StartInfo = procStartInfo;
            proc.Start();
            string result = proc.StandardOutput.ReadToEnd();
            Console.WriteLine(result);
            Console.WriteLine("Press ENTER to exit.");
            Console.Read();
        }
    }
}
