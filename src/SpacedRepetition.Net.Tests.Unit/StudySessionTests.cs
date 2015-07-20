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

        [TestCase(ReviewAnswer.Perfect)]
        [TestCase(ReviewAnswer.Hesitant)]
        public void correct_answer_increments_CorrectReviewStreak(ReviewAnswer answer)
        {
            var correctReviewStreak = 3;
            var item = new ReviewItemBuilder().Due().WithCorrectReviewStreak(correctReviewStreak).Build();

            var session = new StudySession<ReviewItem>(new[] { item });
            session.Answer(item, answer);

            Assert.That(item.CorrectReviewStreak, Is.EqualTo(correctReviewStreak + 1));
        }

        [Test]
        public void incorrect_answer_resets_CorrectReviewStreak()
        {
            var correctReviewStreak = 3;
            var item = new ReviewItemBuilder().Due().WithCorrectReviewStreak(correctReviewStreak).Build();

            var session = new StudySession<ReviewItem>(new[] { item });
            session.Answer(item, ReviewAnswer.Incorrect);

            Assert.That(item.CorrectReviewStreak, Is.EqualTo(0));
        }

        [TestCase(ReviewAnswer.Perfect)]
        [TestCase(ReviewAnswer.Hesitant)]
        [TestCase(ReviewAnswer.Incorrect)]
        public void answering_updates_LastReviewDate_to_now(ReviewAnswer answer)
        {
            var item = new ReviewItemBuilder().Due().Build();

            var session = new StudySession<ReviewItem>(new[] {item}) {Clock = _clock};
            session.Answer(item, answer);

            Assert.That(item.LastReviewDate, Is.EqualTo(_clock.Now()));
        }

        [TestCase(ReviewAnswer.Perfect)]
        [TestCase(ReviewAnswer.Hesitant)]
        [TestCase(ReviewAnswer.Incorrect)]
        public void answering_updates_DifficultyRating_based_on_review_strategy(ReviewAnswer answer)
        {
            var item = new ReviewItemBuilder().Due().WithDifficultyRating(DifficultyRating.MostDifficult).Build();

            var session = new StudySession<ReviewItem>(new[] { item }) { ReviewStrategy = new SimpleReviewStrategy() };
            session.Answer(item, answer);

            Assert.That(item.DifficultyRating, Is.EqualTo(DifficultyRating.Easiest));
        }

        [TestCase(ReviewAnswer.Perfect)]
        [TestCase(ReviewAnswer.Hesitant)]
        public void correct_items_are_removed_from_review_queue(ReviewAnswer answer)
        {
            var items = new ReviewItemListBuilder()
                            .WithDueItems(1)
                            .Build();
            var session = new StudySession<ReviewItem>(items);

            var item = session.First();
            session.Answer(item, answer);

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
            session.Answer(item, ReviewAnswer.Incorrect);

            Assert.That(session.First(), Is.EqualTo(item));
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

            Assert.That(session.Count(x => x.IsNewItem), Is.EqualTo(_maxNewCardsPerSession));
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

            Assert.That(session.Count(x => !x.IsNewItem), Is.EqualTo(_maxExistingCardsPerSession));
        }
    }
}
