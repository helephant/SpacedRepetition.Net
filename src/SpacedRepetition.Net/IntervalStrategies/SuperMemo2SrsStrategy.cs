using System;

namespace SpacedRepetition.Net.IntervalStrategies
{
    public class SuperMemo2SrsStrategy : IIntervalStrategy
    {
        private readonly IClock _clock;

        public SuperMemo2SrsStrategy() : this(new Clock())
        {
        }

        public SuperMemo2SrsStrategy(IClock clock)
        {
            _clock = clock;
        }

        public DateTime NextReview(ISpacedRepetitionItem item)
        {
            var now = _clock.Now();
            if(item.Streak == 0)
                return now;
            if(item.Streak == 1)
                return now.AddDays(6);

            return item.LastReviewDate.AddDays((item.Streak - 1)*item.EasinessFactor);
        }
    }
}