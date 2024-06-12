using Magnetify.Common;
using Magnetify.Data;

namespace Magnetify.Tests.Common
{
    public class BetterCollectionFadeOutTests
    {
        [Fact]
        public void InitializationTest()
        {
            // Arrange
            int maxLength = 5;

            // Act
            var collection = new BetterCollectionFadeOut(maxLength);

            // Assert
            Assert.Equal(0, collection.Count);
        }

        [Fact]
        public void AddAtStart_AddsItemAtStart()
        {
            // Arrange
            int maxLength = 5;
            var collection = new BetterCollectionFadeOut(maxLength);
            var item1 = new OpacityItem();
            var item2 = new OpacityItem();

            // Act
            collection.AddAtStart(item1);
            collection.AddAtStart(item2);

            // Assert
            Assert.Equal(item2, collection[0]);
            Assert.Equal(item1, collection[1]);
        }

        [Fact]
        public void AddAtStart_DoesNotExceedMaxLength()
        {
            // Arrange
            int maxLength = 2;
            var collection = new BetterCollectionFadeOut(maxLength);
            var item1 = new OpacityItem();
            var item2 = new OpacityItem();
            var item3 = new OpacityItem();

            // Act
            collection.AddAtStart(item1);
            collection.AddAtStart(item2);
            collection.AddAtStart(item3);

            // Assert
            Assert.Equal(maxLength, collection.Count);
        }

        [Fact]
        public void AddAtStart_UpdatesOpacity()
        {
            // Arrange
            int maxLength = 5;
            var collection = new BetterCollectionFadeOut(maxLength);
            var items = new[]
            {
                new OpacityItem(),
                new OpacityItem(),
                new OpacityItem(),
                new OpacityItem(),
                new OpacityItem()
            };

            // Act
            foreach (var item in items)
            {
                collection.AddAtStart(item);
            }

            // Assert
            for (var i = 0; i < items.Length; i++)
            {
                double expectedOpacity = 0.9 - (0.75 / items.Length) * i;
                Assert.Equal(expectedOpacity, collection[i].Opacity, 3);
            }
        }

        [Fact]
        public void TrimExcessItems_RemovesExcessItems()
        {
            // Arrange
            int maxLength = 2;
            var collection = new BetterCollectionFadeOut(maxLength);
            var item1 = new OpacityItem();
            var item2 = new OpacityItem();
            var item3 = new OpacityItem();

            // Act
            collection.AddAtStart(item1);
            collection.AddAtStart(item2);
            collection.AddAtStart(item3);

            // Assert
            Assert.Equal(2, collection.Count);
            Assert.DoesNotContain(item1, collection);
        }

        [Fact]
        public void AddAtStart_FiresCollectionChangedOnce()
        {
            // Arrange
            int maxLength = 5;
            var collection = new BetterCollectionFadeOut(maxLength);
            var item = new OpacityItem();
            int eventCallCount = 0;
            collection.CollectionChanged += (sender, args) => eventCallCount++;

            // Act
            collection.AddAtStart(item);

            // Assert
            Assert.Equal(1, eventCallCount);
        }
    }
}
