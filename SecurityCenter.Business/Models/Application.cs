using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCenter.Business.Models
{
    public class Application
    {
        public string Name { get; set; }
        public string ExecutablePath { get; set; }
        public string UninstallPath { get; set; }
        public string Version { get; set; }
        public string Publisher { get; set; }
    }
}
