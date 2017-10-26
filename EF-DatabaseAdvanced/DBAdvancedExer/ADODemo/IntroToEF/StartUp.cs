namespace IntroToEF
{
    using System;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;

    public class StartUp
    {
      public static void Main()
        {

            SoftuniContext context = new SoftuniContext();


            //select with ef
            var data = context.Employees.Select(e => new
            {
                Name = e.FirstName + " " + e.LastName,
                TownName = e.Addresses.Town.Name
            }).ToList();

            //join tables in ef
            var employees = context.Employees.Join(
                context.Departments,
                (e => e.DepartmentID),
                (d => d.DepartmentID),
                (e,d)=> new
            {
                EmployeeName= e.FirstName + " " + e.LastName,
                JobTitle = e.JobTitle,
                Department = d.Name
            }).ToList();

            //group by table

            var group = context.Employees.GroupBy(e => e.DepartmentID).Select(e => new AverigeSalariesByDepartment()
            {
                 DepartmentId = e.Key,
                AverageSalary = e.Average(d => d.Salary)
            }).ToList();

            foreach (var item in group)
            {
                PrintSalary(item);
            }

            

            /*  03.EmployeesFullInformation(context);

              04.EmplFNAmeSalaryOver50000(context);

              05.EmplFromResearchAndDev(context);

              06.AddAddressToEmployee(context);

               07.EmplProjFrom20012003(context);

              08.AddressesByTownName(context);

              09.Employee147(context);

              10.DepWithMoreEmpl(context);

              11. FindLatest10Project(context);

              12. IncreaseSalaries(context);

              13. FindEmplStartWithSA(context);

              15.DeleteProjectById(context);

              16. RemoveTowns(context);*/

        }

        public static void PrintSalary(AverigeSalariesByDepartment department)
        {
            Console.WriteLine($"{department.DepartmentId,4}|{department.AverageSalary}");
        }

        //view model
        public class AverigeSalariesByDepartment
        {
            public int DepartmentId { get; set; }
            public decimal AverageSalary { get; set; }
        }

        private static void RemoveTowns(SoftuniContext context)
        {
            var townName = Console.ReadLine();
            var town = context.Towns.FirstOrDefault(t => t.Name == townName);

            if (town == null)
            {
                Console.WriteLine("There isn't a town with that name in database");
            }
            else
            {
                var addressIds = context.Addresses
                    .Where(a => a.TownID == town.TownID).ToList().Select(a => a.AddressID).ToList();

                var employeeWithAdress = context.Employees
                    .Where(e => addressIds.Contains((int)e.AddressID)).ToList();

                foreach (var emp in employeeWithAdress)
                {
                    emp.AddressID = null;
                }

                foreach (var addressId in addressIds)
                {
                    context.Addresses.Remove(context.Addresses.FirstOrDefault(a => a.AddressID == addressId));
                }

                Console.WriteLine($"{addressIds.Count} addresses in {townName} were deleted");

                context.Towns.Remove(town);

                context.SaveChanges();
            }
        }

        private static void DeleteProjectById(SoftuniContext context)
        {
            var project = context.Projects.FirstOrDefault(p => p.ProjectID == 2);


            if (project != null)
            {
                var projectEmployees = project.Employees;

                foreach (var emp in projectEmployees)
                {
                    emp.Projects.Remove(project);
                }

                context.Projects.Remove(project);
                context.SaveChanges();
            }

            var projectsName = context.Projects
                .Select(p => p.Name).Take(10).ToList();

            foreach (var name in projectsName)
            {
                Console.WriteLine(name);
            }
        }

        private static void FindEmplStartWithSA(SoftuniContext context)
        {
            var employees = context.Employees
                .Where(e => e.FirstName.StartsWith("sa"));

            foreach (var emp in employees)
            {
                Console.WriteLine($"{emp.FirstName} {emp.LastName} - {emp.JobTitle} - (${emp.Salary:f4})");
            }
        }

        private static void IncreaseSalaries(SoftuniContext context)
        {
            var employees = context.Employees
                            .Where(e => e.Department.Name == "Engineering" || e.Department.Name == "Tool Design" ||
                                        e.Department.Name == "Marketing" || e.Department.Name == "Information Services");

            foreach (var emp in employees)
            {
                emp.Salary *= 1.12m;
            }

            context.SaveChanges();

            foreach (var e in employees)
            {
                Console.WriteLine($"{e.FirstName} {e.LastName} (${e.Salary:f6})");
            }
        }

        private static void FindLatest10Project(SoftuniContext context)
        {
            var projects = context.Projects
                            .OrderByDescending(p => p.StartDate)
                            .Take(10)
                            .OrderBy(p => p.Name);

            foreach (var pr in projects)
            {
                Console.WriteLine($"{pr.Name} {pr.Description} {pr.StartDate:M/d/yyyy h:mm:ss tt} {pr.EndDate:M/d/yyyy h:mm:ss tt}");
            }
        }

        private static void DepWithMoreEmpl(SoftuniContext context)
        {
            var departments = context.Departments
                .Where(d => d.DepartmentEmployees.Count > 5)
                .OrderBy(d => d.DepartmentEmployees.Count)
                .ToList();
            foreach (var dep in departments)
            {
                Console.WriteLine($"{dep.Name} {dep.Manager.FirstName}");

                foreach (var emp in dep.DepartmentEmployees)
                {
                    Console.WriteLine($"{emp.FirstName} {emp.LastName} {emp.JobTitle}");
                }
            }
        }

        private static void Employee147(SoftuniContext context)
        {
            var employee = context.Employees.FirstOrDefault(e => e.EmployeeID == 147);
            if (employee == null)
            {
                throw new ArgumentNullException("There is no employee with ID 147");
            }
            Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.JobTitle}");
            foreach (var name in employee.Projects.OrderBy(p => p.Name).Select(p => p.Name).ToList())
            {

                Console.WriteLine(name);
            }
        }

        private static void AddressesByTownName(SoftuniContext context)
        {
            var adresses = context.Addresses
                            .OrderByDescending(a => a.Employees.Count)
                            .ThenBy(a => a.Town.Name)
                            .Take(10);

            foreach (var a in adresses)
            {
                Console.WriteLine($"{a.AddressText}, {a.Town.Name} - {a.Employees.Count} employees");
            }
        }

        private static void EmplProjFrom20012003(SoftuniContext context)
        {
            var employees = context.Employees.ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var employee in employees)
            {
                var emplProjects = employee.Projects
                    .Where(p => p.StartDate.Year >= 2001 && p.StartDate.Year <= 2003).ToList();

                if (emplProjects.Count > 0)
                {
                    if (employee.Manager == null)
                    {
                        sb.AppendLine($"{employee.FirstName} {employee.LastName} ");
                    }
                    else
                    {
                        sb.AppendLine($"{employee.FirstName} {employee.LastName} {employee.Manager.FirstName}");
                    }

                    foreach (var project in emplProjects)
                    {
                        sb.AppendLine($"--{project.Name} {project.StartDate:M/d/yyyy h:mm:ss tt} {project.EndDate:M/d/yyyy h:mm:ss tt}");
                    }
                }
            }

            Console.Write(sb.ToString());
        }


        private static void AddAddressToEmployee(SoftuniContext context)
        {
            Address address = new Address();
            address.AddressText = "Vitoshka 15";
            address.TownID = 4;

            context.Addresses.Add(address);

            var employees = context.Employees
                .Where(e => e.LastName == "Nakov");

            foreach (var employee in employees)
            {
                employee.Addresses = address;
            }
            context.SaveChanges();

            var emplAdresses = context.Employees
                 .OrderByDescending(e => e.AddressID)
                 .Take(10)
                 .Select(e => e.Addresses.AddressText)
                 .ToList();

            foreach (var adress in emplAdresses)
            {
                Console.WriteLine(adress);
            }
        }

        private static void EmplFromResearchAndDev(SoftuniContext context)
        {
            var employeesReserchDevelop = context.Employees
                            .Where(e => e.Department.Name == "Research and Development")
                            .OrderBy(e => e.Salary)
                            .ThenByDescending(e => e.FirstName);

            foreach (var emp in employeesReserchDevelop)
            {
                Console.WriteLine($"{emp.FirstName} {emp.LastName} from Research and Development - ${emp.Salary:f2}");
            }
        }

        private static void EmplFNAmeSalaryOver50000(SoftuniContext context)
        {
            var employeesName = context.Employees
                .Where(e => e.Salary > 50000)
                .Select(e => e.FirstName);

            foreach (var empName in employeesName)
            {
                Console.WriteLine(empName);
            }
        }

        private static void EmployeesFullInformation(SoftuniContext context)
        {
            var employees = context.Employees.ToList().OrderBy(e => e.EmployeeID);


            foreach (var e in employees)
            {
                Console.WriteLine($"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:f4}");
            }
        }
    }
}
