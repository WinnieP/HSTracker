using System;
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

        public static Deck Mage()
        {
            return new Deck(
                new List<Card> {
                    new Card("Arcane Missiles", 1, 2),
                    new Card("Ice Lance", 1, 2),
                    new Card("Mirror Image", 1, 2),
                    new Card("Leper Gnome", 1, 2),
                    new Card("Mana Wyrm", 1, 2),
                    new Card("Frostbolt", 2, 2),
                    new Card("Bloodmage Thalnos", 2, 1),
                    new Card("Knife Juggler", 2, 2),
                    new Card("Sorcerer's Apprentice", 2, 2),
                    new Card("Arcane Intellect", 3, 2),
                    new Card("Acolyte of Pain", 3, 1),
                    new Card("Fireball", 4, 2),
                    new Card("Violet Teacher", 4, 2),
                    new Card("Water Elemental", 4, 2),
                    new Card("Azure Drake", 5, 2),
                    new Card("Flamestrike", 7, 1),
                    new Card("Archmage Antonidas", 7, 1),
                }
            );
        }

        public static Deck Paladin()
        {
            return new Deck(
                new List<Card> {
                    new Card("Blessing of Might", 1, 2),
                    new Card("Noble Sacrifice", 1, 2),
                    new Card("Abusive Sergeant", 1, 2),
                    new Card("Argent Squire", 1, 2),
                    new Card("Leper Gnome", 1, 2),
                    new Card("Argent Protector", 2, 2),
                    new Card("Bluegill Warrior", 2, 2),
                    new Card("Ironbeak Owl", 2, 2),
                    new Card("Knife Juggler", 2, 2),
                    new Card("Sword of Justice", 3, 1),
                    new Card("Divine Favor", 3, 2),
                    new Card("Arcane Golem", 3, 1),
                    new Card("Harvest Golem", 3, 1),
                    new Card("Wolfrider", 3, 2),
                    new Card("Truesilver Champion", 4, 2),
                    new Card("Blessing of Kings", 4, 2),
                    new Card("Leeroy Jenkins",6, 1),
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
