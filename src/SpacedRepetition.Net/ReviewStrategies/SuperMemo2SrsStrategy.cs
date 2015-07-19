using System;

namespace SpacedRepetition.Net.ReviewStrategies
{
    /// <summary>
    /// Implementation of the SuperMemo2 algorithm described here: http://www.supermemo.com/english/ol/sm2.htm
    /// </summary>
    public class SuperMemo2SrsStrategy : IReviewStrategy
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
            if(item.CorrectReviewStreak == 0)
                return now;
            if(item.CorrectReviewStreak == 1)
                return item.LastReviewDate.AddDays(6);

            var easinessFactor = CalculateEasinessFactor(item.DifficultyRating.Percentage);
            return item.LastReviewDate.AddDays((item.CorrectReviewStreak - 1)*easinessFactor);
        }

        public DifficultyRating AdjustDifficulty(ISpacedRepetitionItem item, SrsAnswer answer)
        {
            throw new NotImplementedException();
        }

        private double CalculateEasinessFactor(int difficultyRating)
        {
            // using a linear equation
            return (-0.012 * difficultyRating) + 2.5;
        }
    }
}