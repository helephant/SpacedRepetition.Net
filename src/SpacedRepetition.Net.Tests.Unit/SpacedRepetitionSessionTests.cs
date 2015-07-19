using System;
using System.Linq;
using NUnit.Framework;
using SpacedRepetition.Net.ReviewStrategies;

namespace SpacedRepetition.Net.Tests.Unit
{
    public class SpacedRepetitionSessionTests
    {
        private const int _maxNewCardsPerSession = 5;
        private const int _maxExistingCardsPerSession = 7;
        private readonly ClockStub _clock = new ClockStub(DateTime.Now);

        [TestCase(SrsAnswer.Perfect)]
        [TestCase(SrsAnswer.Hesitant)]
        public void correct_answer_increments_CorrectReviewStreak(SrsAnswer answer)
        {
            var correctReviewStreak = 3;
            var item = new SrsItemBuilder().Due().WithCorrectReviewStreak(correctReviewStreak).Build();

            var session = new SpacedRepetitionSession<SpacedRepetitionItem>(new[] { item });
            session.Answer(item, answer);

            Assert.That(item.CorrectReviewStreak, Is.EqualTo(correctReviewStreak + 1));
        }

        [Test]
        public void incorrect_answer_resets_CorrectReviewStreak()
        {
            var correctReviewStreak = 3;
            var item = new SrsItemBuilder().Due().WithCorrectReviewStreak(correctReviewStreak).Build();

            var session = new SpacedRepetitionSession<SpacedRepetitionItem>(new[] { item });
            session.Answer(item, SrsAnswer.Incorrect);

            Assert.That(item.CorrectReviewStreak, Is.EqualTo(0));
        }

        [TestCase(SrsAnswer.Perfect)]
        [TestCase(SrsAnswer.Hesitant)]
        [TestCase(SrsAnswer.Incorrect)]
        public void answering_updates_LastReviewDate_to_now(SrsAnswer answer)
        {
            var item = new SrsItemBuilder().Due().Build();

            var session = new SpacedRepetitionSession<SpacedRepetitionItem>(new[] {item}) {Clock = _clock};
            session.Answer(item, answer);

            Assert.That(item.LastReviewDate, Is.EqualTo(_clock.Now()));
        }

        [TestCase(SrsAnswer.Perfect)]
        [TestCase(SrsAnswer.Hesitant)]
        [TestCase(SrsAnswer.Incorrect)]
        public void answering_updates_DifficultyRating_based_on_review_strategy(SrsAnswer answer)
        {
            var item = new SrsItemBuilder().Due().WithDifficultyRating(DifficultyRating.MostDifficult).Build();

            var session = new SpacedRepetitionSession<SpacedRepetitionItem>(new[] { item }) { ReviewStrategy = new SimpleReviewStrategy() };
            session.Answer(item, answer);

            Assert.That(item.DifficultyRating, Is.EqualTo(DifficultyRating.Easiest));
        }

        [Test]
        public void only_return_due_items()
        {
            var dueItems = 2;
            var items = new SrsItemListBuilder()
                            .WithDueItems(dueItems)
                            .WithFutureItems(3)
                            .Build();
            var session = new SpacedRepetitionSession<SpacedRepetitionItem>(items);

            Assert.That(session.Count(), Is.EqualTo(dueItems));
        }

        [Test]
        public void limit_new_cards_per_session()
        {
            var items = new SrsItemListBuilder().WithNewItems(_maxNewCardsPerSession + 1).Build();
            var session = new SpacedRepetitionSession<SpacedRepetitionItem>(items) { MaxNewCards = _maxNewCardsPerSession };

            Assert.That(session.Count(), Is.EqualTo(_maxNewCardsPerSession));
        }

        [Test]
        public void limit_new_cards_when_there_are_also_existing_cards()
        {
            var items = new SrsItemListBuilder()
                .WithNewItems(_maxNewCardsPerSession - 1)
                .WithExistingItems(1)
                .WithNewItems(2)
                .Build();

            var session = new SpacedRepetitionSession<SpacedRepetitionItem>(items) { MaxNewCards = _maxNewCardsPerSession };

            Assert.That(session.Count(x => x.IsNewItem), Is.EqualTo(_maxNewCardsPerSession));
        }

        [Test]
        public void limit_existing_cards_per_session()
        {
            var items = new SrsItemListBuilder().WithExistingItems(_maxExistingCardsPerSession + 1).Build();
            var session = new SpacedRepetitionSession<SpacedRepetitionItem>(items) { MaxExistingCards = _maxExistingCardsPerSession };

            Assert.That(session.Count(), Is.EqualTo(_maxExistingCardsPerSession));
        }

        [Test]
        public void limit_existing_cards_when_there_are_also_new_cards()
        {
            var items = new SrsItemListBuilder()
                               .WithExistingItems(_maxExistingCardsPerSession - 1)
                               .WithNewItems(1)
                               .WithExistingItems(2)
                               .Build();

            var session = new SpacedRepetitionSession<SpacedRepetitionItem>(items) { MaxExistingCards = _maxExistingCardsPerSession};

            Assert.That(session.Count(x => !x.IsNewItem), Is.EqualTo(_maxExistingCardsPerSession));
        }
    }
}
