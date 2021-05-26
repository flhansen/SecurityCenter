using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public Dictionary<int, bool> ScannedPorts { get; set; } = new Dictionary<int, bool>();
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

        private async Task CheckPort(int port)
        {
            bool isPortOpen = false;

            using (TcpClient client = new TcpClient())
            {
                try
                {
                    await client.ConnectAsync(Destination, port);
                    isPortOpen = true;
                }
                catch
                {
                }
            }

            ScannedPorts.Add(port, isPortOpen);
        }

        public async Task ScanPorts()
        {
            int[] ports = Enumerable.Range(StartPort, EndPort - StartPort + 1).ToArray();
            Task[] tasks = new Task[ports.Length];

            for (int i = 0; i < ports.Length; i++)
            {
                Trace.WriteLine($"Checking port {ports[i]}");
                tasks[i] = CheckPort(ports[i]);
            }

            await Task.WhenAll(tasks);
        }

    }
}
