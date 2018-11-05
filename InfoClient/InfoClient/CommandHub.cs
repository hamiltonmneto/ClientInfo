using Microsoft.AspNet.SignalR;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace InfoClient
{
    public class CommandHub : Hub
    {
        public override Task OnConnected()
        {
            var version = Context.QueryString["contosochatversion"];
            Clients.All.showMessage();
            return base.OnConnected();
        }

        public string CommandExec(string command)
        {
            //using (Process processo = new Process())
            //{
            //    processo.StartInfo.FileName = Environment.GetEnvironmentVariable("comspec");

            //    // Formata a string para passar como argumento para o cmd.exe
            //    processo.StartInfo.Arguments = string.Format("/c {0}", command);

            //    processo.StartInfo.RedirectStandardOutput = true;
            //    processo.StartInfo.UseShellExecute = false;
            //    processo.StartInfo.CreateNoWindow = true;

            //    processo.Start();
            //    string saida = processo.StandardOutput.ReadToEnd();

            //    processo.WaitForExit();
            //    //return saida;
            //}

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.FileName = "CMD.exe";
            startInfo.Arguments = "/c " + command;
            process.StartInfo = startInfo;
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return output;

        }

    }
}
