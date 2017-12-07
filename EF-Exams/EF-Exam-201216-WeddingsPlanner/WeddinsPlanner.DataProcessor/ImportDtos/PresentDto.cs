namespace WeddinsPlanner.DataProcessor.ImportDtos
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("present")]
    public class PresentDto
    {
        [XmlAttribute("type")]
        [Required]
        public string Type { get; set; }

        [XmlAttribute("invitation-id")]
        [Required]
        public int InvitationId { get; set; }

        [XmlAttribute("present-name")]
        public string Name { get; set; }

        [XmlAttribute("size")]
        public string Size { get; set; }

        [XmlAttribute("amount")]
        public decimal Amount { get; set; }
    }
}