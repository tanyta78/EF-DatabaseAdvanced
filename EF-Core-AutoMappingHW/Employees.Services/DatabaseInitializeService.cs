namespace Employees.Services
{
    using System;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class DatabaseInitializeService: IDatabaseInitializeService
    {
        private readonly EmployeeDbContext context;

        public DatabaseInitializeService(EmployeeDbContext context)
        {
            this.context = context;
        }

        public void DatabaseInitialize()
        {

            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();
            
            
          // this.context.Database.EnsureDeleted();
           // this.context.Database.Migrate();
           // Console.WriteLine("Successfull creation/migration");

        }
    }
}
