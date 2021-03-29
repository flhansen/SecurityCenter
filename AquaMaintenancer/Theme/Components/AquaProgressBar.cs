using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace AquaMaintenancer.Theme.Components
{
    public class AquaProgressBar : ContentControl
    {
        private static Border path;
        private static Border indicator;
        static AquaProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AquaProgressBar), new FrameworkPropertyMetadata(typeof(AquaProgressBar)));
        }
        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value), typeof(double), typeof(AquaProgressBar),
            new PropertyMetadata(double.NaN, HandlePropertyChanged));

        private static void HandlePropertyChanged(object d, DependencyPropertyChangedEventArgs e)
        {
            if (indicator != null || path != null)
            {
                indicator.Width = (double)e.NewValue * (path.ActualWidth / 100);
                indicator.InvalidateVisual();
            }
        }

        public string LoadingInfo
        {
            get => (string)GetValue(LoadingInfoProperty);
            set => SetValue(LoadingInfoProperty, value);
        }

        public static readonly DependencyProperty LoadingInfoProperty = DependencyProperty.Register(
            nameof(LoadingInfo), typeof(string), typeof(AquaProgressBar),
            new PropertyMetadata(string.Empty));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            indicator = GetTemplateChild("Indicator") as Border;
            path = GetTemplateChild("Path") as Border;

            if (indicator == null || path == null)
            {
                throw new NullReferenceException("Templatepart not found");
            }
        }
    }
}
