using System;

namespace SpacedRepetition.Net.IntervalStrategies
{
    public class NowIntervalStrategy : IIntervalStrategy
    {
        public DateTime NextReview(ISpacedRepetitionItem item)
        {
            return DateTime.Now;
        }
    }
}