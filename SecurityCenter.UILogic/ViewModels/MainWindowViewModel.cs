using SecurityCenter.UILogic.Commands;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SecurityCenter.UILogic.ViewModels
{
    /// <summary>
    /// The ViewModel class for the main window.
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindowViewModel()
        {
            CurrentPageViewModel = DashboardPageViewModel;

            DashboardPageSelectionCommand = new RelayCommand(DashboardPageSelectionAction);
            ApplicationPageSelectionCommand = new RelayCommand(ApplicationPageSelectionAction);
            ServicePageSelectionCommand = new RelayCommand(ServicePageSelectionAction);
            UpdatePageSelectionCommand = new RelayCommand(UpdatePageSelectionAction);
            FirewallPageSelectionCommand = new RelayCommand(FirewallPageSelectionAction);
            PortPageSelectionCommand = new RelayCommand(PortPageSelectionAction);
            PluginPageSelectionCommand = new RelayCommand(PluginPageSelectionAction);
        }

        /// <summary>
        /// The viewmodel for the dashboard page.
        /// </summary>
        public DashboardPageViewModel DashboardPageViewModel { get; set; } = new DashboardPageViewModel();

        /// <summary>
        /// The viewmodel for the application page.
        /// </summary>
        public ApplicationPageViewModel ApplicationPageViewModel { get; set; } = new ApplicationPageViewModel();

        /// <summary>
        /// The viewmodel for the service page.
        /// </summary>
        public ServicePageViewModel ServicePageViewModel { get; set; } = new ServicePageViewModel();

        /// <summary>
        /// The viewmodel for the update page.
        /// </summary>
        public UpdatePageViewModel UpdatePageViewModel { get; set; } = new UpdatePageViewModel();

        /// <summary>
        /// The viewmodel for the firewall page.
        /// </summary>
        public FirewallPageViewModel FirewallPageViewModel { get; set; } = new FirewallPageViewModel();

        /// <summary>
        /// The viewmodel for the port page.
        /// </summary>
        public PortPageViewModel PortPageViewModel { get; set; } = new PortPageViewModel();

        /// <summary>
        /// The viewmodel for the plugin page.
        /// </summary>
        public PluginPageViewModel PluginPageViewModel { get; set; } = new PluginPageViewModel();

        /// <summary>
        /// The current selected page viewmodel. This is used to determine which page to be displayed.
        /// </summary>
        private ViewModelBase currentPageViewModel;
        /// <summary>
        /// The current selected page viewmodel. This is used to determine which page to be displayed.
        /// </summary>
        public ViewModelBase CurrentPageViewModel
        {
            get => currentPageViewModel;
            set => SetProperty(ref currentPageViewModel, value);
        }

        /// <summary>
        /// The command to select the dashboard page.
        /// </summary>
        public ICommand DashboardPageSelectionCommand { get; private set; }
        /// <summary>
        /// The method to select the dashboard page.
        /// </summary>
        private void DashboardPageSelectionAction(object sender)
        {
            CurrentPageViewModel = DashboardPageViewModel;
        }

        /// <summary>
        /// The command to select the application page.
        /// </summary>
        public ICommand ApplicationPageSelectionCommand { get; private set; }
        /// <summary>
        /// The method to select the application page.
        /// </summary>
        private void ApplicationPageSelectionAction(object sender)
        {
            CurrentPageViewModel = ApplicationPageViewModel;
        }

        /// <summary>
        /// The command to select the service page.
        /// </summary>
        public ICommand ServicePageSelectionCommand { get; private set; }
        /// <summary>
        /// The method to select the service page.
        /// </summary>
        private void ServicePageSelectionAction(object sender)
        {
            CurrentPageViewModel = ServicePageViewModel;
        }

        /// <summary>
        /// The command to select the update page.
        /// </summary>
        public ICommand UpdatePageSelectionCommand { get; private set; }
        /// <summary>
        /// The method to select the update page.
        /// </summary>
        private void UpdatePageSelectionAction(object sender)
        {
            CurrentPageViewModel = UpdatePageViewModel;
        }

        /// <summary>
        /// The command to select the firewall page.
        /// </summary>
        public ICommand FirewallPageSelectionCommand { get; private set; }
        /// <summary>
        /// The method to select the firewall page.
        /// </summary>
        private void FirewallPageSelectionAction(object sender)
        {
            CurrentPageViewModel = FirewallPageViewModel;
        }

        /// <summary>
        /// The command to select the port page.
        /// </summary>
        public ICommand PortPageSelectionCommand { get; private set; }
        /// <summary>
        /// The method to select the port page.
        /// </summary>
        private void PortPageSelectionAction(object sender)
        {
            CurrentPageViewModel = PortPageViewModel;
        }

        /// <summary>
        /// The command to select the plugin page.
        /// </summary>
        public ICommand PluginPageSelectionCommand { get; private set; }
        /// <summary>
        /// The method to select the plugin page.
        /// </summary>
        /// <param name="obj"></param>
        private void PluginPageSelectionAction(object obj)
        {
            CurrentPageViewModel = PluginPageViewModel;
        }
    }
}
