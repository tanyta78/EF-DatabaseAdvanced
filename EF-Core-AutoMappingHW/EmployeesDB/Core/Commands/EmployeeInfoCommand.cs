namespace Employees.App.Core.Commands
{
    using System;
    using Contracts;
    using Services;

    public class EmployeeInfoCommand:ICommand
    {
        private readonly EmployeeService emplService;

        public EmployeeInfoCommand(EmployeeService emplService)
        {
            this.emplService = emplService;
        }
        //•	EmployeeInfo <employeeId> – prints on the console the information for an employee in the format "ID: {employeeId} - {firstName} {lastName} -  ${salary:f2}"
        public string Execute(params string[] args)
        {
            if (args.Length != 1)
            {
                throw new InvalidOperationException($"Command arguments are not valid");
            }

            var emplId = int.Parse(args[0]);

            var employee = this.emplService.ById(emplId);


            return $"ID: {emplId} - { employee.FirstName} { employee.LastName} - ${ employee.Salary:f2}";
        }
    }
}
