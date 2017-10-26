namespace Migrations
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Models;

    public class StartUp
    {
       public static void Main()
        {
            SalesContext context = new SalesContext();

            //Sale carSale = new Sale();
            //carSale.Product = car;
            //carSale.Customer = ivan;
            //carSale.StoreLocation = sofia;
            //carSale.Date = DateTime.Now;

            //Sale motorSale = new Sale();
            //motorSale.Product = motorcicle;
            //motorSale.Customer = goro;
            //motorSale.StoreLocation = plovdiv;
            //motorSale.Date = DateTime.Now;

            //Sale truckSale = new Sale();
            //truckSale.Product = truck;
            //truckSale.Customer = ivan;
            //truckSale.StoreLocation = sofia;
            //truckSale.Date = DateTime.Now;

            //context.Sales.Add(carSale);
            //context.Sales.Add(truckSale);
            //context.Sales.Add(motorSale);

            //context.SaveChanges();

            // Database.SetInitializer<SalesContext>(new DropCreateDatabaseAlways<SalesContext>());


           // context.Database.Delete();
           var res= context.Customers.Find(1);
            Console.WriteLine(res.Age);
            context.Customers.Add(new Customer()
            {
                FirstName = "Ivan",
                LastName = "fff",
                CreditCardNumber = "rtertewywey"
            });
            context.SaveChanges();

            var result = context.Customers.FirstOrDefault(c => c.LastName == "fff");
            Console.WriteLine(result.Age);
        }
    }
}
