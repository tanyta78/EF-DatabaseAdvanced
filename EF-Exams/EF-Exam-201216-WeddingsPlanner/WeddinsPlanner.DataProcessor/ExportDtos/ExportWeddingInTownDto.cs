namespace WeddinsPlanner.DataProcessor.ExportDtos
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlType("wedding")]
    public class ExportWeddingInTownDto
    {
        [XmlAttribute("cash")]
        public decimal Cash { get; set; }
        
        [XmlAttribute("presents")]
        public decimal Present { get; set; }
        
        [XmlElement("bride")]
        public string Bride { get; set; }
        
        [XmlElement("bridegroom")]
        public string Bridegroom { get; set; }
        
        [XmlArray("guests")]
        [XmlArrayItem("guest")]
        public List<ExportGuestsDto> Guests { get; set; }
    }
}