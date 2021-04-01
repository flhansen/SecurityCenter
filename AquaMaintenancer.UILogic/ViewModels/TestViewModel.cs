using AquaMaintenancer.Business.Models;
using AquaMaintenancer.UILogic.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaMaintenancer.UILogic.ViewModels
{
    public class TestViewModel : ViewModelBase<TestModel>
    {
        public TestViewModel(TestModel model) : base(model)
        {

        }

        public string Prop1 { get => Model.Prop1; }
        public string Prop2 { get => Model.Prop2; }
        public string Prop3 { get => Model.Prop3; }
        public string Prop4 { get => Model.Prop4; }
        public string Prop5 { get => Model.Prop5; }
    }
}
