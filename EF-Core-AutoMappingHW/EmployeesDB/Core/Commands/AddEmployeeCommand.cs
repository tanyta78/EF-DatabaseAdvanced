namespace Employees.App.Core.Commands
{
    using System;
    using Contracts;
    using ModelsDTO;
    using Services;

    public class AddEmployeeCommand:ICommand
    {
        private readonly EmployeeService emplService;

        public AddEmployeeCommand(EmployeeService emplService)
        {
            this.emplService = emplService;
        }
        
        //•	AddEmployee <firstName> <lastName> <salary> –  adds a new Employee to the database

        public string Execute(params string[] args)
        {
            if (args.Length != 3)
            {
                throw new InvalidOperationException($"Command arguments are not valid");
            }

            var firstName = args[0];
            var lastName = args[1];
            var salary = decimal.Parse(args[2]);

            var emplDto = new EmployeeDto(firstName, lastName, salary);
            
            this.emplService.AddEmployee(emplDto);
            
            return $"Employee {firstName} {lastName} successfully added!";
        }
    }
}
