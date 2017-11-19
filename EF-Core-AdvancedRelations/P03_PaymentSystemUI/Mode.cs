namespace P03_PaymentSystemUI
{
    using System;
    
    public class Mode
    {
        public enum AppMode
        {
            Menu,
            Details,
            BillsPayment
        }


        public static void Set(AppMode gm)
        {
            switch (gm)
            {
               case AppMode.Menu:
                    UpdateConsole(Constants.ConsoleMenuWidth, Constants.ConsoleMenuHeigth,ConsoleColor.Blue,ConsoleColor.White);
                    break;
                case AppMode.Details:
                    UpdateConsole(Constants.ConsoleDetailsWidth, Constants.ConsoleDetailsHeigth, ConsoleColor.DarkMagenta, ConsoleColor.White);
                    break;
                case AppMode.BillsPayment:
                    UpdateConsole(Constants.ConsolePaymentWidth, Constants.ConsolePaymentHeigth, ConsoleColor.Green, ConsoleColor.White);
                    break;
            }
        }

        private static void UpdateConsole(int width, int height, ConsoleColor back, ConsoleColor fore)
        {
            Console.Clear();
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
            Console.BackgroundColor = back;
            Console.ForegroundColor = fore;
        }
    }
}