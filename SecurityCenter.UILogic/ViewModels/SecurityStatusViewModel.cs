using SecurityCenter.Business.Models;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCenter.UILogic.ViewModels
{
    public class SecurityStatusViewModel : ViewModel<SecurityStatus>
    {
        public SecurityStatusViewModel(SecurityStatus model) : base(model)
        {
        }

        public string AntiVirusSoftware
        {
            get => string.Join(", ", Model.AntiViruses.Select(x => x.DisplayName).ToArray());
        }

        public string ActiveFirewallName
        {
            get => "Windows Defender Firewall";
        }

        public bool IsPublicProfileProtected
        {
            get => Model.FirewallInformation.IsPublicActivated;
        }

        public bool IsPrivateProfileProtected
        {
            get => Model.FirewallInformation.IsPrivateActivated;
        }

        public bool IsDomainProfileProtected
        {
            get => Model.FirewallInformation.IsDomainActivated;
        }
    }
}
