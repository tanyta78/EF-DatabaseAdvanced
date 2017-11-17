namespace P01_BillsPaymentSystem.Data.DBInitializer.Generators
{
    using System;
    using Models;

    public class BankAccountGenerator
    {
        /*
            BankAccountId
            Balance
            BankName (up to 50 characters, unicode)
            SWIFT Code (up to 20 characters, non-unicode)
          */

        private static Random rnd = new Random();

        private static string[] bankNames =
        {
            "FI BANK",
            "DSK BANK",
            "BULBANK",
            "RAIFAIZENBANK",
            "SGExpressbank"
        };

        private static string[] SwiftCodes =
        {
            "ERTG4567",
            "FRTGKJHU",
            "TYHUVARS",
            "SOF4RGTH",
            "SGERVARS",
            "PLOVRFTG",
            "BURGFTRH",
            "SOFTYHGT"
        };

        private static decimal NewBalance()
        {
            double balance = rnd.NextDouble() * 1600;

            return Convert.ToDecimal(balance);
        }

        public static void InitialBankAccountSeed(BillsPaymentSystemContext db, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var account = NewAccount();

                db.BankAccounts.Add(account);
                db.SaveChanges();
            }
        }

        private static BankAccount NewAccount()
        {
            BankAccount account = new BankAccount()
            {
                Balance = NewBalance(),
                BankName = GetRandomBankName(),
                SwiftCode = GetRandomSwiftCode()
            };

            return account;
        }

        private static string GetRandomSwiftCode()
        {
            return SwiftCodes[rnd.Next(0, SwiftCodes.Length)];
        }

        private static string GetRandomBankName()
        {
            return bankNames[rnd.Next(0, bankNames.Length)];
        }
    }
}