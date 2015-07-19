using System;
using System.Collections.Generic;
using System.Linq;

namespace SpacedRepetition.Net.Tests.Unit
{
    public class SrsItemListBuilder
    {
        private DateTime _now = DateTime.Now;
        private readonly List<SpacedRepetitionItem>  _items = new List<SpacedRepetitionItem>();

        public SrsItemListBuilder WithNewItems(int count)
        {
            var newItems = new SrsItemBuilder().NeverReviewed().Build(count);
            _items.AddRange(newItems);
            return this;
        }

        public SrsItemListBuilder WithExistingItems(int count)
        {
            var existingItems = new SrsItemBuilder().Due().Build(count);
            _items.AddRange(existingItems);
            return this;
        }

        public SrsItemListBuilder WithFutureItems(int count)
        {
            var itemsToAdd = new SrsItemBuilder()
                .WithLastReviewDate(DateTime.Now)
                .WithDifficultyRating(DifficultyRating.Easiest)
                .WithCorrectReviewStreak(15)
                .Build(count);
            _items.AddRange(itemsToAdd);

            return this;
        }

        public SrsItemListBuilder WithDueItems(int count)
        {
            var itemsToAdd = new SrsItemBuilder().Due().Build(count);
            _items.AddRange(itemsToAdd);

            return this;
        }

        public IEnumerable<SpacedRepetitionItem> Build()
        {
            return _items;
        }

        public static implicit operator List<SpacedRepetitionItem>(SrsItemListBuilder builder)
        {
            return builder.Build().ToList();
        }
    }
}