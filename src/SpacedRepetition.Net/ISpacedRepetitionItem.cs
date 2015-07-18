using System;

namespace SpacedRepetition.Net
{
    public interface ISpacedRepetitionItem
    {
        int Streak { get; set; }
        DateTime LastReviewDate { get; set; }
        double EasinessFactor { get; set; }
    }
}