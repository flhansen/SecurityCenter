using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WUApiLib;

namespace SecurityCenter.Business.Updates
{
    public class UpdateManager
    {
        #region Events
        public event EventHandler<UpdateDownloadProgressChangedEventArgs> UpdateDownloadProgressChanged;
        public event EventHandler<UpdateDownloadCompletedEventArgs> UpdateDownloadCompleted;
        public event EventHandler<UpdateDownloadCanceledEventArgs> UpdateDownloadCanceled;
        public event EventHandler<UpdateInstallationProgressChangedEventArgs> UpdateInstallationProgressChanged;
        public event EventHandler<UpdateInstallationCompletedEventArgs> UpdateInstallationCompleted;
        public event EventHandler<UpdateInstallationCanceledEventArgs> UpdateInstallationCanceled;
        #endregion

        #region Members
        private object currentJob;
        #endregion

        #region Properties
        public IUpdateDownloader Downloader { get; private set; }
        public IUpdateInstaller Installer { get; private set; }
        #endregion

        #region Methods
        public void Cancel()
        {
            if (currentJob is IDownloadJob)
                ((IDownloadJob)currentJob).RequestAbort();
            else if (currentJob is IInstallationJob)
                ((IInstallationJob)currentJob).RequestAbort();
        }

        /// <summary>
        /// Starts the download of updates using the Windows Update API.
        /// </summary>
        /// <param name="updates"></param>
        /// <returns></returns>
        public IDownloadJob DownloadUpdates(UpdateCollection updates)
        {
            var progressChangedCallback = new UpdateDownloadProgressChangedCallback(this);
            var completedCallback = new UpdateDownloadCompletedCallback(this);

            UpdateSession session = new UpdateSession();
            Downloader = session.CreateUpdateDownloader();
            Downloader.Updates = updates;
            IDownloadJob job = Downloader.BeginDownload(progressChangedCallback, completedCallback, null);
            currentJob = job;
            return job;
        }

        /// <summary>
        /// Installs updates using the Windows Update API.
        /// </summary>
        /// <param name="updates"></param>
        /// <returns></returns>
        public IInstallationJob InstallUpdates(UpdateCollection updates)
        {
            var progressChangedCallback = new UpdateInstallationProgressChangedCallback(this);
            var completedCallback = new UpdateInstallationCompletedCallback(this);

            UpdateSession session = new UpdateSession();
            Installer = session.CreateUpdateInstaller();
            Installer.Updates = updates;
            IInstallationJob job = Installer.BeginInstall(progressChangedCallback, completedCallback, null);
            currentJob = job;
            return job;
        }
        #endregion

        #region Update manager callbacks
        private abstract class UpdateManagerCallback
        {
            public UpdateManager UpdateManager
            {
                get;
                set;
            }

            public UpdateManagerCallback(UpdateManager updateManager)
            {
                UpdateManager = updateManager;
            }
        }

        private class UpdateDownloadProgressChangedCallback : UpdateManagerCallback, IDownloadProgressChangedCallback
        {
            public UpdateDownloadProgressChangedCallback(UpdateManager updateManager) : base(updateManager)
            {
            }

            public void Invoke(IDownloadJob downloadJob, IDownloadProgressChangedCallbackArgs callbackArgs)
            {
                UpdateManager.UpdateDownloadProgressChanged?.Invoke(this, new UpdateDownloadProgressChangedEventArgs
                {
                    Progress = callbackArgs.Progress
                });
            }
        }

        private class UpdateDownloadCompletedCallback : UpdateManagerCallback, IDownloadCompletedCallback
        {
            public UpdateDownloadCompletedCallback(UpdateManager updateManager) : base(updateManager)
            {
            }

            public void Invoke(IDownloadJob downloadJob, IDownloadCompletedCallbackArgs callbackArgs)
            {
                IDownloadResult result = UpdateManager.Downloader.EndDownload(downloadJob);

                if (result.ResultCode == OperationResultCode.orcSucceeded)
                {
                    UpdateManager.UpdateDownloadCompleted?.Invoke(this, new UpdateDownloadCompletedEventArgs
                    {
                        Job = downloadJob
                    });
                }
                else
                {
                    UpdateManager.UpdateDownloadCanceled?.Invoke(this, new UpdateDownloadCanceledEventArgs
                    {
                        Job = downloadJob
                    });
                }
            }
        }

        private class UpdateInstallationProgressChangedCallback : UpdateManagerCallback, IInstallationProgressChangedCallback
        {
            public UpdateInstallationProgressChangedCallback(UpdateManager updateManager) : base(updateManager)
            {
            }

            public void Invoke(IInstallationJob installationJob, IInstallationProgressChangedCallbackArgs callbackArgs)
            {
                UpdateManager.UpdateInstallationProgressChanged?.Invoke(this, new UpdateInstallationProgressChangedEventArgs
                {
                    Progress = callbackArgs.Progress
                });
            }
        }

        private class UpdateInstallationCompletedCallback : UpdateManagerCallback, IInstallationCompletedCallback
        {
            public UpdateInstallationCompletedCallback(UpdateManager updateManager) : base(updateManager)
            {
            }

            public void Invoke(IInstallationJob installationJob, IInstallationCompletedCallbackArgs callbackArgs)
            {
                IInstallationResult result = UpdateManager.Installer.EndInstall(installationJob);

                if (result.ResultCode == OperationResultCode.orcSucceeded)
                {
                    UpdateManager.UpdateInstallationCompleted?.Invoke(this, new UpdateInstallationCompletedEventArgs());
                }
                else
                {
                    UpdateManager.UpdateInstallationCanceled?.Invoke(this, new UpdateInstallationCanceledEventArgs
                    {
                        Job = installationJob
                    });
                }
            }
        }
        #endregion
    }
}
