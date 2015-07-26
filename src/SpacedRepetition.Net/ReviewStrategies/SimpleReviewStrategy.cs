using System;

namespace SpacedRepetition.Net.ReviewStrategies
{
    public class SimpleReviewStrategy : IReviewStrategy
    {
        public DateTime NextReview(IReviewItem item)
        {
            return DateTime.Now;
        }

        public DifficultyRating AdjustDifficulty(IReviewItem item, ReviewOutcome outcome)
        {
            return DifficultyRating.Easiest;
        }
    }
}