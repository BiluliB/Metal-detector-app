using System.Collections.Specialized;
using Magnetify.Common;

namespace Magnetify.Tests.Common
{
    public class BetterCollectionTests
    {
        [Fact]
        public void AddAtStart_AddsItemToStartOfCollection()
        {
            // Arrange
            var collection = new BetterCollection<int>(5);

            // Act
            collection.AddAtStart(1);

            // Assert
            Assert.Equal(1, collection[0]);
        }

        [Fact]
        public void AddAtStart_ExceedsMaxLength_RemovesLastItem()
        {
            // Arrange
            var collection = new BetterCollection<int>(3);
            collection.Add(1);
            collection.Add(2);
            collection.Add(3);

            // Act
            collection.AddAtStart(0);

            // Assert
            Assert.Equal(3, collection.Count);
            Assert.Equal(0, collection[0]);
            Assert.Equal(1, collection[1]);
            Assert.Equal(2, collection[2]);
        }

        [Fact]
        public void AddAtStart_NotifiesCollectionChanged()
        {
            // Arrange
            var collection = new BetterCollection<int>(3);
            bool eventFired = false;
            collection.CollectionChanged += (sender, args) =>
            {
                if (args.Action == NotifyCollectionChangedAction.Reset)
                {
                    eventFired = true;
                }
            };

            // Act
            collection.AddAtStart(1);

            // Assert
            Assert.True(eventFired);
        }

        [Fact]
        public void AddAtStart_NotifiesCollectionChangedOnce()
        {
            // Arrange
            var collection = new BetterCollection<int>(3);
            int notificationCount = 0;
            collection.CollectionChanged += (sender, args) =>
            {
                if (args.Action == NotifyCollectionChangedAction.Reset)
                {
                    notificationCount++;
                }
            };

            // Act
            collection.AddAtStart(1);

            // Assert
            Assert.Equal(1, notificationCount);
        }

        [Fact]
        public void TrimExcessItems_RemovesItemsBeyondMaxLength()
        {
            // Arrange
            var collection = new BetterCollection<int>(3);
            collection.Add(1);
            collection.Add(2);
            collection.Add(3);
            collection.Add(4); // This should trigger TrimExcessItems

            // Act
            collection.AddAtStart(0);

            // Assert
            Assert.Equal(3, collection.Count);
            Assert.Equal(0, collection[0]);
            Assert.Equal(1, collection[1]);
            Assert.Equal(2, collection[2]);
        }
    }
}
