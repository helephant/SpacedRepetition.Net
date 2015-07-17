using System;

namespace SpacedRepetition.Net
{
    public interface ISpacedRepetitionItem
    {
        int TimesReviewed { get; set; }
        DateTime LastReviewDate { get; set; }
    }
}