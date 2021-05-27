using SecurityCenter.Data.System;
using SecurityCenter.UILogic.Commands;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows.Input;
using System.Threading.Tasks;
using SecurityCenter.Business.Updates;
using WUApiLib;

namespace SecurityCenter.UILogic.ViewModels
{
    public class UpdatePageViewModel : ViewModelBase
    {
        private UpdateManager updateManager;

        public UpdatePageViewModel()
        {
            LoadAvailableUpdates();
            InstallUpdatesCommand = new RelayCommand(InstallUpdates);
            RefreshAvailableUpdatesCommand = new RelayCommand(RefreshAvailableUpdates);
            CancelUpdateInstallationCommand = new RelayCommand(CancelUpdateInstallation);

            updateManager = new UpdateManager();
            updateManager.UpdateDownloadProgressChanged += UpdateDownloadProgressChanged;
            updateManager.UpdateDownloadCompleted += UpdateDownloadCompleted;
            updateManager.UpdateDownloadCanceled += UpdateDownloadCanceled;
            updateManager.UpdateInstallationProgressChanged += UpdateInstallationProgressChanged;
            updateManager.UpdateInstallationCompleted += UpdateInstallationCompleted;
            updateManager.UpdateInstallationCanceled += UpdateInstallationCanceled;
        }

        private void UpdateDownloadProgressChanged(object sender, UpdateDownloadProgressChangedEventArgs e)
        {
            UpdateProgress = e.Progress.PercentComplete;
        }

        private void UpdateDownloadCompleted(object sender, UpdateDownloadCompletedEventArgs e)
        {
            if (e.Job.IsCompleted)
            {
                // Reset the progress bar for installation process.
                UpdateProgress = 0;

                // Begin to install the updates
                UpdateText = "Installiere Updates...";
                updateManager.InstallUpdates(e.Job.Updates);
            }
        }
        
        private void UpdateDownloadCanceled(object sender, UpdateDownloadCanceledEventArgs e)
        {
            ShowUpdateDialog = false;
            RefreshAvailableUpdates(null);
        }

        private void UpdateInstallationProgressChanged(object sender, UpdateInstallationProgressChangedEventArgs e)
        {
            UpdateProgress = e.Progress.PercentComplete;
        }

        private void UpdateInstallationCompleted(object sender, UpdateInstallationCompletedEventArgs e)
        {
            ShowUpdateDialog = false;
            RefreshAvailableUpdates(null);
        }

        private void UpdateInstallationCanceled(object sender, UpdateInstallationCanceledEventArgs e)
        {
            ShowUpdateDialog = false;
            RefreshAvailableUpdates(null);
        }

        private async void LoadAvailableUpdates()
        {
            // Only start a new update scan process, if there is no other runnning at the moment.
            if (!IsLoadingUpdates)
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
        }

        public ICommand InstallUpdatesCommand { get; private set; }
        private void InstallUpdates(object sender)
        {
            var selectedUpdates = AvailableUpdates.Where(x => x.IsSelected).ToList();

            if (selectedUpdates.Count > 0)
            {
                // Show the update dialog, which contains visual feedback for the user
                // who wants to install new updates.
                ShowUpdateDialog = true;
                UpdateText = "Lade Updates herunter...";

                // Build a collection of updates for the update manager.
                UpdateCollection updates = new UpdateCollection();
                selectedUpdates.ForEach(x => updates.Add(x.Model.Update));

                // Start the update process by download the updates first.
                // If this is finished, the installation will automatically begin.
                updateManager.DownloadUpdates(updates);
            }
        }

        public ICommand CancelUpdateInstallationCommand { get; private set; }
        private void CancelUpdateInstallation(object sender)
        {
            updateManager.Cancel();
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

        private double updateProgress = 0.0;
        public double UpdateProgress
        {
            get => updateProgress;
            set => SetProperty(ref updateProgress, value);
        }

        private string updateText = string.Empty;
        public string UpdateText
        {
            get => updateText;
            set => SetProperty(ref updateText, value);
        }
    }
}
