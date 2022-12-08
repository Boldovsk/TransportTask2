using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using ForPath;
using FindOptimal;
using Table;
using System.Runtime.Serialization;

namespace Transport
{
    class Transport1 : Optimal
    {
        static List<int> currentPath;
        static TableCell[,] cells;
        //считываем файл
        string[] lines = File.ReadAllLines("input.txt");

        public void Main()
        {
            //получаем размеры
            string[] sizes = lines[0].Split();

            //n поставщиков, m потребителей
            int n = Int32.Parse(sizes[0]);
            int m = Int32.Parse(sizes[1]);

            //запасы поставщиков
            int[] suppliers = new int[n];
            suppliers = lines[1].Split(' ').Select(Int32.Parse).ToArray();

            //потребности магазинов
            int[] markets = new int[m];
            markets = lines[2].Split(' ').Select(Int32.Parse).ToArray();

            //матрица ячеек
            cells = new TableCell[n, m];
            cells = TableWork.FormCosts(lines, cells, m, n);

            //нахождение начального БДР методом северо-западного угла
            int row = 0; int col = 0;

            cells = Optimal.BDR(cells, suppliers, markets, n, m, row, col);

            //метод потенциалов
            int?[] U = new int?[n]; //в строке (a)
            int?[] V = new int?[m]; //в столбце (b)
            var list = new List<int>();
            while (true)
            {
                //зануляем для разрешимости системы, находим U и V
                U[0] = 0;
                while (U.Contains(null) || V.Contains(null))
                {
                    for (int i = 0; i < n; i++)
                        for (int j = 0; j < m; j++)
                        {
                            if (cells[i, j].x == null)
                                continue;

                            if (U[i] != null)
                                V[j] = cells[i, j].c - U[i];
                            else
                                if (V[j] != null)
                                U[i] = cells[i, j].c - V[j];

                        }
                }

                //оценки для небазисных (не исп.) переменных
                bool opt = true;
                int minS = Int32.MaxValue;
                int minI = -1; int minJ = -1;
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < m; j++)
                    {
                        if (cells[i, j].x != null)
                            continue;

                        cells[i, j].s = (int)cells[i, j].c - U[i] - V[j];

                        // поиск минимальной оценки
                        if (cells[i, j].s < minS)
                        {
                            minS = (int)cells[i, j].s;
                            minI = i; minJ = j;
                        }

                        //проверка на оптимальность
                        if (cells[i, j].s < 0)
                            opt = false;
                    }

                // целевая функция
                int z = TargetFunction(cells, n, m);

                if (opt)
                {

                    StreamWriter f = new StreamWriter("output.txt");
                    f.WriteLine("Сумма затрат: " + z + "\n");
                    f.WriteLine("Оптимальный план:");
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < m; j++)
                        {
                            //если не перевозится товаров 0
                            if (cells[i, j].x == null)
                                f.Write(0);
                            else
                                //если какое-то кол-во товаров перевозится
                                //то записывается это кол-во
                                f.Write(cells[i, j].x);

                            if (j < cells.GetLength(1))
                                f.Write(" ");
                        }
                        f.WriteLine();
                    }
                    f.Close();
                    return;
                }

                // '+' в ячейку с минимальной оценкой
                cells[minI, minJ].sign = 1;

                // строим цикл

                currentPath = Metod.FindPath(minI, minJ, cells);
                var sign = -1;
                int lambda = int.MaxValue;

                //устанавливаем знаки и берем минимальное значение с минусом
                foreach (var index in currentPath)
                {
                    row = index / cells.GetLength(1);
                    col = index - cells.GetLength(1) * row;
                    cells[row, col].sign = 1;
                    cells[row, col].sign *= sign;

                    if (sign == -1 && cells[row, col].x < lambda)
                    {
                        lambda = (int)cells[row, col].x;
                        minI = row; minJ = col;
                    }

                    sign *= -1;
                }

                //пересчет таблицы
                for (int i = 0; i < n; i++)
                {
                    U[i] = null;
                    for (int j = 0; j < m; j++)
                    {
                        V[j] = null;
                        cells[i, j].s = null;
                        if (cells[i, j].sign != null)
                        {
                            if (cells[i, j].x == null)
                                cells[i, j].x = 0;
                            cells[i, j].x += lambda * cells[i, j].sign;
                        }
                        cells[i, j].sign = null;
                    }
                }

                //удаление клетки из базиса
                cells[minI, minJ].x = null;
            }
        }
    }
}
