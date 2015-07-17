using System;

namespace SpacedRepetition.Net
{
    public class SpacedRepetitionItem : ISpacedRepetitionItem
    {
        public int TimesReviewed { get; set; }
        public DateTime LastReviewDate { get; set; }
    }
}