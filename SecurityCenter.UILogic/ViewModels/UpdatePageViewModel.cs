using SecurityCenter.Data.System;
using SecurityCenter.UILogic.Commands;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows.Input;
using System.Threading.Tasks;

namespace SecurityCenter.UILogic.ViewModels
{
    public class UpdatePageViewModel : ViewModelBase
    {
        public UpdatePageViewModel()
        {
            LoadAvailableUpdates();
            InstallUpdatesCommand = new RelayCommand(InstallUpdates);
            RefreshAvailableUpdatesCommand = new RelayCommand(RefreshAvailableUpdates);
            CancelUpdateInstallationCommand = new RelayCommand(CancelUpdateInstallation);
        }

        private async void LoadAvailableUpdates()
        {
            await Task.Run(() =>
            {
                // Make the list empty.
                AvailableUpdates = new WindowsUpdateCollectionViewModel();

                // Load new list of updates.
                IsLoadingUpdates = true;
                AvailableUpdates = new WindowsUpdateCollectionViewModel(SystemAccess.GetAvailableUpdates());
                IsLoadingUpdates = false;
            });
        }

        public ICommand InstallUpdatesCommand { get; private set; }
        private void InstallUpdates(object sender)
        {
            ShowUpdateDialog = true;
            var selectedUpdates = AvailableUpdates.Where(x => x.IsSelected);

            // TODO: Implement me!
        }

        public ICommand CancelUpdateInstallationCommand { get; private set; }
        private void CancelUpdateInstallation(object sender)
        {
            ShowUpdateDialog = false;
        }

        public ICommand RefreshAvailableUpdatesCommand { get; private set; }
        private void RefreshAvailableUpdates(object sender)
        {
            LoadAvailableUpdates();
        }

        private bool showUpdateDialog;
        public bool ShowUpdateDialog
        {
            get => showUpdateDialog;
            set => SetProperty(ref showUpdateDialog, value);
        }

        private bool isLoadingUpdates;
        public bool IsLoadingUpdates
        {
            get => isLoadingUpdates;
            set => SetProperty(ref isLoadingUpdates, value);
        }

        private WindowsUpdateCollectionViewModel availableUpdates;
        public WindowsUpdateCollectionViewModel AvailableUpdates
        {
            get => availableUpdates;
            set => SetProperty(ref availableUpdates, value);
        }
    }
}
