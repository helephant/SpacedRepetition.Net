using System;
using NUnit.Framework;
using SpacedRepetition.Net.IntervalStrategies;

namespace SpacedRepetition.Net.Tests.Unit
{
    public class SuperMemoSrsStrategyTests
    {
        private readonly ClockStub _clock = new ClockStub(DateTime.Now);

        [Test]
        public void ItemsWithNoCorrectReviewAreDueImmediately()
        {
            var item = new SrsItemBuilder().NeverReviewed().Build();
            var strategy = new SuperMemo2SrsStrategy(_clock);

            var nextReview = strategy.NextReview(item);

            Assert.That(nextReview, Is.EqualTo(_clock.Now()));
        }

        [Test]
        public void ItemsThatHaveBeenReviewedCorrectlyOnceAreDueInSixDaysFromFirstReview()
        {
            var item = new SrsItemBuilder().WithLastReviewDate(_clock.Now()).WithCorrectReviewStreak(1).Build();
            var strategy = new SuperMemo2SrsStrategy(_clock);

            var nextReview = strategy.NextReview(item);

            Assert.That(nextReview, Is.EqualTo(_clock.Now().AddDays(6)));
        }

        [Test]
        public void ShortIntervalForCardWithLowTimesReviewedAndLowEasinessFactor()
        {
            var easinessFactor = 1.3;
            var timesReviewed = 3;
            var lastReviewDate = _clock.Now().AddDays(-2);
            var item = new SrsItemBuilder().WithLastReviewDate(lastReviewDate).WithEasinessFactor(easinessFactor).WithCorrectReviewStreak(timesReviewed).Build();
            var strategy = new SuperMemo2SrsStrategy(_clock);

            var nextReview = strategy.NextReview(item);

            var expectedInterval = lastReviewDate.AddDays((timesReviewed - 1) * easinessFactor);
            Assert.That(nextReview, Is.EqualTo(expectedInterval));
        }

        [Test]
        public void LongIntervalForCardWithHighTimesReviewedAndHighEasinessFactor()
        {
            var easinessFactor = 2.5;
            var timesReviewed = 30;
            var lastReviewDate = _clock.Now().AddDays(-2);
            var item = new SrsItemBuilder().WithLastReviewDate(lastReviewDate).WithEasinessFactor(easinessFactor).WithCorrectReviewStreak(timesReviewed).Build();
            var strategy = new SuperMemo2SrsStrategy(_clock);

            var nextReview = strategy.NextReview(item);

            var expectedInterval = lastReviewDate.AddDays((timesReviewed - 1) * easinessFactor);
            Assert.That(nextReview, Is.EqualTo(expectedInterval));
        }
    }
}
