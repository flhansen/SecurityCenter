using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;

namespace SecurityCenter.Business.Models
{
    public class Service
    {
        public ServiceController Controller { get; set; }

        public string DisplayName { get; set; }

        public string ServiceName { get; set; }

        public ServiceStartMode StartMode { get; set; }

        public ServiceControllerStatus Status { get; set; }
    }
}
