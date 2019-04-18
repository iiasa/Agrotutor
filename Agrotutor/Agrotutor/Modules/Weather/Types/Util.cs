using System;
using System.Collections.Generic;
using System.Linq;

namespace Agrotutor.Modules.Weather.Types
{
    public static class Util
    {
        public static bool IsNight(DateTime time)
        {
            int month = time.Month;
            int hour = time.Hour;
            int minute = time.Minute;
            
            Time sunrise = SunriseTimes.ElementAt(month - 1);
            Time sunset = SunsetTimes.ElementAt(month - 1);

            return !IsBetween(minute, hour, sunrise, sunset);
        }

        private static bool IsBetween(int minute, int hour, Time sunrise, Time sunset)
        {
            if (hour < sunrise.Hours) return false; // Before sunrise hours
            if (hour > sunset.Hours) return false; // After sunset hours
            if (hour > sunrise.Hours && hour < sunset.Hours) return true; // Between sunrise and sunset hours

            if (
                hour >= sunrise.Hours && minute > sunrise.Minutes &&
                hour <= sunset.Hours && minute < sunset.Minutes) return true; // Between sunrise and sunset incl. Minutes

            return false; // Hour of sunset or sunrise, but Minutes are before sunrise or after sunset
        }

        private static readonly List<Time> SunriseTimes = new List<Time> // Monthly mid (15th) for Guanajuato
        {
            new Time(7,25),
            new Time(7,16),
            new Time(6,54),
            new Time(7,27),
            new Time(7,08),
            new Time(7,03),
            new Time(7,12),
            new Time(7,24),
            new Time(7,32),
            new Time(7,40),
            new Time(6,55),
            new Time(7,14)
        };
        private static readonly List<Time> SunsetTimes = new List<Time> // Monthly mid (15th) for Guanajuato
        {
            new Time(18,24),
            new Time(18,43),
            new Time(18,54),
            new Time(20,04),
            new Time(20,15),
            new Time(20,27),
            new Time(20,30),
            new Time(20,16),
            new Time(19,49),
            new Time(19,22),
            new Time(18,04),
            new Time(18,06)
        };

        class Time
        {
            public Time(int hours, int minutes)
            {
                Hours = hours;
                Minutes = minutes;
            }
            
            public int Hours { get; set; }
            public int Minutes { get; set; }
        }
    }
}