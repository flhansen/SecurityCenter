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
    public class ChartData
    {
        public IEnumerable<float> Values { get; set; }
        public string Category { get; set; }
    }

    public class BarChart : Canvas
    {
        public static Pen PenLabels = new Pen(Brushes.White, 1.0);
        public static Pen PenAxis = new Pen(Brushes.White, 1.0);
        public static Pen PenGrid = new Pen(Brushes.DarkGray, 1.0);

        public double SpacingLabels { get; set; } = 16.0;
        public List<FormattedText> ValueLabels { get; private set; }
        public List<FormattedText> CategoryLabels { get; private set; }

        private IEnumerable<ChartData> data = new List<ChartData>()
        {
            new ChartData { Values = new List<float>() { 57.0f, 25.0f, 13.0f }, Category = "07.03.2021" },
            new ChartData { Values = new List<float>() { 13.0f }, Category = "08.03.2021" },
            new ChartData { Values = new List<float>() { 30.0f }, Category = "09.03.2021" },
            new ChartData { Values = new List<float>() { 80.0f }, Category = "10.03.2021" },
            new ChartData { Values = new List<float>() { 29.0f }, Category = "11.03.2021" },
        };

        public BarChart()
        {
            ValueLabels = GetValueLabels(data, 5);
            CategoryLabels = GetCategoryLabels(data);
        }

        protected override void OnRender(DrawingContext ctx)
        {
            double categoryLabelsHeight = CalculateCategoryLabelsHeight(CategoryLabels);
            double valueLabelsWidth = CalculateValueLabelsWidth(ValueLabels);

            double categoryLabelsWidth = CalculateCategoryLabelsWidth(CategoryLabels, valueLabelsWidth);
            double valueLabelsHeight = CalculateValueLabelsHeight(ValueLabels, categoryLabelsHeight);

            // Draw the labels on y-axis
            DrawValueLabels(ctx, ValueLabels, valueLabelsHeight, categoryLabelsHeight);
            DrawCategoryLabels(ctx, CategoryLabels, valueLabelsWidth, categoryLabelsWidth);
            DrawGridLines(ctx, valueLabelsWidth);

            // Draw the x-axis
            ctx.DrawLine(PenLabels, new System.Windows.Point(valueLabelsWidth, ActualHeight - categoryLabelsHeight), new System.Windows.Point(ActualWidth, ActualHeight - categoryLabelsHeight));

            // Draw the y-axis
            ctx.DrawLine(PenLabels, new System.Windows.Point(valueLabelsWidth, 0), new System.Windows.Point(valueLabelsWidth, ActualHeight - categoryLabelsHeight));

        }

        private double Remap(double value, double min1, double max1, double min2, double max2)
        {
            return (value - min1) * (max2 - min2) / (max1 - min1) + min2;
        }

        private double GetValueInChart(double value)
        {
            double categoryLabelsHeight = CalculateCategoryLabelsHeight(CategoryLabels);

            double min1 = 0.0;
            double max1 = ValueLabels.Max(label => double.Parse(label.Text));
            double min2 = ActualHeight - categoryLabelsHeight;
            double max2 = ValueLabels.Last().Height / 2.0;

            return Remap(value, min1, max1, min2, max2);
        }

        private double CalculateCategoryLabelsWidth(List<FormattedText> labels, double valueLabelsWidth)
        {
            return ActualWidth - valueLabelsWidth - labels.Last().Width;
        }

        private double CalculateCategoryLabelsHeight(List<FormattedText> labels)
        {
            return labels.Max(label => label.Height) + SpacingLabels;
        }

        private double CalculateValueLabelsWidth(List<FormattedText> labels)
        {
            return labels.Max(label => label.Width) + SpacingLabels;
        }

        private double CalculateValueLabelsHeight(List<FormattedText> labels, double categoryLabelsHeight)
        {
            return ActualHeight - categoryLabelsHeight - labels.Last().Height / 2;
        }

        private List<FormattedText> GetValueLabels(IEnumerable<ChartData> data, int count)
        {
            float max = data.Max(d => d.Values.Max());
            List<FormattedText> labels = new List<FormattedText>();

            for (int i = 0; i < count; i++)
            {
                float yValue = max - i * max / (count - 1);
                FormattedText label = new FormattedText(yValue.ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Roboto"), 14, Brushes.White, 1.25);
                labels.Add(label);
            }

            return labels;
        }

        private List<FormattedText> GetCategoryLabels(IEnumerable<ChartData> data)
        {
            return data.Select(d => new FormattedText(d.Category, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Roboto"), 14, Brushes.White, 1.25)).ToList();
        }

        private void DrawGridLines(DrawingContext ctx, double valueLabelsWidth)
        {
            for (int i = 0; i < ValueLabels.Count - 1; i++)
            {
                FormattedText label = ValueLabels[i];
                double y = GetValueInChart(double.Parse(label.Text));
                ctx.DrawLine(PenGrid, new System.Windows.Point(valueLabelsWidth, y), new System.Windows.Point(ActualWidth, y));
            }
        }

        private void DrawValueLabels(DrawingContext ctx, List<FormattedText> labels, double valueLabelsHeight, double categoryLabelsHeight)
        {
            double maxLabelWidth = labels.Max(l => l.Width);
            double steps = valueLabelsHeight / (labels.Count - 1);

            for (int i = 0; i < labels.Count; i++)
            {
                FormattedText label = labels[i];
                double x = maxLabelWidth - label.Width;
                double y = i * steps;

                ctx.DrawText(label, new System.Windows.Point(x, y));
            }
        }

        private void DrawCategoryLabels(DrawingContext ctx, List<FormattedText> labels, double valueLabelsWidth, double categoryLabelsWidth)
        {
            int numberLabels = labels.Count;
            double steps = categoryLabelsWidth / (numberLabels - 1);

            for (int i = 0; i < numberLabels; i++)
            {
                double x = i * steps;

                FormattedText label = labels[i];
                ctx.DrawText(label, new System.Windows.Point(x + valueLabelsWidth, ActualHeight - label.Height));
            }
        }

    }
}
