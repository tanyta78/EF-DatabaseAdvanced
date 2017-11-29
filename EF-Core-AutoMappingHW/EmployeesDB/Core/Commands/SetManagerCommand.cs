namespace Employees.App.Core.Commands
{
    using System;
    using Contracts;
    using Services;

    public class SetManagerCommand:ICommand
    {
        private readonly EmployeeService emplService;

        public SetManagerCommand(EmployeeService emplService)
        {
            this.emplService = emplService;
        }
        //•	SetManager <employeeId> <managerId> – sets the second employee to be a manager of the first employee
        
        public string Execute(params string[] args)
        {
            if (args.Length != 2)
            {
                throw new InvalidOperationException($"Command arguments are not valid");
            }

            var emplId = int.Parse(args[0]);

            var managerId = int.Parse(args[1]);

           return this.emplService.SetManagerById(emplId, managerId);
        }
    }
}
