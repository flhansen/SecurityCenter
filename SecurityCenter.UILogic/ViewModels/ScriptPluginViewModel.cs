using SecurityCenter.Business.Plugins;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCenter.UILogic.ViewModels
{
    public class ScriptPluginViewModel : ViewModel<ScriptPlugin>
    {
        public ScriptPluginViewModel(ScriptPlugin model) : base(model)
        {
        }

        public string Name
        {
            get => Model.Name;
        }

        public string Description
        {
            get => Model.Description;
        }

        public string Author
        {
            get => Model.Author;
        }
    }
}
