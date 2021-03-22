using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace AquaMaintenancer.Theme.Components
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

        public static DependencyProperty IconProperty = 
            DependencyProperty.Register(nameof(Icon), typeof(FontAwesomeIcon),
                typeof(PrimaryButton), new PropertyMetadata());
    }
}
