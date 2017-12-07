namespace WeddinsPlanner.Models
{
    public class WeddingsVenue
    {
        public int WeddingId { get; set; }

        public Wedding Wedding { get; set; }

        public int VenueId { get; set; }

        public Venue Venue { get; set; }
    }
}