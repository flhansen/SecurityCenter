using SecurityCenter.Business.Models;
using SecurityCenter.UILogic.Commands;
using SecurityCenter.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Text;
using System.Windows.Input;

namespace SecurityCenter.UILogic.ViewModels
{
    public class ApplicationViewModel : ViewModel<Application>
    {
        public ApplicationViewModel(Application model) : base(model)
        {
            UninstallApplicationCommand = new RelayCommand(UninstallApplication); 
        }

        public ICommand UninstallApplicationCommand { get; private set; }
        private void UninstallApplication(object obj)
        {
            Console.WriteLine(UninstallationPath);
            ProcessStartInfo startInfo = new ProcessStartInfo();

            int indexOfExe = UninstallationPath.IndexOf(".exe");
            if (indexOfExe > 0 && indexOfExe < 9)
            {

                string uninstallString = UninstallationPath.Replace(@"""", string.Empty);
                Console.WriteLine("indexOfExe: " + indexOfExe);
                Console.WriteLine("uninstallerString: " + uninstallString);
                string uninstallerPath = uninstallString.Substring(0, indexOfExe + 4);
                Console.WriteLine("uninstallerPath: " + uninstallerPath);
                startInfo.FileName = uninstallerPath;

                if ( uninstallerPath.Length != uninstallString.Length)
                {
                    string args = uninstallString.Substring(uninstallerPath.Length);
                    if (!string.IsNullOrEmpty(args))
                    {
                        startInfo.UseShellExecute = false;
                        startInfo.Arguments = args;
                    }
                }

                Process.Start(startInfo).WaitForExit();
            }
            else
            {
                Console.WriteLine("hansi");
                Process.Start(UninstallationPath).WaitForExit();
            }
        }

        public string Name
        {
            get => Model.Name;
        }

        public string Manufacturer
        {
            get => Model.Publisher;
        }

        public string InstallationPath
        {
            get => Model.ExecutablePath;
        }

        public string UninstallationPath
        {
            get => Model.UninstallPath;
        }

        public string Version
        {
            get => Model.Version;
        }
    }
}
