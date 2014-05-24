using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

using Microsoft.Win32;
using Utility.ModifyRegistry;

namespace HSTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        Conf conf = new Conf();

        public MainWindow()
        {
            InitializeComponent();

            InitApp();

            //Application.Current.Shutdown();
        }

        private void InitApp()
        {
            showMessage("Starting");

            var tail = new Utility.TailThread.TailThread(@"c:\Users\winston\test.log", new Utility.TailThread.AppendTextDelegate(blah));
            tail.Start();
        }

        private void blah(string input)
        {
            showMessage(input, "logtail");
        }

        private void showMessage(string text, string caption = "ReadMe")
        {
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;

            MessageBox.Show(text, caption, button, icon);
        }
    }
}
