using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Xml.Serialization;

namespace TestHarness
{
  
    public  class Customer
    {
        
        public string CustomerID { get; set; }
        
        public string CompanyName { get; set; }
        
        public string ContactName { get; set; }
        
        public string ContactTitle { get; set; }

        public string Address { get; set; }
        
        public string City { get; set; }

        public string Region { get; set; }
        
        public string PostalCode { get; set; }

        public string Country { get; set; }
        
        public string Phone { get; set; }
        
        public string Fax { get; set; }
        
        public List<Order> Orders { get; set; }

        // arrays can also be used
       // public Order[] Orders { get; set; }
    }
}
