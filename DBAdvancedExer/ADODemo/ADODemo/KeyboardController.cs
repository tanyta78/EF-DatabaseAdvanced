namespace ADODemo
{
    using System;
    using System.Runtime.CompilerServices;

    public class KeyboardController
    {
        public static bool PageController(ConsoleKeyInfo key, Paginator paginator)
        {
            switch (key.Key.ToString())
            {
               /* case "Enter":
                    var currentProject = projects.Skip(pageSize * page + pointer - 1).First();
                    ShowDetails(currentProject);
                    break;*/
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
    }
}
