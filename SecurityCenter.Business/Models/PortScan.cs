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

            // Create new TCP client to be able to connect to remote services.
            using (TcpClient client = new TcpClient())
            {
                try
                {
                    // Try to connect to target by iterating over all specified
                    // ports. This is kinda like a brute force attack.
                    await client.ConnectAsync(Destination, port);

                    // If the client was able to connect to an enpoint, the
                    // port must be open.
                    isPortOpen = true;
                }
                catch { }
            }

            // Insert the result into the port state dictionary.
            ScannedPorts.Add(port, isPortOpen);
        }

        public async Task ScanPorts()
        {
            // Remove all previous scanned ports from the dictionary.
            ScannedPorts.Clear();

            // Construct an array of ports using start and end port, such that
            // every port is in [StartPort; EndPort].
            int[] ports = Enumerable.Range(StartPort, EndPort - StartPort + 1)
                .ToArray();

            // This array stores all tasks used to scan a port respectively. It
            // is used to check if all tasks are done.
            Task[] tasks = new Task[ports.Length];

            // Start all of the scanning tasks and add them to the array. Every
            // task checks one part and uses one virtual core of the CPU.
            for (int i = 0; i < ports.Length; i++)
                tasks[i] = CheckPort(ports[i]);

            // Wait until all tasks are done.
            await Task.WhenAll(tasks);
        }

    }
}
