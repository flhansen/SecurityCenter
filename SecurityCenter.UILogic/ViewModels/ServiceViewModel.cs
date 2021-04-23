using AquaMaintenancer.Business.Models;
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
    }
}
