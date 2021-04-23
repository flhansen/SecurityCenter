using AquaMaintenancer.Business.Models;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SecurityCenter.UILogic.ViewModels
{
    public class ApplicationCollectionViewModel : ViewModelCollectionBase<ApplicationViewModel, Application>
    {
        public ApplicationCollectionViewModel(ObservableCollection<Application> applications) : base(applications)
        {

        }
    }
}
