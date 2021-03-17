using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace AquaMaintenancer.Theme.Components
{
    public class TextField : TextBox
    {
        static TextField()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextField), new FrameworkPropertyMetadata(typeof(TextField)));
        }

        #region Label Property
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register(nameof(Label), typeof(string),
                typeof(TextField), new PropertyMetadata(string.Empty));

        public string Label
        {
            get => GetValue(LabelProperty) as string;
            set => SetValue(LabelProperty, value);
        }
        #endregion

        #region Icon Property
        public static readonly DependencyProperty IconProperty = 
            DependencyProperty.Register("Icon", typeof(FontAwesomeIcon),
                typeof(TextField), new FrameworkPropertyMetadata());

        public FontAwesomeIcon Icon
        {
            get => (FontAwesomeIcon)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }
        #endregion

    }
}
