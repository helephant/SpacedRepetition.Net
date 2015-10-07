using NUnit.Framework;

namespace SpacedRepetition.Net.Tests.Unit
{
    [TestFixture]
    public class DifficultyRatingTests
    {
        [Test]
        public void implicitly_convert_from_difficulty_rating_to_int()
        {
            var difficultyRating = new DifficultyRating(50);

            int percentage = difficultyRating;
            Assert.That(percentage, Is.EqualTo(difficultyRating.Percentage));
        }

        [Test]
        public void implicitly_convert_from_int_to_difficulty_rating()
        {
            int percentage = 65;
            DifficultyRating difficultyRating = percentage;

            Assert.That(difficultyRating.Percentage, Is.EqualTo(percentage));
        }

        [Test]
        public void implicitly_convert_from_difficulty_rating_to_byte()
        {
            var difficultyRating = new DifficultyRating(50);

            byte percentage = difficultyRating;
            Assert.That(percentage, Is.EqualTo(difficultyRating.Percentage));
        }

        [Test]
        public void implicitly_convert_from_byte_to_difficulty_rating()
        {
            byte percentage = 65;
            DifficultyRating difficultyRating = percentage;

            Assert.That(difficultyRating.Percentage, Is.EqualTo(percentage));
        }
    }
}
