using SecurityCenter.Business.Plugins;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCenter.UILogic.ViewModels
{
    public class ScriptPluginCollectionViewModel : ViewModelCollectionBase<ScriptPluginViewModel, ScriptPlugin>
    {
        public ScriptPluginCollectionViewModel(ObservableCollection<ScriptPlugin> models) : base(models)
        {
        }
    }
}
