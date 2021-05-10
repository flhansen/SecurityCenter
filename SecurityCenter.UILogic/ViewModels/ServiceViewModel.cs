using SecurityCenter.Business.Models;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
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

        public string Status
        {
            get => Model.Status.ToString();
        }
        
    }
}
