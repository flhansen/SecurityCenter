using SecurityCenter.Data.System;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityCenter.UILogic.ViewModels
{
    /// <summary>
    /// The ViewModel class for the firewall page.
    /// </summary>
    public class FirewallPageViewModel : ViewModelBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FirewallPageViewModel()
        {
            SecurityStatus = new SecurityStatusViewModel(SystemAccess.GetSecurityStatus());
        }

        /// <summary>
        /// The security status viewmodel.
        /// </summary>
        private SecurityStatusViewModel securityStatus;
        /// <summary>
        /// The security status viewmodel.
        /// </summary>
        public SecurityStatusViewModel SecurityStatus
        {
            get => securityStatus;
            set => SetProperty(ref securityStatus, value);
        }
    }
}
