namespace P01_BillsPaymentSystem.Data.DBInitializer.Generators
{
    using System;
    using System.Linq;
    using Models;

    public class PaymentMethodsGenerator
    {
        private static Random rnd = new Random();

        public static void InitialPaymentMethodSeed(BillsPaymentSystemContext db, int count)
        {
            for (int i = 1; i <= count/2; i++)
            {
                var paymentMethod = NewAccountPaymentMethod(db,i);

                db.PaymentMethods.Add(paymentMethod);
                db.SaveChanges();
            }

            for (int i = 1; i <= count/2; i++)
            {
                var paymentMethod = NewCreditPaymentMethod(db,i,count);

                db.PaymentMethods.Add(paymentMethod);
                db.SaveChanges();
            }
        }

        private static PaymentMethod NewAccountPaymentMethod(BillsPaymentSystemContext db, int i)
        {
            var user = db.Users.FirstOrDefault(u => u.UserId == i);
            var acc = db.BankAccounts.FirstOrDefault(ba => ba.BankAccountId == i);

            PaymentMethod accountPaymentMethod = new PaymentMethod()
            {
                User = user,
                BankAccount =acc ,
                Type = PaymentMethodType.BankAccount
            };

            return accountPaymentMethod;
        }

        private static PaymentMethod NewCreditPaymentMethod(BillsPaymentSystemContext db, int i, int count)
        {
            PaymentMethod creditPaymentMethod = new PaymentMethod()
            {
                User = db.Users.FirstOrDefault(u=>u.UserId==i+count/2),
                CreditCard = db.CreditCards.FirstOrDefault(cc=>cc.CreditCardId==i),
                Type = PaymentMethodType.CreditCard
            };

            return creditPaymentMethod;
        }

        private static PaymentMethod NewRandomCreditPaymentMethod()
        {
            PaymentMethod creditPaymentMethod = new PaymentMethod()
            {
                UserId = GetRandomUserFromDb(),
                CreditCardId = GetRandomCreditCardFromDb(),
                Type = PaymentMethodType.CreditCard
            };

            return creditPaymentMethod;
        }

        private static int? GetRandomCreditCardFromDb()
        {
            using (var db = new BillsPaymentSystemContext())
            {
                var creditCarsIds = db
                    .CreditCards
                    .Select(cc=>cc.CreditCardId)
                    .ToArray();

                return creditCarsIds[rnd.Next(0, creditCarsIds.Length)];
            }
        }

        private static PaymentMethod NewRandomAccountPaymentMethod()
        {
            PaymentMethod accountPaymentMethod = new PaymentMethod()
            {
                UserId = GetRandomUserFromDb(),
                BankAccountId = GetRandomBankAccountFromDb(),
                Type = PaymentMethodType.BankAccount
            };

            return accountPaymentMethod;
        }

        private static int? GetRandomBankAccountFromDb()
        {
            using (var db = new BillsPaymentSystemContext())
            {
                var accountsIds = db
                    .BankAccounts
                    .Select(ba=>ba.BankAccountId)
                    .ToArray();

                return accountsIds[rnd.Next(0, accountsIds.Length)];
            }
        }

        private static int GetRandomUserFromDb()
        {
            using (var db = new BillsPaymentSystemContext())
            {
                var usersIds = db
                    .Users
                    .Select(s => s.UserId)
                    .ToArray();

                return usersIds[rnd.Next(0, usersIds.Length)];
            }
        }
    }
}