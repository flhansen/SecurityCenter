using AquaMaintenancer.Business.Models;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCenter.UILogic.ViewModels
{
    public class WindowsEventViewModel : ViewModel<WindowsEvent>
    {
        public WindowsEventViewModel(WindowsEvent model) : base(model)
        {

        }

        private string Shorten(string input, int length)
        {
            if (input == null)
                return "";

            if (input.Length <= length)
                return input;

            return input.Substring(0, length) + "...";
        }

        public DateTime Time
        {
            get => Model.TimeGenerated;
        }

        public byte Level
        {
            get => Model.Level;
        }

        public string TimeString
        {
            get => Model.TimeGenerated.ToShortDateString();
        }

        public string Message
        {
            get => Shorten(Model.Message, 20);
        }

        public string Source
        {
            get => Shorten(Model.Source, 20);
        }

        public string EntryType
        {
            get => Shorten(Model.EntryType.ToString(), 20);
        }
    }
}
