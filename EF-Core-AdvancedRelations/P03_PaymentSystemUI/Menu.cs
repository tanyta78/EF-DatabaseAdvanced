namespace P03_PaymentSystemUI
{
    using System;
    using System.Collections.Generic;
    using P01_BillsPaymentSystem.Data;

    public class Menu
    {
        public static int Choice = 1;

        public static void Initialize()
        {
            ConsoleSetup.SetUp();
          
            Mode.Set(Mode.AppMode.Menu);
            
            MakeChoice();
            ExecuteCommand();
        }
        

        private static Dictionary<int, Action> Commands = new Dictionary<int, Action>()
        {
            {1, UsersInfo.ListAll },
            {2, UsersInfo.UserDetails },
            {3, UserUpdate.AddUser },
            {4, UserUpdate.RemoveUser },
            {5, UserBills.PayBills },
            {6, () => Console.WriteLine(Constants.ThanksMessage) }
        };

        public static List<string> Choices = new List<string>()
        {
            "List users",
            "View user details",
            "Add User",
            "Delete User",
            "Pay user's bills",
            "Exit"
        };

        private static void PrintChoices(List<string> Choices)
        {
            Console.Clear();
           
            Console.WriteLine("Hello User! Please choose one option!");
            Console.WriteLine("=====================================");
            
            for (int i = 0; i < Choices.Count; i++)
            { 
                if (Choice != i + 1)
                {
                    Console.WriteLine($"{i + 1}. {Choices[i]}", System.Drawing.Color.GhostWhite);
                }
                else
                {
                    
                    Console.WriteLine($">> {i + 1}. {Choices[i]} <<", System.Drawing.Color.Lime);
                }
            }
        }

       
        public static void MakeChoice()
        {
            
            while (true)
            {
                PrintChoices(Choices);

                var pressedKey = Console.ReadKey().Key;
                if (pressedKey == ConsoleKey.Enter)
                {
                    break;
                }
                switch (pressedKey)
                {
                    case ConsoleKey.UpArrow:
                        if (Choice == 1)
                        {
                           Choice = Choices.Count;
                        }
                        else
                        {
                            Choice--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (Choice == Choices.Count)
                        {
                            Choice = 1;
                        }
                        
                        else
                        {
                            Choice++;
                        }
                        break;
                    //case ConsoleKey.Escape:
                    //    Console.WriteLine(Constants.ThanksMessage);
                    //    break;
                }
            }
        }

        private static void ExecuteCommand()
        {
            if (!Commands.ContainsKey(Choice))
            {
                throw new NotImplementedException();
            }
            else
            {
                Commands[Choice]();
            }
        }
    }
}
