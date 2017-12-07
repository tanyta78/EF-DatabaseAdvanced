namespace Photography.DataProcessor.ExportDtos
{
    using System.Xml.Serialization;
   
    [XmlType(TypeName = "workshop")]
    public class WorkshopExportDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("total-profit")]
        public decimal TotalProfit { get; set; }

        [XmlElement("participants")]
        public ParticipantsDto ParticipantDto { get; set; }
    }
}