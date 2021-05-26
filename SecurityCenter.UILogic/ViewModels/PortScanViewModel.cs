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
            await Model.ScanPorts();
            OpenPorts = Model.ScannedPorts.Where(x => x.Value).Select(x => x.Key).ToList();
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

        public string StartPort
        {
            get => Model.StartPort.ToString();
            set => Model.StartPort = int.Parse(value);
        }

        public string EndPort
        {
            get => Model.EndPort.ToString();
            set => Model.EndPort = int.Parse(value);
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
