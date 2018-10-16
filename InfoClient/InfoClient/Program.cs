using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace ClientInformation
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(s =>
            {
                s.Service<GetInfo>(p =>
                {
                    p.ConstructUsing(gi => new GetInfo());
                    p.WhenStarted(gi => gi.Start());
                    p.WhenStopped(gi => gi.Stop());
                });
                s.StartAutomatically();
                s.OnException(ex => Console.WriteLine(ex));
                s.SetDescription("Get client information test");
                s.SetDisplayName("ClientInfoTest");
                s.SetServiceName("ClientInfo");
            });
        }
    }
}
