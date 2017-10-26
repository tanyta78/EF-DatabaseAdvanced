namespace Organazer.ScreenElements
{
    using System;

    public class MenuItem : ScreenElement
    {
        private string content;

        public MenuItem(int x, int y, int w, string content)
            : base(x, y)
        {
            if (content.Length > w)
            {
                this.content = content.Substring(0, w - 2) + "..";
            }
            else if (content.Length < w)
            {
                int leadingSpaces = (w - content.Length) / 2;
                int trailingSpaces = w - content.Length - leadingSpaces;
                this.content = new string(' ', leadingSpaces) + content + new string(' ', trailingSpaces);
            }
            else
            {
                this.content = content;
            }
        }

        protected override void Render()
        {
            Console.Write(this.content);
        }
    }
}
