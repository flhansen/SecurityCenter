using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SecurityCenter;

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

        public static PortScan Default => new PortScan
        {
            Destination = IPAddress.Parse("127.0.0.1"),
            StartPort = 20,
            EndPort = 9999,
            IsAggressive = true,
            ScanIndividualPorts = true,
            ScanRunningServices = true,
            InterpretOSFingerprint = true
        };

    }
}
