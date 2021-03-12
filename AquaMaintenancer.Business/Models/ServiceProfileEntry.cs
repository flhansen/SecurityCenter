using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace AquaMaintenancer.Business.Models
{
    [JsonObject]
    public class ServiceProfileEntry
    {
        [JsonProperty("serviceName")]
        public string ServiceName { get; set; }

        [JsonProperty("startType")]
        public ServiceStartMode StartType { get; set; }

        [JsonProperty("statusOnLoad")]
        public string StatusOnLoad { get; set; }
    }
}
