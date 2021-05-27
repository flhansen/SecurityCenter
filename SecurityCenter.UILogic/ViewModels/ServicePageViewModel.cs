using SecurityCenter.Data.System;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SecurityCenter.Business.Models;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Threading.Tasks;
using SecurityCenter.UILogic.Commands;

namespace SecurityCenter.UILogic.ViewModels
{
    /// <summary>
    /// The ViewModel class for the service page.
    /// </summary>
    public class ServicePageViewModel : ViewModelBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ServicePageViewModel()
        {
            // Initialize all the commands.
            RefreshServicesCommand = new RelayCommand(RefreshServices);

            // Get all services of this system.
            ServiceCollection services = new ServiceCollection(SystemAccess.GetServices().OrderBy(s => s.ServiceName).ToList());
            Services = new ServiceCollectionViewModel(services);
            FilteredServices = Services.AsEnumerable();
        }

        /// <summary>
        /// The command to refresh the application list.
        /// </summary>
        public ICommand RefreshServicesCommand { get; private set; }
        /// <summary>
        /// The method to refresh the application list.
        /// </summary>
        private void RefreshServices(object obj)
        {
            if (!IsRefreshing)
            {
                IsRefreshing = true;

                Task.Run(() =>
                {
                    ServiceCollection services = new ServiceCollection(SystemAccess.GetServices().OrderBy(s => s.ServiceName).ToList());
                    Services = new ServiceCollectionViewModel(services);
                    FilteredServices = Services.AsEnumerable();
                    FilterText = "";

                    IsRefreshing = false;
                });
            }
        }

        /// <summary>
        /// The collection of services.
        /// </summary>
        private ServiceCollectionViewModel services;
        /// <summary>
        /// The collection of services.
        /// </summary>
        public ServiceCollectionViewModel Services
        {
            get => services;
            set => SetProperty(ref services, value);
        }

        /// <summary>
        /// The list of filtered services.
        /// </summary>
        private IEnumerable<ServiceViewModel> filteredServices;
        /// <summary>
        /// The list of filtered services.
        /// </summary>
        public IEnumerable<ServiceViewModel> FilteredServices
        {
            get => filteredServices;
            set => SetProperty(ref filteredServices, value);
        }

        /// <summary>
        /// If the viewmodel is refreshing the service list.
        /// </summary>
        private bool isRefreshing = false;
        /// <summary>
        /// If the viewmodel is refreshing the service list.
        /// </summary>
        public bool IsRefreshing
        {
            get => isRefreshing;
            set => SetProperty(ref isRefreshing, value);
        }

        /// <summary>
        /// The filter string, which is used to filter services.
        /// </summary>
        private string filterText;
        /// <summary>
        /// The filter string, which is used to filter services.
        /// </summary>
        public string FilterText
        {
            get => filterText;
            set
            {
                SetProperty(ref filterText, value);
                FilteredServices = Services.Where(vm => Regex.IsMatch(vm.Name.ToLowerInvariant(), filterText.ToLowerInvariant()));
            }
        }
    }
}
