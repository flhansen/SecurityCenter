using SecurityCenter.Business.Models;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCenter.UILogic.ViewModels
{
    public class PortScanViewModel : ViewModel<PortScan>
    {
        public PortScanViewModel(PortScan model) : base(model)
        {
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
