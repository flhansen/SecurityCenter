using SecurityCenter.Business.Models;
using SecurityCenter.UILogic.Commands;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SecurityCenter.UILogic.ViewModels
{
    /// <summary>
    /// The ViewModel class for the port scanner.
    /// </summary>
    public class PortScanViewModel : ViewModel<PortScan>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model">The port scanner model</param>
        public PortScanViewModel(PortScan model) : base(model)
        {
            ScanCommand = new RelayCommand(Scan);
        }

        /// <summary>
        /// The command to scan ports.
        /// </summary>
        public ICommand ScanCommand { get; private set; }
        /// <summary>
        /// The method to scan ports asynchronously.
        /// </summary>
        private async void Scan(object obj)
        {
            IsScanning = true;

            // Popular ports being used.
            int[] ports = { 22, 23, 80, 443, 445 };

            if (ScanIndividualPorts)
            {
                // Construct an array of ports using start and end port, such that
                // every port is in [StartPort; EndPort].
                ports = Enumerable.Range(StartPort, EndPort - StartPort + 1)
                    .ToArray();
            }

            // Start scanning ports.
            await Model.ScanPorts(ports);

            // Read out scanned ports, which are open and sort them.
            OpenPorts = Model.ScannedPorts
                .Where(x => x.Value)
                .Select(x => x.Key)
                .OrderBy(x => x)
                .ToList();

            IsScanning = false;
        }

        /// <summary>
        /// If the viewmodel is scanning currently.
        /// </summary>
        private bool isScanning = false;
        /// <summary>
        /// If the viewmodel is scanning currently.
        /// </summary>
        public bool IsScanning
        {
            get => isScanning;
            set => SetProperty(ref isScanning, value);
        }

        /// <summary>
        /// The list of open ports.
        /// </summary>
        private List<int> openPorts = new List<int>();
        /// <summary>
        /// The list of open ports.
        /// </summary>
        public List<int> OpenPorts
        {
            get => openPorts;
            set => SetProperty(ref openPorts, value);
        }

        /// <summary>
        /// The destination ip address string.
        /// </summary>
        public string Destination
        {
            get => Model.Destination.ToString();
            set => Model.Destination = IPAddress.Parse(value);
        }

        /// <summary>
        /// If the port scanner should scan custom defined ports.
        /// </summary>
        public bool ScanIndividualPorts
        {
            get => Model.ScanIndividualPorts;
            set => Model.ScanIndividualPorts = value;
        }

        /// <summary>
        /// The start port of the scanning process.
        /// </summary>
        public int StartPort
        {
            get => Model.StartPort;
            set => Model.StartPort = value;
        }

        /// <summary>
        /// The end port of the scanning process.
        /// </summary>
        public int EndPort
        {
            get => Model.EndPort;
            set => Model.EndPort = value;
        }
    }
}
