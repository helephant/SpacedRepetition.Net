using System;

namespace SpacedRepetition.Net
{
    public class SpacedRepetitionItem : ISpacedRepetitionItem
    {
        public int CorrectReviewStreak { get; set; }
        public DateTime LastReviewDate { get; set; }
        public DifficultyRating DifficultyRating { get; set; }

        public SpacedRepetitionItem Clone()
        {
            return new SpacedRepetitionItem
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