namespace Photography.DataProcessor.ExportDtos
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlType(TypeName = "photographer")]
   public class SameCameraMakePhotographerDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("primary-camera")]
        public string PrimaryCamera { get; set; }

        [XmlArray(ElementName = "lenses")]
        [XmlArrayItem(ElementName = "lens")]
        public List<string> Lenses { get; set; }

        public bool ShouldSerializeLenses()
        {
            return Lenses.Count != 0;
        }
    }
}
