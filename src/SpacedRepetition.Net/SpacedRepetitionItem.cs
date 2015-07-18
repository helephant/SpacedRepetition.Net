using System;

namespace SpacedRepetition.Net
{
    public class SpacedRepetitionItem : ISpacedRepetitionItem
    {
        public int Streak { get; set; }
        public DateTime LastReviewDate { get; set; }
        public double EasinessFactor { get; set; }
    }
}