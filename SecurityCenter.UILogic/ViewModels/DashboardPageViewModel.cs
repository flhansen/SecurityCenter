using AquaMaintenancer.Data.System;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityCenter.UILogic.ViewModels
{
    public class DashboardPageViewModel : ViewModelBase
    {
        public DashboardPageViewModel()
        {
            Applications = new ApplicationCollectionViewModel(SystemAccess.GetApplications());
            Services = new ServiceCollectionViewModel(SystemAccess.GetServices());
            SystemInformation = new SystemInformationViewModel(SystemAccess.GetSystemInformation());
        }

        private ApplicationCollectionViewModel applications;
        public ApplicationCollectionViewModel Applications
        {
            get => applications;
            set => SetProperty(ref applications, value);
        }

        private ServiceCollectionViewModel services;
        public ServiceCollectionViewModel Services
        {
            get => services;
            set => SetProperty(ref services, value);
        }

        private SystemInformationViewModel systemInformation;
        public SystemInformationViewModel SystemInformation
        {
            get => systemInformation;
            set => SetProperty(ref systemInformation, value);
        }
    }
}
