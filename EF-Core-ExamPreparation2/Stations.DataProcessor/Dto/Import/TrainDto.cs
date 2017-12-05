namespace Stations.DataProcessor.Dto.Import
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Models;

    public class TrainDto
    {
        [Required]
        [MaxLength(10)]
        public string TrainNumber { get; set; }

        public string Type { get; set; } = "HighSpeed";

        public TrainSeatDto[] Seats { get; set; }=new TrainSeatDto[0];
    }
}