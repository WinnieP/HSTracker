using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utility.TailThread;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using System.Text.RegularExpressions;

namespace HSTracker
{
    class EventStream
    {
//$ grep '^\[Zone.*\[name=.*zone=(PLAY|HAND).* HAND ' output_log.txt | cut -d' ' -f5- | cut -d'=' -f2- | uniq | sed -e 's/^True/ME:   /;s/^False/THEM: /;s/\[name=\(.*\) id=[0-9]*.*/\1/'      
//[Zone] ZoneChangeList.ProcessChanges() - id=1 local=False [name=Gul'dan id=36 zone=PLAY zonePos=0 cardId=HERO_07 player=2] zone from  -> OPPOSING PLAY (Hero)
//[Zone] ZoneChangeList.ProcessChanges() - id=65 local=False [name=Cold Blood id=12 zone=HAND zonePos=0 cardId=CS2_073 player=1] zone from FRIENDLY DECK -> FRIENDLY HAND
//[Zone] ZoneChangeList.ProcessChanges() - id=4 local=False [name=Northshire Cleric id=21 zone=DECK zonePos=2 cardId=CS2_235 player=1] zone from FRIENDLY HAND -> FRIENDLY DECK
//[Zone] ZoneChangeList.ProcessChanges() - id=7 local=False [name=Argent Commander id=16 zone=GRAVEYARD zonePos=3 cardId=EX1_067 player=1] zone from FRIENDLY HAND -> FRIENDLY GRAVEYARD
//[Zone] ZoneChangeList.ProcessChanges() - id=2 local=False [name=The Coin id=68 zone=HAND zonePos=5 cardId=GAME_005 player=1] zone from  -> FRIENDLY HAND
//[Zone] ZoneChangeList.ProcessChanges() - id=190 local=False [name=Knife Juggler id=25 zone=GRAVEYARD zonePos=0 cardId=NEW1_019 player=1] zone from FRIENDLY DECK -> FRIENDLY GRAVEYARD
        string LOG_LINE_PATTERN  = @"^\[Zone";
        string CARD_PLAY_PATTERN = @"\[name=(.*) id=\d+ zone=(PLAY|HAND).* HAND ";
        string CARD_DRAW_PATTERN = @"\[name=(.*) id=\d+ zone=(PLAY|HAND).* FRIENDLY DECK -> FRIENDLY HAND";
        string COIN_DRAW_PATTERN = @"\[name=The Coin.*  -> FRIENDLY HAND";
        string MULLIGAN_PATTERN  = @"\[name=(.*) id=\d+ zone=DECK.* FRIENDLY HAND -> FRIENDLY DECK";
        string DISCARD_PATTERN   = @"\[name=(.*) id=\d+ zone=GRAVEYARD.* FRIENDLY HAND -> FRIENDLY GRAVEYARD";
        string MILL_PATTERN      = @"\[name=(.*) id=\d+ zone=GRAVEYARD.* FRIENDLY DECK -> FRIENDLY GRAVEYARD";

        Conf conf = new Conf();
        public Subject<string> stream = new Subject<string>();
        TailThread tail;

        private IObservable<string> _logLines, _rawPlays, _rawDraws, _myPlays, _theirPlays, _myDraws, _myCoinDraws, _myMulligans, _myDiscards, _myMills;

        public EventStream()
        {
            tail = new TailThread(conf.LogPath(), new AppendTextDelegate(stream.OnNext));
            tail.Start();

            _logLines = stream.Where(x => Regex.IsMatch(x, LOG_LINE_PATTERN));
            _rawPlays = _logLines.Where(x => Regex.IsMatch(x, CARD_PLAY_PATTERN));
            _rawDraws = _logLines.Where(x => Regex.IsMatch(x, CARD_DRAW_PATTERN));

            _myPlays = _rawPlays
                .Where(x => Regex.IsMatch(x, "local=True"))
                .Select(x => Regex.Matches(x, CARD_PLAY_PATTERN)[0].Groups[1].Value);

            _theirPlays = _rawPlays
                .Where(x => Regex.IsMatch(x, "local=False"))
                .Select(x => Regex.Matches(x, CARD_PLAY_PATTERN)[0].Groups[1].Value);

            _myDraws = _rawDraws.Select(x => Regex.Matches(x, CARD_DRAW_PATTERN)[0].Groups[1].Value);

            _myCoinDraws = _logLines
                .Where(x => Regex.IsMatch(x, COIN_DRAW_PATTERN))
                .Select(_ => "The Coin");

            _myMulligans = _logLines
                .Where(x => Regex.IsMatch(x, MULLIGAN_PATTERN))
                .Select(x => Regex.Matches(x, MULLIGAN_PATTERN)[0].Groups[1].Value);

            _myDiscards = _logLines
                .Where(x => Regex.IsMatch(x, DISCARD_PATTERN))
                .Select(x => Regex.Matches(x, DISCARD_PATTERN)[0].Groups[1].Value);

            _myMills = _logLines
                .Where(x => Regex.IsMatch(x, MILL_PATTERN))
                .Select(x => Regex.Matches(x, MILL_PATTERN)[0].Groups[1].Value);                
        }

        public IObservable<string> MyPlays()
        {
            return _myPlays;
        }

        public IObservable<string> TheirPlays()
        {
            return _theirPlays;
        }

        public IObservable<string> MyDiscards()
        {
            return _myDiscards;
        }

        public IObservable<string> MyDraws()
        {
            return Observable.Merge(_myDraws, _myMills);
        }

        public IObservable<string> MyMulligans()
        {
            return _myMulligans;
        }
    }
}
