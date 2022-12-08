using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Table;

namespace Table
{
    /* Класс одной ячейки таблицы, где:
     * c - стоимость перевозки со склада i в магазин j
     * sign - знак для лямбды
     * s - оценка
     * x - количество перевозимых товаров со склада i в магазин j
     */

    public class TableCell
    {
        public int? c;
        public int? sign;
        public int? s;
        public int? x;

        public TableCell()
        {
            this.c = null;
            this.sign = null;
            this.s = null;
            this.x = null;
        }
    }
    public class TableWork
    {

        public static TableCell[,] FormCosts(string[] lines, TableCell[,] cells, int m, int n)
        {
            int[] matrixLine = new int[m];

            //задаем стоимости перевозки
            for (int i = 0; i < n; i++)
            {
                matrixLine = lines[3 + i].Split(' ').Select(Int32.Parse).ToArray();
                for (int j = 0; j < m; j++)
                {
                    cells[i, j] = new TableCell();
                    cells[i, j].c = matrixLine[j];
                }
            }
            return cells;
        }
    }
}
