using CodingChallenge.Extensions;
using System;

namespace CodingChallenge.Model
{
    public class StarShip
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }
        public String Url { get; set; }
        public string Mglt { get; set; }
        public string Consumables { get; set; }

        private int MgltNumber
        {
            get
            {
                if (Mglt != "unknown")
                    return Convert.ToInt32(Mglt);

                return 0;
            }
        }
        public int ConsumablesHours => Consumables.ConvertConsumablesToHours();
        public int ConsumablesByMglt => MgltNumber * ConsumablesHours;
        public int NumberOfStops { get; private set; }

        public void CalculateNumberOfStops(long numberMglt)
        {
            if (ConsumablesByMglt > 0)
            {
                var stops = numberMglt / ConsumablesByMglt;
                NumberOfStops = (int)stops;
            }
            else
                NumberOfStops = 0;
        }
    }
}
