using CodingChallenge.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodingChallenge.Tests.UnitTests
{
    [TestClass]
    public class StarShipTest
    {
        [DataTestMethod]
        [DataRow("Years", "3 years", "35", 907200, 1000000, 1)]
        [DataRow("Months", "2 months", "75", 108000, 1000000, 9)]
        [DataRow("Weeks", "1 week", "80", 13440, 1000000, 74)]
        [DataRow("Days", "9 days", "20", 4320, 1000000, 231)]
        public void StarShip_Test(string name, string consumables, string mglt, int expectedConsumables, long distance, int expectedStops)
        {
            var starShip = new StarShip
            {
                Name = name,
                Consumables = consumables,
                Mglt = mglt
            };

            starShip.CalculateNumberOfStops(distance);

            Assert.AreEqual(expectedConsumables, starShip.ConsumablesByMglt);
            Assert.AreEqual(expectedStops, starShip.NumberOfStops);
        }
    }
}
