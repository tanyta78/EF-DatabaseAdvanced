namespace EFAdvancedQuering
{
    using System;
    using System.Data;
    using System.Data.Entity;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Reflection;
    using Data;
    using EntityFramework.Extensions;
    using Models;

    public class Startup
    {
        public static void Main()
        {
            var context = new QueryContext();
            context.Database.Initialize(true);

           // var query = "SELECT * FROM Clients WHERE Name LIKE @nameParam";
           // var nameParam = new SqlParameter("@nameParam","Peter%");
           // var clients = context.Database.SqlQuery<Client>(query,nameParam);

           // foreach (var client in clients)
           // {
           //     Console.WriteLine(client.Name + " | " + client.Address);
           // }

           // var cliento = context.Clients.FirstOrDefault();
           // Console.WriteLine(context.Entry(cliento).State);

           // context.Entry(cliento).State = EntityState.Detached;
           // cliento.Name = "Stoyan Stoyanov";
           // context.SaveChanges();

           //var types = Assembly.GetExecutingAssembly().DefinedTypes;
           // foreach (var type in types)
           // {
           //     Console.WriteLine(type.Name);
               
           // }

           //var firstclients= context.Clients.Take(3);

           // context.Clients.Where(c=>c.Name=="Ivan Ivanov").Delete();

           // context.SaveChanges();
           
           // context.Clients.Update(c => new Client {Age = 18});
           // context.SaveChanges();

           // context.Clients.Where(c => c.Age < 18).Delete();
           // context.SaveChanges();

            //var param= new SqlParameter("@param",SqlDbType.Int);
            //param.Value = 3;
            //context.Database.ExecuteSqlCommand("UpdateAge @param", param);
        }
    }
}
