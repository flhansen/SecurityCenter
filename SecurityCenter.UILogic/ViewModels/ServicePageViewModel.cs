using AquaMaintenancer.Data.System;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AquaMaintenancer.Business.Models;
using System.Text.RegularExpressions;

namespace SecurityCenter.UILogic.ViewModels
{
    public class ServicePageViewModel : ViewModelBase
    {
        public ServicePageViewModel()
        {
            ServiceCollection services = new ServiceCollection(SystemAccess.GetServices().OrderBy(s => s.ServiceName).ToList());
            Services = new ServiceCollectionViewModel(services);
            FilteredServices = Services.AsEnumerable();
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
