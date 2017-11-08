namespace P02_DatabaseFirst
{
    using System;
    using System.Linq;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Remotion.Linq.Clauses;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new SoftUniContext();
            using (context)
            {
                //EmployeeFullInfo(context);
                //EmplNameWithSalary(context);
                //EmplFromDepartment(context);
                // AddNewAddressUpdatecontext(context);
                // EmployeesAndProjects(context);
                // AddressesByTown(context);
                //Employee147Info(context);
                DepWithMore5Empl(context);
                //Find10latestProjects(context);
                //IncreaseSalary(context);
                //EmployeesFirstNameWithSA(context);
                //DeleteProjectById(context);
                //DeleteTownWithId(context);

            }
        }

        private static void DeleteTownWithId(SoftUniContext context)
        {
            var townName = Console.ReadLine();
            var town = context.Towns.FirstOrDefault(t => t.Name == townName);

            if (town == null)
            {
                Console.WriteLine("There isn't a town with that name in database");
            }
            else
            {
                var AddressIds = context.Addresses
                    .Where(a => a.TownId == town.TownId).ToList().Select(a => a.AddressId).ToList();

                var employeeWithAdress = context.Employees
                    .Where(e => AddressIds.Contains((int)e.AddressId)).ToList();

                foreach (var emp in employeeWithAdress)
                {
                    emp.AddressId = null;
                }

                foreach (var AddressId in AddressIds)
                {
                    context.Addresses.Remove(context.Addresses.FirstOrDefault(a => a.AddressId == AddressId));
                }

                Console.WriteLine($"{AddressIds.Count} addresses in {townName} were deleted");

                context.Towns.Remove(town);

                context.SaveChanges();

            }
        }

        private static void DeleteProjectById(SoftUniContext context)
        {
            var project = context.Projects.Find(2);
            var removed = context.EmployeesProjects.Where(e => e.ProjectId == 2).ToList();

            context.EmployeesProjects.RemoveRange(removed);
            context.SaveChanges();

            context.Projects.Remove(project);
            context.SaveChanges();

            context.Projects.Take(10).ToList().ForEach(p => Console.WriteLine(p.Name));
        }

        private static void EmployeesFirstNameWithSA(SoftUniContext context)
        {
            var employees = context.Employees
                .Where(e => e.FirstName.StartsWith("sa"))
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName);

            foreach (var emp in employees)
            {
                Console.WriteLine($"{emp.FirstName} {emp.LastName} - {emp.JobTitle} - (${emp.Salary:f2})");
            }
        }

        private static void IncreaseSalary(SoftUniContext context)
        {
            var emplFromDep = context.Employees
                .Where(e => e.Department.Name == "Engineering" || e.Department.Name == "Tool Design" ||
                            e.Department.Name == "Marketing" || e.Department.Name == "Information Services")
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName);

            foreach (var emp in emplFromDep)
            {
                emp.Salary *= 1.12m;
            }

            context.SaveChanges();


            foreach (var e in emplFromDep)
            {
                Console.WriteLine($"{e.FirstName} {e.LastName} (${e.Salary:f2})");
            }
        }

        private static void Find10latestProjects(SoftUniContext context)
        {
            var latestProject = context.Projects
                .OrderByDescending(p => p.StartDate)
                .Take(10).OrderBy(p => p.Name);

            foreach (var project in latestProject)
            {
                Console.WriteLine(project.Name);
                Console.WriteLine(project.Description);
                Console.WriteLine($"{project.StartDate:M/d/yyyy h:mm:ss tt}");
            }
        }

        private static void DepWithMore5Empl(SoftUniContext context)
        {
            var departmentsSelected = context.Departments
                .Where(d => d.Employees.Count > 5)
                .OrderBy(d => d.Employees.Count)
                .ThenBy(d=>d.Name)
                .Include(d => d.Manager)
                .Include(d => d.Employees);

            foreach (var d in departmentsSelected)
            {
                Console.WriteLine($"{d.Name} - {d.Manager.FirstName} {d.Manager.LastName}");

                var employees = d.Employees.ToList();

                foreach (var e in employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName))
                {
                    Console.WriteLine($"{e.FirstName} {e.LastName} - {e.JobTitle}");
                }

                Console.WriteLine(new string('-', 10));
            }
        }

        private static void Employee147Info(SoftUniContext context)
        {
            var emp147 = context.Employees
                .FirstOrDefault(e => e.EmployeeId == 147);

            Console.WriteLine($"{emp147.FirstName} {emp147.LastName} - {emp147.JobTitle}");

            var projects = context.EmployeesProjects
                .Where(e => e.EmployeeId == 147)
                .Include(e => e.Employee)
                .Include(e => e.Project);

            foreach (var employeeProject in projects.OrderBy(p => p.Project.Name))
            {
                Console.WriteLine(employeeProject.Project.Name);
            }
        }

        private static void AddressesByTown(SoftUniContext context)
        {
            var adresses = context.Addresses
                .OrderByDescending(a => a.Employees.Count)
                .ThenBy(a => a.Town.Name)
                .ThenBy(a => a.AddressText)
                .Include(a => a.Employees)
                .ThenInclude(e => e.Address.Town)
                .Take(10).ToList();

            foreach (var adress in adresses)
            {
                var count = adress.Employees.Count;
                var adressText = adress.AddressText;
                var townName = adress.Town.Name;
                Console.WriteLine($"{adressText}, {townName} - {count} employees");
            }
        }

        private static void EmployeesAndProjects(SoftUniContext context)
        {
            var emplIds = context.EmployeesProjects
                .Where(p => p.Project.StartDate.Year >= 2001 && p.Project.StartDate.Year <= 2003)
                .Select(e => e.EmployeeId).Distinct().ToList();

            var empList = context.Employees.ToList();

            foreach (var employee in empList.Where(e => emplIds.Contains(e.EmployeeId)).Take(30).ToList())
            {
                var emplProjects = context.EmployeesProjects.Where(e => e.EmployeeId == employee.EmployeeId)
                    .Select(p => p.Project).ToList();

                if (employee.Manager == null)
                {
                    Console.WriteLine($"{employee.FirstName} {employee.LastName} ");
                }
                else
                {
                    Console.WriteLine(
                        $"{employee.FirstName} {employee.LastName} - Manager: {employee.Manager.FirstName} {employee.Manager.LastName}");
                }

                foreach (var project in emplProjects)
                {
                    if (project.EndDate == null)
                    {
                        Console.WriteLine($"--{project.Name} - {project.StartDate:M/d/yyyy h:mm:ss tt} - not finished");
                    }
                    else
                        Console.WriteLine(
                            $"--{project.Name} - {project.StartDate:M/d/yyyy h:mm:ss tt} - {project.EndDate:M/d/yyyy h:mm:ss tt}");
                }
            }
        }

        private static void AddNewAddressUpdatecontext(SoftUniContext context)
        {
            var address = new Address();
            address.AddressText = "Vitoshka 15";
            address.TownId = 4;

            var searchedEmp = context.Employees.FirstOrDefault(e => e.LastName == "Nakov");

            searchedEmp.Address = address;

            context.SaveChanges();

            var allEmpAddress = context.Employees
                .OrderByDescending(e => e.AddressId)
                .Take(10)
                .Select(e => e.Address.AddressText);

            foreach (var empAddress in allEmpAddress)
            {
                Console.WriteLine(empAddress);
            }
        }

        private static void EmplFromDepartment(SoftUniContext context)
        {
            var selectedEmpl = context.Employees
                .Where(e => e.Department.Name == "Research and Development")
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    DepartmentName = e.Department.Name,
                    e.Salary
                });

            foreach (var emp in selectedEmpl)
            {
                Console.WriteLine($"{emp.FirstName} {emp.LastName} from {emp.DepartmentName} - ${emp.Salary:f2}");
            }
        }

        private static void EmplNameWithSalary(SoftUniContext context)
        {
            var emplWithSalary = context.Employees
                .Where(e => e.Salary > 50000)
                .OrderBy(e => e.FirstName)
                .Select(e => e.FirstName);
            foreach (var emp in emplWithSalary)
            {
                Console.WriteLine(emp);
            }
        }

        private static void EmployeeFullInfo(SoftUniContext context)
        {
            var employees = context.Employees.OrderBy(e => e.EmployeeId).ToList();

            foreach (var emp in employees)
            {
                var firstName = emp.FirstName;
                var lastName = emp.LastName;
                var middleName = emp.MiddleName ?? "";
                var job = emp.JobTitle;
                var salary = emp.Salary;

                Console.WriteLine($"{firstName} {lastName} {middleName} {job} {salary:f2}");

            }
        }
    }
}