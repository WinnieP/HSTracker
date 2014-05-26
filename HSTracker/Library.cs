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
            string cardsData = conf.CardsData();
            List<string> lines = cardsData.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).ToList();
            cards = lines.Select(x =>
                {
                    string[] pieces = x.Split(new[] { ',' });
                    return Tuple.Create(pieces[0], Convert.ToUInt32(pieces[1]));
                }).ToList();
        }

        public List<string> CardNames()
        {
            return cards.Select(x => x.Item1).OrderBy(x => x).ToList();
        }

        public List<CardInfo> FindByFragment(string fragment)
        {
            return cards.Where(x => x.Item1.ToLower().Contains(fragment.ToLower())).ToList();
        }
    }
}
