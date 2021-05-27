using SecurityCenter.Business.Models;
using SecurityCenter.Data.System;
using SecurityCenter.UILogic.Commands;
using SecurityCenter.UILogic.ViewModels.Core;
using System.Windows.Input;

namespace SecurityCenter.UILogic.ViewModels
{
    /// <summary>
    /// ViewModel class for applcation models.
    /// </summary>
    public class ApplicationViewModel : ViewModel<Application>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model">The application model</param>
        public ApplicationViewModel(Application model) : base(model)
        {
        }

        /// <summary>
        /// The name of the application.
        /// </summary>
        public string Name
        {
            get => Model.Name;
        }

        /// <summary>
        /// The manufacturer of the application.
        /// </summary>
        public string Manufacturer
        {
            get => Model.Publisher;
        }

        /// <summary>
        /// The installation path of the application.
        /// </summary>
        public string InstallationPath
        {
            get => Model.ExecutablePath;
        }

        /// <summary>
        /// The uninstallation path of the application.
        /// </summary>
        public string UninstallationPath
        {
            get => Model.UninstallPath;
        }

        /// <summary>
        /// The version string of the application.
        /// </summary>
        public string Version
        {
            get => Model.Version;
        }
    }
}
