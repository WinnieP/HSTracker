using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using System.Windows;

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

        #region Properties

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

        public bool NoneDrawn
        {
            get { return _count == _maxCount && _count > 0; }
        }

        public bool SomeDrawn
        {
            get { return _count == 1 && _maxCount == 2; }
        }

        #endregion

        public void Draw()
        {
            _count--;
            this.RaisePropertyChanged("Count");
            this.RaisePropertyChanged("NoneDrawn");
            this.RaisePropertyChanged("SomeDrawn");
        }

        public void Restore()
        {
            _count++;
            this.RaisePropertyChanged("Count");
            this.RaisePropertyChanged("NoneDrawn");
            this.RaisePropertyChanged("SomeDrawn");
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
