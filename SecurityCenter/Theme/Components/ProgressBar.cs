using System;
using System.Collections.Generic;
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
            pgb.ChangeProgressValue((double)e.NewValue);
        }
        /// <summary>
        /// The displayed text above the progressbar.
        /// </summary>
        public string LoadingInfo
        {
            get => (string)GetValue(LoadingInfoProperty);
            set => SetValue(LoadingInfoProperty, value);
        }

        public static readonly DependencyProperty LoadingInfoProperty = DependencyProperty.Register(
            nameof(LoadingInfo), typeof(string), typeof(ProgressBar),
            new PropertyMetadata(string.Empty));
        
        /// <summary>
        /// Converts the given value to the ui-value
        /// </summary>
        /// <param name="value"></param>
        private void ChangeProgressValue(double value)
        {
            if (indicator != null || path != null)
            {
                indicator.Width = value * (path.ActualWidth / 100);
                indicator.InvalidateVisual();
            }
        }

        /// <summary>
        /// Gets the ui-elements and stored them.
        /// </summary>
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
