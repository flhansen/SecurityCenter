using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SecurityCenter.Theme.Components
{
    public class CheckBox : System.Windows.Controls.CheckBox
    {
        public static DependencyProperty CheckSignColorProperty = DependencyProperty.Register(
            nameof(CheckSignColor), typeof(SolidColorBrush), typeof(CheckBox), new PropertyMetadata(Brushes.White));


        public SolidColorBrush CheckSignColor
        {
            get => (SolidColorBrush)GetValue(CheckSignColorProperty);
            set => SetValue(CheckSignColorProperty, value);
        }

        public static DependencyProperty CheckBoxLabelProperty = DependencyProperty.Register(
            nameof(CheckBoxLabel), typeof(string), typeof(CheckBox), new PropertyMetadata(string.Empty));

        public string CheckBoxLabel
        {
            get => (string)GetValue(CheckBoxLabelProperty);
            set => SetValue(CheckBoxLabelProperty, value);
        }
    }
}
