using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SecurityCenter.Theme.Components
{
    public class ProgressBar : ContentControl
    {
        private Border path;
        private Border indicator;

        static ProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressBar), new FrameworkPropertyMetadata(typeof(ProgressBar)));
        }

        public ProgressBar()
        {
            Loaded += OnLoad;
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            indicator = GetTemplateChild("Indicator") as Border;
            path = GetTemplateChild("Path") as Border;
            UpdateIndicator();
        }

        /// <summary>
        /// The fillvalue of the progressbar. Value between 0.0 and 100.0
        /// </summary>
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
            pgb.UpdateIndicator();
        }
        /// <summary>
        /// The displayed text above the progressbar.
        /// </summary>
        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
            nameof(Label), typeof(string), typeof(ProgressBar),
            new PropertyMetadata(string.Empty));
        
        /// <summary>
        /// Converts the given value to the ui-value
        /// </summary>
        /// <param name="value"></param>
        private void UpdateIndicator()
        {
            if (indicator != null && path != null)
            {
                indicator.Width = Value * (path.ActualWidth / 100);
                indicator.InvalidateVisual();
            }
        }
    }
}
