namespace Organazer.ScreenElements.Composite
{
    using System.Collections.Generic;

    public class Grid : ScreenElement
    {
        private List<Label> items;

        public Grid(int x, int y) : base(x, y)
        {
            this.items = new List<Label>();
        }

        public void Clear()
        {
            this.items.Clear();
        }

        public void AddItem(int x, int y, int width = -1, string content = "")
        {
            Label item = new Label(x, y, width, content);
            this.items.Add(item);
        }

        protected override void Render()
        {
            foreach (var item in this.items)
            {
                item.Print();
            }
        }
    }
}
