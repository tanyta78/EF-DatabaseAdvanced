namespace Organazer.ScreenElements
{
    using System;

    public class Layout : ScreenElement
   {
       private string[] layout;

        public Layout(int x, int y) : base(x, y)
        {
            this.layout = new[] { "" };
        }

       public void SetLayout(string[] layout)
       {
           this.layout = layout;
       }

        protected override void Render()
        {
            for (int r = 0; r < this.layout.Length; r++)
            {
                Console.Write(this.layout[r]);
                Console.CursorLeft -= this.layout[r].Length;
                Console.CursorTop++;
            }
        }
    }
}
