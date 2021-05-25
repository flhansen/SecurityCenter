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

        public void ChangeState(ServiceControllerStatus value)
        {
            switch (value)
            {
                case ServiceControllerStatus.Running:
                    Controller.Start();
                    break;

                case ServiceControllerStatus.Stopped:
                    Controller.Stop();
                    break;

                case ServiceControllerStatus.Paused:
                    Controller.Pause();
                    break;
            }

            Controller.WaitForStatus(value);
            Status = Controller.Status;
        }
    }
}
