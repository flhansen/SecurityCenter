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
    /// The collection class for windows event viewmodels.
    /// </summary>
    public class WindowsEventCollectionViewModel : ViewModelCollectionBase<WindowsEventViewModel, WindowsEvent>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public WindowsEventCollectionViewModel() : base(new List<WindowsEventViewModel>())
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="windowsEvents">The collection of windows event models</param>
        public WindowsEventCollectionViewModel(ObservableCollection<WindowsEvent> windowsEvents) : base(windowsEvents)
        {
        }
    }
}
