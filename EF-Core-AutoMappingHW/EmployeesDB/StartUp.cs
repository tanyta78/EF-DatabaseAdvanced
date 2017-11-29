namespace EmployeesDB
{
    using System;
    using AutoMapper;
    using Employees.App;
    using Employees.App.Core;
    using Employees.Data;
    using Employees.Services;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;


    public class StartUp
    {
        public static void Main()
        {

            var serviceProvider = ConfigureServices();
            Engine engine = new Engine(serviceProvider);
            engine.Run();

        }

        private static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<EmployeeDbContext>(options =>
                options.UseSqlServer(ServerConfig.ConnectionString)
            );

            serviceCollection.AddAutoMapper(cfg=>cfg.AddProfile<MappingProfile>());
            
            serviceCollection.AddTransient<EmployeeService>(); 
           
            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
