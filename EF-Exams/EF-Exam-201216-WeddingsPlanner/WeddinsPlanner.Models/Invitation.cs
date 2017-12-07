namespace WeddinsPlanner.Models
{
    using System.ComponentModel.DataAnnotations;
    using Enums;

    public class Invitation
    {
        public int Id { get; set; }

        public int WeddingId { get; set; }

        [Required]
        public Wedding Wedding { get; set; }

        public int GuestId { get; set; }
        [Required]
        public Person Guest { get; set; }

        public int? PresentId { get; set; }
        public Present Present { get; set; }

        public bool IsAttending { get; set; }

        [Required]
        public Family Family { get; set; }
    }
}
