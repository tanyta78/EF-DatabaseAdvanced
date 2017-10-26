namespace ADODemo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;


    public class Paginator
    {
        public Paginator(List<string> data, int top, int left, int pageSize, bool hasHighLigth = false)
        {
            this.Data = data;
            this.Top = top;
            this.Left = left;
            this.PageSize = pageSize;
            this.HasHighLigth = hasHighLigth;
            this.MaxPages = (int) Math.Ceiling(this.Data.Count / (double) this.PageSize);
            this.CurrentPage = 0;
            this.CursorPos = 1;
        }

        public List<string> Data { get; set; }

        public int Top { get; set; }

        public int Left { get; set; }

        public int PageSize { get; set; }

        public int MaxPages { get; set; }

        public int CurrentPage { get; set; }

        public bool HasHighLigth { get; set; }

        public int CursorPos { get; set; }

        public void Print()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(this.Left, this.Top);

            int currenr = 1;

            foreach (var line in this.Data.Skip(this.PageSize * this.CurrentPage).Take(this.PageSize))
            {
                if (this.HasHighLigth)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;

                    if (currenr == this.CursorPos)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    currenr++;
                }
                Console.WriteLine(line);


            }

        }
    }
}
