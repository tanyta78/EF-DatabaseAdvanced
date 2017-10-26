namespace CompanyRoster
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
       public static void Main(string[] args)
       {
           var numberOfEmployees = int.Parse(Console.ReadLine());
           var employees = new List<Employee>();

           for (int i = 0; i < numberOfEmployees; i++)
           {
               var arrg = Console.ReadLine().Split(' ').ToArray();
               ReadInput(arrg,employees);
           }

           var DeptWithHighestSalary = employees
               .GroupBy(e => e.Department)
               .Select(e => new
               {
                   Department = e.Key,
                   AverageSalary = e.Average(emp => emp.Salary),
                   Employees = e.OrderByDescending(emp => emp.Salary)
               })
               .OrderByDescending(dep => dep.AverageSalary)
               .FirstOrDefault();

           Console.WriteLine($"Highest Average Salary: {DeptWithHighestSalary.Department}");

           foreach (var employee in DeptWithHighestSalary.Employees)
           {
               Console.WriteLine($"{employee.Name} {employee.Salary:F2} {employee.Email} {employee.Age}");
           }
        }

        private static void ReadInput(string[] arrg, List<Employee> employees)
        {
            var name = arrg[0];
            var salary = double.Parse(arrg[1]);
            var position = arrg[2];
            var department = arrg[3];
            var employee = new Employee(name, salary, position, department);

            if (arrg.Length == 5)
            {
                int age;
                string email=String.Empty;
                if (!int.TryParse(arrg[4], out age)) email = arrg[4];
                if (age != 0)
                {
                    employee.Age = age;
                }
                else
                {
                    employee.Email = email;
                }
               
            }else if (arrg.Length == 6)
            {
                employee.Age = int.Parse(arrg[5]);
                employee.Email = arrg[4];
            }

            employees.Add(employee);
        }
    }
}
