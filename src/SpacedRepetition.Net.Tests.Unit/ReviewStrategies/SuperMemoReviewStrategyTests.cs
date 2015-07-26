using System;
using NUnit.Framework;
using SpacedRepetition.Net.ReviewStrategies;

namespace SpacedRepetition.Net.Tests.Unit.ReviewStrategies
{
    public class SuperMemoReviewStrategyTests
    {
        private readonly ClockStub _clock = new ClockStub(DateTime.Now);

        [Test]
        public void one_day_interval_for_items_without_correct_review()
        {
            var item = new ReviewItemBuilder().NeverReviewed().Build();
            var strategy = new SuperMemo2ReviewStrategy(_clock);

            var nextReview = strategy.NextReview(item);

            Assert.That(nextReview, Is.EqualTo(_clock.Now()));
        }

        [Test]
        public void six_day_interval_after_card_is_reviewed_correctly_once()
        {
            var item = new ReviewItemBuilder().WithLastReviewDate(_clock.Now().AddDays(-10)).WithCorrectReviewStreak(1).Build();
            var strategy = new SuperMemo2ReviewStrategy(_clock);

            var nextReview = strategy.NextReview(item);

            Assert.That(nextReview, Is.EqualTo(item.LastReviewDate.AddDays(6)));
        }

        [Test]
        public void short_interval_for_difficult_cards()
        {
            var easinessFactor = 1.3;
            var timesReviewed = 3;
            var lastReviewDate = _clock.Now().AddDays(-2);
            var item = new ReviewItemBuilder().WithLastReviewDate(lastReviewDate).WithDifficultyRating(DifficultyRating.MostDifficult).WithCorrectReviewStreak(timesReviewed).Build();
            var strategy = new SuperMemo2ReviewStrategy(_clock);

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
            var item = new ReviewItemBuilder().WithLastReviewDate(lastReviewDate).WithDifficultyRating(DifficultyRating.Easiest).WithCorrectReviewStreak(timesReviewed).Build();
            var strategy = new SuperMemo2ReviewStrategy(_clock);

            var nextReview = strategy.NextReview(item);

            var expectedInterval = lastReviewDate.AddDays((timesReviewed - 1) * easinessFactor);
            Assert.That(nextReview, Is.EqualTo(expectedInterval));
        }

        [Test]
        public void perfect_review_lowers_difficulty()
        {
            var item = new ReviewItemBuilder().Due().WithDifficultyRating(50).Build();
            var strategy = new SuperMemo2ReviewStrategy();

            var actualDifficulty = strategy.AdjustDifficulty(item, ReviewOutcome.Perfect);

            var expectedDifficulty = new DifficultyRating(41);
            Assert.That(actualDifficulty, Is.EqualTo(expectedDifficulty));
        }

        [Test]
        public void hesitant_review_leaves_difficulty_the_same()
        {
            var item = new ReviewItemBuilder().Due().WithDifficultyRating(50).Build();
            var strategy = new SuperMemo2ReviewStrategy();

            var actualDifficulty = strategy.AdjustDifficulty(item, ReviewOutcome.Hesitant);

            var expectedDifficulty = new DifficultyRating(50);
            Assert.That(actualDifficulty, Is.EqualTo(expectedDifficulty));
        }

        [Test]
        public void incorrect_review_increases_difficulty()
        {
            var item = new ReviewItemBuilder().Due().WithDifficultyRating(50).Build();
            var strategy = new SuperMemo2ReviewStrategy();

            var actualDifficulty = strategy.AdjustDifficulty(item, ReviewOutcome.Incorrect);

            var expectedDifficulty = new DifficultyRating(61);
            Assert.That(actualDifficulty, Is.EqualTo(expectedDifficulty));
        }

        [Test]
        public void difficulty_can_not_be_greater_than_100()
        {
            var item = new ReviewItemBuilder().Due().WithDifficultyRating(100).Build();
            var strategy = new SuperMemo2ReviewStrategy();

            var actualDifficulty = strategy.AdjustDifficulty(item, ReviewOutcome.Incorrect);

            var expectedDifficulty = new DifficultyRating(100);
            Assert.That(actualDifficulty, Is.EqualTo(expectedDifficulty));
        }

        [Test]
        public void difficulty_can_not_be_less_than_0()
        {
            var item = new ReviewItemBuilder().Due().WithDifficultyRating(0).Build();
            var strategy = new SuperMemo2ReviewStrategy();

            var actualDifficulty = strategy.AdjustDifficulty(item, ReviewOutcome.Perfect);

            var expectedDifficulty = new DifficultyRating(0);
            Assert.That(actualDifficulty, Is.EqualTo(expectedDifficulty));
        }
    }
}
