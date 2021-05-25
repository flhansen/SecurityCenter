using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SecurityCenter.Theme.Components
{
    public class Card : ContentControl, INotifyPropertyChanged
    {
        static Card()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Card), new FrameworkPropertyMetadata(typeof(Card)));
        }

        public Card()
        {
            SizeChanged += HandleSizeChanged;
        }

        private void HandleSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.HeightChanged && Template != null)
            {
                ContentPresenter presenter = Template.FindName("PART_ContentContainer", this) as ContentPresenter;

                if (presenter != null) 
                    ActualContentHeight = presenter.ActualHeight;
            }
        }

        /// <summary>
        /// The title of the card.
        /// </summary>
        public string Title
        {
            get => GetValue(TitleProperty) as string;
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string),
                typeof(Card), new PropertyMetadata(string.Empty));

        public double ActualContentHeight
        {
            get => (double)GetValue(ActualContentHeightProperty);
            private set
            {
                SetValue(ActualContentHeightProperty, value);
                NotifyPropertyChanged();
            }
        }

        public static readonly DependencyProperty ActualContentHeightProperty =
            DependencyProperty.Register(nameof(ActualContentHeight), typeof(double),
                typeof(ListView), new PropertyMetadata());

        public bool FitContent
        {
            get => (bool)GetValue(FitContentProperty);
            set => SetValue(FitContentProperty, value);
        }

        public static readonly DependencyProperty FitContentProperty =
            DependencyProperty.Register(nameof(FitContent), typeof(bool),
                typeof(Card), new PropertyMetadata(true));

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
