using System;

namespace SpacedRepetition.Net.ReviewStrategies
{
    public interface IReviewStrategy
    {
        DateTime NextReview(IReviewItem item);

        DifficultyRating AdjustDifficulty(IReviewItem item, ReviewAnswer answer);
    }
}