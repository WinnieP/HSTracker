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
using Utility.TailThread;
using System.Reactive.Subjects;

namespace HSTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        Conf            conf    = new Conf();
        Subject<string> subject = new Subject<string>();
        TailThread      tail;

        public MainWindow()
        {
            InitializeComponent();

            InitApp();

            //Application.Current.Shutdown();
        }

        private void InitApp()
        {
            subject.Subscribe(line => showMessage(line, "logtail"));
            tail = new TailThread(conf.LogPath(), new AppendTextDelegate(subject.OnNext));

            tail.Start();
        }

        private void showMessage(string text, string caption = "ReadMe")
        {
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;

            MessageBox.Show(text, caption, button, icon);
        }
    }
}
