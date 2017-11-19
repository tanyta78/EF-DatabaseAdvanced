namespace P03_PaymentSystemUI
{
    using System;
    using System.Linq;
    using P01_BillsPaymentSystem.Data;

    public class UserBills
    {
        public static void PayBills()
        {
            using (var db = new BillsPaymentSystemContext())
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Please insert userId");
                Console.CursorVisible = true;
                Console.WriteLine("===================================");
                var userId = int.Parse(Console.ReadLine());
                var user = db.Users.Find(userId);

               Console.WriteLine("Please insert amount for bills:");

                Console.WriteLine("===================================");
                var amount = decimal.Parse(Console.ReadLine());

                try
                {
                    PayBills(userId, amount, db);
                    Console.WriteLine("Bills have been successfully paid.");
                    Console.CursorVisible = false;
                    Console.ReadKey(true);
                    Menu.Initialize();
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static void PayBills(int userId, decimal amount, BillsPaymentSystemContext context)
        {
            var user = context.Users.Find(userId);

            if (user == null)
            {
                Console.WriteLine($"User with id {userId} not found!");
                return;
            }

            decimal userMoney = 0m;

            var bankAccounts = context
                .BankAccounts.Join(context.PaymentMethods,
                    (ba => ba.BankAccountId),
                    (p => p.BankAccountId),
                    (ba, p) => new
                    {
                        UserId = p.UserId,
                        BankAccountId = ba.BankAccountId,
                        Balance = ba.Balance
                    })
                .Where(ba => ba.UserId == userId)
                .ToList();


            var creditCards = context
                .CreditCards.Join(context.PaymentMethods,
                    (c => c.CreditCardId),
                    (p => p.CreditCardId),
                    (c, p) => new
                    {
                        UserId = p.UserId,
                        CreditCardId = c.CreditCardId,
                        LimitLeft = c.LimitLeft
                    })
                .Where(c => c.UserId == userId)
                .ToList();

            userMoney += bankAccounts.Sum(b => b.Balance);
            userMoney += creditCards.Sum(c => c.LimitLeft);

            if (userMoney < amount)
            {
                throw new InvalidOperationException("Insufficient funds!");
            }

            bool isBillsPayed = false;
            foreach (var bankAccount in bankAccounts.OrderBy(b => b.BankAccountId))
            {
                var currentAccount = context.BankAccounts.Find(bankAccount.BankAccountId);

                if (amount <= currentAccount.Balance)
                {
                    currentAccount.Withdraw(amount);
                    isBillsPayed = true;
                }
                else
                {
                    amount -= currentAccount.Balance;
                    currentAccount.Withdraw(currentAccount.Balance);
                }

                if (isBillsPayed)
                {
                    context.SaveChanges();
                    return;
                }
            }

            foreach (var creditCard in creditCards.OrderBy(c => c.CreditCardId))
            {
                var currentCreditCard = context.CreditCards.Find(creditCard.CreditCardId);

                if (amount <= currentCreditCard.LimitLeft)
                {
                    currentCreditCard.Withdraw(amount);
                    isBillsPayed = true;
                }
                else
                {
                    amount -= currentCreditCard.LimitLeft;
                    currentCreditCard.Withdraw(currentCreditCard.LimitLeft);
                }

                if (isBillsPayed)
                {
                    context.SaveChanges();
                    return;
                }
            }
        }
    }
}