using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SecurityCenter.UILogic.Commands
{
    /// <summary>
    /// Implementation of a relay command.
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// The action to be executed.
        /// </summary>
        private Action<object> action;
        /// <summary>
        /// If the action can be executed.
        /// </summary>
        private Func<object, bool> canExecute;

        /// <summary>
        /// Event handler when the action execution statement changed.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="action">The action</param>
        /// <param name="canExecute">When the action can be executed</param>
        public RelayCommand(Action<object> action, Func<object, bool> canExecute)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="action">The action</param>
        public RelayCommand(Action<object> action) : this(action, null)
        {
        }

        /// <summary>
        /// If the action can be executed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            if (canExecute != null)
            {
                return canExecute.Invoke(parameter);
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Execute the action.
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            action.Invoke(parameter);
        }
    }
}
