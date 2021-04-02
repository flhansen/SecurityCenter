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

    public class UserComponentData
    {
        public string FirstName { get; set; }
        public string LastName{ get; set; }
        public string Position { get; set; }
        public string PicturePath { get; set; }
    }
    public class UserComponent : ContentControl
    {
        private DoubleAnimation RotateForth;
        private DoubleAnimation RotateBack;
        private Storyboard Sb;
        private RotationStatus RotationDirection = RotationStatus.Forth;

        public static readonly RoutedEvent OnClickRotateEvent = EventManager.RegisterRoutedEvent(
            "OnClickRotate", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UserComponent));

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
    }
}
