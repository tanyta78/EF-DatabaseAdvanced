namespace Employees.App.Core.Commands
{
    using System;
    using System.Linq;
    using Contracts;
    using ModelsDTO;
    using Services;
    using AutoMapper.QueryableExtensions;

    public class ManagerInfoCommand : ICommand
    {
        private readonly EmployeeService emplService;

        public ManagerInfoCommand(EmployeeService emplService)
        {
            this.emplService = emplService;
        }
        //	ManagerInfo<employeeId> – prints on the console information about a manager in the following format:
        //  Steve Jobbsen | Employees: 2
        //- Stephen Bjorn - $4300.00
        //- Kirilyc Lefi - $4400.00


        public string Execute(params string[] args)
        {
            if (args.Length != 1)
            {
                throw new InvalidOperationException($"Command arguments are not valid");
            }

            var emplId = int.Parse(args[0]);
            
           return this.emplService.ManagerInfo(emplId); 
        }

    }
}
