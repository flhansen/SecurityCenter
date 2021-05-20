using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SecurityCenter.Theme.Converter
{
    public class BoolToProtectionStatusStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool val = (bool)value;
            return val ? "Geschützt" : "Ungeschützt";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string val = (string)value;
            return val.Equals("Geschützt");
        }
    }
}
