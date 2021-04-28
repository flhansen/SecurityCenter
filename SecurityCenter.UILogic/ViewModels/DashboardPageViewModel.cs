using AquaMaintenancer.Business.Models;
using AquaMaintenancer.Data.System;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace SecurityCenter.UILogic.ViewModels
{
    public class DashboardPageViewModel : ViewModelBase
    {
        public DashboardPageViewModel()
        {
            Applications = new ApplicationCollectionViewModel(SystemAccess.GetApplications());
            Services = new ServiceCollectionViewModel(SystemAccess.GetServices());
            SystemInformation = new SystemInformationViewModel(SystemAccess.GetSystemInformation());

            LoadWindowsEventsAsync();
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

        private WindowsEventCollectionViewModel windowsEvents;
        public WindowsEventCollectionViewModel WindowsEvents
        {
            get => windowsEvents;
            set => SetProperty(ref windowsEvents, value);
        }

        private void LoadWindowsEventsAsync()
        {
            Task.Run(() => {
                WindowsEventCollection windowsEvents = SystemAccess.GetEvents(100);
                WindowsEvents = new WindowsEventCollectionViewModel(windowsEvents);
            });
        }
    }
}
