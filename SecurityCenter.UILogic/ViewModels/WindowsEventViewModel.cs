using SecurityCenter.Business.Models;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCenter.UILogic.ViewModels
{
    /// <summary>
    /// The ViewModel class for windows events.
    /// </summary>
    public class WindowsEventViewModel : ViewModel<WindowsEvent>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model">The windows event model</param>
        public WindowsEventViewModel(WindowsEvent model) : base(model)
        {
        }

        /// <summary>
        /// Helper function to shorten a string to a constant length.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private string Shorten(string input, int length)
        {
            if (input == null)
                return "";

            if (input.Length <= length)
                return input;

            return input.Substring(0, length) + "...";
        }

        /// <summary>
        /// The time of generation of the windows event.
        /// </summary>
        public DateTime Time
        {
            get => Model.TimeGenerated;
        }

        /// <summary>
        /// The level of the windows event.
        /// </summary>
        public byte Level
        {
            get => Model.Level;
        }

        /// <summary>
        /// The generation time of the windows event (formatted).
        /// </summary>
        public string TimeString
        {
            get => Model.TimeGenerated.ToShortDateString();
        }

        /// <summary>
        /// The short message of the windows event.
        /// </summary>
        public string MessageShort
        {
            get => Shorten(Model.Message, 20);
        }
        
        /// <summary>
        /// The message of the windows event.
        /// </summary>
        public string Message
        {
            get => Model.Message;
        }

        /// <summary>
        /// The short name of the source of the windows event.
        /// </summary>
        public string SourceShort
        {
            get => Shorten(Model.Source, 20);
        }

        /// <summary>
        /// The source of the windows event.
        /// </summary>
        public string Source
        {
            get => Model.Source;
        }

        /// <summary>
        /// The entry type of the windows event.
        /// </summary>
        public string EntryType
        {
            get => Shorten(Model.EntryType.ToString(), 20);
        }
    }
}
