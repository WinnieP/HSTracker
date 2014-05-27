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
        private ObservableCollection<DeckCreationCard> _selectedCards = new ObservableCollection<DeckCreationCard>();
        private DeckCreationDelegate _callback;

        public DeckCreation(List<string> cards, DeckCreationDelegate callback)
        {
            _cards = cards;
            _callback = callback;
            InitializeComponent();

            this.Loaded += delegate { this.cardList.ItemsSource = _selectedCards; };
        }

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
                DeckCreationCard match = _selectedCards.FirstOrDefault(x => x.Name == cardName);
                if (match != null)
                {
                    match.Add();
                }
                else
                {
                    _selectedCards.Add(new DeckCreationCard(cardName));
                }
            }
        }
        
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            List<Card> cards = _selectedCards.Select(x =>
            {
                var cardInfo = Library.GetCardInfo(x.Name);
                return new Card(cardInfo.Item1, cardInfo.Item2, x.Count);
            }).ToList();

            Deck deck = new Deck(this.deckName.Text, cards);

            _callback(deck);
            this.Close();
        }
    }

}
