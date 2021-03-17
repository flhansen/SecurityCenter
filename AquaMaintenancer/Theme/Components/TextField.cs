using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace AquaMaintenancer.Theme.Components
{
    public class TextField : TextBox
    {
        public string Label
        {
            get => GetValue(LabelProperty) as string;
            set => SetValue(LabelProperty, value);
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register(nameof(Label), typeof(string),
                typeof(TextField), new PropertyMetadata(string.Empty));
    }
}
