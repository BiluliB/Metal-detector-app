using Magnetify.Data;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Magnetify.Common
{
    public class BetterCollectionFadeOut : ObservableCollection<OpacityItem>
    {
        private readonly int _maxLength;

        public BetterCollectionFadeOut(int maxLength)
        {
            _maxLength = maxLength;
        }

        public void AddAtStart(OpacityItem item)
        {
            CheckReentrancy();

            // Insert the new item at the start
            InsertItem(0, item);

            // Check if the collection exceeds the maximum length
            TrimExcessItems();

            for (var i = 0; i < Count; i++)
            {
                this[i].Opacity = 0.9 - (0.75 / Count) * i;
            }

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
