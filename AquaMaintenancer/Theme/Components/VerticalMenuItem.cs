using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace AquaMaintenancer.Theme.Components
{
    public class VerticalMenuItem : Button
    {
        /// <summary>
        /// The title of the menu item.
        /// </summary>
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string),
                typeof(VerticalMenuItem), new PropertyMetadata(string.Empty));

        /// <summary>
        /// The icon of the menu item.
        /// </summary>
        public FontAwesomeIcon Icon
        {
            get => (FontAwesomeIcon)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(nameof(Icon), typeof(FontAwesomeIcon),
                typeof(VerticalMenuItem), new PropertyMetadata());

        /// <summary>
        /// Whether the menu item is selected or not.
        /// </summary>
        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register(nameof(IsSelected), typeof(bool),
                typeof(VerticalMenuItem), new PropertyMetadata(false));
    }
}
