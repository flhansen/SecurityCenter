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
            BarChartSubCategories = new List<string>() { "Error", "Information", "Warning" };
            BarChartColors = new List<string>() { "#5D4ADA", "#66CA67", "#2E9BFF" };

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

        private WindowsEventCollectionViewModel windowsEvents = new WindowsEventCollectionViewModel();
        public WindowsEventCollectionViewModel WindowsEvents
        {
            get => windowsEvents;
            set => SetProperty(ref windowsEvents, value);
        }

        private List<string> barChartSubCategories;
        public List<string> BarChartSubCategories
        {
            get => barChartSubCategories;
            set => SetProperty(ref barChartSubCategories, value);
        }

        private List<string> barChartColors = new List<string>();
        public List<string> BarChartColors
        {
            get => barChartColors;
            set => SetProperty(ref barChartColors, value);
        }

        private void LoadWindowsEventsAsync()
        {
            Task.Run(() => {
                WindowsEventCollection windowsEvents = SystemAccess.GetEvents(1000);
                WindowsEventCollection windowsEvents = SystemAccess.GetEvents(DateTime.Now.AddDays(-7));
                WindowsEvents = new WindowsEventCollectionViewModel(windowsEvents);
            });
        }
    }
}
