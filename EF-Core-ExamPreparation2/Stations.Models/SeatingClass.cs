namespace Stations.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


    public class SeatingClass
    {
        public SeatingClass()
        {
            this.TrainSeats = new List<TrainSeat>();
        }
        public int Id { get; set; }

        [Required,MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(2)]
        public string Abbreviation { get; set; }

        public ICollection<TrainSeat> TrainSeats { get; set; }
    }
}

