namespace WeddinsPlanner.DataProcessor.ExportDtos
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlType("agency")]
    public class AgencyInTownDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("profit")]
        public decimal Profit { get; set; }
        
        [XmlElement("wedding")]
        public List<ExportWeddingInTownDto> Weddings { get; set; }
    }
}