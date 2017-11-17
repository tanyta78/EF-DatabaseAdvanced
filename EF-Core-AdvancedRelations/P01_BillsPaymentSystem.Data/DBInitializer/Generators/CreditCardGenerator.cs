namespace P01_BillsPaymentSystem.Data.DBInitializer.Generators
{
    using System;
    using Models;
    using UtilGenerators;

    internal class CreditCardGenerator
    {
        /*
            CreditCardId
            Limit
            MoneyOwed
            LimitLeft (calculated property, not included in the         database)
            ExpirationDate

         */
        private static Random rnd = new Random();

        private static decimal Limit()
        {
            double balance = rnd.NextDouble() * 2600;

            return Convert.ToDecimal(balance);
        }

        private static decimal MoneyOwed()
        {
            double balance = rnd.NextDouble() * 600;

            return Convert.ToDecimal(balance);
        }

        private static CreditCard NewCreditCard()
        {
            CreditCard creditCard = new CreditCard()
            {
                Limit = Limit(),
                MoneyOwed = MoneyOwed(),
                ExpirationDate = DateGenerator.GenerateDate()
            };

            return creditCard;
        }

        public static void InitialCreditCardSeed(BillsPaymentSystemContext db, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var creditCard = NewCreditCard();

                db.CreditCards.Add(creditCard);
                db.SaveChanges();
            }
        }
    }
}
