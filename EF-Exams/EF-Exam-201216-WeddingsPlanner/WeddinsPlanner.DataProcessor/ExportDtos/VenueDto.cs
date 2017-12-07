namespace WeddinsPlanner.DataProcessor.ExportDtos
{
    using System.Xml.Serialization;

    [XmlType("venue")]
    public class VenueDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlElement("capacity")]
        public int Capacity { get; set; }
        [XmlElement("town")]
        public string Town { get; set; }

    }
}