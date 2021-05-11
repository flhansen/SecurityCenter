using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SecurityCenter.Theme.Components
{
    public class Dialog : ContentControl
    {
        static Dialog()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Dialog), new FrameworkPropertyMetadata(typeof(Dialog)));
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string),
                typeof(Dialog), new PropertyMetadata(string.Empty));

        public double DialogWidth
        {
            get => (double)GetValue(DialogWidthProperty);
            set => SetValue(DialogWidthProperty, value);
        }

        public static readonly DependencyProperty DialogWidthProperty =
            DependencyProperty.Register(nameof(DialogWidth), typeof(double),
                typeof(Dialog), new PropertyMetadata(double.NaN));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var dialog = GetTemplateChild("Dialog") as Grid;
            dialog.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            dialog.Arrange(new Rect(dialog.DesiredSize));
            DialogWidth = dialog.DesiredSize.Width;
        }
    }
}
