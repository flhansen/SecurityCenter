using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SecurityCenter.Theme.Components
{
    public class PieChart : Canvas
    {
        private double PartStartAngle;
        private double PartEndAngle;
        private double totalValue;

        #region DependencyPropertys

        public Dictionary<string, double> ChartData
        {
            get => (Dictionary<string, double>)GetValue(ChartDataProperty);
            set => SetValue(ChartDataProperty, value);
        }

        public static readonly DependencyProperty ChartDataProperty =
            DependencyProperty.Register(nameof(ChartData), typeof(Dictionary<string, double>),
                typeof(PieChart), new PropertyMetadata(new Dictionary<string, double>(), HandleChartPropertyChanged));

        public IEnumerable<Brush> Colors
        {
            get => (IEnumerable<Brush>)GetValue(ColorsProperty);
            set => SetValue(ColorsProperty, value);
        }

        public static readonly DependencyProperty ColorsProperty =
            DependencyProperty.Register(nameof(Colors), typeof(IEnumerable<Brush>),
                typeof(PieChart), new PropertyMetadata(new List<Brush>(), HandleChartPropertyChanged));

        public double InitialAngle
        {
            get => (double)GetValue(InitialAngleProperty);
            set => SetValue(InitialAngleProperty, value);
        }

        public static DependencyProperty InitialAngleProperty = DependencyProperty.Register(
            nameof(InitialAngle), typeof(double), typeof(PieChart),
            new PropertyMetadata(0.0, HandleChartPropertyChanged));

        public double Radius
        {
            get => (double)GetValue(RadiusProperty);
            set => SetValue(RadiusProperty, value);
        }

        public static DependencyProperty RadiusProperty = DependencyProperty.Register(
            nameof(Radius), typeof(double), typeof(PieChart),
            new PropertyMetadata(0.0, HandleChartPropertyChanged));


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
                typeof(PieChart), new PropertyMetadata(2.5, HandleChartPropertyChanged));

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
                typeof(PieChart), new PropertyMetadata(8.0, HandleChartPropertyChanged));

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
                typeof(PieChart), new PropertyMetadata(12.0, HandleChartPropertyChanged));

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
                typeof(PieChart), new PropertyMetadata(64.0, HandleChartPropertyChanged));

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
                typeof(PieChart), new PropertyMetadata(12.0, HandleChartPropertyChanged));

        private static void HandleChartPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PieChart chart = d as PieChart;

            chart.DeconstructDictionary();
            chart.CalcBasicValues();
            chart.InvalidateVisual();
        }

        public List<FormattedText> LegendLabels { get; private set; }
        public List<double> Values { get; private set; }
        private double LegendWidth = 0;

        #endregion

        public PieChart() : base()
        {
            SnapsToDevicePixels = true;

            DeconstructDictionary();
            CalcBasicValues();
        }

        private void DeconstructDictionary()
        {
            List<string> sKeys = ChartData.Keys.ToList();
            Values = ChartData.Values.ToList();
            if (LegendLabels != null)
            {
                LegendLabels.Clear();
            }
            else
            {
                LegendLabels = new List<FormattedText>();
            }

            foreach (string sKey in sKeys)
            {
                LegendLabels.Add(new FormattedText(
                    sKey,
                    CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    new Typeface("Roboto"),
                    12,
                    Brushes.White,
                    1.25)
                );
            }
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            PartStartAngle = InitialAngle;
            PartEndAngle = InitialAngle;

            if (Radius == 0.0) Radius = (float)ActualHeight / 3;

            for (int i = 0; i < Values.Count; i++)
            {
                Brush pieceColor = GetColorForIndex(i);
                DrawPiePiece(dc, Values[i], pieceColor);
            }
            DrawLegend(dc);

            PartEndAngle = PartStartAngle;
        }

        #region Utils
        private void CalcBasicValues()
        {
            Radius = ActualHeight / 3;
            LegendWidth = CalculateLegendWidth();
            totalValue = 0;
            foreach (double value in Values)
            {
                totalValue += value;
            }
        }
        private double CalculateLegendWidth()
        {
            if (LegendLabels.Count > 0)
            {
                double labelsWidth = LegendLabels.Max(x => x.Width);
                return labelsWidth + 2 * LegendEllipsesRadius + LegendEllipsesSpacing;
            }
            else
            {
                return 0;
            }
        }

        private double GetAngle(double percent)
        {
            return percent * 360;

        }

        private double ToRadians(double degrees)
        {
            return degrees / 180 * Math.PI;
        }
        #endregion

        private void DrawPiePiece(DrawingContext dc, double entry, Brush color)
        {
            dc.PushTransform(new TranslateTransform(ActualWidth / 5, ActualHeight / 2));
            PartEndAngle += GetAngle(entry / totalValue);

            PathFigure figure = new PathFigure
            {
                StartPoint = new Point(0, 0)
            };

            PathFigureCollection pathFigures = new PathFigureCollection();
            PathSegmentCollection pathSegments = new PathSegmentCollection
            {
                new LineSegment(new Point(
                    Radius * Math.Sin(ToRadians(PartStartAngle)),
                    Radius * -Math.Cos(ToRadians(PartStartAngle))),
                    true),
                new ArcSegment(
                    new Point(
                        Radius * Math.Sin(ToRadians(PartEndAngle)),
                        Radius * -Math.Cos(ToRadians(PartEndAngle))),
                    new Size(Radius, Radius),
                    0,
                    PartEndAngle - PartStartAngle >= 180,
                    SweepDirection.Clockwise,
                    true)
            };

            figure.Segments = pathSegments;
            figure.IsClosed = true;
            pathFigures.Add(figure);

            dc.DrawGeometry(color as SolidColorBrush, new Pen(color as SolidColorBrush, 1), new PathGeometry(pathFigures));
            dc.Pop();
            PartStartAngle = PartEndAngle;
        }

        private void DrawLegend(DrawingContext ctx)
        {
            // Calculate the start point of rendering
            //double x = ActualWidth - LegendWidth + LegendEllipsesRadius;
            double x = ActualWidth * 3 / 5;
            double y = ActualHeight / 3;

            for (int i = 0; i < LegendLabels.Count; i++)
            {
                FormattedText subCategory = LegendLabels[i];

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
                Brush brush = GetColorForIndex(i);

                // Draw the ellipse and the label
                ctx.DrawEllipse(brush, null, new Point(ellipseX, ellipseY), LegendEllipsesRadius, LegendEllipsesRadius);
                ctx.DrawText(subCategory, new Point(labelX + LegendEllipsesRadius + LegendEllipsesSpacing, labelY));

                // Update the y value, such that the next label will be drawn below
                y += subCategory.Height + LegendLabelSpacing;
            }
        }

        private Brush GetColorForIndex(int index)
        {
            Brush color = Brushes.White;

            if (Colors != null && Colors.Count() > 0)
                color = Colors.ElementAt(index % Colors.Count());

            return color;
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            CalcBasicValues();
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            CalcBasicValues();
        }
    }
}
