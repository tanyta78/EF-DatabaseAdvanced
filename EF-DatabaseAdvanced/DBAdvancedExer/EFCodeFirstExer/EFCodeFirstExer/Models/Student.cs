namespace EFCodeFirstExer.Models
{
    public class Student
    {
        private string name;
        public static int NumberOfCreatedStudents=0;

        public Student(string name)
        {
            this.Name = name;
            NumberOfCreatedStudents++;
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
    }
}
