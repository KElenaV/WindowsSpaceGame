using System;

namespace SpaceGame
{
    static class Time
    {
        private static DateTime _startTime;
        private static DateTime _endTime;

        public static float DeltaTime { get; private set; }

        public static void CalculateDeltaTime()
        {
            _startTime = DateTime.Now;

            if (_endTime.Ticks != 0)
                DeltaTime = (_startTime.Ticks - _endTime.Ticks) / 10000000f;

            _endTime = _startTime;
        }
    }
}
