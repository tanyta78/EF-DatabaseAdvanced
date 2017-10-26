namespace Organazer.ScreenElements.Composite
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ScrollList : ScreenElement
    {
        private int width;
        private int height;
        private Dictionary<int, ListItem> content;
        private int selected;
        private int offset;

        // Scrollbar

        public ScrollList(int x, int y, int w, int h)
            : base(x, y)
        {
            this.width = w;
            this.height = h;
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
            if (this.selected < this.content.Count - 1 && this.selected < this.height - 1)
            {
                this.selected++;
            }
            else if (this.offset + this.height < this.content.Count)
            {
                this.offset++;
            }
        }

        public void AddItem(string item, int refId)
        {
            var listItem = new ListItem(0, 0, this.width - 1, item, refId);
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
            foreach (var item in this.content.Values)
            {
                item.SetBackgroundColor(color);
            }
        }

        public override void SetForegroundColor(ConsoleColor color)
        {
            base.SetForegroundColor(color);
            foreach (var item in this.content.Values)
            {
                item.SetForegroundColor(color);
            }
        }

        protected override void Render()
        {
            int current = this.offset + this.selected;

            if (this.content.Count > this.height)
            {
                foreach (var item in this.content.Skip(this.offset).Take(this.height))
                {
                    item.Value.Print(item.Key == current);
                    Console.CursorTop++;
                }

                // Scrollbar
                int sbHeight = this.height;
                int sbVisible = (int)Math.Round((double)(sbHeight * sbHeight) / this.content.Count);
                int offsetHeight = (int)Math.Round(((double)this.offset * sbHeight) / this.content.Count);
                Console.CursorTop -= this.height;
                Console.CursorLeft += this.width - 2;
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
