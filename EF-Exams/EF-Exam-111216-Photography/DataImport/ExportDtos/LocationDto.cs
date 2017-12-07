namespace Photography.DataProcessor.ExportDtos
{
    using System.Collections.Generic;
    using System.Xml.Serialization;
    using ImportDtos;

    [XmlType("location")]
    public class LocationDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("workshop")]
        public List<WorkshopExportDto> WorkshopsDtos { get; set; }
    }
}
