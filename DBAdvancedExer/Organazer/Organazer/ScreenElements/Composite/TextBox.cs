namespace Organazer.ScreenElements.Composite
{
    using System;
    using Utility;

    public class TextBox : ScreenElement
    {
        private Layout layout;
        private Label label;


        public TextBox(int x, int y, int width, int height, string content) : base(x, y)
        {
            this.Init(width, height, content);
        }

        private void Init(int w, int h, string text)
        {
            this.layout = new Layout(this.X, this.Y);
            var matrix = Composer.MakeBoxLayout(w, h);
            this.layout.SetLayout(Composer.Compose(matrix));

            int textX = this.X + ((w - text.Length) / 2);
            int textY = this.Y + ((h - 1) / 2);
            this.label = new Label(textX, textY, content: text);
        }

        public override void SetBackgroundColor(ConsoleColor color)
        {
            base.SetBackgroundColor(color);
            this.layout.SetBackgroundColor(color);
            this.label.SetBackgroundColor(color);
        }

        public override void SetForegroundColor(ConsoleColor color)
        {
            base.SetForegroundColor(color);
            this.layout.SetForegroundColor(color);
            this.label.SetForegroundColor(color);
        }

        protected override void Render()
        {
            this.layout.Print();
            this.label.Print();
        }

    }
}
