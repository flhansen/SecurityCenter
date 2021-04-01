using AquaMaintenancer.Business.Models;
using AquaMaintenancer.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaMaintenancer.UILogic.ViewModels
{
    public class TestCollectionViewModel : ViewModelCollectionBase<TestViewModel, TestModel>
    {
        public TestCollectionViewModel(IEnumerable<TestViewModel> viewModels) : base(viewModels)
        {

        }
    }
}
