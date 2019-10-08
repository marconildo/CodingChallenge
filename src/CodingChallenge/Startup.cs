using CodingChallenge.Api;
using CodingChallenge.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace CodingChallenge
{
    public class Startup
    {
        private static IServiceProvider _serviceProvider;
        IConfigurationRoot configuration { get; }

        public Startup()
        {
            // retrieving application settings
            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");

            configuration = builder.Build();
        }

        /// <summary>
        /// records services for dependency injection
        /// </summary>
        public void RegisterServices()
        {
            var collection = new ServiceCollection();
            collection.AddSingleton(configuration);
            collection.AddScoped<IStarShipsApi, StarShipsApi>();
            collection.AddScoped<IStarShipsService, StarShipsService>();

            _serviceProvider = collection.BuildServiceProvider();
        }
        public void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }

        /// <summary>
        /// recover service
        /// </summary>
        /// <typeparam name="T">Type of Service</typeparam>
        /// <returns></returns>
        public T GetService<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }
}
