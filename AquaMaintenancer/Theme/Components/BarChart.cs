using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq;

namespace AquaMaintenancer.Theme.Components
{
    public class BarChartData
    {
        public IEnumerable<float> Values { get; set; }
        public string Category { get; set; }
    }

    public class BarChartBounds
    {
        public double XAxisWidth { get; private set; }
        public double XAxisHeight { get; private set; }
        public double YAxisWidth { get; private set; }
        public double YAxisHeight { get; private set; }
        public double LegendWidth { get; private set; }

        private BarChart chart;

        public BarChartBounds(BarChart chart)
        {
            this.chart = chart;

            XAxisHeight = CalculateCategoryLabelsHeight();
            YAxisWidth = CalculateValueLabelsWidth();
            LegendWidth = CalculateLegendWidth();
            XAxisWidth = CalculateCategoryLabelsWidth(YAxisWidth, LegendWidth);
            YAxisHeight = CalculateValueLabelsHeight(XAxisHeight);
        }

        private double CalculateLegendWidth()
        {
            if (chart.SubCategoryLegendLabels.Count > 0)
            {
                double labelsWidth = chart.SubCategoryLegendLabels.Max(x => x.Width);
                return labelsWidth + 2 * chart.LegendEllipsesRadius + chart.LegendEllipsesSpacing;
            }
            else
            {
                return 0;
            }
        }

        private double CalculateCategoryLabelsWidth(double valueLabelsWidth, double legendWidth)
        {
            if (chart.CategoryLabels.Count > 0)
                return chart.ActualWidth - valueLabelsWidth - chart.CategoryLabels.Last().Width - 2 * chart.InnerSpacing - legendWidth - chart.LegendSpacing;
            else
                return 0;
        }

        private double CalculateCategoryLabelsHeight()
        {
            if (chart.CategoryLabels.Count > 0)
                return chart.CategoryLabels.Max(label => label.Height) + chart.LabelSpacing;
            else
                return 0;
        }

        private double CalculateValueLabelsWidth()
        {
            if (chart.ValueLabels.Count > 0)
                return chart.ValueLabels.Max(label => label.Width) + chart.LabelSpacing;
            else
                return 0;
        }

        private double CalculateValueLabelsHeight(double categoryLabelsHeight)
        {
            if (chart.ValueLabels.Count > 0)
                return chart.ActualHeight - categoryLabelsHeight - chart.ValueLabels.Last().Height / 2;
            else
                return 0;
        }

    }

    public class BarChart : Canvas
    {
        public Pen PenLabels { get; set; } = new Pen();
        public Pen PenAxis { get; set; } = new Pen();
        public Pen PenGrid { get; set; } = new Pen();
        public Pen PenLegendLabels { get; set; } = new Pen();
        public List<Brush> Colors { get; set; } = new List<Brush>();
        public BarChartBounds Bounds { get; private set; }

        #region Framework Properties
        private static void HandleChartPropertyChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // Reinitialize the properties of the chart
            BarChart chart = sender as BarChart;
            chart.ValueLabels = chart.GetValueLabels(5);
            chart.CategoryLabels = chart.GetCategoryLabels();
            chart.SubCategoryLegendLabels = chart.GetSubCategoryLabels(chart.SubCategories);

            // Force the chart to redraw everything
            chart.InvalidateVisual();
        }

