namespace Employees.App.Core.Commands
{
    using System;
    using Contracts;
    using Services;

    public class SetBirthdayCommand:ICommand
    {
        private readonly EmployeeService emplService;

        public SetBirthdayCommand(EmployeeService emplService)
        {
            this.emplService = emplService;
        }
        //•	SetBirthday <employeeId> <date: "dd-MM-yyyy"> – sets the birthday of the employee to the given date

        public string Execute(params string[] args)
        {
            if (args.Length != 2)
            {
                throw new InvalidOperationException($"Command arguments are not valid");
            }

            var emplId = int.Parse(args[0]);
            var date = DateTime.ParseExact(args[1], "dd-MM-yyyy",null);

            var employeeName=this.emplService.SetBirthday(emplId,date);

            return $"{employeeName}'s birthday was set to {args[1]}";
        }
    }
}
