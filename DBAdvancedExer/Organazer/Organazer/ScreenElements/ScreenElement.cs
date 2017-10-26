namespace Organazer.ScreenElements
{
    using System;
    using Utility;

    public abstract class ScreenElement
    {
        private int x;
        private  int y;
        private int startingX;
        private int startingY;
        protected  ConsoleColor backgroundColor;
        protected ConsoleColor foregroundColor;

        public ScreenElement(int x, int y)
        {
            this.x = x;
            this.y = y;

            this.backgroundColor = ScreenElement.DefaultBGColor;
            this.foregroundColor = ScreenElement.DefaultFGColor;

        }

        public int X { get { return this.x; } }
        public int Y { get { return this.x; } }

        // Default settings for all elements
        public static ConsoleColor DefaultBGColor = ConsoleColor.Black;
        public static ConsoleColor DefaultFGColor = ConsoleColor.Gray;

        public virtual void SetBackgroundColor(ConsoleColor color)
        {
            this.backgroundColor = color;
        }

        public virtual void SetForegroundColor(ConsoleColor color)
        {
            this.foregroundColor = color;
        }

        private void BeginPrint()
        {
            this.startingX = Console.CursorLeft;
            this.startingY = Console.CursorTop;
        }

        protected abstract void Render();

        private void EndPrint()
        {
            Console.CursorLeft = this.startingX;
            Console.CursorTop = this.startingY;
        }

        private void ApplyRelativePosition()
        {
            Console.CursorLeft += this.x;
            Console.CursorTop += this.y;
        }

        public void Print(bool highlighted = false)
        {
            if (highlighted)
            {
                Console.BackgroundColor = this.foregroundColor;
                Console.ForegroundColor = this.backgroundColor;
            }
            else
            {
                Console.BackgroundColor = this.backgroundColor;
                Console.ForegroundColor = this.foregroundColor;
            }

            this.BeginPrint();
            this.ApplyRelativePosition();
            this.Render();
            this.EndPrint();

            if (highlighted)
            {
                Console.BackgroundColor = this.backgroundColor;
                Console.ForegroundColor = this.foregroundColor;
            }
        }

    }
}
