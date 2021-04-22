using AquaMaintenancer.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace AquaMaintenancer.Theme.Converter
{
    public class PageViewModelToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Type targetViewModelType = parameter as Type;
            ViewModelBase currentViewModel = value as ViewModelBase;

            return currentViewModel.GetType().Equals(targetViewModelType);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
