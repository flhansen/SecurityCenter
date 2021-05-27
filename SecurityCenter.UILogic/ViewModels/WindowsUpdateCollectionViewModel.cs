using SecurityCenter.Business.Models;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCenter.UILogic.ViewModels
{
    /// <summary>
    /// The collection class for windows update viewmodels.
    /// </summary>
    public class WindowsUpdateCollectionViewModel : ViewModelCollectionBase<WindowsUpdateViewModel, WindowsUpdate>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public WindowsUpdateCollectionViewModel() : base()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="updates">The collection of update models</param>
        public WindowsUpdateCollectionViewModel(ObservableCollection<WindowsUpdate> updates) : base(updates)
        {

        }
    }
}
