using System;

namespace SpacedRepetition.Net.IntervalStrategies
{
    public interface IIntervalStrategy
    {
        DateTime NextReview(ISpacedRepetitionItem item);
    }
}