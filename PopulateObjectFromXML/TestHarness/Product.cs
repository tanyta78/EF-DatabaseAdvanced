using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Xml.Serialization;

namespace TestHarness
{

    [System.Xml.Serialization.XmlType("Product", IncludeInSchema = true)]
    public partial class Product
    {
        public int ProductID { get; set; }
      
        public string ProductName { get; set; }

        public int SupplierID { get; set; }

        public int CategoryID { get; set; }

        public string QuantityPerUnit { get; set; }
       
        public decimal UnitPrice { get; set; }

        public short UnitsInStock { get; set; }

        public short UnitsOnOrder { get; set; }

        public short ReorderLevel { get; set; }

        public bool Discontinued { get; set; }

        //[XmlArray("Order_Details")]
        //public  List<Product_Order_Detail> Order_Details { get; set; }
    }
}
