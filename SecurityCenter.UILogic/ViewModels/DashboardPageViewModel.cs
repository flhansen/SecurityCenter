using SecurityCenter.Business.Models;
using SecurityCenter.Data.System;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;

namespace SecurityCenter.UILogic.ViewModels
{
    /// <summary>
    /// ViewModel class for the dashboard page.
    /// </summary>
    public class DashboardPageViewModel : ViewModelBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DashboardPageViewModel()
        {
            Applications = new ApplicationCollectionViewModel(SystemAccess.GetApplications());
            Services = new ServiceCollectionViewModel(SystemAccess.GetServices());
            SystemInformation = new SystemInformationViewModel(SystemAccess.GetSystemInformation());
            BarChartSubCategories = new List<string>() { "Kritisch", "Fehler", "Warnung" };
            BarChartColors = new List<string>() { "#5D4ADA", "#66CA67", "#2E9BFF" };
            PieChartColors = new List<string>() { "#5D4ADA", "#66CA67", "#2E9BFF" };

            LoadWindowsEventsAsync();
        }

        /// <summary>
        /// The collection of application viewmodels.
        /// </summary>
        private ApplicationCollectionViewModel applications;
        /// <summary>
        /// The collection of application viewmodels.
        /// </summary>
        public ApplicationCollectionViewModel Applications
        {
            get => applications;
            set => SetProperty(ref applications, value);
        }

        /// <summary>
        /// The collection of service viewmodels.
        /// </summary>
        private ServiceCollectionViewModel services;
        /// <summary>
        /// The collection of service viewmodels.
        /// </summary>
        public ServiceCollectionViewModel Services
        {
            get => services;
            set => SetProperty(ref services, value);
        }

        /// <summary>
        /// The viewmodel of system information.
        /// </summary>
        private SystemInformationViewModel systemInformation;
        /// <summary>
        /// The viewmodel of system information.
        /// </summary>
        public SystemInformationViewModel SystemInformation
        {
            get => systemInformation;
            set => SetProperty(ref systemInformation, value);
        }

        /// <summary>
        /// The collection of windows event viewmodels.
        /// </summary>
        private WindowsEventCollectionViewModel windowsEvents = new WindowsEventCollectionViewModel();
        /// <summary>
        /// The collection of windows event viewmodels.
        /// </summary>
        public WindowsEventCollectionViewModel WindowsEvents
        {
            get => windowsEvents;
            set => SetProperty(ref windowsEvents, value);
        }

        /// <summary>
        /// The list of sub categories of the bar chart.
        /// </summary>
        private List<string> barChartSubCategories;
        /// <summary>
        /// The list of sub categories of the bar chart.
        /// </summary>
        public List<string> BarChartSubCategories
        {
            get => barChartSubCategories;
            set => SetProperty(ref barChartSubCategories, value);
        }

        /// <summary>
        /// The colors used in the bar chart.
        /// </summary>
        private List<string> barChartColors = new List<string>();
        /// <summary>
        /// The colors used in the bar chart.
        /// </summary>
        public List<string> BarChartColors
        {
            get => barChartColors;
            set => SetProperty(ref barChartColors, value);
        }

        /// <summary>
        /// The colors used in the pie chart.
        /// </summary>
        private List<string> pieChartColors = new List<string>();
        /// <summary>
        /// The colors used in the pie chart.
        /// </summary>
        public List<string> PieChartColors
        {
            get => pieChartColors;
            set => SetProperty(ref pieChartColors, value);
        }

        /// <summary>
        /// Load windows events asynchronously for the last week.
        /// </summary>
        private void LoadWindowsEventsAsync()
        {
            Task.Run(() => {
                WindowsEventCollection windowsEvents = SystemAccess.GetEvents(DateTime.Now.AddDays(-6));
                windowsEvents = new WindowsEventCollection(windowsEvents.OrderByDescending(e => e.TimeGenerated).ToList());
                WindowsEvents = new WindowsEventCollectionViewModel(windowsEvents);
            });
        }
    }
}
