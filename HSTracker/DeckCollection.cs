using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSTracker
{
    class DeckCollection
    {
        #region Hard-coded decks

        // Some hardcoded decks to start with
        private Dictionary<string, Deck> _collection = new Dictionary<string, Deck>
        {
            { "Zoo", new Deck("Zoo",
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
            )},
            { "Mage", new Deck("Mage",
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
            )},
            { "Paladin", new Deck("Paladin",
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
            )},
            { "Miracle", new Deck("Miracle",
                new List<Card> {
                    new Card("Backstab", 0, 2),
                    new Card("Preparation", 0, 2),
                    new Card("Shadowstep", 0, 2),
                    new Card("Cold Blood", 1, 2),
                    new Card("Conceal", 1, 1),
                    new Card("Deadly Poison", 1, 2),
                    new Card("Blade Flurry", 2, 1),
                    new Card("Eviscerate", 2, 2),
                    new Card("Sap", 2, 2),
                    new Card("Shiv", 2, 2),
                    new Card("Bloodmage Thalnos", 2, 1),
                    new Card("Fan of Knives", 3, 2),
                    new Card("Earthen Ring Farseer", 3, 2),
                    new Card("Edwin VanCleef", 3, 1),
                    new Card("SI:7 Agent", 3, 2),
                    new Card("Leeroy Jenkins", 4, 1),
                    new Card("Azure Drake", 5, 1),
                    new Card("Gadgetzan Auctioneer", 5, 2),
                }
            )}
        };

        #endregion

        public DeckCollection()
        {

        }

        public Deck GetDeck(string name)
        {
            return _collection[name];
        }

        public void AddDeck(string name, Deck deck)
        {
            _collection[name] = deck;
        }

        public void RemoveDeck(string name)
        {
            _collection.Remove(name);
        }

        public List<string> DeckNames()
        {
            return _collection.Select(pair => { return pair.Key; }).ToList();
        }
    }
}
