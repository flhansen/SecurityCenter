using AquaMaintenancer.Business.Models;
using Microsoft.VisualBasic.Devices;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AquaMaintenancer.Data.System
{
    public static class SystemAccess
    {
        public static ApplicationCollection GetApplications()
        {
            ApplicationCollection applications = new ApplicationCollection();

            string uninstallKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(uninstallKey))
            {
                string[] applicationEntryNames = key.GetSubKeyNames();
                foreach (string applicationEntryName in applicationEntryNames)
                {
                    RegistryKey applicationKey = key.OpenSubKey(applicationEntryName);

                    try
                    {
                        Application app = new Application();
                        app.Name = removeSpecialChars(applicationKey.GetValue("DisplayName") as string);
                        app.Version = removeSpecialChars(applicationKey.GetValue("DisplayVersion") as string);
                        app.Publisher = removeSpecialChars(applicationKey.GetValue("Publisher") as string);
                        app.ExecutablePath = applicationKey.GetValue("DisplayIcon") as string;
                        app.UninstallPath = applicationKey.GetValue("UninstallString") as string;

                        if (app.Name.Length > 0)
                        {
                            applications.Add(app);
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }

            return applications;
        }

        static private string removeSpecialChars(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9._]", string.Empty);
        }

        public static ServiceCollection GetServices()
        {
            ServiceCollection services = new ServiceCollection();

            foreach (ServiceController serviceController in ServiceController.GetServices())
            {
                Service service = new Service
                {
                    Controller = serviceController,
                    DisplayName = serviceController.DisplayName,
                    ServiceName = serviceController.ServiceName,
                    StartMode = serviceController.StartType,
                    Status = serviceController.Status
                };

                services.Add(service);
            }

            return services;
        }

        public static FirewallInformation GetFirewallInformation()
        {
            FirewallInformation firewallInfo = new FirewallInformation();

            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(CultureInfo.CurrentCulture.TextInfo.OEMCodePage);
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/C netsh advfirewall show allprofiles";
            p.Start();

            StreamReader sr = p.StandardOutput;

            bool[] firewallState = new bool[3];
            int firewallProfile = 0;
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();

                if (line.Contains("Domänenprofil"))
                {
                    firewallProfile = 0;
                }
                else if (line.Contains("Privates Profil-Einstellungen"))
                {
                    firewallProfile = 1;
                }
                else if (line.Contains("Öffentliches Profil-Einstellungen"))
                {
                    firewallProfile = 2;
                }
                else if (line.Contains("Status"))
                {
                    string state = Regex.Replace(line, @"\s+", "").Replace("Status", "");
                    firewallState[firewallProfile] = state.Equals("EIN");
                }
            }

            firewallInfo.IsDomainActivated = firewallState[0];
            firewallInfo.IsPrivateActivated = firewallState[1];
            firewallInfo.IsPublicActivated = firewallState[2];

            return firewallInfo;
        }

        public static AntiVirusCollection GetAntiViruses()
        {
            AntiVirusCollection antiViruses = new AntiVirusCollection();

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"root\SecurityCenter2", "SELECT * FROM AntiVirusProduct");
                ManagementObjectCollection coll = searcher.Get();

                foreach (ManagementObject antivir in coll)
                {
                    AntiVirus av = new AntiVirus
                    {
                        DisplayName = antivir["displayName"] as string,
                        InstanceGuid = antivir["instanceGuid"] as string,
                        PathToSignedProductExe = antivir["pathToSignedProductExe"] as string,
                        PathToSignedReportingExe = antivir["pathToSignedReportingExe"] as string,
                        ProductState = (uint)antivir["productState"],
                        Timestamp = antivir["timestamp"] as string
                    };

                    antiViruses.Add(av);
                }
            }
            catch (ManagementException)
            {
                Logger.Instance.Error("Der Status der Antiviren-Software konnte nicht ermittelt werden, da keine Berechtigung dafür vorhanden ist");
            }

            return antiViruses;
        }

        public static WindowsEventCollection GetEvents(DateTime until)
        {
            string[] logNames = { "Security", "Application", "System" };
            WindowsEventCollection records = new WindowsEventCollection();

            string formattedDateTime = string.Format("{0}-{1}-{2}T{3}:{4}:{5}.000000000Z",
            until.Year,
            until.Month.ToString("D2"),
            until.Day.ToString("D2"),
            until.Hour.ToString("D2"),
            until.Minute.ToString("D2"),
            until.Second.ToString("D2"));

            string query = $"*[System[TimeCreated[@SystemTime >= '{formattedDateTime}'] and (Level=1 or Level=2 or Level=3)]]";

            foreach (string logName in logNames)
            {
                var queryResult = new EventLogQuery(logName, PathType.LogName, query);
                var reader = new EventLogReader(queryResult);

                for (var e = reader.ReadEvent(); e != null; e = reader.ReadEvent())
                {
                    WindowsEvent windowsEvent = new WindowsEvent
                    {
                        TimeGenerated = (DateTime)e.TimeCreated,
                        EntryType = e.LevelDisplayName,
                        Message = e.FormatDescription(),
                        Level = (byte)e.Level
                    };

                    records.Add(windowsEvent);
                }
            }

            return records;
        }

        public static SystemInformation GetSystemInformation()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            ManagementObject osInfoObject = searcher.Get().Cast<ManagementObject>().FirstOrDefault();

            ComputerInfo computerInfo = new ComputerInfo();

            SystemInformation info = new SystemInformation
            {
                ComputerName = Environment.MachineName,
                UserName = Environment.UserName,
                OperatingSystem = osInfoObject.GetPropertyValue("Caption") as string,
                Architecture = Environment.Is64BitOperatingSystem ? "64 Bit" : "32 Bit",
                Manufacturer = osInfoObject.GetPropertyValue("Manufacturer") as string,
                SystemType = "Unbekannt",
                BytesRam = computerInfo.TotalPhysicalMemory

            };

            return info;
        }
    }
}
