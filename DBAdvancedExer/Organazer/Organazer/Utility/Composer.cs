namespace Organazer.Utility
{
    public static class Composer
    {
        public static char[][] MakeBoxLayout(int width, int height)
        {
            char[][] matrix = new char[height][];

            for (int row = 0; row < height; row++)
            {
                matrix[row] = new char[width];
                for (int col = 0; col < width; col++)
                {
                    if (row == 0)
                    {
                        if (col == 0)
                        {
                            matrix[row][col] = (char)9556;
                        }
                        else if (col == width - 1)
                        {
                            matrix[row][col] = (char)9559;
                        }
                        else
                        {
                            matrix[row][col] = (char)9552;
                        }
                    }
                    else if (row == height - 1)
                    {
                        if (col == 0)
                        {
                            matrix[row][col] = (char)9562;
                        }
                        else if (col == width - 1)
                        {
                            matrix[row][col] = (char)9565;
                        }
                        else
                        {
                            matrix[row][col] = (char)9552;
                        }
                    }
                    else if (col == 0 || col == width - 1)
                    {
                        matrix[row][col] = (char)9553;
                    }
                    else
                    {
                        matrix[row][col] = ' ';
                    }
                }
            }
            return matrix;
        }

        public static string[] Compose(char[][] matrix)
        {
            string[] layout = new string[matrix.Length];

            for (int row = 0; row < matrix.Length; row++)
            {
                layout[row] = string.Join("", matrix[row]);
            }

            return layout;
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
