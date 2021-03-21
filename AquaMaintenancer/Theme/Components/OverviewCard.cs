using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AquaMaintenancer.Theme.Components
{
    public class OverviewCard : ContentControl
    {

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register(nameof(Label), typeof(string),
                typeof(OverviewCard), new PropertyMetadata(string.Empty));

        public string Information
        {
            get => (string)GetValue(InformationProperty);
            set => SetValue(InformationProperty, value);
        }

        public static readonly DependencyProperty InformationProperty =
            DependencyProperty.Register(nameof(Information), typeof(string),
                typeof(OverviewCard), new PropertyMetadata(string.Empty));

        public Brush IconColor
        {
            get => (Brush)GetValue(IconColorProperty);
            set => SetValue(IconColorProperty, value);
        }

        public static readonly DependencyProperty IconColorProperty =
            DependencyProperty.Register(nameof(IconColor), typeof(Brush),
                typeof(OverviewCard), new PropertyMetadata());

        public Brush TextColor
        {
            get => (Brush)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public static readonly DependencyProperty TextColorProperty =
            DependencyProperty.Register(nameof(TextColor), typeof(Brush),
                typeof(OverviewCard), new PropertyMetadata());

        public FontAwesomeIcon Icon
        {
            get => (FontAwesomeIcon)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(nameof(Icon), typeof(FontAwesomeIcon),
                typeof(OverviewCard), new PropertyMetadata(FontAwesomeIcon.None));

    }
}
