﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace LAB10_EN
{
    class Program
    {
        static void Main(string[] args)
        {
            //Stage 1. - 1 point
            {
                Console.WriteLine("STAGE 1");
                List<int> ints = new List<int> { 1, 23, -56, 4, -563, 1241, -1, 0 };
                Console.Write("List: ");
                PrintList(ints);
                //Use lambda function here
                ints.Sort((int a, int b) =>
                {
                    if(a < 0)
                    {
                        if (b >= 0)
                        {
                            return 1;
                        }
                        else
                        {
                            return b-a;
                        }
                    }
                    else
                    {
                        if (b < 0)
                        {
                            return -1;
                        }
                        else
                        {
                            return a - b;
                        }
                    }
                });
                Console.Write("Sorted list: ");
                PrintList(ints);
                Console.WriteLine("\n\n");

                int diff = 0;
                //Use lambda function here
                ints.RemoveAll((int x) =>
                {
                    if (x < 0)
                    {
                        diff -= x;
                        return true;
                    }

                    return false;
                });
                Console.Write("List: ");
                PrintList(ints);
                Console.WriteLine($"Diff: {diff}");
            }

            //Stage 2. 1 - point
            {
                Console.WriteLine("STAGE 2");
                Console.WriteLine("Constant function");
                //Create constant function which returns 13
                var funC = BaseFunctions.ConstantFunction(13);
             for (int i = 13; i < 27; i++)
                {
                    Console.WriteLine((i, funC(i)));
                }

                 Console.WriteLine("\n\n");

                //Create quadratic function with a=13, b=-5 and c=1
                Console.WriteLine("Quadratic function");
                var funQ = BaseFunctions.QuadraticFunction(13, -5, 1);
                 for (int i = 0; i <= 10; i++)
                {
                    Console.WriteLine((i, funQ(i)));
                }

                Console.WriteLine("\n\n");

                //Create polynomial with following coefficients 8,-2,4,9,2,3
                Console.WriteLine("Polynomial function");
                var funP = BaseFunctions.PolynomialFunction(8, -2, 4, 9, 2, 3);
                 for (int i = 0; i <= 10; i++)
                {
                    Console.WriteLine((i, funP(i)));
                }

                Console.WriteLine("\n\n");
            }

            //Stage 3. - 1 point
            {
                Console.WriteLine("STAGE 3");
                Console.WriteLine("Max function");
                var f = BaseFunctions.ConstantFunction(-3);
                var g = BaseFunctions.QuadraticFunction(0, 1, -2);
                var funM = FunctionsManipulator.NewPoint(f, g);
                for (int i = -5; i <= 5; i++)
                {
                    Console.WriteLine((i, funM(i)));
                }

                Console.WriteLine("\n\n");

                Console.WriteLine("Difference function");
                f = BaseFunctions.ConstantFunction(5);
                g = BaseFunctions.QuadraticFunction(1, 0, -4);
                var funD = FunctionsManipulator.Power(f, g);
                for (int i = -5; i <= 5; i++)
                {
                    Console.WriteLine((i, funD(i)));
                }

                Console.WriteLine("\n\n");

                Console.WriteLine("Combine functions");
                f = BaseFunctions.QuadraticFunction(1, 0, 0);
                g = BaseFunctions.QuadraticFunction(0, 1, -4);
                var funC = FunctionsManipulator.CombineFunctions(f, g);
                for (int i = -5; i <= 5; i++)
                {
                    Console.WriteLine((i, funC(i)));
                }

                Console.WriteLine("\n\n");
            }

            //Stage 4. - 2 points
            {
                Console.WriteLine("STAGE 4");
                Random random = new Random(0);


                Console.WriteLine("ForEachWithBrake extension");
                var floats = new List<double>()
                 {
                 12.98, 12.3, -5.59, -13, 12.1212, 23.85, 31.68, 12.92, 345.45, 16.1683, 10, 49.86, 2000
                 };
                PrintList(floats);
                //Complete
                floats.ForEachWithBrake((double x) =>
                {
                    double res = Math.Abs(x % 1.0);
                    Console.WriteLine(res);
                }, (double x) =>
                {
                    return x < 27 && x > -15;
                });
                Console.WriteLine("\n\n");


                Console.WriteLine("Distinct extension");
                var doubles = new List<double>()
                 {
                 3, MathF.PI, 3.98, 12, 12, 9, 14.5, 13.9, 12.2121, 129, 3429, 9.99999, 10
                 };
                PrintList(doubles);
                //Complete
                var distinctDoubles = doubles.Distinct(Comparer<double>.Create( (double x, double y) =>
                {
                    double x0 = Math.Floor(x);
                    double y0 = Math.Floor(y);
                    if (x0 < y0)
                    {
                        return -1;
                    }else if (x0 == y0)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }));
                PrintList(distinctDoubles);


                Console.WriteLine("SortRange extension");
                List<int> numbers = new List<int>()
                 {
                 1, 2, 3, 4, 5, 6, 12, 8, 13, 16, 11, 7, 14, 13, 9, 15
                 };
                PrintList(numbers);
                //Complete
                numbers.SortRange(^10..15, Comparer<int>.Default);
                PrintList(numbers);
            }
        }

        private static void PrintList<T>(List<T> ints)
        {
            foreach (var i in ints)
            {
                Console.Write(i + ", ");
            }

            Console.WriteLine();
        }
    }
}
