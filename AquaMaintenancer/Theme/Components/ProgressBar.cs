using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace AquaMaintenancer.Theme.Components
{
    public class ProgressBar : ContentControl
    {
        private Border path;
        private Border indicator;
        static ProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressBar), new FrameworkPropertyMetadata(typeof(ProgressBar)));
        }
        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value), typeof(double), typeof(ProgressBar),
            new PropertyMetadata(double.NaN, HandlePropertyChanged));

        private static void HandlePropertyChanged(object d, DependencyPropertyChangedEventArgs e)
        {
            ProgressBar pgb = d as ProgressBar;
            pgb.ChangeProgressValue((double)e.NewValue);
        }

        public string LoadingInfo
        {
            get => (string)GetValue(LoadingInfoProperty);
            set => SetValue(LoadingInfoProperty, value);
        }

        public static readonly DependencyProperty LoadingInfoProperty = DependencyProperty.Register(
            nameof(LoadingInfo), typeof(string), typeof(ProgressBar),
            new PropertyMetadata(string.Empty));

        private void ChangeProgressValue(double value)
        {
            if (indicator != null || path != null)
            {
                indicator.Width = value * (path.ActualWidth / 100);
                indicator.InvalidateVisual();
            }
        }
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
