using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SecurityCenter.Theme.Components
{
    public class PrimaryButton : Button
    {
        /// <summary>
        /// The icon inside the button. If <em>None</em>, it will not be displayed.
        /// </summary>
        public FontAwesomeIcon Icon
        {
            get => (FontAwesomeIcon)GetValue(IconProperty);
            set => SetValue(IconProperty, value); 
        }

        public static readonly DependencyProperty IconProperty = 
            DependencyProperty.Register(nameof(Icon), typeof(FontAwesomeIcon),
                typeof(PrimaryButton), new PropertyMetadata());

        /// <summary>
        /// Describes how much the background color will be darkened on mouse over.
        /// The value must be between -1.0 and 1.0.
        /// </summary>
        public double BrightnessValue
        {
            get => (double)GetValue(BrightnessValueProperty);
            set => SetValue(BrightnessValueProperty, value); 
        }

        public static readonly DependencyProperty BrightnessValueProperty = 
            DependencyProperty.Register(nameof(BrightnessValue), typeof(double),
                typeof(PrimaryButton), new PropertyMetadata(-1.0));
    }
}
