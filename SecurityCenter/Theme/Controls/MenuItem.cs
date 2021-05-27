using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SecurityCenter.Theme.Controls
{
    public class MenuItem : ContentControl
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
                typeof(MenuItem), new PropertyMetadata());

    }
}
