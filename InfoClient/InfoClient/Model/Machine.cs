using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoClient.Model
{
    public class Machine
    {
        public string _Ip { get; set; }

        public string _Name { get; set; }

        public string _LogguedUser { get; set; }

        public string _OSVersion { get; set; }

        public List<HardDrive> _HardDrivers { get; set; }

        public string _MacAddress { get; set; }

        public Machine(string ip, string name, string logguedUser, string osversion, List<HardDrive> hardDrive, string macAdress)
        {
            _Ip = ip;
            _Name = name;
            _LogguedUser = logguedUser;
            _OSVersion = osversion;
            _HardDrivers = hardDrive;
            _MacAddress = macAdress;
        }

    }
}
