using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AquaMaintenancer.UILogic.ViewModels.Core
{
    public abstract class ViewModelBase<TModel> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public TModel Model { get; private set; }

        public ViewModelBase(TModel model)
        {
            Model = model;
        }

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;

            storage = value;
            NotifyPropertyChanged(propertyName);

            return true;
        }
    }
}
