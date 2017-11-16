namespace P01_StudentSystem.DBInitializer.Generators
{
    using System;
    using Data;
    using Data.Models;

    public class StudentGenetaror
    {
        public static void InitialStudentSeed(StudentSystemContext db, int count)
        {
            for (int i = 0; i < count; i++)
            {
                db.Students.Add(NewStudent());
                db.SaveChanges();
            }
        }

        public static Student NewStudent()
        {
            string name = NameGenerator.FirstName() + " " + NameGenerator.LastName();
            DateTime registeredOn = DateGenerator.GenerateDate();
            string phoneNumber = PhoneNumberGenerator.NewPhoneNumber();

            Student customer = new Student()
            {
                Name = name,
                RegisteredOn = registeredOn,
                PhoneNumber = phoneNumber
            };

            return customer;
        }
    }
}