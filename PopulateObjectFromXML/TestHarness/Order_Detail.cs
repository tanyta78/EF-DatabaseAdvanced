using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Xml.Serialization;

namespace TestHarness
{
    
    [System.Xml.Serialization.XmlType("Order_Detail", IncludeInSchema = true)]    
    public class Product_Order_Detail
    {
        
        public int OrderID { get; set; }
        
        public int ProductID { get; set; }

        public decimal UnitPrice { get; set; }

        public short Quantity { get; set; }

        public float Discount { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }

    }
}
