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
using System.Threading.Tasks;

namespace SecurityCenter.UILogic.ViewModels
{
    /// <summary>
    /// ViewModel class for the application page.
    /// </summary>
    public class ApplicationPageViewModel : ViewModelBase
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public ApplicationPageViewModel()
        {
            // Initialize all the commands.
            UninstallApplicationCommand = new RelayCommand(UninstallApplication);
            RefreshApplicationsCommand = new RelayCommand(RefreshApplications);

            // Get all applications of this system and store them into the ViewModel.
            Applications = new ApplicationCollectionViewModel(SystemAccess.GetApplications());
            FilteredApplications = Applications.AsEnumerable();
        }

        /// <summary>
        /// Command to refresh the applications.
        /// </summary>
        public ICommand RefreshApplicationsCommand { get; private set; }
        /// <summary>
        /// Method to refresh the applications.
        /// </summary>
        private void RefreshApplications(object obj)
        {
            if (!IsRefreshingApplications)
            {
                IsRefreshingApplications = true;

                Task.Run(() =>
                {
                    Applications = new ApplicationCollectionViewModel(SystemAccess.GetApplications());
                    FilteredApplications = Applications.AsEnumerable();
                    FilterText = "";

                    IsRefreshingApplications = false;
                });
            }
        }

        /// <summary>
        /// Command to uninstall a specific application.
        /// </summary>
        public ICommand UninstallApplicationCommand { get; private set; }
        /// <summary>
        /// Method to uninstall a specific application.
        /// </summary>
        private void UninstallApplication(object obj)
        {
            string uninstallPath = obj as string;
            SystemAccess.UninstallApplication(uninstallPath);
        }

        /// <summary>
        /// Collection of applications.
        /// </summary>
        private ApplicationCollectionViewModel applications;
        /// <summary>
        /// Collection of applications.
        /// </summary>
        public ApplicationCollectionViewModel Applications
        {
            get => applications;
            set => SetProperty(ref applications, value);
        }

        /// <summary>
        /// List of applications filtered by FilterText property.
        /// </summary>
        private IEnumerable<ApplicationViewModel> filteredApplications;
        /// <summary>
        /// List of applications filtered by FilterText property.
        /// </summary>
        public IEnumerable<ApplicationViewModel> FilteredApplications
        {
            get => filteredApplications;
            set => SetProperty(ref filteredApplications, value);
        }

        /// <summary>
        /// Returns true, if the viewmodel is in refreshing state.
        /// </summary>
        private bool isRefreshingApplications = false;
        /// <summary>
        /// Returns true, if the viewmodel is in refreshing state.
        /// </summary>
        public bool IsRefreshingApplications
        {
            get => isRefreshingApplications;
            set => SetProperty(ref isRefreshingApplications, value);
        }

        /// <summary>
        /// String, which is used to filter the application collection.
        /// </summary>
        private string filterText;
        /// <summary>
        /// String, which is used to filter the application collection.
        /// </summary>
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
