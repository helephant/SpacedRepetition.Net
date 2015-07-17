using NUnit.Framework;
using SpacedRepetition.Net.IntervalStrategies;

namespace SpacedRepetition.Net.Tests.Unit
{
    public class SpacedRepetitionSetTests
    {
        [Test]
        public void LoadNextCard()
        {
            var item = new SpacedRepetitionItem();
            var strategy = new NowIntervalStrategy();
            var set = new SpacedRepetitionSet<SpacedRepetitionItem>(new[] { item }, strategy);

            var next = set.Next();
            Assert.That(next, Is.EqualTo(item));
        }
    }
}
