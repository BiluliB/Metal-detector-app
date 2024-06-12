using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magnetify.Common
{
    public class BetterCollection<T> : ObservableCollection<T>
    {
        private readonly int _maxLength;

        public BetterCollection(int maxLength)
        {
            _maxLength = maxLength;
        }

        public void AddAtStart(T item)
        {
            CheckReentrancy();

            // Insert the new item at the start
            InsertItem(0, item);

            // Check if the collection exceeds the maximum length
            TrimExcessItems();

            // Notify about the changes after all modifications
            //OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            //OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        private void TrimExcessItems()
        {
            while (Count > _maxLength)
            {
                RemoveItem(Count - 1); // Remove the last item
            }
        }
    }
}
