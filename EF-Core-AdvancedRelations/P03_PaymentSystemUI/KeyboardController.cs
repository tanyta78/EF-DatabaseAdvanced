namespace P03_PaymentSystemUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
     using P01_BillsPaymentSystem.Data;
    using P01_BillsPaymentSystem.Data.Models;

    public class KeyboardController
    {
        public static bool PageController(ConsoleKeyInfo key, Paginator paginator, BillsPaymentSystemContext context)
        {
            switch (key.Key.ToString())
            {
                case "Enter":
                    var userIDAsString = paginator
                        .Data
                        .Skip(paginator.PageSize * paginator.CurrentPage + paginator.CursorPos - 1)
                        .First()
                        .Substring(0, 4)
                       ;
                    int userId = int.Parse(userIDAsString);

                    var currentUser = context.Users.FirstOrDefault(u => u.UserId == userId);

                    if (currentUser==null)
                    {
                        Console.WriteLine($"User with id {userId} not found!");
                        break;
                    }
                    
                    ShowDetails(currentUser,context);
                    break;
                case "UpArrow":
                    if (paginator.CursorPos > 1)
                    {
                        paginator.CursorPos--;
                    }
                    else if (paginator.CurrentPage > 0)
                    {
                        paginator.CurrentPage--;
                        paginator.CursorPos = paginator.PageSize;
                    }
                    break;
                case "DownArrow":
                    if (paginator.CursorPos < paginator.PageSize)
                    {
                        if (paginator.CurrentPage == paginator.MaxPages - 1 && paginator.CursorPos + 1 > paginator.Data.Count % paginator.PageSize)
                        {
                            break;
                        }
                        paginator.CursorPos++;
                    }
                    else if (paginator.CurrentPage + 1 < paginator.MaxPages)
                    {
                        paginator.CurrentPage++;
                        paginator.CursorPos = 1;
                    }
                    break;
                case "Escape": return false;
            }

            return true;
        }

        public static void ShowDetails(User user, BillsPaymentSystemContext context)
        {

            //------------------------------------------
            Console.Clear();
            Console.WriteLine($"ID: {user.UserId,4}   |  Name {user.FirstName} {user.LastName} ");
            Console.Write(new string('=', Console.WindowWidth));
           
            Console.WriteLine($"(Page )");
            Console.WriteLine("===================================");
            var pageSize = 16 - Console.CursorTop;

            var accounts = context.PaymentMethods.Where(pm =>
                (pm.UserId == user.UserId && pm.Type == PaymentMethodType.BankAccount)).ToList();
            var creditCards = context.PaymentMethods.Where(pm =>
                (pm.UserId == user.UserId && pm.Type == PaymentMethodType.CreditCard)).ToList();

            int page = 0;
            int maxPages = (int)Math.Ceiling(accounts.Count / (double)pageSize);
            int pointer = 1;

            while (true)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();

                Console.WriteLine($"ID: {user.UserId,4}   |  Name {user.FirstName} {user.LastName} ");
                Console.Write(new string('=', Console.WindowWidth));
                
                PrintBankAccounts(pageSize, accounts, page, maxPages, pointer,context);

                PrintCreditCards(pageSize, creditCards, page, maxPages, pointer, context);

                var key = Console.ReadKey(true);
                switch (key.Key.ToString())
                {
                    case "UpArrow":
                        if (pointer > 1)
                        {
                            pointer--;
                        }
                        else if (page > 0)
                        {
                            page--;
                            pointer = pageSize;
                        }
                        break;
                    case "DownArrow":
                        if (pointer < pageSize)
                        {
                            pointer++;
                        }
                        else if (page + 1 < maxPages)
                        {
                            page++;
                            pointer = 1;
                        }
                        break;
                    case "Escape": return;
                }
            }
            //=================================
        }

        private static void PrintCreditCards(int pageSize, List<PaymentMethod> creditCards, int page, int maxPages, int pointer, BillsPaymentSystemContext context)
        {
            Console.WriteLine($"Credit cards:");
            Console.Write(new string('=', Console.WindowWidth));
            if (creditCards.Count==0)
            {
                Console.WriteLine("No credit cards owned.");
                return;
            }
           
            Console.WriteLine($"(Page {page + 1} of {maxPages})");
            Console.WriteLine("===================================");

            int current = 1;
            foreach (var card in creditCards.Skip(pageSize * page).Take(pageSize))
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.White;

                if (current == pointer)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                }

                var cardInfo = context.CreditCards.First(cc => cc.CreditCardId == card.CreditCardId);

                Console.WriteLine($"-- ID:{cardInfo.CreditCardId}");
                Console.WriteLine($"---Limit: {cardInfo.Limit}");
                Console.WriteLine($"---Money Owed: {cardInfo.MoneyOwed}");
                Console.WriteLine($"---Limit left: {cardInfo.LimitLeft}");
                Console.WriteLine($"---Expiration Date: {cardInfo.ExpirationDate}");
                current++;

            }

            var key = Console.ReadKey(true);
            switch (key.Key.ToString())
            {
                case "UpArrow":
                    if (pointer > 1)
                    {
                        pointer--;
                    }
                    else if (page > 0)
                    {
                        page--;
                        pointer = pageSize;
                    }
                    break;
                case "DownArrow":
                    if (pointer < pageSize)
                    {
                        pointer++;
                    }
                    else if (page + 1 < maxPages)
                    {
                        page++;
                        pointer = 1;
                    }
                    break;
                case "Escape": return;
            }
        }

        private static void PrintBankAccounts(int pageSize, List<PaymentMethod> accounts, int page, int maxPages, int pointer, BillsPaymentSystemContext context)
        {
            Console.WriteLine($"Bank Accounts:");
            Console.Write(new string('=', Console.WindowWidth));
            if (accounts.Count == 0)
            {
                Console.WriteLine("No bank account owned.");
                return;
            }
            Console.WriteLine($"(Page {page + 1} of {maxPages})");
            Console.WriteLine("===================================");

            int current = 1;
            foreach (var acc in accounts.Skip(pageSize * page).Take(pageSize))
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.White;

                if (current == pointer)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                }

                var accInfo = context.BankAccounts.First(ba => ba.BankAccountId == acc.BankAccountId);
                
                Console.WriteLine($"-- ID:{accInfo.BankAccountId}");
                Console.WriteLine($"---Balance: {accInfo.Balance}");
                Console.WriteLine($"---Bank: {accInfo.BankName}");
                Console.WriteLine($"---SWIFT: {accInfo.SwiftCode}");
                current++;
                
            }

            var key = Console.ReadKey(true);
            switch (key.Key.ToString())
            {
                case "UpArrow":
                    if (pointer > 1)
                    {
                        pointer--;
                    }
                    else if (page > 0)
                    {
                        page--;
                        pointer = pageSize;
                    }
                    break;
                case "DownArrow":
                    if (pointer < pageSize)
                    {
                        pointer++;
                    }
                    else if (page + 1 < maxPages)
                    {
                        page++;
                        pointer = 1;
                    }
                    break;
                case "Escape": return;
            }
        }
    }
}