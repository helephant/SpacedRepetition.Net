using System;

namespace SpacedRepetition.Net
{
    public interface IReviewItem
    {
        int CorrectReviewStreak { get; set; }
        DateTime ReviewDate { get; set; }
        DateTime PreviousCorrectReview { get; set; }
        DifficultyRating DifficultyRating { get; set; }
    }
}