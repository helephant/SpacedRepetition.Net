using System;

namespace SpacedRepetition.Net
{
    public interface IReviewItem
    {
        int CorrectReviewStreak { get; set; }
        DateTime ReviewDate { get; set; }
        DifficultyRating DifficultyRating { get; set; }
    }
}