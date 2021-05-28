using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCenter.Business.Plugins
{
    public class PluginManager
    {
        /// <summary>
        /// Extensions of plugin files, that are allowed.
        /// </summary>
        public static readonly string[] AllowedExtensions = { ".ps1", ".sh" };

        /// <summary>
        /// The directory, in which the plugins are stored.
        /// </summary>
        private string pluginDirectory;
        /// <summary>
        /// The directory, in which the plugins are stored.
        /// </summary>
        public string PluginDirectory
        {
            get => pluginDirectory;
            private set => pluginDirectory = value;
        }

        /// <summary>
        /// The plugin scripts.
        /// </summary>
        private ScriptPluginCollection scripts = new ScriptPluginCollection();
        /// <summary>
        /// The plugin scripts.
        /// </summary>
        public ScriptPluginCollection Scripts
        {
            get => scripts;
            set => scripts = value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pluginDirectory">The directory, in which the plugins are stored.</param>
        public PluginManager(string pluginDirectory)
        {
            PluginDirectory = pluginDirectory;

            if (!Directory.Exists(pluginDirectory))
            {
                try
                {
                    Directory.CreateDirectory(pluginDirectory);
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e.Message);
                    Trace.WriteLine(e.StackTrace);
                }
            }

            LoadPlugins();
        }

        /// <summary>
        /// Enumerates all plugin scripts, which have the specified extension.
        /// </summary>
        /// <param name="pluginDirectory">The directory, where the plugins are stored</param>
        /// <param name="allowedExtensions">The extensions, that plugins are allowed to have</param>
        public void LoadPlugins()
        {
            // Reset the script list.
            Scripts.Clear();

            // Get all the plugin files and filter them by the given extensions.
            IEnumerable<string> plugins = Directory.GetFiles(pluginDirectory)
                .Where(file => AllowedExtensions.Contains(Path.GetExtension(file.ToLower())));
            
            // Add all script plugins to list.
            foreach (string plugin in plugins)
                Scripts.Add(new ScriptPlugin(plugin));
        }
    }
}
