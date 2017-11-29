namespace Employees.ModelsDTO
{
    using System;
    using System.Collections.Generic;

    public class ManagerDto
    {
        public ManagerDto()
        {
            this.Subordinates = new List<EmployeeDto>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int SubordinatesCount { get; set; }

        public ICollection<EmployeeDto> Subordinates { get; set; }

     
    }
}
