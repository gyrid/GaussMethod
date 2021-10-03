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
                Console.Write("\n1)Тестирование (рандом)\n2)Заполнение\nВыбери вариант: ");
                bool flag = true;
                while (flag)
                {
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
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n + 1; j++)
                    {
                        Console.Write(mtrExt[i, j] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("Сортировка матриц");
                //Заполнение расширенной матрицы



                ////Реализация прямого хода Гауса
                //for (int i = 0; i < n - 1; i++)
                //{
                //    for (int k = i + 1; k < n; k++)
                //    {
                //        for (int j = i + 1; j < n + 1; j++)
                //        {
                //            //   mtrExt[k, j] = mtrExt[k, j] * mtrExt[i, i] - mtrExt[i, j] * mtrExt[k, i];
                //            mtrExt[k, j] = mtrExt[k, j] / mtrExt[k, i] - mtrExt[i, j] / mtrExt[i, i];
                //        }

                //    }
                //}

                ////Вывод полученной треугольной расширенной матрицы 2 знака после запятой
                //Console.WriteLine("\n\nПосле реализации прямого хода Гауса:");
                //for (int i = 0; i < n; i++)
                //{
                //    Console.WriteLine();
                //    for (int j = i; j < n + 1; j++)
                //    {
                //        Console.Write(Math.Round(mtrExt[i, j], 2) + " ");
                //    }
                //}

                //n -= 1;
                //mtrSol[n] = mtrExt[n, n + 1] / mtrExt[n, n];
                //// Реализация обратного хода Гауса
                //for (int i = n - 1; i > -1; i--)
                //{
                //    for (int j = 1; j < n - i + 1; j++)
                //    {
                //        mtrExt[i, n + 1] -= mtrSol[i + j] * mtrExt[i, i + j];
                //    }
                //    mtrSol[i] = mtrExt[i, n + 1] / mtrExt[i, i];
                //}
                //Console.Write("\nРешение: ");
                //foreach (var item in mtrSol)
                //{
                //    Console.Write(Math.Round(item, 4) + "  ");
                //}

                Console.ReadKey();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
