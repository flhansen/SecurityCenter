using AquaMaintenancer.Business.Models;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCenter.UILogic.ViewModels
{
    public class SystemInformationViewModel : ViewModel<SystemInformation>
    {
        public SystemInformationViewModel(SystemInformation model) : base(model)
        {
        }

        public string ComputerName
        {
            get => Model.ComputerName;
            set => Model.ComputerName = value;
        }

        public string UserName
        {
            get => Model.UserName;
            set => Model.UserName = value;
        }

        public string OperatingSystem
        {
            get => Model.OperatingSystem;
            set => Model.OperatingSystem = value;
        }

        public string Manufacturer
        {
            get => Model.Manufacturer;
            set => Model.Manufacturer = value;
        }

        public string Architecture
        {
            get => Model.Architecture;
            set => Model.Architecture = value;
        }

        public string Ram
        {
            get => Math.Round(Model.BytesRam * 0.0000000009313226).ToString() + " GB";
        }
    }
}
