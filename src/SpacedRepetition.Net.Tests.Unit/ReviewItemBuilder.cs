using System;
using System.Collections.Generic;

namespace SpacedRepetition.Net.Tests.Unit
{
    public class ReviewItemBuilder
    {
        private readonly ReviewItem _item = new ReviewItem()
        {
            DifficultyRating = DifficultyRating.Easiest,
        };

        public ReviewItemBuilder NeverReviewed()
        {
            _item.CorrectReviewStreak = 0;
            _item.ReviewDate = DateTime.MinValue;
            _item.PreviousCorrectReview = DateTime.MinValue;
            _item.DifficultyRating = DifficultyRating.Easiest;
            return this;
        }

        public ReviewItemBuilder Due()
        {
            _item.CorrectReviewStreak = 3;
            _item.ReviewDate = DateTime.Now.AddDays(-10);
            _item.PreviousCorrectReview = DateTime.Now.AddDays(-12);
            _item.DifficultyRating = DifficultyRating.MostDifficult;
            return this;
        }

        public ReviewItemBuilder Future()
        {
            _item.CorrectReviewStreak = 3;
            _item.ReviewDate = DateTime.Now.AddDays(-2);
            _item.PreviousCorrectReview = DateTime.Now.AddDays(-12);
            _item.DifficultyRating = DifficultyRating.Easiest;
            return this;
        }

        public ReviewItemBuilder WithCorrectReviewStreak(int correctReviewStreak)
        {
            _item.CorrectReviewStreak = correctReviewStreak;
            return this;
        }

        public ReviewItemBuilder WithLastReviewDate(DateTime lastReviewDate)
        {
            _item.ReviewDate = lastReviewDate;
            return this;
        }

        public ReviewItemBuilder WitPreviousReviewDate(int daysBeforeToReviewDate)
        {
            _item.PreviousCorrectReview = _item.ReviewDate.AddDays(daysBeforeToReviewDate*-1);
            return this;
        }

        public ReviewItemBuilder WithDifficultyRating(int difficultyPercentage)
        {
            _item.DifficultyRating = new DifficultyRating(difficultyPercentage);
            return this;
        }

        public ReviewItemBuilder WithDifficultyRating(DifficultyRating difficulty)
        {
            _item.DifficultyRating = difficulty;
            return this;
        }

        public ReviewItem Build()
        {
            return _item;
        }

        public IEnumerable<ReviewItem> Build(int count)
        {
            for (var x = 0; x < count; x++)
                yield return _item.Clone();
        }
    }
}