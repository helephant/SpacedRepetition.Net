using System;
using NUnit.Framework;
using SpacedRepetition.Net.ReviewStrategies;

namespace SpacedRepetition.Net.Tests.Unit
{
    public class SuperMemoSrsStrategyTests
    {
        private readonly ClockStub _clock = new ClockStub(DateTime.Now);

        [Test]
        public void one_day_interval_for_items_without_correct_review()
        {
            var item = new SrsItemBuilder().NeverReviewed().Build();
            var strategy = new SuperMemo2SrsStrategy(_clock);

            var nextReview = strategy.NextReview(item);

            Assert.That(nextReview, Is.EqualTo(_clock.Now()));
        }

        [Test]
        public void six_day_interval_after_card_is_reviewed_correctly_once()
        {
            var item = new SrsItemBuilder().WithLastReviewDate(_clock.Now().AddDays(-10)).WithCorrectReviewStreak(1).Build();
            var strategy = new SuperMemo2SrsStrategy(_clock);

            var nextReview = strategy.NextReview(item);

            Assert.That(nextReview, Is.EqualTo(item.LastReviewDate.AddDays(6)));
        }

        [Test]
        public void short_interval_for_difficult_cards()
        {
            var easinessFactor = 1.3;
            var timesReviewed = 3;
            var lastReviewDate = _clock.Now().AddDays(-2);
            var item = new SrsItemBuilder().WithLastReviewDate(lastReviewDate).WithDifficultyRating(DifficultyRating.MostDifficult).WithCorrectReviewStreak(timesReviewed).Build();
            var strategy = new SuperMemo2SrsStrategy(_clock);

            var nextReview = strategy.NextReview(item);

            var expectedInterval = lastReviewDate.AddDays((timesReviewed - 1) * easinessFactor);
            Assert.That(nextReview, Is.EqualTo(expectedInterval));
        }

        [Test]
        public void long_interval_for_easy_cards()
        {
            var easinessFactor = 2.5;
            var timesReviewed = 30;
            var lastReviewDate = _clock.Now().AddDays(-2);
            var item = new SrsItemBuilder().WithLastReviewDate(lastReviewDate).WithDifficultyRating(DifficultyRating.Easiest).WithCorrectReviewStreak(timesReviewed).Build();
            var strategy = new SuperMemo2SrsStrategy(_clock);

            var nextReview = strategy.NextReview(item);

            var expectedInterval = lastReviewDate.AddDays((timesReviewed - 1) * easinessFactor);
            Assert.That(nextReview, Is.EqualTo(expectedInterval));
        }
    }
}
