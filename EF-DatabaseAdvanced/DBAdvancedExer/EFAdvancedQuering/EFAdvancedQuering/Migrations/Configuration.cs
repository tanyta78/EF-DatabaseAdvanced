namespace EFAdvancedQuering.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq.Expressions;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<EFAdvancedQuering.Data.QueryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "EFAdvancedQuering.Data.QueryContext";
        }

        protected override void Seed(EFAdvancedQuering.Data.QueryContext context)
        {


            var product = new Product
            {
                Name = "Samsung J6 2016"
            };

            var client = new Client
            {
                Name = "Maria Marinova",
                Address = "Varna 34-35",
                Age = 35,
            };

            var order = new Order();
          
            order.Products.Add(product);
            order.Client = client;
            client.Orders.Add(order);

            context.Clients.AddOrUpdate(c => c.Name,
                new Client
                {
                    Name = "Peter Petrov",
                    Address = "Varna 34-35",
                    Age = 18
                },
                new Client
                {
                    Name = "Ivan Ivanov",
                    Address = "Pozitano 5",
                    Age = 22
                },
                new Client
                {
                    Name = "George Georgiev",
                    Address = "Stolipinovo 3",
                    Age = 12
                },
               new Client
               {
                   Name = "Vanya Ivanova",
                   Address = "Varna 33",
                   Age = 33
               },
               client

            );
            context.Products.AddOrUpdate(p => p.Name, product);
            context.Orders.AddOrUpdate(o=>o.ClientId,order);

            context.SaveChanges();
        }
    }
}
