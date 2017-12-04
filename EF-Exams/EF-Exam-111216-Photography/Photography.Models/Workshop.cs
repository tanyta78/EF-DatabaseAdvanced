namespace Photography.Models
{
    using System;
    using System.Collections.Generic;

    public class Workshop
    {
        public Workshop()
        {
            this.Participants=new List<PhotographersWorkshop>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Location { get; set; }

        public decimal PricePerParticipant { get; set; }

        public int TrainerId { get; set; }

        public Photographer Trainer { get; set; }

        public ICollection<PhotographersWorkshop> Participants { get; set; }
    }
}
