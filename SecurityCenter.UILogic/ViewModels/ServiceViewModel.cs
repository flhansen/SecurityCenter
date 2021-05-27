using SecurityCenter.Business.Models;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using System.Linq;

namespace SecurityCenter.UILogic.ViewModels
{
    public class ServiceViewModel : ViewModel<Service>
    {
        public ServiceViewModel(Service model) : base(model)
        {

        }

        public string Name
        {
            get => Model.ServiceName;
        }

        public string Description
        {
            get => Model.DisplayName;
        }

        public ServiceStartMode StartType
        {
            get => Model.StartMode;
            set => Model.ChangeStartType(value);
        }

        public IEnumerable<ServiceStartMode> PossibleStartTypes => Enum.GetValues(typeof(ServiceStartMode)).Cast<ServiceStartMode>();

        public ServiceControllerStatus Status
        {
            get => Model.Status;
            set => Model.ChangeState(value);
        }

        public IEnumerable<ServiceControllerStatus> PossibleStatusTypes => new List<ServiceControllerStatus>
        {
            ServiceControllerStatus.Running,
            ServiceControllerStatus.Stopped,
            ServiceControllerStatus.Paused
        };
        
    }
}
