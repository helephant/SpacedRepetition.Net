using NUnit.Framework;

namespace SpacedRepetition.Net.Tests.Unit
{
    public class SpacedRepetitionItemTests
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
            var item = new SrsItemBuilder().Build();

            Assert.That(item.IsNewItem, Is.False);
        }
    }
}
