namespace WeddinsPlanner.Models
{
    using System.Collections.Generic;

    public class Agency
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? EmployeesCount { get; set; }

        public string Town { get; set; }

        public ICollection<Wedding> Weddings { get; set; }=new List<Wedding>();
    }
}
