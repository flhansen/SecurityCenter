using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCenter.Business.Models
{
    public class SecurityStatus
    {
        public AntiVirusCollection AntiViruses { get; set; }
        public FirewallInformation FirewallInformation { get; set; }
    }
}
