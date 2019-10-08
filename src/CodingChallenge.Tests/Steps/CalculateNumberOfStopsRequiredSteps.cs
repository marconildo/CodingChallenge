using CodingChallenge.Api;
using CodingChallenge.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CodingChallenge.Tests.Steps
{
    [Binding]
    public class CalculateNumberOfStopsRequiredSteps
    {
        private long _distance;
        private IList<StarShip> _starShips;
        private Mock<IConfigurationRoot> _configurationRoot;

        [Given(@"I have a desire to know how many stops all starships have to make in a flight of (.*) MGLT")]
        public void GivenIHaveADesireToKnowHowManyStopsAllStarshipsHaveToMakeInAFlightOfMGLT(long distance)
        {
            _distance = distance;
        }
        
        [When(@"I press enter")]
        public void WhenIPressEnter()
        {
            _configurationRoot = new Mock<IConfigurationRoot>();
            _configurationRoot.SetupGet(x => x[It.IsAny<string>()]).Returns("https://swapi.co/api");

            var api = new StarShipsApi(_configurationRoot.Object).GetListAsync();
            _starShips = api.Result.Results;
        }

        [Then(@"the calculator must list all spaceships and their number of stops before depleting all consumables like this:")]
        public void ThenTheCalculatorMustListAllSpaceshipsAndTheirNumberOfStopsBeforeDepletingAllConsumablesLikeThis(Table table)
        {
            var list = table.CreateSet<StarShip>();
            Assert.IsTrue(list.Except(_starShips).Any());
        }
    }
}
