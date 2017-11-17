namespace P01_BillsPaymentSystem.Data.DBInitializer.Generators
{
    using System;
    using Models;

    public class UserGenerator
    {
        /*
            FirstName (up to 50 characters, unicode)
            LastName (up to 50 characters, unicode)
            Email (up to 80 characters, non-unicode)
            Password (up to 25 characters, non-unicode)

         */
        private static Random rnd = new Random();

        private static string[] userFirstNames =
        {
            "Ivan",
            "Stoyan",
            "Georgy",
            "Petar",
            "Marian",
            "Todor",
            "Dimitar"
        };

        private static string[] userLastNames =
        {
            "Ivanov",
            "Stoyanov",
            "Georgiev",
            "Petrov",
            "Marianov",
            "Todorov",
            "Dimitrov"
        };

        private static string[] userEmails =
        {
            "pass@gmail.com",
            "1@abv.bg",
            "GermanMan@gmail.com",
            "gtgt@yahoo.com",
            "propp23@abv.bg",
            "TrfW@gmail.com",
            "Dim@abv.bg"
        };

        private static string[] userPasswords =
        {
            "vf45dfwgter!",
            "fd1dsge24!",
            "ggtSDEDF1232!",
            "GYHJ67sdf!3453",
            "hyg!34rfth",
            "dream23TEAM",
            "GoTOtheMO00!!34N"
        };

        internal static void InitialUserSeed(BillsPaymentSystemContext db, int count)
        {
            for (int i = 0; i < count; i++)
            {
                db.Users.Add(NewUser());
                db.SaveChanges();
            }
        }

        private static User NewUser()
        {
            User user = new User()
            {
                FirstName = GenerateUserFirstName(),
                LastName = GenerateUserLastName(),
                Email = GenerateEmail(),
                Password = GeneratePassword()
            };

            return user;
        }

        private static string GeneratePassword()
        {
            return userPasswords[rnd.Next(0, userPasswords.Length)];
        }

        private static string GenerateEmail()
        {
            return userEmails[rnd.Next(0, userEmails.Length)];
        }

        private static string GenerateUserLastName()
        {
            return userLastNames[rnd.Next(0, userLastNames.Length)];
        }

        private static string GenerateUserFirstName()
        {
            return userFirstNames[rnd.Next(0, userFirstNames.Length)];
        }
    }
}