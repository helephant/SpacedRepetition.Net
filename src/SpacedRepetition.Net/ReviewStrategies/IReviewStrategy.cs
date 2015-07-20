using System;

namespace SpacedRepetition.Net.ReviewStrategies
{
    public interface IReviewStrategy
    {
        DateTime NextReview(ISrsItem item);

        DifficultyRating AdjustDifficulty(ISrsItem item, SrsAnswer answer);
    }
}