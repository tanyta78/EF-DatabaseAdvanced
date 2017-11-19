namespace P03_UserDetailsOrganizer.Utility
{
    static class Composer
    {
        public static string[] Compose(char[][] matrix)
        {
            string[] layout = new string[matrix.Length];

            for (int row = 0; row < layout.Length; row++)
            {
                layout[row] = string.Join("", matrix[row]);
            }

            return layout;
        }

        public static char[][] MakeBoxLayout(int width, int height)
        {
            char[][] matrix = new char[height][];
            char current = ' ';

            for (int y = 0; y < height; y++)
            {
                char[] line = new char[width];
                for (int x = 0; x < width; x++)
                {
                    if (x == 0)
                    {
                        if (y == 0) current = (char)9556;
                        else if (y == height - 1) current = (char)9562;
                        else current = (char)9553;
                    }
                    else if (x == width - 1)
                    {
                        if (y == 0) current = (char)9559;
                        else if (y == height - 1) current = (char)9565;
                        else current = (char)9553;
                    }
                    else
                    {
                        if (y == 0 || y == height - 1) current = (char)9552;
                        else current = ' ';
                    }
                    line[x] = current;
                }
                matrix[y] = line;
            }

            return matrix;
        }

        public static void AddHorizontalLine(char[][] matrix, int row, int begin, int end)
        {
            for (int col = begin; col <= end; col++)
            {
                if (col == begin)
                {
                    matrix[row][col] = (char)9568;
                }
                else if (col == end)
                {
                    matrix[row][col] = (char)9571;
                }
                else
                {
                    matrix[row][col] = (char)9552;
                }
            }
        }

        public static void AddVerticalLine(char[][] matrix, int column, int top, int bottom)
        {
            for (int row = top; row <= bottom; row++)
            {
                if (row == top)
                {
                    matrix[row][column] = (char)9574;
                }
                else if (row == bottom)
                {
                    matrix[row][column] = (char)9577;
                }
                else
                {
                    matrix[row][column] = (char)9553;
                }
            }
        }
    }
}
