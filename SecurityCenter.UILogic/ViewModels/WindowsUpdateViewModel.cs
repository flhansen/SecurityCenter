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
    /// The ViewModel class of updates.
    /// </summary>
    public class WindowsUpdateViewModel : ViewModel<WindowsUpdate>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model">The update model</param>
        public WindowsUpdateViewModel(WindowsUpdate model) : base(model)
        {
        }

        /// <summary>
        /// If the update is selected.
        /// </summary>
        private bool isSelected;
        /// <summary>
        /// If the update is selected.
        /// </summary>
        public bool IsSelected
        {
            get => isSelected;
            set => SetProperty(ref isSelected, value);
        }

        /// <summary>
        /// The name of the update.
        /// </summary>
        public string Name
        {
            get => Model.Name;
        }

        /// <summary>
        /// The description of the update.
        /// </summary>
        public string Description
        {
            get => Model.Description;
        }

        /// <summary>
        /// The KB number of the update.
        /// </summary>
        public string KbNumber
        {
            get => Model.KbNumber;
        }
    }
}
