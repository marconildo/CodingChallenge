using CodingChallenge.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingChallenge.Tests.UnitTests
{
    [TestClass]
    public class ConvertToHoursTest
    {
        [TestMethod]
        public void ConvertToHours_ConvertDaysToHour_IsValidTest()
        {
            var strDays = "4 days";
            var totalHours = strDays.ConvertConsumablesToHours();

            Assert.IsTrue(totalHours.Equals(96));
        }

        [TestMethod]
        public void ConvertToHours_ConvertDaysToHour_IsInvalidTest()
        {
            var strDays = "6 days";
            var totalHours = strDays.ConvertConsumablesToHours();

            Assert.IsFalse(totalHours.Equals(96));
        }

        [TestMethod]
        public void ConvertToHours_ConvertWeeksToHour_IsValidTest()
        {
            var strWeeks = "2 weeks";
            var totalHours = strWeeks.ConvertConsumablesToHours();

            Assert.IsTrue(totalHours.Equals(336));
        }

        [TestMethod]
        public void ConvertToHours_ConvertWeeksToHour_IsInvalidTest()
        {
            var strWeeks = "1 weeks";
            var totalHours = strWeeks.ConvertConsumablesToHours();

            Assert.IsFalse(totalHours.Equals(336));
        }

        [TestMethod]
        public void ConvertToHours_ConvertMonthsToHour_IsValidTest()
        {
            var strMonths = "5 months";
            var totalHours = strMonths.ConvertConsumablesToHours();

            Assert.IsTrue(totalHours.Equals(3600));
        }

        [TestMethod]
        public void ConvertToHours_ConvertMonthsToHour_IsInvalidTest()
        {
            var strMonths = "2 months";
            var totalHours = strMonths.ConvertConsumablesToHours();

            Assert.IsFalse(totalHours.Equals(336));
        }

        [TestMethod]
        public void ConvertToHours_ConvertYearsToHour_IsValidTest()
        {
            var strYears = "3 years";
            var totalHours = strYears.ConvertConsumablesToHours();

            Assert.IsTrue(totalHours.Equals(25920));
        }

        [TestMethod]
        public void ConvertToHours_ConvertYearsToHour_IsInvalidTest()
        {
            var strYears = "2 years";
            var totalHours = strYears.ConvertConsumablesToHours();

            Assert.IsFalse(totalHours.Equals(17270));
        }

        [TestMethod]
        public void ConvertToHours_Unknown_IsValidTest()
        {
            var str = "unknown";
            var totalHours = str.ConvertConsumablesToHours();

            Assert.IsTrue(totalHours.Equals(0));
        }
    }
}
