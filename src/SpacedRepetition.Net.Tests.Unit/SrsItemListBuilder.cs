using System;
using System.Collections.Generic;
using System.Linq;

namespace SpacedRepetition.Net.Tests.Unit
{
    public class SrsItemListBuilder
    {
        private DateTime _now = DateTime.Now;
        private readonly List<SrsItem>  _items = new List<SrsItem>();

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
                .WithLastReviewDate(_now)
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

        public IEnumerable<SrsItem> Build()
        {
            return _items;
        }

        public static implicit operator List<SrsItem>(SrsItemListBuilder builder)
        {
            return builder.Build().ToList();
        }
    }
}