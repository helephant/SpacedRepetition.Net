using System.Linq;
using NUnit.Framework;

namespace SpacedRepetition.Net.Tests.Unit
{
    public class SpacedRepetitionSessionTests
    {
        private const int _maxNewCardsPerSession = 5;
        private const int _maxExistingCardsPerSession = 7;

        [Test]
        public void load_next_item()
        {
            var item = new SrsItemBuilder().Build();
            var session = new SpacedRepetitionSession<SpacedRepetitionItem>(new[] { item });

            Assert.That(session.First(), Is.EqualTo(item));
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
                .WithItemsWaitingReview(1)
                .WithNewItems(2)
                .Build();

            var session = new SpacedRepetitionSession<SpacedRepetitionItem>(items) { MaxNewCards = _maxNewCardsPerSession };

            Assert.That(session.Count(x => x.IsNewItem), Is.EqualTo(_maxNewCardsPerSession));
        }

        [Test]
        public void limit_existing_cards_per_session()
        {
            var items = new SrsItemListBuilder().WithItemsWaitingReview(_maxExistingCardsPerSession + 1).Build();
            var session = new SpacedRepetitionSession<SpacedRepetitionItem>(items) { MaxExistingCards = _maxExistingCardsPerSession };

            Assert.That(session.Count(), Is.EqualTo(_maxExistingCardsPerSession));
        }

        [Test]
        public void limit_existing_cards_when_there_are_also_new_cards()
        {
            var items = new SrsItemListBuilder()
                               .WithItemsWaitingReview(_maxExistingCardsPerSession - 1)
                               .WithNewItems(1)
                               .WithItemsWaitingReview(2)
                               .Build();

            var session = new SpacedRepetitionSession<SpacedRepetitionItem>(items) { MaxExistingCards = _maxExistingCardsPerSession};

            Assert.That(session.Count(x => !x.IsNewItem), Is.EqualTo(_maxExistingCardsPerSession));
        }
    }
}
