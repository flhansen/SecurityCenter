using SecurityCenter.Business.Models;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using System.Linq;

namespace SecurityCenter.UILogic.ViewModels
{
    /// <summary>
    /// The ViewModel class for services.
    /// </summary>
    public class ServiceViewModel : ViewModel<Service>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model">The service model</param>
        public ServiceViewModel(Service model) : base(model)
        {
        }

        /// <summary>
        /// The name of the service.
        /// </summary>
        public string Name
        {
            get => Model.ServiceName;
        }

        /// <summary>
        /// The description of the service.
        /// </summary>
        public string Description
        {
            get => Model.DisplayName;
        }

        /// <summary>
        /// The start type of the service.
        /// </summary>
        public ServiceStartMode StartType
        {
            get => Model.StartMode;
            set => Model.ChangeStartType(value);
        }

        /// <summary>
        /// The list of possible start type values of the service.
        /// </summary>
        public IEnumerable<ServiceStartMode> PossibleStartTypes => Enum.GetValues(typeof(ServiceStartMode)).Cast<ServiceStartMode>();

        /// <summary>
        /// The status of the service.
        /// </summary>
        public ServiceControllerStatus Status
        {
            get => Model.Status;
            set => Model.ChangeState(value);
        }

        /// <summary>
        /// The list of possible status values of the service.
        /// </summary>
        public IEnumerable<ServiceControllerStatus> PossibleStatusTypes => new List<ServiceControllerStatus>
        {
            ServiceControllerStatus.Running,
            ServiceControllerStatus.Stopped,
            ServiceControllerStatus.Paused
        };
        
    }
}
