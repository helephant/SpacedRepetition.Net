using System;
using NUnit.Framework;
using SpacedRepetition.Net.IntervalStrategies;

namespace SpacedRepetition.Net.Tests.Unit
{
    public class SuperMemoSrsStrategyTests
    {
        private readonly ClockStub _clock = new ClockStub(DateTime.Now);

        [Test]
        public void Test()
        {
            var item = new SpacedRepetitionItem();
            var strategy = new SuperMemo2SrsStrategy();
            var set = new SpacedRepetitionSet<SpacedRepetitionItem>(new[] { item }, strategy);

            var next = set.Next();
            Assert.That(next, Is.EqualTo(item));
        }

        [Test]
        public void ItemsThatHaveNeverBeenReviewedAreDueImmediately()
        {
            var item = new SrsItemBuilder().NeverReviewed().Build();
            var strategy = new SuperMemo2SrsStrategy(_clock);

            var nextReview = strategy.NextReview(item);

            Assert.That(nextReview, Is.EqualTo(_clock.Now()));
        }

        [Test]
        public void ItemsThatHaveBeenReviewedOnceAreDueInSixDaysFromFirstReview()
        {
            var item = new SrsItemBuilder().WithLastReviewDate(_clock.Now()).WithTimesReviewed(1).Build();
            var strategy = new SuperMemo2SrsStrategy(_clock);

            var nextReview = strategy.NextReview(item);

            Assert.That(nextReview, Is.EqualTo(_clock.Now().AddDays(6)));
        }
    }

    public class SuperMemo2SrsStrategy : IIntervalStrategy
    {
        private readonly IClock _clock;

        public SuperMemo2SrsStrategy() : this(new Clock())
        {
        }

        public SuperMemo2SrsStrategy(IClock clock)
        {
            _clock = clock;
        }

        public DateTime NextReview(ISpacedRepetitionItem item)
        {
            if(item.TimesReviewed == 0)
                return _clock.Now();

            return _clock.Now().AddDays(6);
        }
    }
}
