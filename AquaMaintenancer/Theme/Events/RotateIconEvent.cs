using AquaMaintenancer.Theme.Components;
using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace AquaMaintenancer.Theme.Events
{
    public class RotateIconEvent : Control
    {
        public event RoutedEventHandler Test;

         public static readonly RoutedEvent OnClickRotateEvent = EventManager.RegisterRoutedEvent(
            "OnClickRotate", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(RotateIconEvent));

        public event RoutedEventHandler OnClickRotate
        {
            add => AddHandler(OnClickRotateEvent, value);
            remove => RemoveHandler(OnClickRotateEvent, value);
        }

        void RaiseRotateEvent()
        { 
            RoutedEventArgs newEventArgs = new RoutedEventArgs(OnClickRotateEvent);
            RaiseEvent(newEventArgs);

        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            RaiseRotateEvent();
        }

        public void ScaleYIcon(Storyboard sb, PropertyPath property, string targetName ,RotationStatus dir)
        {
            DoubleAnimation anim = GetAnimationByDirection(dir);
            sb.Stop();
            Storyboard.SetTargetName(anim, targetName);
            Storyboard.SetTargetProperty(anim, property);
            sb.Children.Add(anim);
            sb.Begin();
        }

        private DoubleAnimation GetAnimationByDirection(RotationStatus dir)
        {
            DoubleAnimation anim = new DoubleAnimation();

            if (dir == RotationStatus.Forth)
            {
                anim.To = -1;
                anim.Duration = new Duration(TimeSpan.FromMilliseconds(1000));
            }
            else if (dir == RotationStatus.Back)
            {
                anim.To = 1;
                anim.Duration = new Duration(TimeSpan.FromMilliseconds(1000));
            }

            return anim;
        }

    }
}
