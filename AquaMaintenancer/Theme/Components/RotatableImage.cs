using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace AquaMaintenancer.Theme.Components
{
    public enum RotationStatus { Back = 0, Forth = 1 };
    public class RotatableImage : ContentControl
    {
        private DoubleAnimation RotateForth;
        private DoubleAnimation RotateBack;
        private Storyboard Sb;
        private RotationStatus RotationDirection = RotationStatus.Forth;

        public static readonly RoutedEvent OnClickRotateEvent = EventManager.RegisterRoutedEvent(
            "OnClickRotate", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(RotatableImage));

        public event RoutedEventHandler OnClickRotate
        {
            add => AddHandler(OnClickRotateEvent, value);
            remove => RemoveHandler(OnClickRotateEvent, value);
        }

        void RaiseRotateEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(OnClickRotateEvent);
            RaiseEvent(newEventArgs);

            Sb.Stop();

            if (RotationDirection == RotationStatus.Forth)
            {
                Sb.Children.Add(RotateForth);
                RotationDirection = RotationStatus.Back;
            }
            else
            {
                Sb.Children.Add(RotateBack);
                RotationDirection = RotationStatus.Forth;
            }

            Sb.Begin(); 
        }
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            RaiseRotateEvent();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            RotateBack = GetTemplateChild("RotateBack") as DoubleAnimation;
            RotateForth = GetTemplateChild("RotateForth") as DoubleAnimation;
            Sb = GetTemplateChild("Carot") as Storyboard;

            if (RotateForth == null || RotateForth == null || Sb == null)
            {
                throw new NullReferenceException("Animations not found.");
            }
        }
        
        /// <summary>
        /// The icon to rotate. If <em>None</em>, it will not be displayed.
        /// </summary>
        public FontAwesomeIcon Icon
        {
            get => (FontAwesomeIcon)GetValue(IconProperty);
            set => SetValue(IconProperty, value); 
        }

        public static readonly DependencyProperty IconProperty = 
            DependencyProperty.Register(nameof(Icon), typeof(FontAwesomeIcon),
                typeof(RotatableImage), new PropertyMetadata());


        public bool IsOpened
        {
            get => (bool)GetValue(IsOpenedProperty);
            set => SetValue(IsOpenedProperty, value);
        }

        public static readonly DependencyProperty IsOpenedProperty = DependencyProperty.Register(
            nameof(IsOpened), typeof(bool), typeof(RotatableImage),
            new PropertyMetadata(false, HandlePropertyChanged));

        private static void HandlePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RotatableImage ri = d as RotatableImage;
            ri.SetValue(IsOpenedProperty, !(bool)ri.GetValue(IsOpenedProperty));
        }

    }
}
