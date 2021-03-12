using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaMaintenancer.Business.Models
{
    [JsonObject]
    public class ServiceProfile
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("entries")]
        public ServiceProfileEntryCollection Entries { get; set; }
    }
}
