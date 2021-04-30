using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaMaintenancer.Business.Models
{
    public class WindowsEventCollection : ObservableCollection<WindowsEvent>
    {
        public string LogDisplayName { get; set; }
        public string LogName { get; set; }

        public WindowsEventCollection() : base()
        {

        }

        public WindowsEventCollection(IEnumerable<WindowsEvent> events) : base(events)
        {

        }
    }
}
