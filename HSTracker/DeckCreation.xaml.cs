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

        private void searchAutoComplete_Populating(object sender, System.Windows.Controls.PopulatingEventArgs e)
        {
            AutoCompleteBox searchBox = sender as AutoCompleteBox;

            string text = searchBox.Text;

            searchBox.ItemsSource = _cards;
            searchBox.PopulateComplete();
        }
    }

}
