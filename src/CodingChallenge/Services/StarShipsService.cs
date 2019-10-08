using CodingChallenge.Api;
using CodingChallenge.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using CodingChallenge.Core;
using System.Threading;

namespace CodingChallenge.Services
{
    public class StarShipsService: IStarShipsService
    {
        private readonly IStarShipsApi _starShipsApi;
        private long _distance;

        private List<StarShip> _starShips;

        public StarShipsService(IStarShipsApi starShipsApi)
        {
            _starShipsApi = starShipsApi;
            _starShips = new List<StarShip>();
        }

        public void Process()
        {
            GetDistance();
            GetAllShips();
            CalculateStarshipDistance();
            PrintStarshipDistance();

            Console.ReadLine();
        }

        /// <summary>
        /// retrieves all registered ships
        /// </summary>
        private void GetAllShips()
        {
            Console.Write("Loading Star Ships: ");

            using var progress = new ProgressBar();

            var ships = _starShipsApi.GetListAsync().Result;
            var totalShips = ships.Count;

            _starShips.AddRange(ships.Results);
            progress.Report((double)_starShips.Count / totalShips);

            while (ships.Next != null)
            {
                ships = _starShipsApi.GetListAsync(ships.Next).Result;
                _starShips.AddRange(ships.Results);
                progress.Report((double)_starShips.Count / totalShips);
                Thread.Sleep(200);
            }

            Console.WriteLine(" Done.");
        }

        /// <summary>
        /// Prompts the user for the distance in mega lights (MGLT) to perform the calculation.
        /// </summary>
        private void GetDistance()
        {
            string strDistance;

            do
            {
                Console.Clear();
                Console.WriteLine("Please enter the distance in mega lights (MGLT):");
                strDistance = Console.ReadLine().Trim();
            }
            while (string.IsNullOrEmpty(strDistance));

            _distance = Convert.ToInt64(strDistance);
        }

        /// <summary>
        /// Processes the calculation of each ship's stops based on its autonomy and based on the reported distance
        /// </summary>
        private void CalculateStarshipDistance()
        {
            Console.Write("Calculate Starship Distance: ");
            using var progress = new ProgressBar();
            int count = 1;

            foreach (var item in _starShips)
            {
                item.CalculateNumberOfStops(_distance);
                
                progress.Report((double)count / _starShips.Count);
                Thread.Sleep(200);
                count++;
            }

            Console.WriteLine("Done.");
        }

        /// <summary>
        /// Prints the result of stop processing based on table format calculations
        /// </summary>
        private void PrintStarshipDistance()
        {
            var starShips = _starShips.Where(p => p.NumberOfStops > 0).OrderBy(p => p.Name);

            Console.WriteLine(starShips.ToStringTable(new[] { "Star Ship Name", "Number of stops" }, a => a.Name, a => a.NumberOfStops));
        }
    }
}
