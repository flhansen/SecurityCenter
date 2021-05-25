using SecurityCenter.Business.Models;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;

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

        public string StartType
        {
            get => Model.StartMode.ToString();
        }

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
