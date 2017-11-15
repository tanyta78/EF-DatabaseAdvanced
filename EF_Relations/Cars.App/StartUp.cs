namespace Cars.App
{
    using System;
    using Data;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
       public static void Main(string[] args)
       {
           var context = new CarDbContext();

           context.Database.EnsureDeleted();
           
           context.Database.Migrate();
           
           Console.WriteLine();
       }
    }
}
