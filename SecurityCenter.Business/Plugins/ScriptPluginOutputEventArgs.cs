using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCenter.Business.Plugins
{
    public class ScriptPluginOutputEventArgs : EventArgs
    {
        public string Output { get; set; }
    }
}
