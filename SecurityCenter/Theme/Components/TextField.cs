using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SecurityCenter.Theme.Components
{
    public class TextField : TextBox
    {
        static TextField()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextField), new FrameworkPropertyMetadata(typeof(TextField)));
        }

        #region Properties
        /// <summary>
        /// The label of the TextField. If <em>string.Empty</em>, it will not be displayed.
        /// </summary>
        public string Label
        {
            get => GetValue(LabelProperty) as string;
            set => SetValue(LabelProperty, value);
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register(nameof(Label), typeof(string),
                typeof(TextField), new PropertyMetadata(string.Empty));

        /// <summary>
        /// The icon inside the TextField. If <em>None</em>, it will not be displayed.
        /// </summary>
        public FontAwesomeIcon Icon
        {
            get => (FontAwesomeIcon)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty IconProperty = 
            DependencyProperty.Register("Icon", typeof(FontAwesomeIcon),
                typeof(TextField), new FrameworkPropertyMetadata());
        #endregion

    }
}
