using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace AquaMaintenancer.Theme.Components
{

    public class UserComponentData
    {
        public string FirstName { get; set; }
        public string LastName{ get; set; }
        public string Position { get; set; }
        public string PicturePath { get; set; }
    }
    public class UserComponent : ContentControl
    {
        static UserComponent()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(UserComponent), new FrameworkPropertyMetadata(typeof(UserComponent)));
        }

        public UserComponentData Data
        {
            get => (UserComponentData)GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }

        public static DependencyProperty DataProperty = DependencyProperty.Register(
            nameof(Data), typeof(UserComponentData), typeof(UserComponent),
            new PropertyMetadata(null));

        public bool IsOpened
        {
            get => (bool)GetValue(IsOpenedProperty);
            set => SetValue(IsOpenedProperty, value);
        }

        public static readonly DependencyProperty IsOpenedProperty = DependencyProperty.Register(
            nameof(IsOpened), typeof(bool), typeof(UserComponent),
            new PropertyMetadata(false));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            RotatableImage ri = GetTemplateChild("OpeningIcon") as RotatableImage;
            ri.OnClickRotate += OpenClose;
        }

        private void OpenClose(object sender, RoutedEventArgs e)
        {
            IsOpened = !IsOpened;
            Console.WriteLine(IsOpened);
        }
    }
}
