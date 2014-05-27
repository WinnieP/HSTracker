using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace HSTracker
{
    public class DeckCreationCard : INotifyPropertyChanged
    {
        public DeckCreationCard(string name)
        {
            Name = name;
            Count = 1;
        }

        public string Name { get; set; }
        public uint Count { get; set; }

        public void Add()
        {
            if (Count < 2)
            {
                Count++;
                RaisePropertyChanged("Count");
            }
        }

        public void Remove()
        {
            if (Count > 0)
            {
                Count--;
                RaisePropertyChanged("Count");
            }
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
