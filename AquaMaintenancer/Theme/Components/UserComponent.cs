using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            MessageBox.Show("test");
        }
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            RaiseRotateEvent();
        }

    }
}
