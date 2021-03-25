using AquaMaintenancer.Theme.Components;
using AquaMaintenancer.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace AquaMaintenancer.Theme.Converter
{
    public class BrushBrightnessConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(parameter is PrimaryButton))
                return null;

            PrimaryButton button = parameter as PrimaryButton;
            double perc = button.BrightnessValue;

            SolidColorBrush brush = value as SolidColorBrush;
            HSLColor hsl = ColorUtils.RgbToHls(brush.Color);

            if (hsl.L >= perc)
                hsl.L += perc;

            return new SolidColorBrush(ColorUtils.HlsToRgb(hsl));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
