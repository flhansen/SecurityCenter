using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace AquaMaintenancer.Theme.Components
{
    public class ProgressBar : ContentControl
    {
        private static Border indicator;
        static ProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressBar), new FrameworkPropertyMetadata(typeof(ProgressBar)));
        }
        public float Value 
        { 
            get => (float)GetValue(ValueProperty);
            set => SetValue(ValueProperty, Value);
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value), typeof(float), typeof(ProgressBar), 
            new PropertyMetadata(float.NaN, HandlePropertyChanged));
        
        private static void HandlePropertyChanged(object d, DependencyPropertyChangedEventArgs e)
        {
            indicator.Width = (double)e.NewValue;
            indicator.InvalidateVisual();
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
