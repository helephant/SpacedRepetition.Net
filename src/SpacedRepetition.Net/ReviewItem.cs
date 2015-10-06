using System;

namespace SpacedRepetition.Net
{
    public class ReviewItem : IReviewItem
    {
        public int CorrectReviewStreak { get; set; }
        public DateTime ReviewDate { get; set; }
        public DifficultyRating DifficultyRating { get; set; }

        public ReviewItem Clone()
        {
            return new ReviewItem
            {
                DifficultyRating = DifficultyRating,
                ReviewDate = ReviewDate,
                CorrectReviewStreak = CorrectReviewStreak
            };
        }
    }
}