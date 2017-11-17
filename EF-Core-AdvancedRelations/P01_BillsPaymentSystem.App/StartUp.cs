namespace P01_BillsPaymentSystem.App
{
    using System;
    using Data;
    using Data.DBInitializer;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (var db = new BillsPaymentSystemContext())
            {
               
               DatabaseInitializer.ResetDatabase();
                
                //db.Database.EnsureDeleted();
                ////db.Database.EnsureCreated();
                //db.Database.Migrate();
                //Seed(db);
            }
        }

        private static void Seed(BillsPaymentSystemContext db)
        {
            using (db)
            {

                var user = new User()
                {
                    FirstName = "Ivan",
                    LastName = "Petrov",
                    Email = "pesho@abv.bg",
                    Password = "azsympesho"
                };

                var creditCards = new CreditCard[]
                {
                    new CreditCard()
                    {
                        ExpirationDate = DateTime.ParseExact("20.05.2020", "dd.MM.yyyy", null),
                        Limit = 1000m,
                        MoneyOwed = 5m
                    },
                    new CreditCard()
                    {
                        ExpirationDate = DateTime.ParseExact("20.05.2020", "dd.MM.yyyy", null),
                        Limit = 2500m,
                        MoneyOwed = 300m
                    }
                };

                var bankAccount = new BankAccount()
                {
                    Balance = 3000m,
                    BankName = "Swiss Bank",
                    SwiftCode = "SSWSBKJS"
                };
                
                var paymentMethods = new PaymentMethod[]
                {
                    new PaymentMethod()
                    {
                        User = user,
                        CreditCard = creditCards[0],
                        Type = PaymentMethodType.CreditCard
                    },
                    new PaymentMethod()
                    {
                        User = user,
                        CreditCard = creditCards[1],
                        Type = PaymentMethodType.CreditCard
                    },
                    new PaymentMethod()
                    {
                        User = user,
                        BankAccount = bankAccount,
                        Type = PaymentMethodType.BankAccount
                    }
                    
                };

                db.Users.Add(user);
                db.CreditCards.AddRange(creditCards);
                db.BankAccounts.Add(bankAccount);
                db.PaymentMethods.AddRange(paymentMethods);
                db.SaveChanges();
            }
        }
    }
}
