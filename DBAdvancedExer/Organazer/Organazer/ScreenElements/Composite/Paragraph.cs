namespace Organazer.ScreenElements.Composite
{

    using System.Collections.Generic;

    public class Paragraph : ScreenElement
    {
        private List<string> content;

        public Paragraph(int x, int y)
            : base(x, y)
        {
            this.content = new List<string>();
        }

        public void AddLine(string line)
        {
            this.content.Add(line);
        }

        public void Clear()
        {
            this.content.Clear();
        }

        protected override void Render()
        {
            foreach (var line in content)
            {
                System.Console.Write(line);
                System.Console.CursorLeft -= line.Length;
                System.Console.CursorTop++;
            }
        }

    }
}
