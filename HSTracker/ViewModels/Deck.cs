using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace HSTracker
{
    public class Deck : INotifyPropertyChanged
    {
        private string _name;
        private List<Card> _cards = new List<Card>();

        public Deck(string name, List<Card> cards)
        {
            _name = name;
            _cards = cards;
        }

        public void Reset()
        {
            _cards.ForEach(x => x.Reset());
        }

        #region Properties

        public string Name
        {
            get { return _name; }
        }


        public List<Card> Cards
        {
            get
            {
                return _cards.OrderBy(x => x.Mana).ToList();
            }
        }

        public uint CardsLeft
        {
            get
            {
                return Convert.ToUInt32(_cards.Sum(x => x.Count));
            }
        }

        #endregion

        public void Draw(string name)
        {
            _cards.Find(x => x.Name == name).Draw();
            this.RaisePropertyChanged("CardsLeft");
        }

        public void Restore(string name)
        {
            _cards.Find(x => x.Name == name).Restore();
            this.RaisePropertyChanged("CardsLeft");
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion		
    }
}
