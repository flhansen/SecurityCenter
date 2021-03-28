using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AquaMaintenancer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            double val = 0f;

            new Thread(() =>
                {
                    while (val <= 100)
                    {
                        Thread.Sleep(100);
                        val += 5;
                        Dispatcher.Invoke(new Action(() => pgb.Value = val));
                    }

                }).Start();

        }
    }
}
