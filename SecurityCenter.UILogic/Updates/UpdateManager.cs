using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WUApiLib;

namespace SecurityCenter.UILogic.Updates
{
    public class UpdateManager
    {
        #region Events
        public event EventHandler<UpdateDownloadProgressChangedEventArgs> UpdateDownloadProgressChanged;
        public event EventHandler<UpdateDownloadCompletedEventArgs> UpdateDownloadCompleted;
        public event EventHandler<UpdateInstallationProgressChangedEventArgs> UpdateInstallationProgressChanged;
        public event EventHandler<UpdateInstallationCompletedEventArgs> UpdateInstallationCompleted;
        #endregion

        #region Methods
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
            UpdateDownloader downloader = session.CreateUpdateDownloader();
            downloader.Updates = updates;
            return downloader.BeginDownload(progressChangedCallback, completedCallback, null);
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
            IUpdateInstaller installer = session.CreateUpdateInstaller();
            installer.Updates = updates;
            return installer.BeginInstall(progressChangedCallback, completedCallback, null);
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
                UpdateManager.UpdateDownloadProgressChanged?.Invoke(this, new UpdateDownloadProgressChangedEventArgs());
            }
        }

        private class UpdateDownloadCompletedCallback : UpdateManagerCallback, IDownloadCompletedCallback
        {
            public UpdateDownloadCompletedCallback(UpdateManager updateManager) : base(updateManager)
            {
            }

            public void Invoke(IDownloadJob downloadJob, IDownloadCompletedCallbackArgs callbackArgs)
            {
                UpdateManager.UpdateDownloadCompleted?.Invoke(this, new UpdateDownloadCompletedEventArgs());
            }
        }

        private class UpdateInstallationProgressChangedCallback : UpdateManagerCallback, IInstallationProgressChangedCallback
        {
            public UpdateInstallationProgressChangedCallback(UpdateManager updateManager) : base(updateManager)
            {
            }

            public void Invoke(IInstallationJob installationJob, IInstallationProgressChangedCallbackArgs callbackArgs)
            {
                UpdateManager.UpdateInstallationProgressChanged?.Invoke(this, new UpdateInstallationProgressChangedEventArgs());
            }
        }

        private class UpdateInstallationCompletedCallback : UpdateManagerCallback, IInstallationCompletedCallback
        {
            public UpdateInstallationCompletedCallback(UpdateManager updateManager) : base(updateManager)
            {
            }

            public void Invoke(IInstallationJob installationJob, IInstallationCompletedCallbackArgs callbackArgs)
            {
                UpdateManager.UpdateInstallationCompleted?.Invoke(this, new UpdateInstallationCompletedEventArgs());
            }
        }
        #endregion
    }
}
