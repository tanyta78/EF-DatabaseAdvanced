namespace Employees.App.Core.Commands
{
    using System;
    using Contracts;
    using Services;

    public class ListEmployeesOlderThanCommand:ICommand
    {
        private readonly EmployeeService emplService;

        public ListEmployeesOlderThanCommand(EmployeeService emplService)
        {
            this.emplService = emplService;
        }
        //Create a command "ListEmployeesOlderThan <age>" which lists all employees older than given age and their managers. Order them by salary descending
        //Kirilyc Lefi - $4400.00 - Manager: Jobbsen
        
        public string Execute(params string[] args)
        {
            if (args.Length != 1)
            {
                throw new InvalidOperationException($"Command arguments are not valid");
            }

            var age = int.Parse(args[0]);

            var result = this.emplService.ListEmployeesOlderThan(age);


            return result;
        }
    }
}

