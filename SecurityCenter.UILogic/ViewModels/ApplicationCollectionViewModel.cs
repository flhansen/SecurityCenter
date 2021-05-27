using SecurityCenter.Business.Models;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SecurityCenter.UILogic.ViewModels
{
    /// <summary>
    /// ViewModel class for collections of application viewmodels.
    /// </summary>
    public class ApplicationCollectionViewModel : ViewModelCollectionBase<ApplicationViewModel, Application>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="applications">Collection of application models</param>
        public ApplicationCollectionViewModel(ObservableCollection<Application> applications) : base(applications)
        {
        }
    }
}
