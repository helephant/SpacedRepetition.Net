using System;
using System.Collections.Generic;

namespace SpacedRepetition.Net.Tests.Unit
{
    public class SrsItemBuilder
    {
        private readonly SpacedRepetitionItem _item = new SpacedRepetitionItem()
        {
            CorrectReviewStreak = 3,
            LastReviewDate = DateTime.Now.AddDays(-3),
            EasinessFactor = 2
            
        };

        public SrsItemBuilder NeverReviewed()
        {
            _item.CorrectReviewStreak = 0;
            _item.LastReviewDate = DateTime.MinValue;
            _item.EasinessFactor = 2.5;
            return this;
        }

        public SrsItemBuilder WithCorrectReviewStreak(int correctReviewStreak)
        {
            _item.CorrectReviewStreak = correctReviewStreak;
            return this;
        }

        public SrsItemBuilder WithLastReviewDate(DateTime lastReviewDate)
        {
            _item.LastReviewDate = lastReviewDate;
            return this;
        }

        public SrsItemBuilder WithEasinessFactor(double easinessFactor)
        {
            _item.EasinessFactor = easinessFactor;
            return this;
        }


        public SpacedRepetitionItem Build()
        {
            return _item;
        }

        public IEnumerable<SpacedRepetitionItem> Build(int count)
        {
            for (var x = 0; x < count; x++)
                yield return _item.Clone();
        }
    }

    public class SrsItemListBuilder
    {
        private List<SpacedRepetitionItem>  _items = new List<SpacedRepetitionItem>();

        public SrsItemListBuilder WithNewItems(int count)
        {
            var newItems = new SrsItemBuilder().NeverReviewed().Build(count);
            _items.AddRange(newItems);
            return this;
        }

        public SrsItemListBuilder WithItemsWaitingReview(int count)
        {
            var existingItems = new SrsItemBuilder().Build(count);
            _items.AddRange(existingItems);
            return this;
        }

        public IEnumerable<SpacedRepetitionItem> Build()
        {
            return _items;
        } 
    }
}