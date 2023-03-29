using System;
using System.Collections;
using System.Collections.Generic;

namespace LAB10_EN
{
    //Stage 2. - 1 points
    public static class BaseFunctions
    {
        public static Func<double, double> ConstantFunction(double d)
        {
            double res_func(double x)
            {
                return d;
            }
            return res_func;
        }
        public static Func<double, double> QuadraticFunction(double a, double b, double c)
        {
            double res_func(double x)
            {
                return a * x * x + b * x + c;
            }
            return res_func;
        }
        public static Func<double, double> PolynomialFunction(params double[] tab)
        {
            double res_func(double x)
            {
                double result=0.0;
                for(int i=0; i<tab.Length; ++i)
                {
                    result *= x;
                    result += tab[i];
                    
                }
                return result;
            }
            return res_func;
        }

    }
    
    //Stage 3. - 1 point
    public static class FunctionsManipulator
    {
        public static Func<double, (double, double)> NewPoint(Func<double, double>f, Func<double, double> g)
        {
            return (double x) => (f(x), g(x));
        }
        public static Func<double, double> Power(Func<double, double> f, Func<double, double> g)
        {
            return (double x) => Math.Pow(f(x), g(x));
        }
        public static Func<double, double> CombineFunctions(Func<double, double> f, Func<double, double> g)
        {
            return (double x) => f(g(x));
        }

    }
    
    //Stage 4. - 2 points
    public static class ExtensionMethods
    {
        public static void ForEachWithBrake<T>(this IEnumerable<T> obj, Action<T> act, Predicate<T> pred) {
            foreach(T element in obj){
                if (!pred(element))
                {
                    return;
                }
                act(element);
                
            }
        }

        public static List<T> Distinct<T>(this IEnumerable<T> obj, IComparer<T> comp)
        {
            List<T> result = new List<T>();
            foreach(T x in obj)
            {
                bool exist = false;
                foreach(T y in result)
                {
                    if(comp.Compare(x, y) == 0)
                    {
                        exist = true;
                        break;
                    }
                }
                if (!exist)
                {
                    result.Add(x);
                }
            }
            return result;
        }

        public static void SortRange<T>(this List<T> obj, System.Range range, IComparer<T> comp)
        {
            int begin = range.Start.IsFromEnd ? (obj.Count - range.Start.Value) : range.Start.Value;
            int end = range.End.IsFromEnd ? (obj.Count - range.End.Value) : range.End.Value;
            List<T> sublist = obj.GetRange(begin, end - begin);
            sublist.Sort(comp);
            for(int i=begin; i < end; ++i)
            {
                obj[i] = sublist[i - begin];
            }

        }

    }
}