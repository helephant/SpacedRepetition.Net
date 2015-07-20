using System;
using System.Collections.Generic;
using System.Linq;

namespace SpacedRepetition.Net.Tests.Unit
{
    public class ReviewItemListBuilder
    {
        private DateTime _now = DateTime.Now;
        private readonly List<ReviewItem>  _items = new List<ReviewItem>();

        public ReviewItemListBuilder WithNewItems(int count)
        {
            var newItems = new ReviewItemBuilder().NeverReviewed().Build(count);
            _items.AddRange(newItems);
            return this;
        }

        public ReviewItemListBuilder WithExistingItems(int count)
        {
            var existingItems = new ReviewItemBuilder().Due().Build(count);
            _items.AddRange(existingItems);
            return this;
        }

        public ReviewItemListBuilder WithFutureItems(int count)
        {
            var itemsToAdd = new ReviewItemBuilder()
                .WithLastReviewDate(_now)
                .WithDifficultyRating(DifficultyRating.Easiest)
                .WithCorrectReviewStreak(15)
                .Build(count);
            _items.AddRange(itemsToAdd);

            return this;
        }

        public ReviewItemListBuilder WithDueItems(int count)
        {
            var itemsToAdd = new ReviewItemBuilder().Due().Build(count);
            _items.AddRange(itemsToAdd);

            return this;
        }

        public IEnumerable<ReviewItem> Build()
        {
            return _items;
        }

        public static implicit operator List<ReviewItem>(ReviewItemListBuilder builder)
        {
            return builder.Build().ToList();
        }
    }
}