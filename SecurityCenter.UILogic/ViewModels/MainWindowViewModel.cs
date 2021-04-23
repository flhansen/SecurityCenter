using SecurityCenter.UILogic.Commands;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SecurityCenter.UILogic.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            CurrentPageViewModel = DashboardPageViewModel;

            DashboardPageSelectionCommand = new RelayCommand(DashboardPageSelectionAction);
            ApplicationPageSelectionCommand = new RelayCommand(ApplicationPageSelectionAction);
            ServicePageSelectionCommand = new RelayCommand(ServicePageSelectionAction);
            UpdatePageSelectionCommand = new RelayCommand(UpdatePageSelectionAction);
            FirewallPageSelectionCommand = new RelayCommand(FirewallPageSelectionAction);
            PortPageSelectionCommand = new RelayCommand(PortPageSelectionAction);
        }

        public DashboardPageViewModel DashboardPageViewModel { get; set; } = new DashboardPageViewModel();
        public ApplicationPageViewModel ApplicationPageViewModel { get; set; } = new ApplicationPageViewModel();
        public ServicePageViewModel ServicePageViewModel { get; set; } = new ServicePageViewModel();
        public UpdatePageViewModel UpdatePageViewModel { get; set; } = new UpdatePageViewModel();
        public FirewallPageViewModel FirewallPageViewModel { get; set; } = new FirewallPageViewModel();
        public PortPageViewModel PortPageViewModel { get; set; } = new PortPageViewModel();

        private ViewModelBase currentPageViewModel;
        public ViewModelBase CurrentPageViewModel
        {
            get => currentPageViewModel;
            set => SetProperty(ref currentPageViewModel, value);
        }

        public ICommand DashboardPageSelectionCommand { get; private set; }
        private void DashboardPageSelectionAction(object sender)
        {
            CurrentPageViewModel = DashboardPageViewModel;
        }

        public ICommand ApplicationPageSelectionCommand { get; private set; }
        private void ApplicationPageSelectionAction(object sender)
        {
            CurrentPageViewModel = ApplicationPageViewModel;
        }

        public ICommand ServicePageSelectionCommand { get; private set; }
        private void ServicePageSelectionAction(object sender)
        {
            CurrentPageViewModel = ServicePageViewModel;
        }

        public ICommand UpdatePageSelectionCommand { get; private set; }
        private void UpdatePageSelectionAction(object sender)
        {
            CurrentPageViewModel = UpdatePageViewModel;
        }

        public ICommand FirewallPageSelectionCommand { get; private set; }
        private void FirewallPageSelectionAction(object sender)
        {
            CurrentPageViewModel = FirewallPageViewModel;
        }

        public ICommand PortPageSelectionCommand { get; private set; }
        private void PortPageSelectionAction(object sender)
        {
            CurrentPageViewModel = PortPageViewModel;
        }
    }
}
