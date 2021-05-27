using SecurityCenter.Theme.Controls;
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
            BarChartData[] data = new BarChartData[7];
            WindowsEventCollectionViewModel events = value as WindowsEventCollectionViewModel;

            for (int i = 0; i < data.Length; i++)
            {
                DateTime targetDate = DateTime.Now.AddDays(-data.Length + i + 1);
                var relatedEvents = events.Where(vm => vm.Time.Day == targetDate.Day && vm.Time.Month == targetDate.Month && vm.Time.Year == targetDate.Year);
                var relatedCriticals = relatedEvents.Where(vm => vm.Level == 1);
                var relatedErrors = relatedEvents.Where(vm => vm.Level == 2);
                var relatedWarnings = relatedEvents.Where(vm => vm.Level == 3);

                data[i] = new BarChartData();
                data[i].Category = targetDate.ToString("dd.MM.yyyy");
                data[i].Values = new List<float>() { relatedCriticals.Count(), relatedErrors.Count(), relatedWarnings.Count() };
            }

            return data;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
