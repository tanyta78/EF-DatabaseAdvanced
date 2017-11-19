namespace P03_PaymentSystemUI
{
    using System;

    internal class UserUpdate
    {
        public static void AddUser()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Under construction...");

            var key = Console.ReadKey(true);
            Menu.Initialize();

        }

        public static void RemoveUser()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Under construction...");

            var key = Console.ReadKey(true);
            Menu.Initialize();
        }
    }
}