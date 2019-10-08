using CodingChallenge.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingChallenge.Tests.UnitTests
{
    [TestClass]
    public class CalculateNumberOfStopsTest
    {
        private StarShip _starShip;
        private long _distance;

        [TestInitialize]
        public void Setup()
        {
            _distance = 10000;
            _starShip = new StarShip
            {
                Name = "Jedi starfighter"
            };
        }


        [TestMethod]
        public void CalculateNumberOfStopsTest_Days()
        {
            _starShip.Consumables = "9 days";
            _starShip.Mglt = "25";
            _starShip.CalculateNumberOfStops(_distance);

            Assert.IsTrue(_starShip.NumberOfStops.Equals(1));
        }


        [TestMethod]
        public void CalculateNumberOfStopsTest_Days2()
        {
            _starShip.Consumables = "30 days";
            _starShip.Mglt = "3";
            _starShip.CalculateNumberOfStops(_distance);

            Assert.IsTrue(_starShip.NumberOfStops.Equals(4));
        }

        [TestMethod]
        public void CalculateNumberOfStopsTest_Years()
        {
            _distance = 1000000;
            _starShip.Consumables = "9 Years";
            _starShip.Mglt = "85";
            _starShip.CalculateNumberOfStops(_distance);

            Assert.IsTrue(_starShip.NumberOfStops.Equals(0));
        }

        [TestMethod]
        public void CalculateNumberOfStopsTest_Years2()
        {
            _distance = 1000000;
            _starShip.Consumables = "9 Years";
            _starShip.Mglt = "2";
            _starShip.CalculateNumberOfStops(_distance);

            Assert.IsTrue(_starShip.NumberOfStops.Equals(6));
        }
    }
}
