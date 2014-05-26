using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSTracker
{
    using CardInfo = Tuple<string, uint>;

    class Library
    {
        private Conf conf = new Conf();
        private List<CardInfo> cards;

        public Library()
        {
            string cardsData = conf.Cards();
            List<string> lines = cardsData.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).ToList();
            cards = lines.Select(x =>
                {
                    string[] pieces = x.Split(new[] { ',' });
                    return Tuple.Create(pieces[0], Convert.ToUInt32(pieces[1]));
                }).ToList();
        }

        public List<CardInfo> FindByFragment(string fragment)
        {
            return cards.Where(x => x.Item1.ToLower().Contains(fragment.ToLower())).ToList();
        }

        private class _CARD
        {
            private readonly string _name;
            private readonly uint _mana;

            public _CARD(string name, uint mana)
            {
                _name = name;
                _mana = mana;
            }

            public string Name { get { return _name; } }
            public uint Mana { get { return _mana; } }
        }
    }
}
