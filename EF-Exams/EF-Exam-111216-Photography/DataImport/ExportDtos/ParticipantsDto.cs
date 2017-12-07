namespace Photography.DataProcessor.ExportDtos
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlType(TypeName = "participants")]
    public class ParticipantsDto
    {
        [XmlAttribute("count")]
        public int ParticipantCount { get; set; }

        [XmlElement("participant")]
        public List<string> Names { get; set; }
    }
}