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

using Utility.TailThread;
using System.Reactive.Linq;

namespace HSTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        EventStream eventStream;

        public MainWindow()
        {
            InitializeComponent();

            eventStream = new EventStream();

            eventStream.MyCardPlays().Subscribe(x => showMessage("My Play: " + x));
            eventStream.TheirCardPlays().Subscribe(x => showMessage("Their Play: " + x));
            eventStream.MyCardDraws().Subscribe(x => showMessage("My Draw: " + x));
            eventStream.MyMulligans().Subscribe(x => showMessage("My Mulligan: " + x));
            eventStream.MyDiscards().Subscribe(x => showMessage("My Discard: " + x));

            //Application.Current.Shutdown();
        }
        private void showMessage(string text, string caption = "ReadMe")
        {
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;

            MessageBox.Show(text, caption, button, icon);
        }
    }
}
