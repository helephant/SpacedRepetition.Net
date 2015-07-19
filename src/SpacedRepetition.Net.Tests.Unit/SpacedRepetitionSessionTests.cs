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
            var set = new SpacedRepetitionSession<SpacedRepetitionItem>(new[] { item });

            var next = set.Next();
            Assert.That(next, Is.EqualTo(item));
        }

        [Test]
        public void limit_new_cards_per_session()
        {
            var items = new SrsItemListBuilder().WithNewItems(_maxNewCardsPerSession + 1).Build();
            var set = new SpacedRepetitionSession<SpacedRepetitionItem>(items) { MaxNewCards = _maxNewCardsPerSession };

            var newCards = 0;
            while (set.Next() != null)
                newCards++; 

            Assert.That(newCards, Is.EqualTo(_maxNewCardsPerSession));
        }

        [Test]
        public void limit_new_cards_when_there_are_also_existing_cards()
        {
            var items = new SrsItemListBuilder()
                .WithNewItems(_maxNewCardsPerSession - 1)
                .WithItemsWaitingReview(_maxExistingCardsPerSession + 1)
                .WithNewItems(2)
                .Build();

            var set = new SpacedRepetitionSession<SpacedRepetitionItem>(items) { MaxNewCards = _maxNewCardsPerSession };

            var newCards = 0;
            SpacedRepetitionItem item;
            while ((item = set.Next()) != null)
            {
                if (item.IsNewItem)
                    newCards++;
            }

            Assert.That(newCards, Is.EqualTo(_maxNewCardsPerSession));
        }

        [Test]
        public void limit_existing_cards_per_session()
        {
            var items = new SrsItemListBuilder().WithItemsWaitingReview(_maxExistingCardsPerSession + 1).Build();
            var set = new SpacedRepetitionSession<SpacedRepetitionItem>(items) { MaxExistingCards = _maxExistingCardsPerSession };

            var existingCards = 0;
            while (set.Next() != null)
                    existingCards++;

            Assert.That(existingCards, Is.EqualTo(_maxExistingCardsPerSession));
        }

        [Test]
        public void limit_existing_cards_when_there_are_also_new_cards()
        {
            var items = new SrsItemListBuilder()
                               .WithItemsWaitingReview(_maxExistingCardsPerSession - 1)
                               .WithNewItems(_maxNewCardsPerSession + 1)
                               .WithItemsWaitingReview(2)
                               .Build();

            var set = new SpacedRepetitionSession<SpacedRepetitionItem>(items) { MaxExistingCards = _maxExistingCardsPerSession};

            var existingCards = 0;
            SpacedRepetitionItem item;
            while ((item = set.Next()) != null)
            {
                if (!item.IsNewItem)
                    existingCards++;
            }

            Assert.That(existingCards, Is.EqualTo(_maxExistingCardsPerSession));
        }
    }
}
