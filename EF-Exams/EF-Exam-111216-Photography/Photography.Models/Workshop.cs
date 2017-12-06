namespace Photography.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Workshop
    {
        public Workshop()
        {
            this.Participants=new List<PhotographersWorkshop>();
        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal PricePerParticipant { get; set; }

        public int TrainerId { get; set; }

        [Required]
        public Photographer Trainer { get; set; }

        public ICollection<PhotographersWorkshop> Participants { get; set; }
    }
}
