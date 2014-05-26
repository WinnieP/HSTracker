﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace HSTracker
{
    class Deck : INotifyPropertyChanged
    {
        private List<Card> _cards = new List<Card>();

        public Deck(List<Card> cards)
        {
            _cards = cards;
        }

        public static Deck Zoo()
        {
            return new Deck(
                new List<Card> {
                    new Card("Soulfire", 0, 2),
                    new Card("Mortal Coil", 1, 1),
                    new Card("Abusive Sergeant", 1, 2),
                    new Card("Argent Squire", 1, 2),
                    new Card("Elven Archer", 1, 1),
                    new Card("Flame Imp", 1, 2),
                    new Card("Shieldbearer", 1, 2),
                    new Card("Voidwalker", 1, 2),
                    new Card("Amani Berserker", 2, 2),
                    new Card("Dire Wolf Alpha", 2, 2),
                    new Card("Knife Juggler", 2, 2),
                    new Card("Harvest Golem", 3, 1),
                    new Card("Shattered Sun Cleric", 3, 2),
                    new Card("Dark Iron Dwarf", 4, 2),
                    new Card("Defender of Argus", 4, 2),
                    new Card("Doomguard", 5, 2),
                    new Card("Argent Commander", 6, 1),
                }
            );
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
