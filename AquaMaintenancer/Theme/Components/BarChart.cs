using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace AquaMaintenancer.Theme.Components
{
    public class ChartData
    {
        public object Category { get; set; }
        public object Value { get; set; }
    }

    public class BarChart : Control
    {
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register(nameof(Data), typeof(IEnumerable<ChartData>),
                typeof(BarChart), new PropertyMetadata());

        public IEnumerable<ChartData> Data
        {
            get => (IEnumerable<ChartData>) GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }
    }
}
