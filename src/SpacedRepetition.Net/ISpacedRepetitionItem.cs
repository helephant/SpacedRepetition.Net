using System;

namespace SpacedRepetition.Net
{
    public interface ISpacedRepetitionItem
    {
        int CorrectReviewStreak { get; set; }
        DateTime LastReviewDate { get; set; }
        double EasinessFactor { get; set; }
        bool IsNewItem { get; }
    }
}