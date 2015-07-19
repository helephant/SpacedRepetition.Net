using System;
using System.Collections;
using System.Collections.Generic;
using SpacedRepetition.Net.IntervalStrategies;

namespace SpacedRepetition.Net
{
    public class SpacedRepetitionSession<T>  : IEnumerable<T> 
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

        public IEnumerator<T> GetEnumerator()
        {
            while (_enumerator.MoveNext())
            {
                var item = _enumerator.Current;
                var nextReview = Strategy.NextReview(item);
                if (nextReview <= DateTime.Now)
                {
                    if (item.IsNewItem)
                    {
                        _newCardsReturned++;
                        if (_newCardsReturned <= MaxNewCards)
                            yield return item;
                    }
                    else
                    {
                        _existingCardsReturned++;
                        if (_existingCardsReturned <= MaxExistingCards)
                            yield return item;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}