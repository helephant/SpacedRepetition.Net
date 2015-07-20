using NUnit.Framework;

namespace SpacedRepetition.Net.Tests.Unit
{
    public class ReviewItemTests
    {
        [Test]
        public void IsNewItem()
        {
            var item = new ReviewItemBuilder().NeverReviewed().Build();

            Assert.That(item.IsNewItem, Is.True);
        }

        [Test]
        public void IsExistingItem()
        {
            var item = new ReviewItemBuilder().Due().Build();

            Assert.That(item.IsNewItem, Is.False);
        }
    }
}
