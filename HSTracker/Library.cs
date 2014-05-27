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
        private static Conf conf = new Conf();
        private static Dictionary<string, CardInfo> cards = new Dictionary<string, CardInfo>();

        static Library()
        {
            string cardsData = conf.CardsData();
            List<string> lines = cardsData.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (string l in lines)
            {
                string[] pieces = l.Split(new[] { ',' });
                string name = pieces[0];
                uint mana = Convert.ToUInt32(pieces[1]);
                // duplicates names in list so iterating to avoid duplicate keys, otherwise LINQ (or figure out how to use Distinct() simply)
                cards[name.ToLower()] = Tuple.Create(name, mana);
            }
        }

        public static List<string> AllCardNames()
        {
            return cards.Select(x => x.Value.Item1).OrderBy(x => x).ToList();
        }

        public static bool HasCard(string name)
        {
            return cards.ContainsKey(name.ToLower());
        }

        public static uint ManaCost(string name)
        {
            return cards[name.ToLower()].Item2;
        }

        public static CardInfo GetCardInfo(string name)
        {
            return cards[name.ToLower()];
        }
    }
}
