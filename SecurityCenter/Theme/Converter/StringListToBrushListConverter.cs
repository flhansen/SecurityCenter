using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace SecurityCenter.Theme.Converter
{
    public class StringListToBrushListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<string> input = value as List<string>;
            List<Brush> result = new List<Brush>();

            foreach (string color in input)
            {
                result.Add(new BrushConverter().ConvertFrom(color) as Brush);
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
