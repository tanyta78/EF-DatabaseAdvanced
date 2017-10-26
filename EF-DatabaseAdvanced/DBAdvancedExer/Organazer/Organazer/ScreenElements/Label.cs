namespace Organazer.ScreenElements
{
    using System;

    public class Label:ScreenElement
    {
        private string content;
        private int width;

        public Label(int x, int y, int w = -1, string content = "") : base(x, y)
        {
            this.content = content;
            if (w == -1)
            {
                this.width = this.content.Length;
            }
            else
            {
                this.width = w;
            }
        }

        protected override void Render()
        {
            if (this.content.Length <= this.width)
            {
                Console.Write(this.content);
            }
            else
            {
                Console.Write(this.content.Substring(0, this.width - 2) + "..");
            }
        }
    }
}
