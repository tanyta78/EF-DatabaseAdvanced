namespace Employees.App.Core.Commands
{
    using System;
    using System.Globalization;
    using Contracts;
    using Services;

    public  class EmployeePersonalInfoCommand:ICommand
    {
        private readonly EmployeeService emplService;

        public EmployeePersonalInfoCommand(EmployeeService emplService)
        {
            this.emplService = emplService;
        }
        //•	EmployeePersonalInfo <employeeId> – prints all the information for an employee in the following format:
        // ID: 1 - Pesho Ivanov - $1000:00
        //Birthday: 15-04-1976
        // Address: Sofia, ul.Vitosha 15

        public string Execute(params string[] args)
        {
            if (args.Length != 1)
            {
                throw new InvalidOperationException($"Command arguments are not valid");
            }

            var emplId = int.Parse(args[0]);

            var employee = this.emplService.EmployeePersonalInfo(emplId);
            string birthday = "no info";
            
            if (employee.Birthday!=null)
            {
                birthday = employee.Birthday.Value.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            }

            string result = $"ID: {emplId} - {employee.FirstName} {employee.LastName} - ${employee.Salary:f2}" +
                            Environment.NewLine + $"Birthday: {birthday}" + Environment.NewLine +
                            $"Address: {employee.Address??"no info"}";
            return result;
        }
    }
}
