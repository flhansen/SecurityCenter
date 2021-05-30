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
    /// <summary>
    /// The ViewModel of the plugin page.
    /// </summary>
    public class PluginPageViewModel : ViewModelBase
    {
        /// <summary>
        /// Directory of plugins.
        /// </summary>
        public static readonly string PluginDirectory = $"{Environment.CurrentDirectory}/Plugins";

        /// <summary>
        /// Constructor
        /// </summary>
        public PluginPageViewModel()
        {
            // Command initialization
            ScriptExecutionCommand = new RelayCommand(ScriptExecution);
            RefreshScriptsCommand = new RelayCommand(RefreshScripts);

            // Initialize the plugin manager and set the script list.
            PluginManager = new PluginManager(PluginDirectory);
            Scripts = new ScriptPluginCollectionViewModel(PluginManager.Scripts);
            FilteredScripts = Scripts.AsEnumerable();
        }

        /// <summary>
        /// The command to execute script plugins.
        /// </summary>
        public ICommand ScriptExecutionCommand { get; private set; }
        private void ScriptExecution(object obj)
        {
            ScriptPlugin plugin = obj as ScriptPlugin;
            plugin.Execute();
        }

        /// <summary>
        /// The command to refresh scripts.
        /// </summary>
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

        /// <summary>
        /// The plugin manager.
        /// </summary>
        public PluginManager PluginManager
        {
            get => pluginManager;
            private set => pluginManager = value;
        }

        private ScriptPluginCollectionViewModel scripts;

        /// <summary>
        /// The script ViewModels.
        /// </summary>
        public ScriptPluginCollectionViewModel Scripts
        {
            get => scripts;
            set => SetProperty(ref scripts, value);
        }

        private IEnumerable<ScriptPluginViewModel> filteredScripts;


        /// <summary>
        /// The scripts ViewModels filtered by FilteredText.
        /// </summary>
        public IEnumerable<ScriptPluginViewModel> FilteredScripts
        {
            get => filteredScripts;
            set => SetProperty(ref filteredScripts, value);
        }

        private bool isRefreshingScripts;

        /// <summary>
        /// If the ViewModel is refreshing scripts right now.
        /// </summary>
        public bool IsRefreshingScripts
        {
            get => isRefreshingScripts;
            set => SetProperty(ref isRefreshingScripts, value);
        }

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
