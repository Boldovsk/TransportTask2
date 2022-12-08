using Table;
using FindOptimal;
using ForPath;

namespace TestProject1
{
    public class Tests : Optimal
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestFormCost()
        {
            TableCell[,] cellsTest;

            //считываем файл
            string[] lines = File.ReadAllLines("test-input.txt");

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

            //матрица €чеек
            cellsTest = new TableCell[n, m];
            cellsTest = TableWork.FormCosts(lines, cellsTest, m, n);

            TableCell[,] expectedArray = new TableCell[n, m];

            int[,] currentArray = new int[n, m];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    currentArray[i, j] = (int)cellsTest[i, j].c;
                }

            int[,] expectedArray2 = {  {9, 7, 7, 6, 5, 4},
                                       {4, 8, 4, 5, 9, 5},
                                       {9, 5, 7, 6, 8, 10},
                                       {5, 7, 4, 9, 7, 3},
                                       {5, 7, 5, 6, 5, 8 } };

            Assert.AreEqual(currentArray, expectedArray2);
        }

        [Test]
        public void NegativeTestFormCost()
        {
            TableCell[,] cellsTest;

            //считываем файл
            string[] lines = File.ReadAllLines("test-input.txt");

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

            //матрица €чеек
            cellsTest = new TableCell[n, m];
            cellsTest = TableWork.FormCosts(lines, cellsTest, m, n);

            TableCell[,] expectedArray = new TableCell[n, m];

            int[,] currentArray = new int[n, m];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    currentArray[i, j] = (int)cellsTest[i, j].c;
                }

            int[,] expectedArray2 = {  {0, -7, -7, -6, -5, -4},
                                       {-4, 0, -4, -5, -9, -5},
                                       {-9, -5, 0, -6, -8, -10},
                                       {-5, -7, -4, 0, -7, -3},
                                       {-5, -7, -5, -6, 0, 0 } };

            Assert.AreNotEqual(currentArray, expectedArray2);
        }

        [Test]
        public void TestTargetFunction()
        {
            TableCell[,] cellsTest;

            //считываем файл
            string[] lines = File.ReadAllLines("test-input.txt");

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

            //матрица €чеек
            cellsTest = new TableCell[n, m];

            int expectedInt = 1110;

            cellsTest = TableWork.FormCosts(lines, cellsTest, m, n);

            int[,] xmassive = {  {0, 0, 0, 30, 15, 20},
                                 {35, 0, 0, 0, 0, 0},
                                 {0, 35, 0, 10, 0, 0},
                                 {0, 0, 25, 0, 0, 0},
                                 {40, 0, 10, 0, 10, 0} };

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    cellsTest[i, j].x = xmassive[i, j];
                }

            int currentInt = TargetFunction(cellsTest, n, m);

            Assert.AreEqual(expectedInt, currentInt);
        }

        [Test]
        public void NegativeTestTargetFunction()
        {
            TableCell[,] cellsTest;

            //считываем файл
            string[] lines = File.ReadAllLines("test-input.txt");

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

            //матрица €чеек
            cellsTest = new TableCell[n, m];

            int expectedInt = 1100;

            cellsTest = TableWork.FormCosts(lines, cellsTest, m, n);

            int[,] xmassive = {  {0, 0, 0, 30, 15, 20},
                                 {35, 0, 0, 0, 0, 0},
                                 {0, 35, 0, 10, 0, 0},
                                 {0, 0, 25, 0, 0, 0},
                                 {40, 0, 10, 0, 10, 0} };

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    cellsTest[i, j].x = xmassive[i, j];
                }

            int currentInt = TargetFunction(cellsTest, n, m);

            Assert.AreNotEqual(expectedInt, currentInt);
        }

        [Test]
        public void TestFindPath()
        {
            List<int> resultPath;
            TableCell[,] cellsTest;

            //считываем файл
            string[] lines = File.ReadAllLines("test-input.txt");

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

            //матрица €чеек
            cellsTest = new TableCell[n, m];
            cellsTest = TableWork.FormCosts(lines, cellsTest, m, n);

            int row = 0; int col = 5;

            int?[,] xmassive = { {45, null, null, null, null, 20},
                                 {30, 5, null, null, null, null},
                                 {30, 15, null, null, null, null},
                                 {null, null, 20, 5, null, null},
                                 {null, null, null, 35, 25, null} };

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    cellsTest[i, j].x = xmassive[i, j];
                }

            List<int> expectedPath = new List<int> { 5, 0, 12, 13, 7, 6, 0 };

            resultPath = Metod.FindPath(row, col, cellsTest);

            CollectionAssert.AreEqual(expectedPath, resultPath);
        }
        [Test]
        public void NegativeTestFindPath()
        {
            List<int> resultPath;
            TableCell[,] cellsTest;

            //считываем файл
            string[] lines = File.ReadAllLines("test-input.txt");

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

            //матрица €чеек
            cellsTest = new TableCell[n, m];
            cellsTest = TableWork.FormCosts(lines, cellsTest, m, n);

            int row = 0; int col = 5;

            int?[,] xmassive = { {45, null, null, null, null, 20},
                                 {30, 5, null, null, null, null},
                                 {30, 15, null, null, null, null},
                                 {null, null, 20, 5, null, null},
                                 {null, null, null, 35, 25, null} };

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    cellsTest[i, j].x = xmassive[i, j];
                }

            List<int> expectedPath = new List<int> { 5, 0, 13, 13, 7, 6, 0 };

            resultPath = Metod.FindPath(row, col, cellsTest);

            Assert.AreNotEqual(expectedPath, resultPath);
        }
    }
}