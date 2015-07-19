using System;

namespace SpacedRepetition.Net.ReviewStrategies
{
    public class SimpleReviewStrategy : IReviewStrategy
    {
        public DateTime NextReview(ISpacedRepetitionItem item)
        {
            return DateTime.Now;
        }

        public DifficultyRating AdjustDifficulty(ISpacedRepetitionItem item, SrsAnswer answer)
        {
            return DifficultyRating.Easiest;
        }
    }
}