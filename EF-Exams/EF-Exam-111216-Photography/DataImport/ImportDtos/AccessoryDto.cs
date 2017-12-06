namespace Photography.DataProcessor.ImportDtos
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("accessory")]
    public class AccessoryDto
    {
        [Required]
        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}