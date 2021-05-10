using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCenter.Business.Models
{
    public class FirewallInformation
    {
        public bool IsPrivateActivated { get; set; }
        public bool IsPublicActivated { get; set; }
        public bool IsDomainActivated { get; set; }
    }
}
