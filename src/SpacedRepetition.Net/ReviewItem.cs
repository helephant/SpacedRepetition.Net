using System;

namespace SpacedRepetition.Net
{
    public class ReviewItem : IReviewItem
    {
        public int CorrectReviewStreak { get; set; }
        public DateTime LastReviewDate { get; set; }
        public DifficultyRating DifficultyRating { get; set; }

        public ReviewItem Clone()
        {
            return new ReviewItem
            {
                DifficultyRating = DifficultyRating,
                LastReviewDate = LastReviewDate,
                CorrectReviewStreak = CorrectReviewStreak
            };
        }

        public bool IsNewItem
        {
            get { return LastReviewDate == DateTime.MinValue;  }
        }
    }
}