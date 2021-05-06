using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace SecurityCenter.Theme.Converter
{
    public class InvertedNullToVisibilityConverter : MarkupExtension, IValueConverter
    {
        private static InvertedNullToVisibilityConverter converter = null;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (converter == null)
            {
                converter = new InvertedNullToVisibilityConverter();
            }

            return converter;
        }
    }
}
