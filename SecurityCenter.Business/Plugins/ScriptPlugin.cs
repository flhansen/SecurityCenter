using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCenter.Business.Plugins
{
    /// <summary>
    /// Class to handle script based plugins.
    /// </summary>
    public class ScriptPlugin
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="path">The path to the plugin</param>
        public ScriptPlugin(string path)
        {
            Path = path;
        }

        /// <summary>
        /// The path to the plugin.
        /// </summary>
        private string path;
        /// <summary>
        /// The path to the plugin.
        /// </summary>
        public string Path
        {
            get => path;
            private set => path = value;
        }

        /// <summary>
        /// Execute the plugin.
        /// </summary>
        public void Execute()
        {
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo
            {
                FileName = Path,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            process.Start();
            process.WaitForExit();
        }
    }
}
