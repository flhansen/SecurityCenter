using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SecurityCenter.UILogic.ViewModels.Core
{
    public abstract class ViewModelCollectionBase<TViewModel, TModel> : ObservableCollection<TViewModel>, INotifyPropertyChanged
        where TViewModel : ViewModel<TModel>
        where TModel : class
    {
        public ObservableCollection<TModel> Collection { get; private set; }
        private bool syncDisabled = false;

        public ViewModelCollectionBase(ObservableCollection<TModel> collection)
        {
            Collection = collection;
            Collection.CollectionChanged += Collection_CollectionChanged;
            CollectionChanged += CollectionViewModel_CollectionChanged;

            LoadFromCollection(collection);
        }

        public ViewModelCollectionBase(IEnumerable<TViewModel> collection)
        {
            Collection = new ObservableCollection<TModel>();
            Collection.CollectionChanged += Collection_CollectionChanged;
            CollectionChanged += CollectionViewModel_CollectionChanged;

            LoadFromCollection(collection);
        }

        private void CollectionViewModel_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (syncDisabled)
                return;

            syncDisabled = true;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (TViewModel vm in e.NewItems)
                        Collection.Add(vm.Model);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (TViewModel vm in e.OldItems)
                        Collection.Remove(Collection.FirstOrDefault(model => model == vm.Model));
                    break;

                case NotifyCollectionChangedAction.Reset:
                    Collection.Clear();
                    foreach (TViewModel vm in Items)
                        Collection.Add(vm.Model);
                    break;
            }

            syncDisabled = false;
        }

        private void Collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (syncDisabled)
                return;

            syncDisabled = true;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (TModel model in e.NewItems)
                        Add(CreateViewModel(model));
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (TModel model in e.OldItems)
                        Remove(Items.FirstOrDefault(vm => vm.Model == model));
                    break;

                case NotifyCollectionChangedAction.Reset:
                    LoadFromCollection(Collection);
                    break;
            }

            syncDisabled = false;
        }

        private void LoadFromCollection(ObservableCollection<TModel> collection)
        {
            syncDisabled = true;

            Clear();

            foreach (TModel model in collection)
                Add(CreateViewModel(model));

            syncDisabled = false;
        }

        private void LoadFromCollection(IEnumerable<TViewModel> collection)
        {
            syncDisabled = true;

            Clear();

            foreach (TViewModel viewModel in collection)
                Add(viewModel);
            foreach (TViewModel viewModel in collection)
                Collection.Add(viewModel.Model);

            syncDisabled = false;
        }

        private TViewModel CreateViewModel(TModel model)
        {
            return Activator.CreateInstance(typeof(TViewModel), model) as TViewModel;
        }
    }
}
