using System;
using System.Collections.Generic;

namespace SpacedRepetition.Net.Tests.Unit
{
    public class SrsItemBuilder
    {
        private readonly SrsItem _item = new SrsItem();

        public SrsItemBuilder NeverReviewed()
        {
            _item.CorrectReviewStreak = 0;
            _item.LastReviewDate = DateTime.MinValue;
            _item.DifficultyRating = DifficultyRating.Easiest;
            return this;
        }

        public SrsItemBuilder Due()
        {
            _item.CorrectReviewStreak = 3;
            _item.LastReviewDate = DateTime.Now.AddDays(-100);
            _item.DifficultyRating = DifficultyRating.MostDifficult;
            return this;
        }

        public SrsItemBuilder WithCorrectReviewStreak(int correctReviewStreak)
        {
            _item.CorrectReviewStreak = correctReviewStreak;
            return this;
        }

        public SrsItemBuilder WithLastReviewDate(DateTime lastReviewDate)
        {
            _item.LastReviewDate = lastReviewDate;
            return this;
        }

        public SrsItemBuilder WithDifficultyRating(int difficultyPercentage)
        {
            _item.DifficultyRating = new DifficultyRating(difficultyPercentage);
            return this;
        }

        public SrsItemBuilder WithDifficultyRating(DifficultyRating difficulty)
        {
            _item.DifficultyRating = difficulty;
            return this;
        }

        public SrsItem Build()
        {
            return _item;
        }

        public IEnumerable<SrsItem> Build(int count)
        {
            for (var x = 0; x < count; x++)
                yield return _item.Clone();
        }

        public static implicit operator SrsItem(SrsItemBuilder builder)
        {
            return builder.Build();
        }
    }
}