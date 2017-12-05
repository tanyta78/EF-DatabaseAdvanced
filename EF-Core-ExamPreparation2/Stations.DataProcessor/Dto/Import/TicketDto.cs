namespace Stations.DataProcessor.Dto.Import
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    using Models;

    [XmlType("Ticket")]
    public class TicketDto
    {
        [XmlAttribute("price")]
        [Required]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        [XmlAttribute("seat")]
        [Required]
       [RegularExpression("^[A-Z]{2}[0-9]{1,6}?")]
        public string Seat { get; set; }
        
        [Required]
        public TicketTripDto Trip { get; set; }
        
        public TicketCardDto Card { get; set; }

    }
}
