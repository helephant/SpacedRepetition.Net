using System;

namespace SpacedRepetition.Net
{
    public interface ISrsItem
    {
        int CorrectReviewStreak { get; set; }
        DateTime LastReviewDate { get; set; }
        DifficultyRating DifficultyRating { get; set; }
        bool IsNewItem { get; }
    }
}