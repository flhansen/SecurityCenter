using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WUApiLib;

namespace AquaMaintenancer.Business.Models
{
    public class WindowsUpdate
    {
        public bool IsOfflineUpdate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string KbNumber { get; set; }
        public DateTime ReleaseDate { get; set; }

        [JsonIgnore]
        public IUpdate Update { get; set; }
    }
}
