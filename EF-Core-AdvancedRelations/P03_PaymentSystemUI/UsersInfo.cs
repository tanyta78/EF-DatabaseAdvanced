namespace P03_PaymentSystemUI
{
    using System;
    using System.Linq;
    using P01_BillsPaymentSystem.Data;

    public class UsersInfo
    {
        public static void ListAll()
        {

            using (var db = new BillsPaymentSystemContext())
            {
                var data = db.Users
                    .Select(u => new
                    {
                        UserId = u.UserId,
                        Name = u.FirstName +" "+ u.LastName
                    })
                    .ToList();

                var usersPaginator = new Paginator(data

                    .Select(p => $"{p.UserId,4}|{p.Name}").ToList(), 2, 0, 14, true);

                while (true)
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(
                        $"ID   |   User name (Page {usersPaginator.CurrentPage + 1} of {usersPaginator.MaxPages})");
                    Console.WriteLine("===================================");

                    usersPaginator.Print();

                    var key = Console.ReadKey(true);

                    if (!KeyboardController.PageController(key, usersPaginator, db))
                    {
                        Menu.Initialize();
                    };
                }
            }
        }

        public static void UserDetails()
        {
          
            using (var db = new BillsPaymentSystemContext())
            {
                Console.Clear();
                Console.WriteLine(
                    $"Please insert userId to search!");
                Console.WriteLine("===================================");

                int userId = int.Parse(Console.ReadLine());

                var currentUser = db.Users.FirstOrDefault(u => u.UserId == userId);

                if (currentUser == null)
                {
                    Console.WriteLine($"User with id {userId} not found!");

                }
                else
                {
                    KeyboardController.ShowDetails(currentUser, db);
                }
                
                var key = Console.ReadKey(true);

                Menu.Initialize();
                

            }
        }

    }

}
