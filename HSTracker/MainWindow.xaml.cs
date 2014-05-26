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
    public partial class MainWindow : Window
    {
        EventStream eventStream;
        Deck deck = Deck.Zoo();

        public MainWindow()
        {
            InitializeComponent();
            InitializeWindow();

            eventStream = new EventStream();

            this.cardCollection.ItemsSource = deck.Cards;
            this.deckControl.ItemsSource = new List<Deck> { deck };

			this.Loaded += delegate { this.StartListening(); };

            //Application.Current.Shutdown();
        }

        // Start window in upper-right
        private void InitializeWindow()
        {
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = SystemParameters.PrimaryScreenWidth - this.Width;
            this.Height = System.Windows.SystemParameters.WorkArea.Height;
            this.Top = 0;
        }

        private void StartListening()
        {
            /*
            eventStream.MyPlays().Subscribe(x => showMessage("My Play: " + x));
            eventStream.TheirPlays().Subscribe(x => showMessage("Their Play: " + x));
            eventStream.MyDraws().Subscribe(x => showMessage("My Draw: " + x));
            eventStream.MyMulligans().Subscribe(x => showMessage("My Mulligan: " + x));
            eventStream.MyDiscards().Subscribe(x => showMessage("My Discard: " + x));
             */

            eventStream.MyDraws().Subscribe(card =>
            {
                Console.WriteLine("Draw: " + card);
                deck.Draw(card);
            });

            eventStream.MyMulligans().Subscribe(card =>
            {
                Console.WriteLine("Mulligan: " + card);
                deck.Restore(card);
            });
        }

        private void showMessage(string text, string caption = "ReadMe")
        {
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;

            MessageBox.Show(text, caption, button, icon);
        }
    }
}
