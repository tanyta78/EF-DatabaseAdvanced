namespace WeddinsPlanner.Models
{
    using System.Collections.Generic;

    public class Venue
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public string Town { get; set; }

        public List<WeddingsVenue> Weddings { get; set; }=new List<WeddingsVenue>();
    }
}
