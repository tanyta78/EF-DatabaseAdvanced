namespace Employees.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using ModelsDTO;
    using AutoMapper;
    using Models;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper.QueryableExtensions;


    public class EmployeeService
    {
        private readonly EmployeeDbContext db;

        public EmployeeService(EmployeeDbContext db)
        {
            this.db = db;
        }

        public EmployeeDto ById(int emplId)
        {
            var employee = this.db.Employees.Find(emplId);

            var emplDto = Mapper.Map<EmployeeDto>(employee);
            
            return emplDto;
        }

        public void AddEmployee(EmployeeDto dto)
        {
            var employee = Mapper.Map<Employee>(dto);
            this.db.Employees.Add(employee);
            this.db.SaveChanges();
        }

        public string SetBirthday(int employeeId,DateTime birthday)
        {
            var employee = this.db.Employees.Find(employeeId);
            employee.Birthday = birthday;
            this.db.SaveChanges();

            return $"{employee.FirstName}{employee.LastName}";
        }

        public string SetAddress(int employeeId, string address)
        {
            var employee = this.db.Employees.Find(employeeId);
            employee.Address = address;
            this.db.SaveChanges();
            
            return $"{employee.FirstName}{employee.LastName}";
        }
        
        
        public EmployeePersonalDto EmployeePersonalInfo(int employeeId)
        {
            var employee = this.db.Employees.Find(employeeId);

            var emplDto = Mapper.Map<EmployeePersonalDto>(employee);

            return emplDto;
        }

        public string SetManagerById(int emplId,int managerId)
        {
            var employee = this.db.Employees.Find(emplId);

            var manager = this.db.Employees.Find(managerId);

            employee.ManagerId = managerId;
            manager.Subordinates.Add(employee);
            
            this.db.SaveChanges();

            return $"{manager.FirstName} {manager.LastName} was successfully set to manager to {employee.FirstName} {employee.LastName}";
        }

        public string ManagerInfo(int employeeId)
        {
            //var employee = this.db.Employees.Include(e=>e.Subordinates).FirstOrDefault(e=>e.Id==employeeId);
            var employee = this.db.Employees.Find(employeeId);
            var managerDto = Mapper.Map<ManagerDto>(employee);

            //var sub = employee.Subordinates.AsQueryable().ProjectTo<EmployeeDto>().ToList().Select(e => $"- {e.FirstName} {e.LastName} - ${e.Salary:f2}");
            var sub = this.db.Employees
                .Where(e => e.ManagerId == employeeId)
                .ProjectTo<EmployeeDto>()
                .ToList().Select(e => $"- {e.FirstName} {e.LastName} - ${e.Salary:f2}");

           
            string result = $"{managerDto.FirstName} {managerDto.LastName} | {managerDto.SubordinatesCount}" +
                            Environment.NewLine + string.Join(Environment.NewLine, sub);

            return result;
        }

        public string ListEmployeesOlderThan(int age)
        {
            DateTime now = DateTime.Today;
          
            var employees = this.db.Employees
                .Where(e=>e.Birthday.Value.Year<DateTime.Now.Year-age)
                .OrderByDescending(e => e.Salary)
                .ProjectTo<EmployeeListDto>()
                .Select(employee=> $"{employee.FirstName} {employee.LastName} {employee.Salary} - Manager: {employee.ManagerFirstName ?? "[no manager]"}");

           return string.Join(Environment.NewLine,employees);
        }
    }
}
