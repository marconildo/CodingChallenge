using CodingChallenge.Services;

namespace CodingChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            // starting application settings
            Startup startup = new Startup();
            // registering services for dependency injection
            startup.RegisterServices();

            // retrieving service that processes starships
            var starShipsService = startup.GetService<IStarShipsService>();
            // running
            starShipsService.Process();

            startup.DisposeServices();
        }
    }
}
