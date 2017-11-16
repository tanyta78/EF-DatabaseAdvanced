namespace P01_StudentSystem
{
    using Data;
    using DBInitializer;

    public class Program
    {
        static void Main()
        {
            DatabaseInitializer.ResetDatabase();
        }
    }
}
