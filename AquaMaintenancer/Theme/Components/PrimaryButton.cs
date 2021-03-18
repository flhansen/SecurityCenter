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
        #region Icon Property
        public static DependencyProperty IconProperty = 
            DependencyProperty.Register(nameof(Icon), typeof(FontAwesomeIcon),
                typeof(PrimaryButton), new PropertyMetadata());

        public FontAwesomeIcon Icon
        {
            get => (FontAwesomeIcon)GetValue(IconProperty);
            set => SetValue(IconProperty, value); 
        }
        #endregion
    }
}
