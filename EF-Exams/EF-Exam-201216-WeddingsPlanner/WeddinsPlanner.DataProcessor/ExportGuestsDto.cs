namespace WeddinsPlanner.DataProcessor
{
    using System.Xml.Serialization;
    
    [XmlType("guest")]
    public class ExportGuestsDto
    {
        [XmlAttribute("family")]
        public string Family { get; set; }

        [XmlText]
        public string Name { get; set; }
    }
}