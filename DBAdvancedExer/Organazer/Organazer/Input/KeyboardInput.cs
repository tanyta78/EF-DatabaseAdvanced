namespace Organazer.Input
{
    using System;
    using Enumerations;

    public class KeyboardInput
    {
        public Command Listen()
        {
            var next = Console.ReadKey(true);
            switch (next.Key.ToString())
            {
                case "UpArrow":
                    return Command.MoveUp;
                case "DownArrow":
                    return Command.MoveDown;
                case "LeftArrow":
                    return Command.MoveLeft;
                case "RightArrow":
                    return Command.MoveRight;
                case "Tab":
                    return Command.Toggle;
                case "Enter":
                    return Command.Execute;
                case "Escape":
                    return Command.Back;
                default:
                    return Command.Unknown;
            }
        }

        public string Read()
        {
            var input = Console.ReadLine();
            return input;
        }
    }
}
