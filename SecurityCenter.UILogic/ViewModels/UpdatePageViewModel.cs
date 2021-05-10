using AquaMaintenancer.Data.System;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityCenter.UILogic.ViewModels
{
    public class UpdatePageViewModel : ViewModelBase
    {
        public UpdatePageViewModel()
        {
            AvailableUpdates = new WindowsUpdateCollectionViewModel(SystemAccess.GetAvailableUpdates());
        }

        private WindowsUpdateCollectionViewModel availableUpdates;
        public WindowsUpdateCollectionViewModel AvailableUpdates
        {
            get => availableUpdates;
            set => SetProperty(ref availableUpdates, value);
        }
    }
}
