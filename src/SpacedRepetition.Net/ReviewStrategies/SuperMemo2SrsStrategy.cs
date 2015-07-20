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

        public DateTime NextReview(ISrsItem item)
        {
            var now = _clock.Now();
            if(item.CorrectReviewStreak == 0)
                return now;
            if(item.CorrectReviewStreak == 1)
                return item.LastReviewDate.AddDays(6);

            var easinessFactor = DifficultyRatingToEasinessFactor(item.DifficultyRating.Percentage);
            return item.LastReviewDate.AddDays((item.CorrectReviewStreak - 1)*easinessFactor);
        }

        public DifficultyRating AdjustDifficulty(ISrsItem item, SrsAnswer answer)
        {
            //EF':=EF+(0.1-(5-q)*(0.08+(5-q)*0.02))
            //where:
            //EF' - new value of the E-Factor,
            //EF - old value of the E-Factor,
            //q - quality of the response in the 0-3 grade scale.
            //If EF is less than 1.3 then let EF be 1.3.

            var currentEasinessFactor = DifficultyRatingToEasinessFactor(item.DifficultyRating.Percentage);
            var newEasinessFactor = currentEasinessFactor + (0.1 - (3 - (int)answer)*(0.08 + (3 - (int)answer)*0.02));
            var newDifficultyRating = EasinessFactorToDifficultyRating(newEasinessFactor);

            if (newDifficultyRating > 100)
                newDifficultyRating = 100;
            if (newDifficultyRating < 0)
                newDifficultyRating = 0;

            return new DifficultyRating(newDifficultyRating);
        }

        private double DifficultyRatingToEasinessFactor(int difficultyRating)
        {
            // using a linear equation - y = mx + b
            return (-0.012 * difficultyRating) + 2.5;
        }

        private int EasinessFactorToDifficultyRating(double easinessFactor)
        {
            // using a linear equation - x = (y - b)/m
            return (int)((easinessFactor - 2.5) / -0.012);
        }
    }
}