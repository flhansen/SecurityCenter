using AquaMaintenancer.Business.Models;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SecurityCenter.UILogic.ViewModels
{
    public class ServiceCollectionViewModel : ViewModelCollectionBase<ServiceViewModel, Service>
    {
        public ServiceCollectionViewModel(ObservableCollection<Service> services) : base(services)
        {

        }
    }
}
