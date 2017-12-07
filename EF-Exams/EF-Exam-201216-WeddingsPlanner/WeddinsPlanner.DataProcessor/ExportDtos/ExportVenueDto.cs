namespace WeddinsPlanner.DataProcessor.ExportDtos
{
    using System.Xml.Serialization;

    [XmlType("venue")]
    public class ExportVenueDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("capacity")]
        public int Capacity { get; set; }
        [XmlElement("weddings-count")]
        public int WeddingsCount { get; set; }

    }
}