using SecurityCenter.Business.Models;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCenter.UILogic.ViewModels
{
    /// <summary>
    /// The ViewModel class for system informations.
    /// </summary>
    public class SystemInformationViewModel : ViewModel<SystemInformation>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model">The system information model</param>
        public SystemInformationViewModel(SystemInformation model) : base(model)
        {
        }

        /// <summary>
        /// The computer name of the system.
        /// </summary>
        public string ComputerName
        {
            get => Model.ComputerName;
            set => Model.ComputerName = value;
        }

        /// <summary>
        /// The user name of the system.
        /// </summary>
        public string UserName
        {
            get => Model.UserName;
            set => Model.UserName = value;
        }

        /// <summary>
        /// The operating system of the system.
        /// </summary>
        public string OperatingSystem
        {
            get => Model.OperatingSystem;
            set => Model.OperatingSystem = value;
        }

        /// <summary>
        /// The manufacturer of the system.
        /// </summary>
        public string Manufacturer
        {
            get => Model.Manufacturer;
            set => Model.Manufacturer = value;
        }

        /// <summary>
        /// The architecture of the system.
        /// </summary>
        public string Architecture
        {
            get => Model.Architecture;
            set => Model.Architecture = value;
        }

        /// <summary>
        /// The amount of available RAM of the system.
        /// </summary>
        public string Ram
        {
            get => Math.Round(Model.BytesRam * 0.0000000009313226).ToString() + " GB";
        }
    }
}
