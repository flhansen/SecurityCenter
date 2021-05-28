using SecurityCenter.Business.Plugins;
using SecurityCenter.UILogic.Commands;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SecurityCenter.UILogic.ViewModels
{
    public class PluginPageViewModel : ViewModelBase
    {
        public static readonly string PluginDirectory = $"{Environment.CurrentDirectory}/Plugins";

        public PluginPageViewModel()
        {
            ScriptExecutionCommand = new RelayCommand(ScriptExecution);

            PluginManager = new PluginManager(PluginDirectory);
            Scripts = new ScriptPluginCollectionViewModel(PluginManager.Scripts);
        }

        public ICommand ScriptExecutionCommand { get; private set; }
        private void ScriptExecution(object obj)
        {
            ScriptPlugin plugin = obj as ScriptPlugin;
            plugin.Execute();
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
