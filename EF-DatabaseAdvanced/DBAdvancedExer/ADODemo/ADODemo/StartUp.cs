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
            var projectPaginator = new Paginator(
                context.Projects
                .Select(p => new
                {
                    p.ProjectID,
                    p.Name
                })
                .ToList()
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

                if (!KeyboardController.PageController(key, projectPaginator)) return;
                
            }
        }

        static void ShowDetails(Project project)
        {
            //------------------------------------------
            Console.Clear();
            Console.WriteLine($"ID: {project.ProjectID,4}   |  Name {project.Name} ");
            Utility.PrintHLine();
            Console.WriteLine($"Description:{project.Description}");
            Utility.PrintHLine();
            Console.WriteLine($"{project.StartDate,-24} |   {project.EndDate}");
            Utility.PrintHLine();
            Console.WriteLine($"(Page )");
            Console.WriteLine("===================================");
            var pageSize = 16 - Console.CursorTop;

            var emploeeys = project.Employees.ToList();
            int page = 0;
            int maxPages = (int)Math.Ceiling(emploeeys.Count / (double)pageSize);
            int pointer = 1;

            while (true)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();

                Console.WriteLine($"ID: {project.ProjectID,4}   |  Name: {project.Name} ");
                Utility.PrintHLine();
                Console.WriteLine($"Description:{project.Description}");
                Utility.PrintHLine();
                Console.WriteLine($"{project.StartDate,-24} |   {project.EndDate}");
                Utility.PrintHLine();
                Console.WriteLine($"(Page {page + 1} of {maxPages})");
                Console.WriteLine("===================================");

                int currenr = 1;
                foreach (var emp in emploeeys.Skip(pageSize * page).Take(pageSize))
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;

                    if (currenr == pointer)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine($"{emp.FirstName,4} |{emp.LastName}");
                    currenr++;
                }

                var key = Console.ReadKey(true);
                switch (key.Key.ToString())
                {
                    /* case "Enter":
                         var currentProject = emploeeys.Skip(pageSize * page + pointer - 1).First();
                         ShowDetails(currentProject);
                         break;*/
                    case "UpArrow":
                        if (pointer > 1)
                        {
                            pointer--;
                        }
                        else if (page > 0)
                        {
                            page--;
                            pointer = pageSize;
                        }
                        break;
                    case "DownArrow":
                        if (pointer < pageSize)
                        {
                            pointer++;
                        }
                        else if (page + 1 < maxPages)
                        {
                            page++;
                            pointer = 1;
                        }
                        break;
                    case "Escape": return;
                }
            }
            //=================================
        }
    }
}
