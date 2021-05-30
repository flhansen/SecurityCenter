using SecurityCenter.Business.Plugins;
using SecurityCenter.UILogic.Commands;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
            RefreshScriptsCommand = new RelayCommand(RefreshScripts);

            PluginManager = new PluginManager(PluginDirectory);
            Scripts = new ScriptPluginCollectionViewModel(PluginManager.Scripts);
            FilteredScripts = Scripts.AsEnumerable();
        }

        public ICommand ScriptExecutionCommand { get; private set; }
        private void ScriptExecution(object obj)
        {
            ScriptPlugin plugin = obj as ScriptPlugin;
            plugin.Execute();
        }

        public ICommand RefreshScriptsCommand { get; private set; }
        private void RefreshScripts(object obj)
        {
            if (!IsRefreshingScripts)
            {
                IsRefreshingScripts = true;

                Task.Run(() =>
                {
                    PluginManager.LoadPlugins();
                    Scripts = new ScriptPluginCollectionViewModel(PluginManager.Scripts);
                    FilteredScripts = Scripts.AsEnumerable();
                    FilterText = "";
                    IsRefreshingScripts = false;
                });
            }
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

        private IEnumerable<ScriptPluginViewModel> filteredScripts;
        public IEnumerable<ScriptPluginViewModel> FilteredScripts
        {
            get => filteredScripts;
            set => SetProperty(ref filteredScripts, value);
        }

        private bool isRefreshingScripts;
        public bool IsRefreshingScripts
        {
            get => isRefreshingScripts;
            set => SetProperty(ref isRefreshingScripts, value);
        }

        /// <summary>
        /// String, which is used to filter the application collection.
        /// </summary>
        private string filterText;
        /// <summary>
        /// String, which is used to filter the application collection.
        /// </summary>
        public string FilterText
        {
            get => filterText;
            set
            {
                SetProperty(ref filterText, value);
                FilteredScripts = Scripts.Where(a => Regex.IsMatch(a.Name.ToLowerInvariant(), filterText.ToLowerInvariant()));
            }
        }
    }
}
