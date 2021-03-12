using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaMaintenancer.Business.Models
{
    public class AntiVirus
    {
        public string InstanceGuid { get; set; }
        public string DisplayName { get; set; }
        public string PathToSignedProductExe { get; set; }
        public string PathToSignedReportingExe { get; set; }
        public uint ProductState { get; set; }
        public string Timestamp { get; set; }
    }
}
