using AquaMaintenancer.Data.System;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AquaMaintenancer.Business.Models;

namespace SecurityCenter.UILogic.ViewModels
{
    public class ServicePageViewModel : ViewModelBase
    {
        public ServicePageViewModel()
        {
            ServiceCollection services = new ServiceCollection(SystemAccess.GetServices().OrderBy(s => s.ServiceName).ToList());
            Services = new ServiceCollectionViewModel(services);
        }

        private ServiceCollectionViewModel services;
        public ServiceCollectionViewModel Services
        {
            get => services;
            set => SetProperty(ref services, value);
        }
    }
}
