using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace HSTracker
{
    class Card : INotifyPropertyChanged
    {
        private string _name;
        private uint _mana;
        private uint _count;
        private uint _maxCount;
        
        public Card(string name, uint mana, uint count)
        {
            _name = name;
            _mana = mana;
            _count = _maxCount = count;
        }

        public string Name
        {
            get { return _name; }
        }

        public uint Mana
        {
            get { return _mana; }
        }

        public uint Count
        {
            get { return _count; }
        }

        public uint MaxCount
        {
            get { return _maxCount; }
        }

        public void Play()
        {
            _count--;
            this.RaisePropertyChanged("Count");
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
