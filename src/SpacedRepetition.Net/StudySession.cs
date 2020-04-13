using System;
using System.Collections;
using System.Collections.Generic;
using SpacedRepetition.Net.ReviewStrategies;

namespace SpacedRepetition.Net
{
    public class StudySession<T>  : IEnumerable<T> 
        where T : IReviewItem, new()
    {
        private readonly IEnumerator<T> _enumerator;
        private readonly List<T> _revisionList = new List<T>(); 
        private int _newCardsReturned;
        private int _existingCardsReturned;


        public IReviewStrategy ReviewStrategy { get; set; }
        public int MaxNewCards { get; set; }
        public int MaxExistingCards { get; set; }
        public IClock Clock { get; set; }


        public StudySession(IEnumerable<T> items)
        {
            _enumerator = items.GetEnumerator();

            ReviewStrategy = new SuperMemo2ReviewStrategy();
            Clock = new Clock();
            MaxNewCards = 25;
            MaxExistingCards = 100;
        }

        public T Review(T item, ReviewOutcome outcome)
        {
            _revisionList.Remove(item);

            var nextReview = new T();

            if (outcome != ReviewOutcome.Incorrect)
            {
                nextReview.CorrectReviewStreak = item.CorrectReviewStreak+1;
                nextReview.PreviousCorrectReview = item.ReviewDate;
                
            }
            else
            {
                nextReview.CorrectReviewStreak = 0;
                nextReview.PreviousCorrectReview = DateTime.MinValue;
                
                _revisionList.Add(nextReview);
            }

            nextReview.ReviewDate = Clock.Now();
            nextReview.DifficultyRating = ReviewStrategy.AdjustDifficulty(item, outcome);
            
            return nextReview;
        }

        public IEnumerator<T> GetEnumerator()
        {
            while (_enumerator.MoveNext())
            {
                var item = _enumerator.Current;
                if (IsDue(item))
                {
                    if (IsNewItem(item))
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

            while (_revisionList.Count > 0)
            {
                yield return _revisionList[0];
            }
        }

        private static bool IsNewItem(IReviewItem item)
        {
            return item.ReviewDate == DateTime.MinValue;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool IsDue(IReviewItem item)
        {
            var nextReview = ReviewStrategy.NextReview(item);
            return nextReview <= Clock.Now();
        }
    }
}