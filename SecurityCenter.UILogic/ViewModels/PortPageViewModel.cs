using SecurityCenter.Business.Models;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityCenter.UILogic.ViewModels
{
    public class PortPageViewModel : ViewModelBase
    {
        public PortPageViewModel()
        {
            PortScanViewModel = new PortScanViewModel(PortScan.Default);
        }

        private PortScanViewModel portScan;
        public PortScanViewModel PortScanViewModel
        {
            get => portScan;
            set => SetProperty(ref portScan, value);
        }
    }

}
