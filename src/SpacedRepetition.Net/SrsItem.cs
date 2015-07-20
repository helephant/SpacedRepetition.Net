using System;

namespace SpacedRepetition.Net
{
    public class SrsItem : ISrsItem
    {
        public int CorrectReviewStreak { get; set; }
        public DateTime LastReviewDate { get; set; }
        public DifficultyRating DifficultyRating { get; set; }

        public SrsItem Clone()
        {
            return new SrsItem
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