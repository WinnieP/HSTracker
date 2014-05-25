using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSTracker
{
    class Card
    {
        private string _name;
        private uint _mana;

        public Card(string name, uint mana)
        {
            _name = name;
            _mana = mana;
        }

        public string Name
        {
            get { return _name; }
        }

        public uint Mana
        {
            get { return _mana; }
        }

        public override string ToString()
        {
            return String.Format("({0}) {1}", _mana, _name);
        }
    }
}
