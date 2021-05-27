using SecurityCenter.Business.Models;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCenter.UILogic.ViewModels
{
    /// <summary>
    /// The ViewModel class for the security status.
    /// </summary>
    public class SecurityStatusViewModel : ViewModel<SecurityStatus>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model">The security status model</param>
        public SecurityStatusViewModel(SecurityStatus model) : base(model)
        {
        }

        /// <summary>
        /// The name of all running anti virus software.
        /// </summary>
        public string AntiVirusSoftware
        {
            get => string.Join(", ", Model.AntiViruses.Select(x => x.DisplayName).ToArray());
        }

        /// <summary>
        /// The name of the running firewall.
        /// </summary>
        public string ActiveFirewallName
        {
            get => "Windows Defender Firewall";
        }

        /// <summary>
        /// If the public profile is protected.
        /// </summary>
        public bool IsPublicProfileProtected
        {
            get => Model.FirewallInformation.IsPublicActivated;
        }

        /// <summary>
        /// If the private profile is protected.
        /// </summary>
        public bool IsPrivateProfileProtected
        {
            get => Model.FirewallInformation.IsPrivateActivated;
        }

        /// <summary>
        /// If the domain profile is protected.
        /// </summary>
        public bool IsDomainProfileProtected
        {
            get => Model.FirewallInformation.IsDomainActivated;
        }
    }
}
