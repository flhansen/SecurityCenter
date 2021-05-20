using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WUApiLib;

namespace SecurityCenter.Business.Updates
{
    public class UpdateDownloadProgressChangedEventArgs : EventArgs
    {
        public IDownloadProgress Progress { get; set; }
    }
}
