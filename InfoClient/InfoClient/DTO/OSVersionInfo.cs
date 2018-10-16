using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoClient.Model
{
    public class OSVersionInfo
    {
        public string Name { get; set; }

        public int Minor { get; set; }

        public int Major { get; set; }

        public int Build { get; set; }

        public OSVersionInfo() { }
    }
}
