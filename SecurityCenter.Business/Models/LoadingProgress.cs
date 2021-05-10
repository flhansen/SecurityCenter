using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCenter.Business.Models
{
    public class LoadingProgress
    {
        public Dictionary<Action, string> Actions { get; set; } = new Dictionary<Action, string>();
    }
}
