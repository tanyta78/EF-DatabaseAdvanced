namespace Organazer.ScreenElements
{
    public class ListItem : Label
    {
        private int refId;
        public ListItem(int x, int y, int w, string content, int refId)
            : base(x, y, w, content)
        {
            this.refId = refId;
        }

        public int RefId { get { return this.refId; } }
    }
}
