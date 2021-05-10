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
    public class WindowsUpdateCollectionViewModel : ViewModelCollectionBase<WindowsUpdateViewModel, WindowsUpdate>
    {
        public WindowsUpdateCollectionViewModel() : base()
        {

        }

        public WindowsUpdateCollectionViewModel(ObservableCollection<WindowsUpdate> updates) : base(updates)
        {

        }
    }
}
