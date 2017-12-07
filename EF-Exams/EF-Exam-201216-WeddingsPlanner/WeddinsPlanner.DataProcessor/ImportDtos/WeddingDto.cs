namespace WeddinsPlanner.DataProcessor.ImportDtos
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Models;

    public class WeddingDto
    {
        [Required]
        public string Bride { get; set; }

        [Required]
        public string Bridegroom { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string Agency { get; set; }

        public List<GuestDto> Guests { get; set; } = new List<GuestDto>();
    }
}