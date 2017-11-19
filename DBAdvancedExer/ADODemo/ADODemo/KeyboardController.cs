namespace ADODemo
{
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class KeyboardController
    {
        public static bool PageController(ConsoleKeyInfo key, Paginator paginator, SoftUniEntities context)
        {
            switch (key.Key.ToString())
            {
                case "Enter":
                    var projectIDAsString = paginator
                        .Data
                        .Skip(paginator.PageSize * paginator.CurrentPage + paginator.CursorPos - 1)
                        .First()
                        .Substring(0,4)
                       ;
                    int projectID = int.Parse(projectIDAsString);
                    
                    var currentProject = context.Projects.First(p=>p.ProjectID==projectID);
                    ShowDetails(currentProject);
                    break;
                case "UpArrow":
                    if (paginator.CursorPos > 1)
                    {
                        paginator.CursorPos--;
                    }
                    else if (paginator.CurrentPage > 0)
                    {
                        paginator.CurrentPage--;
                        paginator.CursorPos = paginator.PageSize;
                    }
                    break;
                case "DownArrow":
                    if (paginator.CursorPos < paginator.PageSize )
                    {
                        if (paginator.CurrentPage==paginator.MaxPages-1 && paginator.CursorPos+1 > paginator.Data.Count % paginator.PageSize)
                        {
                            break;
                        }
                        paginator.CursorPos++;
                    }
                    else if (paginator.CurrentPage + 1 < paginator.MaxPages)
                    {
                        paginator.CurrentPage++;
                        paginator.CursorPos = 1;
                    }
                    break;
                case "Escape": return false;
            }

            return true;
        }

        private static void ShowDetails(Project project)
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
                    /*case "Enter":
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
