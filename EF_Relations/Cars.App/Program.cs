namespace Cars.App
{
    using System;
    using Data;

    public class Program
    {
       public static void Main(string[] args)
       {
           var context = new CarDbContext();

           context.Database.EnsureDeleted();
           
           context.Database.EnsureCreated();
           
           Console.WriteLine();
       }
    }
}
