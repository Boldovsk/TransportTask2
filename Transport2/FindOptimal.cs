using System.Collections.Generic;
using System.IO;
using Transport;
using Table;
using System;

namespace FindOptimal
{
    public class Optimal
    {

        public static TableCell[,] BDR(TableCell[,] cells, int[] suppliers, int[] markets, int n, int m, int row, int col)
        {
            int[] supValues = new int[n]; suppliers.CopyTo(supValues, 0);
            int[] marValues = new int[m]; markets.CopyTo(marValues, 0);

            while (true)
            {
                // определяем минимальную допустимую перевозку
                cells[row, col].x = Math.Min(supValues[row], marValues[col]);

                // убираем запасы/потребности
                supValues[row] -= (int)cells[row, col].x;
                marValues[col] -= (int)cells[row, col].x;

                //если дошли до конца
                if (row == n - 1 && col == m - 1)
                {
                    break;
                }

                //переходим к след. магазину или складу
                if (supValues[row] > 0)
                    col++;
                else
                    row++;
            }

            StreamWriter fz = new StreamWriter("fz.txt");
            fz.WriteLine("counter");
            fz.Close();

            return cells;
        }
        //нахождение целевой функции
        protected static int TargetFunction(TableCell[,] cells, int n, int m)
        {
            int z = 0;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    if (cells[i, j].x != null)
                        z += (int)cells[i, j].x * (int)cells[i, j].c;
                }
            return z;
        }
    }
}