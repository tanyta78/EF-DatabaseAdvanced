namespace Mankind
{
    using System;

    public class Program
    {
       public static void Main()
        {
            var studentInfo = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var studentFirstName = studentInfo[0];
            var studentLastName = studentInfo[1];
            var studentFacultyNumber = studentInfo[2];

            var workerInfo = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var workerFirstName = workerInfo[0];
            var workerLastName = workerInfo[1];
            var weekSalary = decimal.Parse(workerInfo[2]);
            var workHours = decimal.Parse(workerInfo[3]);
            Student student;

            try
            {
                student = new Student(studentFirstName, studentLastName, studentFacultyNumber);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            Worker worker;

            try
            {
                worker = new Worker(workerFirstName, workerLastName, weekSalary, workHours);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            Console.WriteLine(student.ToString());
            Console.WriteLine(worker.ToString());
        }
    }
}
