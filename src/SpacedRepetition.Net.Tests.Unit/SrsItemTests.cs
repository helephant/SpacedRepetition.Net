using NUnit.Framework;

namespace SpacedRepetition.Net.Tests.Unit
{
    public class SrsItemTests
    {
        [Test]
        public void IsNewItem()
        {
            var item = new SrsItemBuilder().NeverReviewed().Build();

            Assert.That(item.IsNewItem, Is.True);
        }

        [Test]
        public void IsExistingItem()
        {
            var item = new SrsItemBuilder().Due().Build();

            Assert.That(item.IsNewItem, Is.False);
        }
    }
}
