namespace Migrations.Models
{
    using System.Collections.Generic;

    public class StoreLocation
    {
        public int Id { get; set; }

        public string LocationName { get; set; }

        public List<Sale> SalesInStore { get; set; }

    }
}
