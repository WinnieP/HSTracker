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
using System.Windows.Shapes;

using System.Collections.ObjectModel;

namespace HSTracker
{
    /// <summary>
    /// Interaction logic for DeckCreation.xaml
    /// </summary>
    public partial class DeckCreation : Window
    {
        private List<string> _cards;
        private ObservableCollection<DeckCreationCard> selectedCards = new ObservableCollection<DeckCreationCard>();

        public DeckCreation(List<string> cards)
        {
            _cards = cards;
            InitializeComponent();

            this.Loaded += delegate { this.cardList.ItemsSource = selectedCards; };
        }

        #region Autocomplete

        private void searchAutoComplete_Loaded(object sender, RoutedEventArgs e)
        {
            AutoCompleteBox searchBox = sender as AutoCompleteBox;
            searchBox.ItemsSource = _cards;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string cardName = this.autoCompleteBox.Text;

            if (Library.HasCard(cardName))
            {
                DeckCreationCard match = selectedCards.FirstOrDefault(x => x.Name == cardName);
                if (match != null)
                {
                    match.Add();
                }
                else
                {
                    selectedCards.Add(new DeckCreationCard(cardName));
                }
            }
        }

        #endregion

        #region Create deck controls

        private void Create_Click(object sender, RoutedEventArgs e)
        {
         //   List<Card> cards = selectedCards.Select
           // Deck deck = new Deck(this.deckName.Text, cards);
        }

        private void searchAutoComplete_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Console.WriteLine("ENTER");
            }

        }

        #endregion
    }

}
