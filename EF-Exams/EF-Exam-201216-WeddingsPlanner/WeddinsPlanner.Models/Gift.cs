namespace WeddinsPlanner.Models
{
    using Enums;

    public class Gift:Present
    {
        public string Name { get; set; }

        public Size Size { get; set; }
    }
}
