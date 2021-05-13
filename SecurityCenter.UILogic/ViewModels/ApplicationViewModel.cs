using SecurityCenter.Business.Models;
using SecurityCenter.Data.System;
using SecurityCenter.UILogic.Commands;
using SecurityCenter.UILogic.ViewModels.Core;
using System.Windows.Input;

namespace SecurityCenter.UILogic.ViewModels
{
    public class ApplicationViewModel : ViewModel<Application>
    {
        public ApplicationViewModel(Application model) : base(model)
        {
            UninstallApplicationCommand = new RelayCommand(UninstallApplication); 
        }

        public ICommand UninstallApplicationCommand { get; private set; }
        private void UninstallApplication(object obj)
        {
            SystemAccess.UninstallApplication(UninstallationPath);   
        }

        public string Name
        {
            get => Model.Name;
        }

        public string Manufacturer
        {
            get => Model.Publisher;
        }

        public string InstallationPath
        {
            get => Model.ExecutablePath;
        }

        public string UninstallationPath
        {
            get => Model.UninstallPath;
        }

        public string Version
        {
            get => Model.Version;
        }
    }
}
