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
    public class PortScanViewModel : ViewModel<PortScan>
    {
        public PortScanViewModel(PortScan model) : base(model)
        {
            ScanCommand = new RelayCommand(Scan);
        }

        public ICommand ScanCommand { get; private set; }
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

        private bool isScanning = false;
        public bool IsScanning
        {
            get => isScanning;
            set => SetProperty(ref isScanning, value);
        }

        private List<int> openPorts = new List<int>();
        public List<int> OpenPorts
        {
            get => openPorts;
            set => SetProperty(ref openPorts, value);
        }

        public string Destination
        {
            get => Model.Destination.ToString();
            set => Model.Destination = IPAddress.Parse(value);
        }

        public bool ScanIndividualPorts
        {
            get => Model.ScanIndividualPorts;
            set => Model.ScanIndividualPorts = value;
        }

        public bool InterpretOSFingerprint
        {
            get => Model.InterpretOSFingerprint;
            set => Model.InterpretOSFingerprint = value;
        }

        public int StartPort
        {
            get => Model.StartPort;
            set => Model.StartPort = value;
        }

        public int EndPort
        {
            get => Model.EndPort;
            set => Model.EndPort = value;
        }

        public bool IsAggressive
        {
            get => Model.IsAggressive;
            set => Model.IsAggressive = value;
        }

        public bool ScanRunningServices
        {
            get => Model.ScanRunningServices;
            set => Model.ScanRunningServices = value;
        }

        public bool SaveResultsInFile
        {
            get => Model.SaveResultsInFile;
            set => Model.SaveResultsInFile = value;
        }

        public string SaveFileName
        {
            get => Model.SaveFileName;
            set => Model.SaveFileName = value;
        }
    }
}
