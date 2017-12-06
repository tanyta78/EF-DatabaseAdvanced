namespace Photography.DataProcessor.ImportDtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    using Models;

    [XmlType("workshop")]
    public class WorkshopDto
    {
        [XmlAttribute("name")]
        [Required]
        public string Name { get; set; }

        [XmlAttribute("start-date")]
        public string StartDate { get; set; }

        [XmlAttribute("end-date")]
        public string EndDate { get; set; }

        [XmlAttribute("location")]
        [Required]
        public string Location { get; set; }

        [XmlAttribute("price")]
        [Required]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal PricePerParticipant { get; set; }

        [XmlElement("trainer")]
        [Required]
        public string Trainer { get; set; }

        [XmlArray("participants")]
        public ParticipantDto[] Participants { get; set; }=new ParticipantDto[0];

    }
}
