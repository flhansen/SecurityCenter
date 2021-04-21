using AquaMaintenancer.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaMaintenancer.UILogic.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            CurrentPageViewModel = new DashboardPageViewModel();
        }

        private ViewModelBase currentPageViewModel;
        public ViewModelBase CurrentPageViewModel
        {
            get => currentPageViewModel;
            set => SetProperty(ref currentPageViewModel, value);
        }
    }
}
