namespace FastFood.DataProcessor
{
    using System.Collections.Generic;
    using System.Xml.Serialization;
    using Dto.Export;
    using Models;

    [XmlType("Category")]
    public class CategoryDto
    {
        [XmlElement("Name")]
        public string Name { get; set; }
        
        [XmlElement("MostPopularItem")]
        public ExportItemDto MostPopulatarItem { get; set; }
    }
}