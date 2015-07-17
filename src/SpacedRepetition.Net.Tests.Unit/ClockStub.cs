using System;

namespace SpacedRepetition.Net.Tests.Unit
{
    public class ClockStub : IClock
    {
        private readonly DateTime _now;

        public ClockStub(DateTime now)
        {
            _now = now;
        }

        public DateTime Now()
        {
            return _now;
        }
    }
}