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
        public static Pen PenGrid = new Pen(Brushes.Gray, 1.0);

        public double SpacingLabels { get; set; } = 16.0;

        private IEnumerable<ChartData> data = new List<ChartData>()
        {
            new ChartData { Values = new List<float>() { 57.0f, 25.0f, 13.0f }, Category = "07.03.2021" },
            new ChartData { Values = new List<float>() { 13.0f }, Category = "08.03.2021" },
            new ChartData { Values = new List<float>() { 30.0f }, Category = "09.03.2021" },
            new ChartData { Values = new List<float>() { 80.0f }, Category = "10.03.2021" },
            new ChartData { Values = new List<float>() { 29.0f }, Category = "11.03.2021" },
        };

        protected override void OnRender(DrawingContext ctx)
        {
            List<FormattedText> valueLabels = GetValueLabels(data, 5);
            List<FormattedText> categoryLabels = GetCategoryLabels(data);

            double categoryLabelsHeight = CalculateCategoryLabelsHeight(categoryLabels);
            double valueLabelsWidth = CalculateValueLabelsWidth(valueLabels);

            double categoryLabelsWidth = CalculateCategoryLabelsWidth(categoryLabels, valueLabelsWidth);
            double valueLabelsHeight = CalculateValueLabelsHeight(valueLabels, categoryLabelsHeight);

            // Draw the labels on y-axis
            DrawValueLabels(ctx, valueLabels, valueLabelsHeight, categoryLabelsHeight);
            DrawCategoryLabels(ctx, categoryLabels, valueLabelsWidth, categoryLabelsWidth);

            // Draw the x-axis
            ctx.DrawLine(PenLabels, new System.Windows.Point(valueLabelsWidth, ActualHeight - categoryLabelsHeight), new System.Windows.Point(ActualWidth, ActualHeight - categoryLabelsHeight));

            // Draw the y-axis
            ctx.DrawLine(PenLabels, new System.Windows.Point(valueLabelsWidth, 0), new System.Windows.Point(valueLabelsWidth, ActualHeight - categoryLabelsHeight));
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
            return ActualHeight - categoryLabelsHeight - labels.Last().Height;
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

        private double DrawValueLabels(DrawingContext ctx, List<FormattedText> labels, double valueLabelsHeight, double categoryLabelsHeight)
        {
            double width = 0.0f;
            double maxLabelWidth = labels.Max(l => l.Width);
            double steps = valueLabelsHeight / (labels.Count - 1);

            for (int i = 0; i < labels.Count; i++)
            {
                FormattedText label = labels[i];
                double x = maxLabelWidth - label.Width;
                double y = i * steps;

                ctx.DrawText(label, new System.Windows.Point(x, y));

                if (label.Width > width)
                    width = label.Width;
            }

            for (int i = 0; i < labels.Count - 1; i++)
            {
                double y = i * steps;
                ctx.DrawLine(PenGrid, new System.Windows.Point(width + SpacingLabels, y), new System.Windows.Point(ActualWidth, y));
            }

            return width;
        }

        private double DrawCategoryLabels(DrawingContext ctx, List<FormattedText> labels, double valueLabelsWidth, double categoryLabelsWidth)
        {
            int numberLabels = labels.Count;

            double height = 0.0f;
            double steps = categoryLabelsWidth / (numberLabels - 1);

            for (int i = 0; i < numberLabels; i++)
            {
                double x = i * steps;

                FormattedText label = labels[i];
                ctx.DrawText(label, new System.Windows.Point(x + valueLabelsWidth, ActualHeight - label.Height));

                if (label.Height > height)
                    height = label.Height;
            }

            return height;
        }

    }
}
