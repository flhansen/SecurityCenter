using SecurityCenter.Theme.Components;
using SecurityCenter.UILogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SecurityCenter.Theme.Converter
{
    public class WindowsEventToBarChartDataConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            WindowsEventCollectionViewModel events = value as WindowsEventCollectionViewModel;
            BarChartData[] data = new BarChartData[7];

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = new BarChartData();
                data[i].Category = DateTime.Now.AddDays(-data.Length + i + 1).ToString("dd.MM.yyyy");
            }

            return data;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
