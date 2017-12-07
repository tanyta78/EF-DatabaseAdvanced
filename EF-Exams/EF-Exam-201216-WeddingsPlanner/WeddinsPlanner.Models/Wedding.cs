namespace WeddinsPlanner.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Wedding
    {
        public int Id { get; set; }

        public int BrideId { get; set; }
        [Required]
        public Person Bride { get; set; }

        public int BridegroomId { get; set; }
        [Required]
        public Person Bridegroom { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int AgencyId { get; set; }
        
        public Agency Agency { get; set; }

        public List<Invitation> Invitations { get; set; } = new List<Invitation>();

        public List<WeddingsVenue> Venues { get; set; }=new List<WeddingsVenue>();

    }
}
