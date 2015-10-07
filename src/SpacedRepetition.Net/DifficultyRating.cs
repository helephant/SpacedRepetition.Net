using System;

namespace SpacedRepetition.Net
{
    public class DifficultyRating : IEquatable<DifficultyRating>, IComparable<DifficultyRating>
    {
        public int Percentage { get; private set; }

        public DifficultyRating(int percentage)
        {
            if (percentage < 0 || percentage > 100)
                throw new ArgumentOutOfRangeException("percentage", "DifficultyRating be between 0 (least difficult) and 100 (most difficult)");

            Percentage = percentage;
        }

        #region Equality
        public bool Equals(DifficultyRating other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Percentage == other.Percentage;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DifficultyRating)obj);
        }

        public override int GetHashCode()
        {
            return Percentage;
        }

        public static bool operator ==(DifficultyRating left, DifficultyRating right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DifficultyRating left, DifficultyRating right)
        {
            return !Equals(left, right);
        }
        #endregion

        #region Comparable
        public int CompareTo(DifficultyRating other)
        {
            return Percentage.CompareTo(other.Percentage);
        }

        public static bool operator >(DifficultyRating left, DifficultyRating right)
        {
            return left.Percentage > right.Percentage;
        }

        public static bool operator >=(DifficultyRating left, DifficultyRating right)
        {
            return left.Percentage >= right.Percentage;
        }

        public static bool operator <(DifficultyRating left, DifficultyRating right)
        {
            return left.Percentage < right.Percentage;
        }

        public static bool operator <=(DifficultyRating left, DifficultyRating right)
        {
            return left.Percentage <= right.Percentage;
        }
        #endregion

        public static implicit operator int(DifficultyRating rating)
        {
            return rating.Percentage;
        }
        public static implicit operator DifficultyRating(int percentage)
        {
            return new DifficultyRating(percentage);
        }

        public static implicit operator byte(DifficultyRating rating)
        {
            return (byte)rating.Percentage;
        }
        public static implicit operator DifficultyRating(byte percentage)
        {
            return new DifficultyRating(percentage);
        }

        public override string ToString()
        {
            return Percentage + "%";
        }

        public static DifficultyRating Easiest { get { return new DifficultyRating(0); } }
        public static DifficultyRating MostDifficult { get { return new DifficultyRating(100); } }
    }
}