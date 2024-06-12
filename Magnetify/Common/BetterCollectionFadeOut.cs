using Magnetify.Data;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Magnetify.Common
{
    /// <summary>
    /// Better collection that allows to add items at the start and removes the last item if the collection exceeds the maximum length.
    /// The collection notifies about the changes after all modifications.
    /// All items are of type <see cref="OpacityItem"/> and their opacity is updated to fade out the older items.
    /// </summary>
    public class BetterCollectionFadeOut : ObservableCollection<OpacityItem>
    {
        private readonly int _maxLength;

        /// <summary>
        /// Initializes a new instance of the <see cref="BetterCollectionFadeOut"/> class.
        /// </summary>
        /// <param name="maxLength">The maximum length of the collection.</param>
        public BetterCollectionFadeOut(int maxLength)
        {
            _maxLength = maxLength;
        }

        /// <summary>
        /// Adds an item at the start of the collection.
        /// Shifts the existing items to the right, removes the last item if the collection exceeds the maximum length.
        /// Change notifications are sent after all modifications.
        /// Updates the opacity of the items in the collection to fade out the older items.
        /// </summary>
        /// <param name="item">Item to add at the start of the collection.</param>
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

        /// <summary>
        /// Trims the collection if it exceeds the maximum length.
        /// </summary>
        private void TrimExcessItems()
        {
            while (Count > _maxLength)
            {
                RemoveItem(Count - 1); // Remove the last item
            }
        }
    }
}
