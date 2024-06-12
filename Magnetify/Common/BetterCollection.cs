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
    /// <summary>
    /// Better collection that allows to add items at the start and removes the last item if the collection exceeds the maximum length.
    /// The collection notifies about the changes after all modifications.
    /// </summary>
    /// <typeparam name="T">The type of the items in the collection.</typeparam>
    public class BetterCollection<T> : ObservableCollection<T>
    {
        /// <summary>
        /// The maximum length of the collection.
        /// </summary>
        private readonly int _maxLength;

        /// <summary>
        /// Initializes a new instance of the <see cref="BetterCollection{T}"/> class.
        /// </summary>
        /// <param name="maxLength">The maximum length of the collection.</param>
        public BetterCollection(int maxLength)
        {
            _maxLength = maxLength;
        }

        /// <summary>
        /// Adds an item at the start of the collection.
        /// Shifts the existing items to the right, removes the last item if the collection exceeds the maximum length.
        /// Change notifications are sent after all modifications.
        /// </summary>
        /// <param name="item">Item to add at the start of the collection.</param>
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
