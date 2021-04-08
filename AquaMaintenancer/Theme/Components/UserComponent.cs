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
    }
}
