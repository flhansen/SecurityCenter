using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace AquaMaintenancer.Theme.Events
{
    class RotateIconEvent
    {
        public event RoutedEventHandler Test;

         public static readonly RoutedEvent OnClickRotateEvent = EventManager.RegisterRoutedEvent(
            "OnClickRotate", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(RotateIconEvent));

        public event RoutedEventHandler OnClickRotate
        {
            add => AddHandler(OnClickRotateEvent, value);
            remove => RemoveHandler(OnClickRotateEvent, value);
        }

    }
}
