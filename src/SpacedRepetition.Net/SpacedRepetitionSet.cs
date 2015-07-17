using System;
using System.Collections.Generic;
using SpacedRepetition.Net.IntervalStrategies;

namespace SpacedRepetition.Net
{
    public class SpacedRepetitionSet<T> 
        where T : ISpacedRepetitionItem
    {
        private readonly IEnumerator<T> _enumerator; 
        private readonly IIntervalStrategy _strategy;

        public SpacedRepetitionSet(IEnumerable<T> items, IIntervalStrategy strategy)
        {
            _enumerator = items.GetEnumerator();
            _strategy = strategy;
        }

        public T Next()
        {
            while (_enumerator.MoveNext())
            {
                var nextReview = _strategy.NextReview(_enumerator.Current);
                if (nextReview >= DateTime.Now)
                {
                    return _enumerator.Current;
                }
            }

            throw new ReachedEndOfSet();
        }
    }

    public class ReachedEndOfSet : Exception
    {
    }
}