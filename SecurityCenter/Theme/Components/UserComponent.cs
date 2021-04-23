using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace SecurityCenter.Theme.Components
{

    public class UserComponentData
    {
        public string FirstName { get; set; } = "Max";
        public string LastName { get; set; } = "Mustermann";
        public string Position { get; set; } = "Mustermacher";
        public string PicturePath { get; set; }
    }
    public class UserComponent : ContentControl
    {
        private TextBlock NameBlock;
        private TextBlock PositionBlock;
        static UserComponent()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(UserComponent), new FrameworkPropertyMetadata(typeof(UserComponent)));
        }
        
        /// <summary>
        /// The user information to display.
        /// </summary>
        public UserComponentData User
        {
            get => (UserComponentData)GetValue(UserProperty);
            set => SetValue(UserProperty, value);
        }

        public static DependencyProperty UserProperty = DependencyProperty.Register(
            nameof(User), typeof(UserComponentData), typeof(UserComponent),
            new PropertyMetadata(null, HandleUserChanged));

        /// <summary>
        /// Changes userinformation if the property changes. 
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void HandleUserChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UserComponent uc = d as UserComponent;
            uc.SetUserData(e.NewValue as UserComponentData);
        }

        /// <summary>
        /// Adds the data to the elements.
        /// </summary>
        /// <param name="UserData"></param>
        private void SetUserData(UserComponentData UserData)
        {
            string FullName = string.Join(" ", UserData.FirstName, UserData.LastName);
            NameBlock.Text = FullName;

            PositionBlock.Text = UserData.Position;
        } 

        /// <summary>
        /// Property who the visibility of the menu is bound to.
        /// </summary>
        public bool IsOpened
        {
            get => (bool)GetValue(IsOpenedProperty);
            set => SetValue(IsOpenedProperty, value);
        }

        public static readonly DependencyProperty IsOpenedProperty = DependencyProperty.Register(
            nameof(IsOpened), typeof(bool), typeof(UserComponent),
            new PropertyMetadata(false));

        /// <summary>
        /// Requesting the user data elements and fill them with default values
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            NameBlock = GetTemplateChild("UserName") as TextBlock;
            PositionBlock = GetTemplateChild("UserPosition") as TextBlock;
            RotatableImage ri = GetTemplateChild("OpeningIcon") as RotatableImage;

            if (NameBlock == null || PositionBlock == null || ri == null)
            {
                throw new Exception("Missing userelements or iconreference");
            }

            SetUserData(new UserComponentData());
            ri.OnClickRotate += OpenClose;
        }

        /// <summary>
        /// Changes the value of the DependencyPorperty
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenClose(object sender, RoutedEventArgs e)
        {
            IsOpened = !IsOpened;
        }
    }
}
