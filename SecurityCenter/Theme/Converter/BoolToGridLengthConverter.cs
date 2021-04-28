using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SecurityCenter.Theme.Converter
{
    public class BoolToGridLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool fitContent = (bool)value;
            double size = 0;
            
            if (parameter != null && parameter is string)
                size = double.Parse((string)parameter);

            if (fitContent)
                return new GridLength(0, GridUnitType.Auto);
            else
                return new GridLength(size, GridUnitType.Star);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
