using CodingChallenge.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingChallenge.Tests.UnitTests
{
    [TestClass]
    public class MgltToConsumablesTest
    {
        private StarShip _starShip;

        [TestInitialize]
        public void Setup()
        {
            _starShip = new StarShip
            {
                Name = "Jedi starfighter"                
            };
        }

        [TestMethod]
        public void CalculateConsumablesByMgltTest_Days()
        {
            _starShip.Consumables = "7 days";
            _starShip.Mglt = "120";
            Assert.IsTrue(_starShip.ConsumablesByMglt.Equals(20160));
        }

        [TestMethod]
        public void CalculateConsumablesByMgltTest_Weeks()
        {
            _starShip.Consumables = "2 Weeks";
            _starShip.Mglt = "27";
            Assert.IsTrue(_starShip.ConsumablesByMglt.Equals(9072));
        }


        [TestMethod]
        public void CalculateConsumablesByMgltTest_Month()
        {
            _starShip.Consumables = "1 Month";
            _starShip.Mglt = "40";
            Assert.IsTrue(_starShip.ConsumablesByMglt.Equals(28800));
        }


        [TestMethod]
        public void CalculateConsumablesByMgltTest_Year()
        {
            _starShip.Consumables = "7 YEARS";
            _starShip.Mglt = "73";
            Assert.IsTrue(_starShip.ConsumablesByMglt.Equals(4415040));
        }

        [TestMethod]
        public void CalculateConsumablesByMgltTest_Unknown()
        {
            _starShip.Consumables = "7 YEARS";
            _starShip.Mglt = "unknown";
            Assert.IsTrue(_starShip.ConsumablesByMglt.Equals(0));
        }
    }
}
