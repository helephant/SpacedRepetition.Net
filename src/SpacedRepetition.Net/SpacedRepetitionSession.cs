using System;
using System.Collections.Generic;
using SpacedRepetition.Net.IntervalStrategies;

namespace SpacedRepetition.Net
{
    public class SpacedRepetitionSession<T> 
        where T : ISpacedRepetitionItem
    {
        private readonly IEnumerator<T> _enumerator;
        private int _newCardsReturned;
        private int _existingCardsReturned;


        public IIntervalStrategy Strategy { get; set; }
        public int MaxNewCards { get; set; }
        public int MaxExistingCards { get; set; }


        public SpacedRepetitionSession(IEnumerable<T> items)
        {
            _enumerator = items.GetEnumerator();

            Strategy = new SuperMemo2SrsStrategy();
            MaxNewCards = 25;
            MaxExistingCards = 100;
        }

        public T Next()
        {
            while (_enumerator.MoveNext())
            {
                var nextReview = Strategy.NextReview(_enumerator.Current);
                if (nextReview >= DateTime.Now)
                {
                    if (_enumerator.Current.IsNewItem)
                    {
                        _newCardsReturned++;
                        if (_newCardsReturned <= MaxNewCards)
                            return _enumerator.Current;
                    }
                    else
                    {
                        _existingCardsReturned++;
                        if (_existingCardsReturned <= MaxExistingCards)
                            return _enumerator.Current;
                    }
                }
            }

            return default(T);
        }
 
    }
}