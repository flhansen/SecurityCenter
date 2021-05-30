using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            Name = GetScriptPropertyValue(path, "Name");
            Description = GetScriptPropertyValue(path, "Description");
            Author = GetScriptPropertyValue(path, "Author");
        }

        #region Events
        public event EventHandler<ScriptPluginOutputEventArgs> OnOutput;
        #endregion

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
        /// The name of the script.
        /// </summary>
        private string name;
        /// <summary>
        /// The name of the script.
        /// </summary>
        public string Name
        {
            get => name;
            private set => name = value;
        }

        /// <summary>
        /// The description of the script.
        /// </summary>
        private string description;
        /// <summary>
        /// The description of the script.
        /// </summary>
        public string Description
        {
            get => description;
            private set => description = value;
        }

        /// <summary>
        /// The author of the script.
        /// </summary>
        private string author;
        /// <summary>
        /// The author of the script.
        /// </summary>
        public string Author
        {
            get => author;
            private set => author = value;
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
                UseShellExecute = false,
                RedirectStandardOutput = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            OnOutput?.Invoke(this, new ScriptPluginOutputEventArgs { Output = output });
            process.WaitForExit();
        }

        /// <summary>
        /// Returns the lines of the meta block.
        /// </summary>
        /// <param name="scriptPath"></param>
        /// <returns></returns>
        private List<string> ReadScriptMetadata(string scriptPath)
        {
            List<string> lines = new List<string>();

            using (var file = new StreamReader(scriptPath))
            {
                string line;
                bool readingMetaBlock = false;
                int i = 0;

                while ((line = file.ReadLine()) != null)
                {
                    if (i == 0 && !line.Replace(" ", "").StartsWith("#---"))
                    {
                        break;
                    }
                    else if (readingMetaBlock && line.Replace(" ", "").StartsWith("#---"))
                    {
                        break;
                    }
                    else if (line.Replace(" ", "").StartsWith("#---"))
                    {
                        readingMetaBlock = true;
                        i++;
                        continue;
                    }

                    if (readingMetaBlock)
                    {
                        string formattedLine = RemovePreamble(line);
                        lines.Add(formattedLine);
                    }

                    i++;
                }
            }

            return lines;
        }

        /// <summary>
        /// Removes all leading characters of the string, which are no letters.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string RemovePreamble(string str)
        {
            string firstLetters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int startPos = str.IndexOfAny(firstLetters.ToCharArray());
            return str.Substring(startPos);
        }

        /// <summary>
        /// Removes all leading spaces of a string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string RemoveLeadingSpaces(string str)
        {
            string result = "";
            bool foundFirstNonSpace = false;

            foreach (char c in str)
            {
                if (!foundFirstNonSpace && c != ' ')
                    foundFirstNonSpace = true;

                if (foundFirstNonSpace)
                    result += c;
            }

            return result;
        }

        /// <summary>
        /// Returns the property value of the script metadata.
        /// </summary>
        /// <param name="scriptPath">The path to the script.</param>
        /// <returns></returns>
        private string GetScriptPropertyValue(string scriptPath, string property)
        {
            string result = string.Empty;

            List<string> lines = ReadScriptMetadata(scriptPath);
            string line = lines.Find(x => x.StartsWith(property));

            if (line != null)
            {
                string[] tokens = line.Split(':');

                if (tokens.Length == 2)
                    result = RemoveLeadingSpaces(tokens[1]);
            }

            return result;
        }
    }
}
