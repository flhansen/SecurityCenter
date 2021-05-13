using SecurityCenter.Data.System;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.Text.RegularExpressions;
using SecurityCenter.UILogic.Commands;
using System.Windows.Input;

namespace SecurityCenter.UILogic.ViewModels
{
    public class ApplicationPageViewModel : ViewModelBase
    {

        public ApplicationPageViewModel()
        {
            Applications = new ApplicationCollectionViewModel(SystemAccess.GetApplications());
            FilteredApplications = Applications.AsEnumerable();
        }

        private ApplicationCollectionViewModel applications;
        public ApplicationCollectionViewModel Applications
        {
            get => applications;
            set => SetProperty(ref applications, value);
        }

        private IEnumerable<ApplicationViewModel> filteredApplications;
        public IEnumerable<ApplicationViewModel> FilteredApplications
        {
            get => filteredApplications;
            set => SetProperty(ref filteredApplications, value);
        }

        private string filterText;
        public string FilterText
        {
            get => filterText;
            set
            {
                SetProperty(ref filterText, value);
                FilteredApplications = Applications.Where(a => Regex.IsMatch(a.Name.ToLowerInvariant(), filterText.ToLowerInvariant()));
            }
        }
    }
}
