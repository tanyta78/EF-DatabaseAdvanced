namespace BusTicketSystem.Client
{
    using System;
    using Core;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Services.Contracts;

    public class Application
    {
        static void Main(string[] args)
        {
            var serviceProvider = ConfigureServices();
            Engine engine = new Engine(serviceProvider);
            engine.Run();
        }

        private static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<BusTicketSystemContext>(options =>
                options.UseSqlServer(ServerConfig.ConnectionString)
            );


            serviceCollection.AddTransient<IBusStationService, BusStationService>();
            serviceCollection.AddTransient<IDatabaseInitializeService, DatabaseInitializeService>();
            serviceCollection.AddTransient<IReviewService, ReviewService>();
            serviceCollection.AddTransient<ITicketService, TicketService>();
            serviceCollection.AddTransient<ITripService, TripService>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
