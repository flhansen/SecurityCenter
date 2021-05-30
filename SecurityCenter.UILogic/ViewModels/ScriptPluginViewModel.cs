using SecurityCenter.Business.Plugins;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SecurityCenter.UILogic.ViewModels
{
    /// <summary>
    /// The ViewModel for script plugins.
    /// </summary>
    public class ScriptPluginViewModel : ViewModel<ScriptPlugin>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model">The script plugin model.</param>
        public ScriptPluginViewModel(ScriptPlugin model) : base(model)
        {
        }

        /// <summary>
        /// The name.
        /// </summary>
        public string Name
        {
            get => Model.Name;
        }

        /// <summary>
        /// The description.
        /// </summary>
        public string Description
        {
            get => Model.Description;
        }

        /// <summary>
        /// The author.
        /// </summary>
        public string Author
        {
            get => Model.Author;
        }
    }
}
