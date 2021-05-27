using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SecurityCenter.Theme.Controls
{
    public class Select : ComboBox
    {
        /// <summary>
        /// Property to display a Text above the selection.
        /// </summary>
        public static readonly DependencyProperty SelectionTitleProperty = DependencyProperty.Register(
            nameof(SelectionTitle), typeof(string), typeof(Select), new PropertyMetadata(string.Empty));

        public string SelectionTitle
        {
            get => (string)GetValue(SelectionTitleProperty);
            set => SetValue(SelectionTitleProperty, value);
        }

        /// <summary>
        /// Property to display a default text if no item is selected.
        /// </summary>
        public static readonly DependencyProperty DefaultTextProperty = DependencyProperty.Register(
            nameof(DefaultText), typeof(string), typeof(Select), new PropertyMetadata(string.Empty));

        public string DefaultText
        {
            get => (string)GetValue(DefaultTextProperty);
            set => SetValue(DefaultTextProperty, value);
        }

        /// <summary>
        /// Property to fill the drpdowm with selectable items.
        /// </summary>
        public static DependencyProperty OptionsProperty = DependencyProperty.Register(
            nameof(Options), typeof(IEnumerable), typeof(Select),
            new PropertyMetadata(null, HandleOptionsChanged));

        public IEnumerable Options
        {
            get => (IEnumerable)GetValue(OptionsProperty);
            set => SetValue(OptionsProperty, value);
        }

        /// <summary>
        /// Handles dropdown content if it changes
        /// </summary>
        private static void HandleOptionsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Select select = d as Select;
            select.SetItems(e.NewValue as IEnumerable);
        }

        /// <summary>
        /// Sets the content of the dropdown.
        /// </summary>
        /// <param name="items">The List to display</param>
        private void SetItems(IEnumerable items)
        {
            Items.Clear();

            foreach (string value in items)
            {
                Items.Add(value);
            }

        }
    }
}
