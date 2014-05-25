using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSTracker
{
    class Deck
    {
        private List<Card> _cards = new List<Card>();

        public Deck(List<Card> cards)
        {
            _cards = cards;
        }

        public List<Card> Cards
        {
            get
            {
                return _cards;
            }
        }

        public void Play(string name)
        {
            _cards.Remove(_cards.FirstOrDefault<Card>(x => x.Name == name));
        }
    }
}
