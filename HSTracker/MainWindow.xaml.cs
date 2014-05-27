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
        Deck currentDeck;
        DeckCollection deckCollection = new DeckCollection();

        public MainWindow()
        {
            InitializeComponent();
            InitializeWindow();

            eventStream = new EventStream();

			this.Loaded += delegate { this.StartListening(); };
        }

        // Start window in upper-right
        private void InitializeWindow()
        {
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = SystemParameters.PrimaryScreenWidth - this.Width;
            this.Height = System.Windows.SystemParameters.WorkArea.Height;
            this.Top = 0;
        }

        private void InitializeDeck(string deckName)
        {
            // Clone instead so we don't have to reset state?
            if (currentDeck != null) currentDeck.Reset();

            currentDeck = deckCollection.GetDeck(deckName);
            this.Dispatcher.Invoke((Action)(() =>
            {
                this.cardCollection.ItemsSource = currentDeck.Cards;
                this.deckControl.ItemsSource = new List<Deck> { currentDeck };
            }));
        }

        private void StartListening()
        {
            eventStream.MyDraws().Subscribe(card =>
            {
                Console.WriteLine("Draw: " + card);
                currentDeck.Draw(card);
            });

            eventStream.MyMulligans().Subscribe(card =>
            {
                Console.WriteLine("Mulligan: " + card);
                currentDeck.Restore(card);
            });

            eventStream.GameOver().Subscribe(_ =>
            {
                Console.WriteLine("Game Over");
                InitializeDeck(currentDeck.Name);
            });
        }

        private void showMessage(string text, string caption = "ReadMe")
        {
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;

            MessageBox.Show(text, caption, button, icon);
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            InitializeDeck(currentDeck.Name);
        }

        #region Change deck ComboBox

        // Can these be done through data bindings instead?
        // http://www.codeproject.com/Articles/301678/Step-by-Step-WPF-Data-Binding-with-Comboboxes

        private void ChangeComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var comboBox = sender as ComboBox;

            PopulateChangeComboBox(comboBox);
            comboBox.SelectedIndex = 0;

            string firstDeck = (string) comboBox.SelectedItem;
            InitializeDeck(firstDeck);
        }

        private void ChangeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
	    {
	        var comboBox = sender as ComboBox;
	        string deckName = comboBox.SelectedItem as string;

            if (deckName == "--- New ---")
            {
                var deckCreationWindow = new DeckCreation(Library.CardNames());
                deckCreationWindow.Show();
                deckCreationWindow.Closed += new EventHandler((window, args) =>
                {
                    PopulateChangeComboBox(comboBox);
                });
            }
            else
            {
                InitializeDeck(deckName);
            }
        }

        private void PopulateChangeComboBox(ComboBox comboBox)
        {
            List<string> deckNames = deckCollection.DeckNames();

            // add fake item to trigger deck builder
            var names = deckNames.Concat(new List<string> { "--- New ---"});
            comboBox.ItemsSource = names;
        }

        #endregion
    }
}
