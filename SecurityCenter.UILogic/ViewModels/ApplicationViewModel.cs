using SecurityCenter.Business.Models;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityCenter.UILogic.ViewModels
{
    public class ApplicationViewModel : ViewModel<Application>
    {
        public ApplicationViewModel(Application model) : base(model)
        {

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

        public string Version
        {
            get => Model.Version;
        }
    }
}
