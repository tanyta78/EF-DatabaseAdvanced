
namespace Organazer.ScreenElements
{
    using System;

    public class EditBox : ScreenElement
    {
        private int width;
        private string content;

        public EditBox(int x, int y, int width, string content)
            : base(x, y)
        {
            this.width = width;
            this.content = content;
        }

        public string Content { get { return this.content; } }

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
