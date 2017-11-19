namespace ADODemo
{
    using System;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Runtime.Remoting.Messaging;

    public class StartUp
    {
        public static void Main()
        {


            //Window initialization ;

            Console.WindowHeight = 17;
            Console.WindowWidth = 50;
            Console.BufferHeight = 17;
            Console.BufferWidth = 50;
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            //db init
            var context = new SoftUniEntities();
            ListAll(context);

        }

        static void ListAll(SoftUniEntities context)
        {
            var data = context.Projects
                .Select(p => new
                {
                    p.ProjectID,
                    p.Name
                })
                .ToList();
            var projectPaginator = new Paginator(data
                
                .Select(p => $"{ p.ProjectID,4}|{ p.Name }").ToList(), 2, 0, 14, true);

            while (true)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;

                Console.Clear();
                Console.WriteLine($"ID   |   Project name (Page {projectPaginator.CurrentPage + 1} of {projectPaginator.MaxPages})");
                Console.WriteLine("===================================");

                projectPaginator.Print();

                var key = Console.ReadKey(true);

                if (!KeyboardController.PageController(key, projectPaginator,context)) return;
                
            }
        }

        public static void ShowDetails(Project project)
        {
           
        }
    }
}
