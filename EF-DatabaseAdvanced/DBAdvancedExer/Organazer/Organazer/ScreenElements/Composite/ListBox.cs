namespace Organazer.ScreenElements.Composite
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utility;

    public class ListBox : ScreenElement
    {
        private int width;
        private int height;
        private Layout layout;
        private Dictionary<int, ListItem> content;
        private int selected;
        private int offset;

        // Scrollbar

        public ListBox(int x, int y, int w, int h)
            : base(x, y)
        {
            this.width = w;
            this.height = h;
            this.Init();
            this.content = new Dictionary<int, ListItem>();
            this.selected = 0;
            this.offset = 0;
        }

        public void MoveUp()
        {
            if (this.selected > 0)
            {
                this.selected--;
            }
            else if (this.offset > 0)
            {
                this.offset--;
            }
        }

        public void MoveDown()
        {
            if (this.selected < this.content.Count - 1 && this.selected < this.height - 3)
            {
                this.selected++;
            }
            else if (this.offset + this.height < this.content.Count)
            {
                this.offset++;
            }
        }

        private void Init()
        {
            this.layout = new Layout(0, 0);
            var matrix = Composer.MakeBoxLayout(this.width, this.height);
            this.layout.SetLayout(Composer.Compose(matrix));
        }

        public void AddItem(string item, int refId)
        {
            var listItem = new ListItem(1, 1, this.width - 2, item, refId);
            listItem.SetBackgroundColor(this.backgroundColor);
            listItem.SetForegroundColor(this.foregroundColor);
            this.content.Add(this.content.Count, listItem);
        }

        public int GetSelected()
        {
            return this.content[this.offset + this.selected].RefId;
        }

        public override void SetBackgroundColor(ConsoleColor color)
        {
            base.SetBackgroundColor(color);
            this.layout.SetBackgroundColor(color);
            foreach (var item in this.content.Values)
            {
                item.SetBackgroundColor(color);
            }
        }

        public override void SetForegroundColor(ConsoleColor color)
        {
            base.SetForegroundColor(color);
            this.layout.SetForegroundColor(color);
            foreach (var item in this.content.Values)
            {
                item.SetForegroundColor(color);
            }
        }

        protected override void Render()
        {
            this.layout.Print();
            int current = this.offset + this.selected;

            if (this.content.Count > this.height - 2)
            {
                foreach (var item in this.content.Skip(this.offset).Take(this.height - 2))
                {
                    item.Value.Print(item.Key == current);
                    Console.CursorTop++;
                }

                // Scrollbar
                int sbHeight = this.height - 2;
                int sbVisible = (int)Math.Round((double)(sbHeight * sbHeight) / this.content.Count);
                int offsetHeight = (int)Math.Round(((double)this.offset * sbHeight) / this.content.Count);
                Console.CursorTop -= this.height - 3;
                Console.CursorLeft += this.width - 1;
                for (int i = 0; i < sbHeight; i++)
                {
                    if (i >= offsetHeight && i < offsetHeight + sbVisible)
                    {
                        Console.Write((char)9608);
                    }
                    else
                    {
                        Console.Write((char)9618);
                    }
                    Console.CursorTop++;
                    Console.CursorLeft--;
                }
            }
            else
            {
                foreach (var item in this.content)
                {
                    item.Value.Print(item.Key == current);
                    Console.CursorTop++;
                }
            }
        }
    }
}
