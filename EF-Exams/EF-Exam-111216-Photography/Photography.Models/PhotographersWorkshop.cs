namespace Photography.Models
{
    public class PhotographersWorkshop
    {
        public int PhotographerId { get; set; }

        public Photographer Photographer { get; set; }

        public int WorkshopId { get; set; }

        public Workshop Workshop { get; set; }
    }
}