using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace AquaMaintenancer.Theme.CustomProperties
{
    public class ContainerSpacing
    {
        public static readonly DependencyProperty SpacingProperty =
            DependencyProperty.RegisterAttached("Spacing", typeof(double),
                typeof(ContainerSpacing), new UIPropertyMetadata(new double(), SpacingChangedCallback));

        public static void SpacingChangedCallback(object sender, DependencyPropertyChangedEventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel == null) return;
            panel.Loaded += new RoutedEventHandler(PanelLoadedHandler);
        }

        static void PanelLoadedHandler(object sender, RoutedEventArgs e)
        {
            Panel panel = sender as Panel;

            for (int i = 0; i < panel.Children.Count - 1; i++)
            {
                var child = panel.Children[i];
                FrameworkElement fe = child as FrameworkElement;
                if (fe == null) continue;

                double spacing = GetSpacing(panel);
                Thickness margin = new Thickness(0, 0, 0, spacing);
                fe.Margin = margin;
            }
        }

        public static double GetSpacing(DependencyObject obj)
        {
            return (double)obj.GetValue(SpacingProperty);
        }

        public static void SetSpacing(DependencyObject obj, double value)
        {
            obj.SetValue(SpacingProperty, value);
        }
    }
}
