namespace Employees.ModelsDTO
{
    using System;

    public class EmployeeListDto
    {
       public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

       public string ManagerFirstName { get; set; }
    }
}
