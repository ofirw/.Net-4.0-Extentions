namespace Net_4._0_Extentions
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    public static class DateTimeExtensions
    {
        public static bool IsBetween(this DateTime input, DateTime date1, DateTime date2)
        {
            return (input >= date1 && input <= date2);
        }

        public static bool EqualsWithPrecision(this DateTime time, DateTime time2, TimeSpan precision)
        {
            var times = new List<DateTime>(new[] { time, time2 });
            times.Sort();
            var timeSpan = times[1] - times[0] - precision;
            return timeSpan.TotalMilliseconds <= 0;
        }

        public static string ToShortReadableString(this TimeSpan time)
        {
            Func<double, string, string> formatNumber = (i, s) =>
            {
                if ((int)i <= 0)
                {
                    return "";
                }
                string format = "####";
                if (i > (int)i)
                {
                    format += "+";
                }

                s = Math.Abs(i - 1) < 0.0001 ? s : s + "s";
                format += " " + s;

                return i.ToString(format);
            };

            Dictionary<Predicate<TimeSpan>, Func<TimeSpan, string>> formatDict =
                new Dictionary<Predicate<TimeSpan>, Func<TimeSpan, string>>
                {
                    {
                        span => span.Days >= 30,
                        span => formatNumber(span.TotalDays / 30, "Month")
                    },
                    {
                        span => span.TotalDays >= 1,
                        span => formatNumber(span.TotalDays, "Day")
                    },
                    {
                        span => span.TotalHours >= 1,
                        span => formatNumber(span.TotalHours, "Hour")
                    },
                    {
                        span => span.TotalMinutes >= 1,
                        span => formatNumber(span.TotalMinutes, "Minute")
                    },
                    {
                        span => span.TotalSeconds >= 1,
                        span => formatNumber(span.TotalSeconds, "Second")
                    }
                };

            return formatDict.First(pair => pair.Key(time)).Value(time);
        }
    }
}