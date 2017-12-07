namespace WeddinsPlanner.DataProcessor.ImportDtos
{
    using Models.Enums;

    public class GuestDto
    {
        public string Name { get; set; }

        public bool RSVP { get; set; }

        public Family Family { get; set; }
    }
}