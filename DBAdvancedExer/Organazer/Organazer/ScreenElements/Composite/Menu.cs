namespace Organazer.ScreenElements.Composite
{
    using System.Collections.Generic;

    public class Menu : ScreenElement
    {
        private List<Label> items;
        private int selected;

        public Menu(int x, int y) : base(x, y)
        {
            this.items = new List<Label>();
            this.selected = 0;
        }

        public void AddItem(int x, int y, int width, string content)
        {
            Label item = new Label(x, y, width, content);
            this.items.Add(item);
        }

        public void MoveUp()
        {
            if (this.selected > 0)
            {
                this.selected--;
            }
        }

        public void MoveDown()
        {
            if (this.selected < this.items.Count - 1)
            {
                this.selected++;
            }
        }

        public Label GetSelected()
        {
            return this.items[this.selected];
        }

        protected override void Render()
        {
            for (int i = 0; i < this.items.Count; i++)
            {
                if (i == this.selected) this.items[i].Print(true);
                else this.items[i].Print(false);
            }
        }
    }
}
