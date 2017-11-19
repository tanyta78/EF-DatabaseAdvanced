namespace P03_PaymentSystemUI
{
    using System;

    public class ConsoleSetup
    {
        private static bool isSetUp = false;

        public static void SetUp()
        {
            if (!isSetUp)
            {
                Console.CursorVisible = false;
                Console.Title = "BILL PAYMENT SYSTEM";
                isSetUp = true;
            }
        }
    }
}
