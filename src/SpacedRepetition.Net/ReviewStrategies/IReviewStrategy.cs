using System;

namespace SpacedRepetition.Net.ReviewStrategies
{
    public interface IReviewStrategy
    {
        DateTime NextReview(ISpacedRepetitionItem item);

        DifficultyRating AdjustDifficulty(ISpacedRepetitionItem item, SrsAnswer answer);
    }
}