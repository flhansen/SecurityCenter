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
    public class ServicePageViewModel : ViewModelBase
    {
        public ServicePageViewModel()
        {
            // Initialize all the commands.
            RefreshServicesCommand = new RelayCommand(RefreshServices);

            // Get all services of this system.
            ServiceCollection services = new ServiceCollection(SystemAccess.GetServices().OrderBy(s => s.ServiceName).ToList());
            Services = new ServiceCollectionViewModel(services);
            FilteredServices = Services.AsEnumerable();
        }

        public ICommand RefreshServicesCommand { get; private set; }
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

        private ServiceCollectionViewModel services;
        public ServiceCollectionViewModel Services
        {
            get => services;
            set => SetProperty(ref services, value);
        }

        private IEnumerable<ServiceViewModel> filteredServices;
        public IEnumerable<ServiceViewModel> FilteredServices
        {
            get => filteredServices;
            set => SetProperty(ref filteredServices, value);
        }

        private bool isRefreshing = false;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set => SetProperty(ref isRefreshing, value);
        }

        private string filterText;
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
