using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCenter.Business.Models
{
    public class SystemInformation
    {
        public string ComputerName { get; set; }
        public string UserName { get; set; }
        public string OperatingSystem { get; set; }
        public string SystemType { get; set; }
        public string Manufacturer { get; set; }
        public string Architecture { get; set; }
        public ulong BytesRam { get; set; }
    }
}
