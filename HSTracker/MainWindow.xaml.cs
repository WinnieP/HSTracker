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
        Library library = new Library();

        public MainWindow()
        {
            InitializeComponent();
            InitializeWindow();
            ResetDeck();

            library.FindByFragment("fire").ForEach(x => Console.WriteLine(x.Item1 + "," + x.Item2));

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

        private void ResetDeck()
        {
            // Clone instead so we don't have to reset state?
            if (currentDeck != null) currentDeck.Reset();

            currentDeck = deckCollection.GetDeck("Zoo");
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
                ResetDeck();
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
            ResetDeck();
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            // hackity hack hack - remember to make `stream` private again once done testing
            //eventStream.stream.OnNext("[Zone] ZoneChangeList.ProcessChanges() - id=65 local=False [name=Bluegill Warrior id=12 zone=HAND zonePos=0 cardId=CS2_073 player=1] zone from FRIENDLY DECK -> FRIENDLY HAND");
            eventStream.stream.OnNext("[Zone] ZoneChangeList.ProcessChanges() - id=84 local=False [name=Uther Lightbringer id=36 zone=GRAVEYARD zonePos=0 cardId=HERO_04 player=2] zone from OPPOSING PLAY (Hero) -> OPPOSING GRAVEYARD");
        }

        private void Change_SelectionChanged(object sender, SelectionChangedEventArgs e)
	    {
	        // ... Get the ComboBox.
	        var comboBox = sender as ComboBox;

	        // ... Set SelectedItem as Window Title.
	        string value = comboBox.SelectedItem as string;
	        this.Title = "Selected: " + value;
	    }
    }
}
