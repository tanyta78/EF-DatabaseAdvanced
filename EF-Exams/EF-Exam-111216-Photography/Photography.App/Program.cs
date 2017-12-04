namespace Photography.App
{
    using System;
    using Data;
    using Microsoft.EntityFrameworkCore;

    public class Program
    {
        static void Main(string[] args)
        {
            var db = new PhotographyDbContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            Console.WriteLine("DATABASE CREATED");
        }
    }
}
