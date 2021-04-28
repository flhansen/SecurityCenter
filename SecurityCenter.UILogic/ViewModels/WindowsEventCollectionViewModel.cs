using AquaMaintenancer.Business.Models;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCenter.UILogic.ViewModels
{
    public class WindowsEventCollectionViewModel : ViewModelCollectionBase<WindowsEventViewModel, WindowsEvent>
    {
        public WindowsEventCollectionViewModel(ObservableCollection<WindowsEvent> windowsEvents) : base(windowsEvents)
        {

        }
    }
}
