namespace PhotoShare.Services
{
    using System;
    using Contracts;
    using Data;
    using Models;
    using System.Linq;

    public class TownService : ITownService
    {
        private readonly PhotoShareContext db;

        public TownService(PhotoShareContext db)
        {
            this.db = db;
        }

        public string AddTown(string townName, string countryName)
        {
            CheckCredentials();

            
            if (db.Towns.Any(t => t.Name == townName))
            {
                throw new ArgumentException($"Town {townName} was already added!");
            }


            Town town = new Town
            {
                Name = townName,
                Country = countryName
            };

            db.Towns.Add(town);
            db.SaveChanges();

            return $"Town {townName} was added to database!";

        }

       private static void CheckCredentials()
        {
            if (Session.User == null)
            {
                throw new ArgumentException("You should login first!");
            }

        }
    }
}
