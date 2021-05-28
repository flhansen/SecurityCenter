using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCenter.Business.Plugins
{
    public class PluginManager
    {
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
        /// The plugin executables.
        /// </summary>
        private List<string> plugins;
        /// <summary>
        /// The plugin executables.
        /// </summary>
        public List<string> Plugins
        {
            get => plugins;
            private set => plugins = value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pluginDirectory">The directory, in which the plugins are stored.</param>
        public PluginManager(string pluginDirectory)
        {
            string[] allowedExtensions = { ".ps1", ".sh" };

            PluginDirectory = pluginDirectory;
            Plugins = LoadPlugins(pluginDirectory, allowedExtensions).ToList();
        }

        /// <summary>
        /// Enumerates all plugin scripts, which have the specified extension.
        /// </summary>
        /// <param name="pluginDirectory">The directory, where the plugins are stored</param>
        /// <param name="allowedExtensions">The extensions, that plugins are allowed to have</param>
        /// <returns></returns>
        private IEnumerable<string> LoadPlugins(string pluginDirectory, string[] allowedExtensions)
        {
            // Get all the plugin files and filter them by the given extensions.
            IEnumerable<string> plugins = Directory.GetFiles(pluginDirectory)
                .Where(file => allowedExtensions.Contains(Path.GetExtension(file.ToLower())));
            return plugins;
        }
    }
}
