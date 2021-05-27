using SecurityCenter.Business.Models;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SecurityCenter.UILogic.ViewModels
{
    /// <summary>
    /// The collection class for service viewmodels.
    /// </summary>
    public class ServiceCollectionViewModel : ViewModelCollectionBase<ServiceViewModel, Service>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="services">Collection of service models</param>
        public ServiceCollectionViewModel(ObservableCollection<Service> services) : base(services)
        {

        }
    }
}