        /// <summary>
        /// The data to be plotted.
        /// </summary>
        public IEnumerable<BarChartData> Data
        {
            get => (IEnumerable<BarChartData>)GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register(nameof(Data), typeof(IEnumerable<BarChartData>),
                typeof(BarChart), new PropertyMetadata(new List<BarChartData>(), HandleChartPropertyChanged));

        /// <summary>
        /// The font size of value labels (y-axis).
        /// </summary>
        public double ValueLabelSize
        {
            get => (double)GetValue(ValueLabelSizeProperty);
            set => SetValue(ValueLabelSizeProperty, value);
        }

        public static readonly DependencyProperty ValueLabelSizeProperty =
            DependencyProperty.Register(nameof(ValueLabelSize), typeof(double),
                typeof(BarChart), new PropertyMetadata(12.0, HandleChartPropertyChanged));

        /// <summary>
        /// The font size of category labels (x-axis).
        /// </summary>
        public double CategoryLabelSize
        {
            get => (double)GetValue(CategoryLabelSizeProperty);
            set => SetValue(CategoryLabelSizeProperty, value);
        }

        public static readonly DependencyProperty CategoryLabelSizeProperty =
            DependencyProperty.Register(nameof(CategoryLabelSize), typeof(double),
                typeof(BarChart), new PropertyMetadata(12.0, HandleChartPropertyChanged));

        /// <summary>
        /// The radius of colored ellipses inside the legend.
        /// </summary>
        public double LegendEllipsesRadius
        { 
            get => (double)GetValue(LegendEllipsesRadiusProperty);
            set => SetValue(LegendEllipsesRadiusProperty, value);
        }

        public static readonly DependencyProperty LegendEllipsesRadiusProperty =
            DependencyProperty.Register(nameof(LegendEllipsesRadius), typeof(double),
                typeof(BarChart), new PropertyMetadata(2.5, HandleChartPropertyChanged));

        /// <summary>
        /// The spacing between legend labels and colored ellipses.
        /// </summary>
        public double LegendEllipsesSpacing
        {
            get => (double)GetValue(LegendEllipsesSpacingProperty);
            set => SetValue(LegendEllipsesSpacingProperty, value);
        }

        public static readonly DependencyProperty LegendEllipsesSpacingProperty =
            DependencyProperty.Register(nameof(LegendEllipsesSpacing), typeof(double),
                typeof(BarChart), new PropertyMetadata(8.0, HandleChartPropertyChanged));

        /// <summary>
        /// The vertical spacing between every label of the legend.
        /// </summary>
        public double LegendLabelSpacing
        {
            get => (double)GetValue(LegendLabelSpacingProperty);
            set => SetValue(LegendLabelSpacingProperty, value);
        }

        public static readonly DependencyProperty LegendLabelSpacingProperty =
            DependencyProperty.Register(nameof(LegendLabelSpacing), typeof(double),
                typeof(BarChart), new PropertyMetadata(12.0, HandleChartPropertyChanged));

        /// <summary>
        /// The spacing between the legend and the graph.
        /// </summary>
        public double LegendSpacing
        {
            get => (double)GetValue(LegendSpacingProperty);
            set => SetValue(LegendSpacingProperty, value);
        }

        public static readonly DependencyProperty LegendSpacingProperty =
            DependencyProperty.Register(nameof(LegendSpacing), typeof(double),
                typeof(BarChart), new PropertyMetadata(64.0, HandleChartPropertyChanged));

        /// <summary>
        /// The font size of labels inside the legend.
        /// </summary>
        public double LegendLabelSize
        {
            get => (double)GetValue(LegendLabelSizeProperty);
            set => SetValue(LegendLabelSizeProperty, value);
        }

        public static readonly DependencyProperty LegendLabelSizeProperty =
            DependencyProperty.Register(nameof(LegendLabelSize), typeof(double),
                typeof(BarChart), new PropertyMetadata(12.0, HandleChartPropertyChanged));

        /// <summary>
        /// The spacing between the axes and its labels.
        /// </summary>
        public double LabelSpacing
        {
            get => (double)GetValue(LabelSpacingProperty);
            set => SetValue(LabelSpacingProperty, value);
        }

        public static readonly DependencyProperty LabelSpacingProperty =
            DependencyProperty.Register(nameof(LabelSpacing), typeof(double),
                typeof(BarChart), new PropertyMetadata(16.0, HandleChartPropertyChanged));

        /// <summary>
        /// The spacing between related bars.
        /// </summary>
        public double BarSpacing
        {
            get => (double)GetValue(BarSpacingProperty);
            set => SetValue(BarSpacingProperty, value);
        }

        public static readonly DependencyProperty BarSpacingProperty =
            DependencyProperty.Register(nameof(BarSpacing), typeof(double),
                typeof(BarChart), new PropertyMetadata(5.0, HandleChartPropertyChanged));

        /// <summary>
        /// The spacing inside the graph.
        /// </summary>
        public double InnerSpacing
        {
            get => (double)GetValue(InsideSpacingProperty);
            set => SetValue(InsideSpacingProperty, value);
        }

        public static readonly DependencyProperty InsideSpacingProperty =
            DependencyProperty.Register(nameof(InnerSpacing), typeof(double),
                typeof(BarChart), new PropertyMetadata(15.0, HandleChartPropertyChanged));

        /// <summary>
        /// The width of every bar inside the chart.
        /// </summary>
        public double BarWidth
        {
            get => (double)GetValue(BarWidthProperty);
            set => SetValue(BarWidthProperty, value);
        }

        public static readonly DependencyProperty BarWidthProperty =
            DependencyProperty.Register(nameof(BarWidth), typeof(double),
                typeof(BarChart), new PropertyMetadata(20.0, HandleChartPropertyChanged));
        #endregion

        public List<FormattedText> ValueLabels { get; private set; }
        public List<FormattedText> CategoryLabels { get; private set; }
        public List<FormattedText> SubCategoryLegendLabels { get; private set; }
        public List<string> SubCategories { get; set; } = new List<string>()
        {
            "Information",
            "Warnung",
            "Fehler"
        };

        public BarChart()
        {
            BrushConverter bc = new BrushConverter();

            // Define the colors used to fill the bars
            Colors.Add(bc.ConvertFrom("#5D4ADA") as SolidColorBrush);
            Colors.Add(bc.ConvertFrom("#66CA67") as SolidColorBrush);
            Colors.Add(bc.ConvertFrom("#2E9BFF") as SolidColorBrush);

            // Define the colors used to draw the chart
            PenGrid.Brush = bc.ConvertFrom("#FFFFFF") as SolidColorBrush;
            PenGrid.Brush.Opacity = 0.05;
            PenAxis.Brush = bc.ConvertFrom("#FFFFFF") as SolidColorBrush;
            PenAxis.Brush.Opacity = 1.0;
            PenLabels.Brush = bc.ConvertFrom("#FFFFFF") as SolidColorBrush;
            PenLabels.Brush.Opacity = 0.3;
            PenLegendLabels.Brush = bc.ConvertFrom("#FFFFFF") as SolidColorBrush;
            PenLegendLabels.Brush.Opacity = 1.0;

            // Generate the actual labels used for rendering later to
            // remove caluclation overhead
            ValueLabels = GetValueLabels(5);
            CategoryLabels = GetCategoryLabels();
            SubCategoryLegendLabels = GetSubCategoryLabels(SubCategories);
        }

        protected override void OnRender(DrawingContext ctx)
        {
            Bounds = new BarChartBounds(this);

            DrawGridLines(ctx);
            DrawYAxis(ctx);
            DrawXAxis(ctx);
            DrawLegend(ctx);
        }

        private double Remap(double value, double min1, double max1, double min2, double max2)
        {
            return (value - min1) * (max2 - min2) / (max1 - min1) + min2;
        }

        /// <summary>
        /// Transforms a real value into a canvas value, which can be
        /// used to represent the value inside the chart.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private double GetCanvasValue(double value)
        {
            double categoryLabelsHeight = Bounds.XAxisHeight;

            double min1 = 0.0;
            double max1 = ValueLabels.Max(label => double.Parse(label.Text));
            double min2 = ActualHeight - categoryLabelsHeight;
            double max2 = ValueLabels.Last().Height / 2.0;

            return Remap(value, min1, max1, min2, max2);
        }

        /// <summary>
        /// Converts a list of chart data into a list of FormattedTexts,
        /// which can directly be used for rendering.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private List<FormattedText> GetValueLabels(int count)
        {
            float max = 0.0f;

            if (Data.Count() > 0)
                max = Data.Max(d => d.Values.Max());

            List<FormattedText> labels = new List<FormattedText>();

            for (int i = 0; i < count; i++)
            {
                float yValue = max - i * max / (count - 1);

                FormattedText label = new FormattedText(
                    yValue.ToString(),
                    CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    new Typeface("Roboto"),
                    ValueLabelSize,
                    PenLabels.Brush,
                    1.25
                );

                labels.Add(label);
            }

            return labels;
        }

        /// <summary>
        /// Converts a list of chart data into a list of FormattedTexts,
        /// which can directly be used for rendering.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private List<FormattedText> GetCategoryLabels()
        {
            return Data.Select(d =>
                new FormattedText(
                    d.Category,
                    CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    new Typeface("Roboto"),
                    CategoryLabelSize,
                    PenLabels.Brush,
                    1.25
                )
            ).ToList();
        }

        /// <summary>
        /// Converts a list of strings to a list of FormattedTexts,
        /// which can directly be used for rendering.
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        private List<FormattedText> GetSubCategoryLabels(IEnumerable<string> names)
        {
            return names.Select(name =>
                new FormattedText(
                    name,
                    CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    new Typeface("Roboto"),
                    LegendLabelSize,
                    PenLegendLabels.Brush,
                    1.25
                )
            ).ToList();
        }

        /// <summary>
        /// Draws some supporting lines for better readability of the chart.
        /// </summary>
        /// <param name="ctx"></param>
        private void DrawGridLines(DrawingContext ctx)
        {
            for (int i = 0; i < ValueLabels.Count - 1; i++)
            {
                FormattedText label = ValueLabels[i];
                double y = GetCanvasValue(double.Parse(label.Text));

                ctx.DrawLine(
                    PenGrid,
                    new System.Windows.Point(Bounds.YAxisWidth, y),
                    new System.Windows.Point(ActualWidth - Bounds.LegendWidth - LegendSpacing, y)
                );
            }
        }

        /// <summary>
        /// Draws the y-axis of the chart.
        /// </summary>
        /// <param name="ctx"></param>
        private void DrawYAxis(DrawingContext ctx)
        {
            double maxLabelWidth = ValueLabels.Max(l => l.Width);
            double steps = Bounds.YAxisHeight / (ValueLabels.Count - 1);

            // Draw the y-axis
            ctx.DrawLine(
                PenAxis,
                new System.Windows.Point(Bounds.YAxisWidth, 0),
                new System.Windows.Point(Bounds.YAxisWidth, ActualHeight - Bounds.XAxisHeight)
            );

            // Draw the labels
            for (int i = 0; i < ValueLabels.Count; i++)
            {
                FormattedText label = ValueLabels[i];
                double x = maxLabelWidth - label.Width;
                double y = i * steps;

                ctx.DrawText(label, new System.Windows.Point(x, y));
            }
        }

        /// <summary>
        /// Draws the x-axis of the chart. This includes the labels and the bars.
        /// </summary>
        /// <param name="ctx"></param>
        private void DrawXAxis(DrawingContext ctx)
        {
            int numberLabels = CategoryLabels.Count;
            double steps = Bounds.XAxisWidth / (numberLabels - 1);

            // Draw the x-axis
            ctx.DrawLine(
                PenAxis,
                new System.Windows.Point(Bounds.YAxisWidth, ActualHeight - Bounds.XAxisHeight),
                new System.Windows.Point(ActualWidth - Bounds.LegendWidth - LegendSpacing, ActualHeight - Bounds.XAxisHeight)
            );

            // Draw the labels
            for (int i = 0; i < numberLabels; i++)
            {
                double x = i * steps + Bounds.YAxisWidth + InnerSpacing;

                FormattedText label = CategoryLabels[i];
                ctx.DrawText(label, new System.Windows.Point(x, ActualHeight - label.Height));

                double zero = GetCanvasValue(0);
                List<float> values = Data.First(d => d.Category.Equals(label.Text)).Values.ToList();

                // Draw the bars
                for (int j = 0; j < values.Count; j++)
                {
                    int n = values.Count;

                    // Calculate the position of the bar. The following example shows the expected result.
                    // 
                    //          3 bars
                    // |           _
                    // |          |#|
                    // |       _  |#|                   1 bar
                    // |      |#| |#|                     _
                    // |      |#| |#|  _                 |#|
                    // |      |#| |#| |#|                |#|
                    // +---------------------------------------------------
                    //        01.01.2001              02.01.2001
                    double dx = x - ((n - 1) * BarSpacing + n * BarWidth) / 2 + (BarWidth + BarSpacing) * j + label.Width / 2;

                    // Now transform the value of the bar to the actual height inside the canvas
                    double height = GetCanvasValue(values[j]);

                    // Get the fill color for the bar
                    Brush brush = GetColorForSubCategoryIndex(j);
                    Brush transparentBrush = brush.Clone();
                    transparentBrush.Opacity = 0.5;

                    // Draw the bar
                    ctx.DrawRectangle(transparentBrush, null, new Rect(dx, height, BarWidth, zero - height));
                    ctx.DrawRectangle(brush, null, new Rect(dx, height, BarWidth, 4));
                }
            }
        }

        /// <summary>
        /// Returns a color based on the index. If the index is outside of
        /// the array bounds, the method starts with the first color again.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private Brush GetColorForSubCategoryIndex(int index)
        {
            // Use the modulo operator to make sure that the
            // color is always one of the defined colors.
            return Colors[index % Colors.Count];
        }

        /// <summary>
        /// Draws the legend of the chart.
        /// </summary>
        /// <param name="ctx"></param>
        private void DrawLegend(DrawingContext ctx)
        {
            // Calculate the start point of rendering
            double x = ActualWidth - Bounds.LegendWidth + LegendEllipsesRadius;
            double y = 0;

            for (int i = 0; i < SubCategoryLegendLabels.Count; i++)
            {
                FormattedText subCategory = SubCategoryLegendLabels[i];

                // Calculate the position of the ellipse (not centered, this will be handled later!)
                double ellipseX = x;
                double ellipseY = y + LegendEllipsesRadius;

                double labelX = x;
                double labelY = y;

                // Check if the ellipse is higher than the label. If the label
                // is higher than the ellipse, align the ellipse. Otherwise
                // align the label itself to be centered.
                if (2 * LegendEllipsesRadius < subCategory.Height)
                    ellipseY += subCategory.Height / 2 - LegendEllipsesRadius;
                else
                    labelY += LegendEllipsesRadius - subCategory.Height / 2;

                // Get the color for the sub category
                Brush brush = GetColorForSubCategoryIndex(i);

                // Draw the ellipse and the label
                ctx.DrawEllipse(brush, null, new System.Windows.Point(ellipseX, ellipseY), LegendEllipsesRadius, LegendEllipsesRadius);
                ctx.DrawText(subCategory, new System.Windows.Point(labelX + LegendEllipsesRadius + LegendEllipsesSpacing, labelY));

                // Update the y value, such that the next label will be drawn below
                y += subCategory.Height + LegendLabelSpacing;
            }
        }

    }
}
