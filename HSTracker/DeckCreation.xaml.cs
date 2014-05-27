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

namespace HSTracker
{
    /// <summary>
    /// Interaction logic for DeckCreation.xaml
    /// </summary>
    public partial class DeckCreation : Window
    {
        private List<string> _cards;

        public DeckCreation(List<string> cards)
        {
            _cards = cards;
            InitializeComponent();
        }

        private void searchAutoComplete_Loaded(object sender, RoutedEventArgs e)
        {
            AutoCompleteBox searchBox = sender as AutoCompleteBox;
            searchBox.ItemsSource = _cards;
        }

        private void searchAutoComplete_KeyDown(object sender, KeyEventArgs e)
        {
            AutoCompleteBox searchBox = sender as AutoCompleteBox;

            Console.WriteLine("keydown");
            if (e.Key == Key.Enter)
            {
                string val = searchBox.Text;
                Console.WriteLine("!!!!!");
                Console.WriteLine(val);
                Console.WriteLine("!!!!!");
            }
        }

        // I've got to be doing this wrong
        private void searchAutoComplete_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AutoCompleteBox searchBox = sender as AutoCompleteBox;

            if (e.AddedItems.Count > 0)
            {
            } 
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {

        }

    }

}
