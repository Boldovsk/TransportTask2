using System.Collections.Generic;
using System.IO;
using Transport;
using Table;
using System;
using System.Security.Cryptography.X509Certificates;

namespace ForPath
{
    public class Metod : Metod2
    {
        public static List<int> FindPath(int row, int col, TableCell[,] cells)
        {
            List<int> path = new List<int>();
            LookHorizontaly(path, row, col, row, col, cells);

            return path;
        }
    }

    public class Metod2
    {
        protected static bool LookHorizontaly(List<int> path, int srow, int scol, int row, int col, TableCell[,] cells)
        {
            for (int j = 0; j < cells.GetLength(1); j++)
            {
                if (j != col && cells[row, j].x != null)
                {
                    // если вернулись к началу
                    if (j == scol)
                    {
                        path.Add(cells.GetLength(1) * row + j);
                        return true;
                    }

                    if (LookVerticaly(path, srow, scol, row, j, cells))
                    {
                        path.Add(cells.GetLength(1) * row + j);
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool LookVerticaly(List<int> path, int srow, int scol, int row, int col, TableCell[,] cells)
        {
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                if (i != row && cells[i, col].x != null)
                {
                    if (LookHorizontaly(path, srow, scol, i, col, cells))
                    {
                        path.Add(cells.GetLength(1) * i + col);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
