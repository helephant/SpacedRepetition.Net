using System;

namespace SpacedRepetition.Net.ReviewStrategies
{
    public class SimpleReviewStrategy : IReviewStrategy
    {
        public DateTime NextReview(ISrsItem item)
        {
            return DateTime.Now;
        }

        public DifficultyRating AdjustDifficulty(ISrsItem item, SrsAnswer answer)
        {
            return DifficultyRating.Easiest;
        }
    }
}