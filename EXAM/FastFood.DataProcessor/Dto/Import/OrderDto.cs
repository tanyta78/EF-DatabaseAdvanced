namespace FastFood.DataProcessor.Dto.Import
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Xml.Serialization;


    [XmlType("Order")]
    public class OrderDto
    {
        [Required]
        public string Customer { get; set; }

        [Required]
        public string Employee { get; set; }

        [Required]
        public string DateTime { get; set; }

        [Required]
        [DefaultValue("ForHere")]
        public string Type { get; set; }

        [XmlArray("Items")]
        public OrderItemDto[] Items { get; set; }
    }
}
