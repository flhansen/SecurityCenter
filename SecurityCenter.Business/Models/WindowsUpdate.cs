using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WUApiLib;

namespace SecurityCenter.Business.Models
{
    public class WindowsUpdate
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string KbNumber { get; set; }
        public DateTime ReleaseDate { get; set; }
        public IUpdate Update { get; set; }
    }
}
