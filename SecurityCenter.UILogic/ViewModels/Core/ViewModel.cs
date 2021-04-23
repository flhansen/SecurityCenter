using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityCenter.UILogic.ViewModels.Core
{
    public class ViewModel<T> : ViewModelBase
    {
        public T Model { get; private set; }

        public ViewModel(T model)
        {
            Model = model;
        }

    }
}
