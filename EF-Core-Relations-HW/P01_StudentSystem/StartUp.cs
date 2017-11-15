namespace P01_StudentSystem
{
    using Data;

    public class Program
    {
        static void Main()
        {
            var context = new StudentSystemContext();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
