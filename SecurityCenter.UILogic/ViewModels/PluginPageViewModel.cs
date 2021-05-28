using SecurityCenter.Business.Plugins;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCenter.UILogic.ViewModels
{
    public class PluginPageViewModel : ViewModelBase
    {
        public static readonly string PluginDirectory = $"{Environment.CurrentDirectory}/Plugins";

        public PluginPageViewModel()
        {
            PluginManager = new PluginManager(PluginDirectory);
            Scripts = new ScriptPluginCollectionViewModel(PluginManager.Scripts);
        }

        private PluginManager pluginManager;
        public PluginManager PluginManager
        {
            get => pluginManager;
            private set => pluginManager = value;
        }

        private ScriptPluginCollectionViewModel scripts;
        public ScriptPluginCollectionViewModel Scripts
        {
            get => scripts;
            set => SetProperty(ref scripts, value);
        }
    }
}
