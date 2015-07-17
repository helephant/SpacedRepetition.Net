using System;

namespace SpacedRepetition.Net.Tests.Unit
{
    public class SrsItemBuilder
    {
        private readonly SpacedRepetitionItem item = new SpacedRepetitionItem()
        {
            TimesReviewed = 3,
            LastReviewDate = DateTime.Now.AddDays(-3)
        };

        public SrsItemBuilder NeverReviewed()
        {
            item.TimesReviewed = 0;
            return this;
        }

        public SrsItemBuilder WithTimesReviewed(int timesReviewed)
        {
            item.TimesReviewed = timesReviewed;
            return this;
        }

        public SrsItemBuilder WithLastReviewDate(DateTime lastReviewDate)
        {
            item.LastReviewDate = lastReviewDate;
            return this;
        }

        public SpacedRepetitionItem Build()
        {
            return item;
        }
    }
}