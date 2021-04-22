using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace AquaMaintenancer.UILogic.Commands
{
    public class RelayCommand : ICommand
    {
        private Action<object> action;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<object> action, Func<object, bool> canExecute)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        public RelayCommand(Action<object> action) : this(action, null)
        {

        }

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

        public void Execute(object parameter)
        {
            action.Invoke(parameter);
        }
    }
}
