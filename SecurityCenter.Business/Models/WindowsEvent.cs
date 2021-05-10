using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCenter.Business.Models
{
    public class WindowsEvent
    {
        public DateTime TimeGenerated { get; set; }
        public string EntryType { get; set; }
        public long InstanceId { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public int Index { get; set; }
        public byte Level { get; set; }
    }
}
