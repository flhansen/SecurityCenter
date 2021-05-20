using SecurityCenter.Data.System;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityCenter.UILogic.ViewModels
{
    public class FirewallPageViewModel : ViewModelBase
    {
        public FirewallPageViewModel()
        {
            SecurityStatus = new SecurityStatusViewModel(SystemAccess.GetSecurityStatus());
        }

        private SecurityStatusViewModel securityStatus;
        public SecurityStatusViewModel SecurityStatus
        {
            get => securityStatus;
            set => SetProperty(ref securityStatus, value);
        }
    }
}
