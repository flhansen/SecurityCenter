using AquaMaintenancer.Data.System;
using SecurityCenter.UILogic.Commands;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows.Input;

namespace SecurityCenter.UILogic.ViewModels
{
    public class UpdatePageViewModel : ViewModelBase
    {
        public UpdatePageViewModel()
        {
            AvailableUpdates = new WindowsUpdateCollectionViewModel(SystemAccess.GetAvailableUpdates());
            InstallUpdatesCommand = new RelayCommand(InstallUpdatesAction);
        }

        public ICommand InstallUpdatesCommand { get; private set; }
        private void InstallUpdatesAction(object sender)
        {
            var selectedUpdates = AvailableUpdates.Where(x => x.IsSelected);
        }

        private WindowsUpdateCollectionViewModel availableUpdates;
        public WindowsUpdateCollectionViewModel AvailableUpdates
        {
            get => availableUpdates;
            set => SetProperty(ref availableUpdates, value);
        }
    }
}
