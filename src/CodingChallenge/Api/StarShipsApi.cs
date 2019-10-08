using CodingChallenge.Core;
using CodingChallenge.Model;
using Microsoft.Extensions.Configuration;

namespace CodingChallenge.Api
{
    public class StarShipsApi : BaseApi<StarShip>, IStarShipsApi
    {
        public StarShipsApi(IConfigurationRoot config) : base(config)
        {
            BaseUri = "starships";
        }
    }
}
