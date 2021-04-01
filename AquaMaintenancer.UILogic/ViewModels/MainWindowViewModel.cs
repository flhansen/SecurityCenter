using System;
using System.Collections.Generic;
using System.Text;

namespace AquaMaintenancer.UILogic.ViewModels
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            TestViewModels = new TestCollectionViewModel(new List<TestViewModel>()
            {
                new TestViewModel(new Business.Models.TestModel
                {
                    Prop1 = "Prop1",
                    Prop2 = "Prop2",
                    Prop3 = "Prop3",
                    Prop4 = "Prop4",
                    Prop5 = "Prop5"

                }),
                new TestViewModel(new Business.Models.TestModel
                {
                    Prop1 = "Prop1",
                    Prop2 = "Prop2",
                    Prop3 = "Prop3",
                    Prop4 = "Prop4",
                    Prop5 = "Prop5"

                }),
                new TestViewModel(new Business.Models.TestModel
                {
                    Prop1 = "Prop1",
                    Prop2 = "Prop2",
                    Prop3 = "Prop3",
                    Prop4 = "Prop4",
                    Prop5 = "Prop5"

                }),
                new TestViewModel(new Business.Models.TestModel
                {
                    Prop1 = "Prop1",
                    Prop2 = "Prop2",
                    Prop3 = "Prop3",
                    Prop4 = "Prop4",
                    Prop5 = "Prop5"

                })


            });
        }

        public TestCollectionViewModel TestViewModels { get; set; }
    }
}
