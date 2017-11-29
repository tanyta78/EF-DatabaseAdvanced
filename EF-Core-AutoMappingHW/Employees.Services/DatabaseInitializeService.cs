namespace Employees.Services
{
    using System;
    using System.Collections.Generic;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Models;

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
            
            this.context.Database.EnsureDeleted();
            this.context.Database.Migrate();
            Seed();
            Console.WriteLine("Successfull creation/migration/seed");

        }

        private static void Seed()
        {
            List<Employee> employees = CreateEmployeesAndManagers();

            var context = new EmployeeDbContext();
            context.Employees.AddRange(employees);
            context.SaveChanges();
        }

        private static List<Employee> CreateEmployeesAndManagers()
        {
            List<Employee> employees = new List<Employee>();

            Employee manager = new Employee()
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                Salary = 1000,
                Birthday = new DateTime(1980, 08, 08)
            };
            Employee manager2 = new Employee()
            {
                FirstName = "Stoyan",
                LastName = "Addov",
                Salary = 2000,
                Birthday = new DateTime(1980, 08, 08)
            };
            Employee employee1 = new Employee()
            {
                FirstName = "Stephen",
                LastName = "Ivanov",
                Salary = 4000,
                Manager = manager,
                Birthday = new DateTime(1980, 08, 08)
            };
            Employee employee2 = new Employee()
            {
                FirstName = "Kiril",
                LastName = "Metodiev",
                Salary = 5000,
                Manager = manager,
                Birthday = new DateTime(1990, 08, 08)
            };
            Employee employee3 = new Employee()
            {
                FirstName = "Gogo",
                LastName = "Gogov",
                Salary = 1000,
                Manager = manager2,
                Birthday = new DateTime(1980, 08, 08)
            };
            Employee employee4 = new Employee()
            {
                FirstName = "Sofia",
                LastName = "Loren",
                Salary = 1234,
                Manager = manager2,
                Birthday = new DateTime(1990, 08, 08)
            };
            Employee employee5 = new Employee()
            {
                FirstName = "Bobob",
                LastName = "Hohoh",
                Salary = 3424,
                Manager = manager2,
                Birthday = new DateTime(1990, 08, 08)
            };

            employees.Add(manager);
            employees.Add(manager2);
            employees.Add(employee1);
            employees.Add(employee2);
            employees.Add(employee3);
            employees.Add(employee4);
            employees.Add(employee5);

            return employees;
        }
    }
}
