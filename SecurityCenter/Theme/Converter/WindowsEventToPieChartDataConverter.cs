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
    public class WindowsEventToPieChartDataConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            WindowsEventCollectionViewModel events = value as WindowsEventCollectionViewModel;

            DateTime targetDate = DateTime.Now.AddDays(-6);
            var relatedEvents = events.Where(vm => vm.Time.Day == targetDate.Day && vm.Time.Month == targetDate.Month && vm.Time.Year == targetDate.Year);
            var relatedCriticals = relatedEvents.Where(vm => vm.Level == 1);
            var relatedErrors = relatedEvents.Where(vm => vm.Level == 2);
            var relatedWarnings = relatedEvents.Where(vm => vm.Level == 3);

            var dict = new Dictionary<string, double>();
            dict.Add("Kritisch", relatedCriticals.Count());
            dict.Add("Fehler", relatedErrors.Count());
            dict.Add("Warnungen", relatedWarnings.Count());
            return dict;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
