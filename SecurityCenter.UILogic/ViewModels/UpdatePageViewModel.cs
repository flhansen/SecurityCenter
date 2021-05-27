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
    /// <summary>
    /// The ViewModel class of the update page.
    /// </summary>
    public class UpdatePageViewModel : ViewModelBase
    {
        private UpdateManager updateManager;

        /// <summary>
        /// Constructor
        /// </summary>
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

        /// <summary>
        /// Callback function to handle update download events.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateDownloadProgressChanged(object sender, UpdateDownloadProgressChangedEventArgs e)
        {
            UpdateProgress = e.Progress.PercentComplete;
        }

        /// <summary>
        /// Callback function to handle update download completion events.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        
        /// <summary>
        /// Callback function to handle update download cancel events.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateDownloadCanceled(object sender, UpdateDownloadCanceledEventArgs e)
        {
            ShowUpdateDialog = false;
            RefreshAvailableUpdates(null);
        }

        /// <summary>
        /// Callback function to handle update installation events.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateInstallationProgressChanged(object sender, UpdateInstallationProgressChangedEventArgs e)
        {
            UpdateProgress = e.Progress.PercentComplete;
        }

        /// <summary>
        /// Callback function to handle update installlation completion events.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateInstallationCompleted(object sender, UpdateInstallationCompletedEventArgs e)
        {
            ShowUpdateDialog = false;
            RefreshAvailableUpdates(null);
        }

        /// <summary>
        /// Callback function to handle update installation cancel events.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateInstallationCanceled(object sender, UpdateInstallationCanceledEventArgs e)
        {
            ShowUpdateDialog = false;
            RefreshAvailableUpdates(null);
        }

        /// <summary>
        /// Loads available updates asynchronously.
        /// </summary>
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

        /// <summary>
        /// The command to install selected updates.
        /// </summary>
        public ICommand InstallUpdatesCommand { get; private set; }
        /// <summary>
        /// The method to install selected updates.
        /// </summary>
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

        /// <summary>
        /// The command to cancel update installation.
        /// </summary>
        public ICommand CancelUpdateInstallationCommand { get; private set; }
        /// <summary>
        /// The method to cancel update installation.
        /// </summary>
        private void CancelUpdateInstallation(object sender)
        {
            updateManager.Cancel();
        }

        /// <summary>
        /// The command to refresh available updates.
        /// </summary>
        public ICommand RefreshAvailableUpdatesCommand { get; private set; }
        /// <summary>
        /// The method to refresh available updates.
        /// </summary>
        private void RefreshAvailableUpdates(object sender)
        {
            LoadAvailableUpdates();
        }

        /// <summary>
        /// If the update dialog should be visible.
        /// </summary>
        private bool showUpdateDialog;
        /// <summary>
        /// If the update dialog should be visible.
        /// </summary>
        public bool ShowUpdateDialog
        {
            get => showUpdateDialog;
            set => SetProperty(ref showUpdateDialog, value);
        }

        /// <summary>
        /// If the update manager processes updates.
        /// </summary>
        private bool isLoadingUpdates;
        /// <summary>
        /// If the update manager processes updates.
        /// </summary>
        public bool IsLoadingUpdates
        {
            get => isLoadingUpdates;
            set => SetProperty(ref isLoadingUpdates, value);
        }

        /// <summary>
        /// The available updates.
        /// </summary>
        private WindowsUpdateCollectionViewModel availableUpdates;
        /// <summary>
        /// The available updates.
        /// </summary>
        public WindowsUpdateCollectionViewModel AvailableUpdates
        {
            get => availableUpdates;
            set => SetProperty(ref availableUpdates, value);
        }

        /// <summary>
        /// The update installation progress.
        /// </summary>
        private double updateProgress = 0.0;
        /// <summary>
        /// The update installation progress.
        /// </summary>
        public double UpdateProgress
        {
            get => updateProgress;
            set => SetProperty(ref updateProgress, value);
        }

        /// <summary>
        /// The current update process information text.
        /// </summary>
        private string updateText = string.Empty;
        /// <summary>
        /// The current update process information text.
        /// </summary>
        public string UpdateText
        {
            get => updateText;
            set => SetProperty(ref updateText, value);
        }
    }
}
