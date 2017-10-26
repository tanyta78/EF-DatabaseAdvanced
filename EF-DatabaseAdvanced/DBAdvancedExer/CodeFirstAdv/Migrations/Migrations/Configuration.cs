namespace Migrations.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using Models;


    internal sealed class Configuration : DbMigrationsConfiguration<SalesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SalesContext context)
        {
            context.Products.AddOrUpdate(new Product(){Name="Car",Description = "Vehicle",Price = 8000m,Quantity = 10});
            context.Products.AddOrUpdate(new Product() { Name = "Mercedes", Description = "Vehicle", Price = 18000m, Quantity = 5 });
            context.Products.AddOrUpdate(new Product() { Name = "BMV", Description = "Vehicle", Price = 5000m, Quantity = 10 });

           context.Customers.AddOrUpdate(new Customer()
           {
               FirstName = "Pesho",
               LastName = "Todorov",
               CreditCardNumber = "wefwergqgr"
            });

            context.Customers.AddOrUpdate(new Customer()
            {
                FirstName = "Goro",
                LastName = "Ivanov",
                CreditCardNumber = "dsfgsh"
            });

            context.Customers.AddOrUpdate(new Customer()
            {
                FirstName = "Ivan",
                LastName = "Stoyanov",
                CreditCardNumber = "rgthrthr"
            });

            context.StoreLocations.AddOrUpdate(new StoreLocation()
            {
                LocationName = "Varna"
            });

            context.StoreLocations.AddOrUpdate(new StoreLocation()
            {
                LocationName = "Sofia"
            });


            context.StoreLocations.AddOrUpdate(new StoreLocation()
            {
                LocationName = "Plovdiv"
            });

           

        }
    }
}
