namespace Mankind
{
    using System;
    using System.Text;

    public class Worker : Human
    {
        private decimal weekSalary;
        private decimal workHoursPerDay;


        public Worker(string firstName, string lastName, decimal weekSalary, decimal workHoursPerDay) : base(firstName, lastName)
        {
            this.WeekSalary = weekSalary;
            this.WorkHoursPerDay = workHoursPerDay;
        }

        public decimal WeekSalary
        {
            get { return this.weekSalary; }
            set
            {
                if (value <= 10)
                {
                    throw new ArgumentException("Expected value mismatch! Argument: weekSalary");
                }

                this.weekSalary = value;
            }
        }

        public decimal WorkHoursPerDay
        {
            get { return this.workHoursPerDay; }
            set
            {
                if (value > 12 || value < 1)
                {
                    throw new ArgumentException("Expected value mismatch! Argument: workHoursPerDay");
                }

                this.workHoursPerDay = value;
            }
        }

        public decimal SalaryPerHour()
        {
            var salaryPerHour = this.weekSalary / (5 * this.workHoursPerDay);
            return salaryPerHour;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"First Name: {this.FirstName}").AppendLine($"Last Name: {this.LastName}")
                .AppendLine($"Week Salary: {this.WeekSalary:f2}").AppendLine($"Hours per day: {this.WorkHoursPerDay:f2}")
                .AppendLine($"Salary per hour: {this.SalaryPerHour():f2}");

            return sb.ToString();
        }
    }
}
