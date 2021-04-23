using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SecurityCenter.Theme.Components
{
    public class OverviewCard : ContentControl
    {

        /// <summary>
        /// The label, which is below the information value.
        /// </summary>
        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register(nameof(Label), typeof(string),
                typeof(OverviewCard), new PropertyMetadata(string.Empty));

        /// <summary>
        /// The information value to be presented.
        /// </summary>
        public string Information
        {
            get => (string)GetValue(InformationProperty);
            set => SetValue(InformationProperty, value);
        }

        public static readonly DependencyProperty InformationProperty =
            DependencyProperty.Register(nameof(Information), typeof(string),
                typeof(OverviewCard), new PropertyMetadata(string.Empty));

        /// <summary>
        /// The color of the icon.
        /// </summary>
        public Brush IconColor
        {
            get => (Brush)GetValue(IconColorProperty);
            set => SetValue(IconColorProperty, value);
        }

        public static readonly DependencyProperty IconColorProperty =
            DependencyProperty.Register(nameof(IconColor), typeof(Brush),
                typeof(OverviewCard), new PropertyMetadata());

        /// <summary>
        /// The text color of the information value and label.
        /// </summary>
        public Brush TextColor
        {
            get => (Brush)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public static readonly DependencyProperty TextColorProperty =
            DependencyProperty.Register(nameof(TextColor), typeof(Brush),
                typeof(OverviewCard), new PropertyMetadata());

        /// <summary>
        /// The icon, which describes the information type.
        /// </summary>
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
