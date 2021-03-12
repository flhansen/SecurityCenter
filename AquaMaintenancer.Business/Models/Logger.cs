using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AquaMaintenancer.Business.Models
{
    public enum LogLevel
    {
        Information,
        Warning,
        Error,
        Fatal
    }

    public class Logger : INotifyPropertyChanged
    {
        public static string LogFileName { get; } = @".\logfile.log";

        #region Singleton implementation
        private static Logger instance;
        private string lastMessage;

        private Logger() { }

        public static Logger Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Logger();
                }

                return instance;
            }
        }
        #endregion

        public string LastMessage
        {
            get
            {
                return lastMessage;
            }

            private set
            {
                lastMessage = value;
                NotifyPropertyChanged();
            }
        }

        public void Log(LogLevel level, string message)
        {
            string levelName = level.ToString();

            using (StreamWriter sw = File.AppendText(LogFileName))
            {
                sw.WriteLine(string.Format("[{0:dd/MM/yyyy HH:mm:ss}] ({1}) {2}", DateTime.Now, levelName, message));
            }

            LastMessage = message;
        }

        public void Information(string message)
        {
            Log(LogLevel.Information, message);
        }

        public void Error(string message)
        {
            Log(LogLevel.Error, message);
        }

        #region NotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
