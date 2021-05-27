using SecurityCenter.Business.Models;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityCenter.UILogic.ViewModels
{
    /// <summary>
    /// The ViewModel class for the port page.
    /// </summary>
    public class PortPageViewModel : ViewModelBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PortPageViewModel()
        {
            PortScanViewModel = new PortScanViewModel(PortScan.Default);
        }

        /// <summary>
        /// The viewmodel for the port scanner.
        /// </summary>
        private PortScanViewModel portScanViewModel;
        /// <summary>
        /// The viewmodel for the port scanner.
        /// </summary>
        public PortScanViewModel PortScanViewModel
        {
            get => portScanViewModel;
            set => SetProperty(ref portScanViewModel, value);
        }
    }

}
