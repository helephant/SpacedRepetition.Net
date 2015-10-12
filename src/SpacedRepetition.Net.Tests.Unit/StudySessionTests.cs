using System;
using System.Linq;
using NUnit.Framework;
using SpacedRepetition.Net.ReviewStrategies;

namespace SpacedRepetition.Net.Tests.Unit
{
    public class StudySessionTests
    {
        private const int _maxNewCardsPerSession = 5;
        private const int _maxExistingCardsPerSession = 7;
        private readonly ClockStub _clock = new ClockStub(DateTime.Now);

        [TestCase(ReviewOutcome.Perfect)]
        [TestCase(ReviewOutcome.Hesitant)]
        public void correct_review_outcome_increments_CorrectReviewStreak(ReviewOutcome outcome)
        {
            var correctReviewStreak = 3;
            var item = new ReviewItemBuilder().Due().WithCorrectReviewStreak(correctReviewStreak).Build();

            var session = new StudySession<ReviewItem>(new[] { item });
            session.Review(item, outcome);

            Assert.That(item.CorrectReviewStreak, Is.EqualTo(correctReviewStreak + 1));
        }

        [Test]
        public void incorrect_review_resets_CorrectReviewStreak()
        {
            var correctReviewStreak = 3;
            var item = new ReviewItemBuilder().Due().WithCorrectReviewStreak(correctReviewStreak).Build();

            var session = new StudySession<ReviewItem>(new[] { item });
            session.Review(item, ReviewOutcome.Incorrect);

            Assert.That(item.CorrectReviewStreak, Is.EqualTo(0));
        }

        [TestCase(ReviewOutcome.Perfect)]
        [TestCase(ReviewOutcome.Hesitant)]
        [TestCase(ReviewOutcome.Incorrect)]
        public void reviewing_updates_LastReviewDate_to_now(ReviewOutcome outcome)
        {
            var item = new ReviewItemBuilder().Due().Build();

            var session = new StudySession<ReviewItem>(new[] {item}) {Clock = _clock};
            session.Review(item, outcome);

            Assert.That(item.ReviewDate, Is.EqualTo(_clock.Now()));
        }

        [TestCase(ReviewOutcome.Perfect)]
        [TestCase(ReviewOutcome.Hesitant)]
        [TestCase(ReviewOutcome.Incorrect)]
        public void reviewing_updates_DifficultyRating_based_on_review_strategy(ReviewOutcome outcome)
        {
            var item = new ReviewItemBuilder().Due().WithDifficultyRating(DifficultyRating.MostDifficult).Build();

            var session = new StudySession<ReviewItem>(new[] { item }) { ReviewStrategy = new SimpleReviewStrategy() };
            session.Review(item, outcome);

            Assert.That(item.DifficultyRating, Is.EqualTo(DifficultyRating.Easiest));
        }

        [TestCase(ReviewOutcome.Perfect)]
        [TestCase(ReviewOutcome.Hesitant)]
        public void correct_items_are_removed_from_review_queue(ReviewOutcome outcome)
        {
            var items = new ReviewItemListBuilder()
                            .WithDueItems(1)
                            .Build();
            var session = new StudySession<ReviewItem>(items);

            var item = session.First();
            session.Review(item, outcome);

            Assert.That(session.Count(), Is.EqualTo(0));
        }

        [Test]
        public void incorrect_items_stay_in_review_queue()
        {
            var items = new ReviewItemListBuilder()
                            .WithDueItems(1)
                            .Build();
            var session = new StudySession<ReviewItem>(items);

            var item = session.First();
            session.Review(item, ReviewOutcome.Incorrect);

            Assert.That(session.First(), Is.EqualTo(item));
        }

        [Test]
        public void incorrect_items_stay_in_review_queue_until_correct()
        {
            var items = new ReviewItemListBuilder()
                            .WithDueItems(1)
                            .Build();
            var session = new StudySession<ReviewItem>(items);

            var incorrectTimes = 0;
            foreach (var reviewItem in session)
            {
                if (incorrectTimes++ < 3)
                    session.Review(reviewItem, ReviewOutcome.Incorrect);
                else break;
            }

            session.Review(session.First(), ReviewOutcome.Perfect);

            Assert.That(session, Is.Empty);
        }

        [Test]
        public void only_return_due_items()
        {
            var dueItems = 2;
            var items = new ReviewItemListBuilder()
                            .WithDueItems(dueItems)
                            .WithFutureItems(3)
                            .Build();
            var session = new StudySession<ReviewItem>(items);

            Assert.That(session.Count(), Is.EqualTo(dueItems));
        }

        [Test]
        public void limit_new_cards_per_session()
        {
            var items = new ReviewItemListBuilder().WithNewItems(_maxNewCardsPerSession + 1).Build();
            var session = new StudySession<ReviewItem>(items) { MaxNewCards = _maxNewCardsPerSession };

            Assert.That(session.Count(), Is.EqualTo(_maxNewCardsPerSession));
        }

        [Test]
        public void limit_new_cards_when_there_are_also_existing_cards()
        {
            var items = new ReviewItemListBuilder()
                .WithNewItems(_maxNewCardsPerSession - 1)
                .WithExistingItems(1)
                .WithNewItems(2)
                .Build();

            var session = new StudySession<ReviewItem>(items) { MaxNewCards = _maxNewCardsPerSession };

            Assert.That(session.Count(x => x.ReviewDate == DateTime.MinValue), Is.EqualTo(_maxNewCardsPerSession));
        }

        [Test]
        public void limit_existing_cards_per_session()
        {
            var items = new ReviewItemListBuilder().WithExistingItems(_maxExistingCardsPerSession + 1).Build();
            var session = new StudySession<ReviewItem>(items) { MaxExistingCards = _maxExistingCardsPerSession };

            Assert.That(session.Count(), Is.EqualTo(_maxExistingCardsPerSession));
        }

        [Test]
        public void limit_existing_cards_when_there_are_also_new_cards()
        {
            var items = new ReviewItemListBuilder()
                               .WithExistingItems(_maxExistingCardsPerSession - 1)
                               .WithNewItems(1)
                               .WithExistingItems(2)
                               .Build();

            var session = new StudySession<ReviewItem>(items) { MaxExistingCards = _maxExistingCardsPerSession};

            Assert.That(session.Count(x => x.ReviewDate != DateTime.MinValue), Is.EqualTo(_maxExistingCardsPerSession));
        }

        [Test]
        public void item_is_due_for_review()
        {
            var items = new ReviewItemListBuilder()
                   .WithNewItems(1)
                   .Build();

            var session = new StudySession<ReviewItem>(items);

            Assert.That(session.IsDue(items.First()));

        }

        [Test]
        public void item_is_not_due_for_review()
        {
            var items = new ReviewItemListBuilder()
                   .WithFutureItems(1)
                   .Build();

            var session = new StudySession<ReviewItem>(items);

            Assert.That(session.IsDue(items.First()), Is.False);

        }
    }
}
