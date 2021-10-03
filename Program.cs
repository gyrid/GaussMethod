using System;

namespace GaussMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            var rnd = new Random();
            Console.Write("n - порядок СЛАУ (n - число натуральное). Введите N: ");
            try
            {
                int n = Convert.ToInt32(Console.ReadLine());
                // n  - количество строк расширенной матрицы
                // n + 1 - количество столбцов расширенной матрицы
                var mtrExt = new decimal[n, n + 1];//Расширенная матрица
                var mtrSol = new decimal[n];//Матрица неизвестных
                var mtrExtCheking = new decimal[n, n + 1];//Копия расширенной матрицы для проверки решения
                Array.Copy(mtrExt, mtrExtCheking, mtrExt.Length);
                Console.Write("\n1)Тестирование (рандом)\n2)Заполнение\nВыбери вариант: ");
                bool flag = true;
                while (flag)
                {
                    //Заполнение расширенной матрицы
                    char choise = (char)Console.ReadKey().Key;
                    Console.WriteLine();
                    switch (choise)
                    {
                        case '1':
                            for (int i = 0; i < n; i++)
                            {
                                for (int j = 0; j < n + 1; j++)
                                {
                                    mtrExt[i, j] = rnd.Next(-1000, 1000)*0.01m;
                                }
                            }
                            flag = false;
                            break;
                        case '2':
                            string inputString;
                            for (int i = 0; i < n; i++)
                            {
                                inputString = Console.ReadLine();
                                if (inputString.Split().Length != n+1)
                                {

                                    Console.WriteLine("Неправильный формат строки: ");
                                    i--; continue;
                                }
                                else
                                {
                                    string[] arrayString = inputString.Split();
                                    for (int j = 0; j < n+1; j++)
                                    {
                                        mtrExt[i, j] = Convert.ToDecimal(arrayString[j]);
                                    }
                                }
                            }
                            flag = false;
                            break;
                        default:
                            Console.WriteLine("Попробуй снова: ");
                            break;
                    }
                }
                Console.WriteLine("Полученная расширенная матрица СЛАУ: ");
                OutputMatrix(mtrExt, n);

                /*Тестирование программы
                  Test1(ref mtrExt, n, 2);// 2 значит, что проверка идет 3-ого столбца
                  Test2(mtrExt, n);
                */

                //Реализация прямого хода Гауса
                for (int i = 0; i < n - 1; i++)
                {
                    SortMatrix(ref mtrExt, n, i);
                    for (int k = i + 1; k < n; k++)
                    {
                        if (mtrExt[k, i] == 0)
                        {
                            break;
                        }
                        for (int j = i + 1; j < n + 1; j++)
                        {
                            mtrExt[k, j] = mtrExt[k, j] / mtrExt[k, i] - mtrExt[i, j] / mtrExt[i, i];
                        }
                    }
                }

                //Вывод полученной треугольной расширенной матрицы 2 знака после запятой
                Console.WriteLine("\n\nПосле реализации прямого хода Гауса:");
                OutputMatrix(mtrExt, n, true);

                if (!FoundExistingSolution(mtrExt, n))
                {
                    Console.WriteLine("Единственного решения не существует");
                }
                else
                {
                    n -= 1;
                    mtrSol[n] = mtrExt[n, n + 1] / mtrExt[n, n];
                    // Реализация обратного хода Гауса
                    for (int i = n - 1; i > -1; i--)
                    {
                        for (int j = 1; j < n - i + 1; j++)
                        {
                            mtrExt[i, n + 1] -= mtrSol[i + j] * mtrExt[i, i + j];
                        }
                        mtrSol[i] = mtrExt[i, n + 1] / mtrExt[i, i];
                    }
                    Console.Write("\nРешение: ");
                    foreach (var item in mtrSol)
                    {
                        Console.Write(Math.Round(item, 4) + "  ");
                    }
                    Console.WriteLine();
                    if (CheckSolution(mtrExtCheking, mtrSol))
                    {
                        Console.WriteLine("Решение верно!");
                    }
                    else
                    {
                        Console.WriteLine("Решение не верно!");
                    }
                }

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static public void SortMatrix(ref decimal[,] matrix, int n, int col = 0 )
        {
            //Сортировка строк по столбцу col для расширенной матрицы[n; n+1]
            //n размерность массива
            int i = col; decimal temp;
            while (matrix[i, col] == 0 && i < n)
            {
                i++;
            }
            for (int j = 0; j < n+1; j++)
            {
                temp = matrix[col, j];
                matrix[col, j] = matrix[i, j];
                matrix[i, j] = temp;
            }
        }
        static public void OutputMatrix(decimal[,] mtrExt, int n, bool flag = false)
        {
            if (!flag)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n + 1; j++)
                    {
                        Console.Write(mtrExt[i, j] + " ");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = i; j < n + 1; j++)
                    {
                        Console.Write(Math.Round(mtrExt[i, j], 2) + " ");
                    }
                    Console.WriteLine();
                }
            }
        }
        static public bool FoundExistingSolution(decimal[,] mtrExt, int n, bool flagOutput = false)
        {
            int rangExtMatrix = 0; int rangBaseMatrix = 0;
            bool flag2 = true;
            for (int i = 0; i < n; i++)
            {
                int j = i;
                for (; j < n + 1; j++)
                {
                    if (mtrExt[i, j] != 0) {
                        flag2 = false;
                        break;
                    }   
                }
                if (j-1 <n){
                    rangBaseMatrix++;
                }
                if(!flag2){
                    rangExtMatrix++;
                    flag2 = true;
                }
            }
            if (rangExtMatrix == rangBaseMatrix)
            {
                if (rangExtMatrix == n)
                {
                    if (flagOutput)
                    {
                        Console.WriteLine("СЛАУ совместная и определенная (единственное решение)");
                    }
                    return true;
                }
                else
                {
                    if (flagOutput)
                    {
                        Console.WriteLine("CЛАУ совместная и неопределенная (бесконечное кол-во решений)");
                    }
                    return false;
                }
            }
            if (flagOutput)
            {
                Console.WriteLine("СЛАУ несовместная (нет решений)");
            }
            return false;
        }
        static public bool CheckSolution(decimal[,] mtrExt, decimal[] mtrSol)
        {
            int n = mtrSol.Length;
            decimal sum = 0.0m;
            for (int i = 0; i < n; i++)
            {
                int j = 0;
                for (; j < n; j++)
                {
                    sum = sum + mtrSol[j] * mtrExt[i, j];
                }
                if (sum != -(mtrExt[i, j])) return false;
            }
            return true;
        }
        static public void Test1(ref decimal[,] mtrExt, int n, int col = 0)
        {
            SortMatrix(ref mtrExt, n, col);
            Console.WriteLine($"\nПосле сортировки матрицы по строкам {col+1}-го столбца: ");
            OutputMatrix(mtrExt, n);
        }
        static public void Test2(decimal[,] mtrExt, int n)
        {
            Console.WriteLine("Представление расширенной матрицы в виде треугольной");
            OutputMatrix(mtrExt, n , true);
            Console.WriteLine("\nХарактер матрицы: ");
            FoundExistingSolution(mtrExt, n, true);
        }
    }
}
