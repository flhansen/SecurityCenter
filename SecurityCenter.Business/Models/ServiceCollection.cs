using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCenter.Business.Models
{
    public class ServiceCollection : ObservableCollection<Service>
    {
        public ServiceCollection() : base()
        {

        }

        public ServiceCollection(IEnumerable<Service> services) : base(services)
        {

        }
    }
}
