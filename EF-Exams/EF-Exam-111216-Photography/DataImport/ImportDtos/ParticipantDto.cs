namespace Photography.DataProcessor.ImportDtos
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

   [XmlType("participant")]
    public class ParticipantDto
    {
        [XmlAttribute("first-name")]
        [Required]
        public string FirstName { get; set; }

        [XmlAttribute("last-name")]
        [Required]
        public string LastName { get; set; }
    }
}