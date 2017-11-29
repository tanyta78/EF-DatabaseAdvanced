namespace Employees.App.Core.Commands
{
    using System;
    using System.Linq;
    using Contracts;
    using Services;

    public class SetAddressCommand:ICommand
    {
        private readonly EmployeeService emplService;

        public SetAddressCommand(EmployeeService emplService)
        {
            this.emplService = emplService;
        }
        //•	SetAddress <employeeId> <address> –  sets the address of the employee to the given string

        public string Execute( params string[] args)
        {
            if (args.Length < 2)
            {
                throw new InvalidOperationException($"Command arguments are not valid");
            }

            var emplId = int.Parse(args[0]);
            var address = string.Join(" ",args.Skip(1));

            var employeeName = this.emplService.SetAddress(emplId, address);

            return $"{employeeName}'s address was set to {address}";
        }
    }
}
