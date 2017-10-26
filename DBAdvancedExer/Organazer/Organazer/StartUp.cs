namespace Organazer
{
    using System;
    using Data;
    using Input;
    using ScreenControllers;
    using ScreenElements;
    using Utility;

    public class StartUp
    {
        public static void Main()
        {
            // Setup screen
            Console.WindowWidth = 50;
            Console.WindowHeight = 17;
            Console.BufferWidth = 50;
            Console.BufferHeight = 17;

            // Setup element style
            ScreenElement.DefaultBGColor = ConsoleColor.Blue;
            ScreenElement.DefaultFGColor = ConsoleColor.Yellow;
            Console.Clear();
            Console.Write("Initializing...");

            // Establish DB connection
            var context = new OrganazerContext();
            context.Database.Initialize(true);

            // Hide cursor after initialization is completed
            Console.CursorVisible = false;

            //ConsoleConfig.DefaultBackgroundColor = ConsoleColor.Black;
            //ConsoleConfig.DefaultForegroundColor = ConsoleColor.Gray;

            var app = new Organazer(context);
            app.Start();

            Console.Clear();
            Console.CursorVisible = true;
            Console.Write("Closing connection...");
            Console.WriteLine();

        }
    }
}
