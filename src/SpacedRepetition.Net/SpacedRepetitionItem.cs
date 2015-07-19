using System;

namespace SpacedRepetition.Net
{
    public class SpacedRepetitionItem : ISpacedRepetitionItem
    {
        public int CorrectReviewStreak { get; set; }
        public DateTime LastReviewDate { get; set; }
        public double EasinessFactor { get; set; }

        public SpacedRepetitionItem Clone()
        {
            return new SpacedRepetitionItem
            {
                EasinessFactor = EasinessFactor,
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