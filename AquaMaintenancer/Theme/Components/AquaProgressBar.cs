using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace AquaMaintenancer.Theme.Components
{
    public class AquaProgressBar : ContentControl
    {
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
            if (indicator != null)
            {
                indicator.Width = (double)e.NewValue;
                indicator.InvalidateVisual();
            }
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            indicator = GetTemplateChild("Indicator") as Border;

            if (indicator == null)
            {
                throw new NullReferenceException("Templatepart not found");
            }
        }
    }
}
