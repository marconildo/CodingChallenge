using System;

namespace CodingChallenge.Extensions
{
    /// <summary>
    /// extension to convert string values to total hours
    /// </summary>
    public static class ConvertToHoursExtensions
    {
        public static int ConvertConsumablesToHours(this string consumables)
        {
            if (string.IsNullOrEmpty(consumables))
                return 0;

            if (consumables.ToLower() == "unknown")
                return 0;

            return CalculateHours(consumables);
        }

        private static int CalculateHours(string consumables)
        {
            var split = consumables.ToLower().Split(" ");
            int value = Convert.ToInt32(split[0]);

            switch (split[1])
            {
                case string a when a.Contains("year"):
                    return value.ConvertYearsToHour();
                case string a when a.Contains("month"):
                    return value.ConvertMonthsToHour();
                case string a when a.Contains("week"):
                    return value.ConvertWeeksToHour();
                case string a when a.Contains("day"):
                    return value.ConvertDaysToHour();
            }

            return 0;
        }

        private static int ConvertYearsToHour(this int years)
        {
            var months = years * 12;
            return months.ConvertMonthsToHour();
        }

        private static int ConvertMonthsToHour(this int months)
        {
            var days = months * 30;
            return days.ConvertDaysToHour();
        }

        private static int ConvertWeeksToHour(this int weeks)
        {
            var days = weeks * 7;
            return days.ConvertDaysToHour();
        }

        private static int ConvertDaysToHour(this int days)
        {
            return Convert.ToInt32(TimeSpan.FromDays(days).TotalHours);
        }
    }
}
