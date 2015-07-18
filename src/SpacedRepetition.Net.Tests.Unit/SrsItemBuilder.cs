using System;

namespace SpacedRepetition.Net.Tests.Unit
{
    public class SrsItemBuilder
    {
        private readonly SpacedRepetitionItem _item = new SpacedRepetitionItem()
        {
            Streak = 3,
            LastReviewDate = DateTime.Now.AddDays(-3),
            EasinessFactor = 2
            
        };

        public SrsItemBuilder NeverReviewed()
        {
            _item.Streak = 0;
            _item.EasinessFactor = 2.5;
            return this;
        }

        public SrsItemBuilder WithTimesReviewed(int timesReviewed)
        {
            _item.Streak = timesReviewed;
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
    }
}