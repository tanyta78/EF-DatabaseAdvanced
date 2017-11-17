namespace P01_BillsPaymentSystem.Data.DBInitializer
{
    using System;
    using Generators;
    using Microsoft.EntityFrameworkCore;

    public class DatabaseInitializer
    {
        private static Random rnd = new Random();

        public static void ResetDatabase()
        {
            using (var db = new BillsPaymentSystemContext())
            {
                db.Database.EnsureDeleted();

                db.Database.Migrate();

                InitialSeed(db);
            }
        }

        public static void InitialSeed(BillsPaymentSystemContext db)
        {
            SeedUsers(db, 100);

            SeedBankAccounts(db, 50);

            SeedCreditCards(db, 50);

            SeedPaymentMethods(db, 100);
        }


        private static void SeedCreditCards(BillsPaymentSystemContext db, int count)
        {
            CreditCardGenerator.InitialCreditCardSeed(db, count);
        }

        private static void SeedPaymentMethods(BillsPaymentSystemContext db, int count)
        {
            PaymentMethodsGenerator.InitialPaymentMethodSeed(db, count);
        }

        private static void SeedBankAccounts(BillsPaymentSystemContext db, int count)
        {
            BankAccountGenerator.InitialBankAccountSeed(db, count);
        }

        private static void SeedUsers(BillsPaymentSystemContext db, int count)
        {
            UserGenerator.InitialUserSeed(db, count);
        }
    }
}
