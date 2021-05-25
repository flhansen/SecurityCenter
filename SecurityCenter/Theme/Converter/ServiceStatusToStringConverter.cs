using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SecurityCenter.Theme.Converter
{
    public class ServiceStatusToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ServiceControllerStatus status = (ServiceControllerStatus)value;
            return status.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string statusText = (string)value;
            return (ServiceControllerStatus)Enum.Parse(typeof(ServiceControllerStatus), statusText);
        }
    }
}
