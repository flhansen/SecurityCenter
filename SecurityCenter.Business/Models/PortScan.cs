using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCenter.Business.Models
{
    public class PortScan
    {
        public IPAddress Destination { get; set; }
        public int StartPort { get; set; }
        public int EndPort { get; set; }
        public bool ScanIndividualPorts { get; set; }
        public bool IsAggressive { get; set; }
        public bool ScanRunningServices { get; set; }
        public bool InterpretOSFingerprint { get; set; }
        public bool SaveResultsInFile { get; set; }
        public string SaveFileName { get; set; }
    }
}
