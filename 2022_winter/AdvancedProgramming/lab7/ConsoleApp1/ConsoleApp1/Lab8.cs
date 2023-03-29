using System.Collections;

namespace Lab8_EN;

interface IFormula
{
    double Calculate(double x) { return x; }
    string PrintFormula() { return "x"; }
}

class DefaultFormula: IFormula { }

class ArithmeticSequenceSumFormula : IFormula
{
    private double a1, d;
    public double A1
    {
        get { return a1; }
        set { a1 = value; }
    }
    public double D
    {
        get { return d; }
        set { d = value; }
    }

    public ArithmeticSequenceSumFormula(double A1, double D)
    {
        this.a1 = A1;
        this.d = D;
    }

    public double Calculate(double x)
    {
        //sum of n: n*(A1+a_n)/2)
        double s = 0;
        double a_n = A1 + D * (x - 1);
        return x * (A1 + a_n) / 2.0;
    }

    public string PrintFormula()
    {
        return $"f(n)=n*({a1}+a_n)/2";
    }
}

class GeometricSequenceSumFormula: IFormula
{
    private double a1, r;
    public double A1
    {
        get { return a1; }
        set { a1 = value; }
    }
    public double R
    {
        get { return r; }
        set { r = value; }
    }

    public GeometricSequenceSumFormula(double a1, double r)
    {
        this.a1 = a1;
        this.r = r;
    }

    public double Calculate(double x)
    {
        return A1 * (1 - Math.Pow(R, x)) / (1 - R);
    }

    public string PrintFormula()
    {
        return $"f(n)={A1}(1-{R}^n)/(1-{R})";
    }
}


abstract class Generator : IEnumerable
{
    protected IFormula formula;

    public Generator(IFormula? formula=null)
    {
        if (formula == null)
        {
            this.formula = new DefaultFormula();
        }
        else
        {
            this.formula = formula;
        }
    }

    public string Formula
    {
        get { return formula.PrintFormula(); }
    }

    abstract public IEnumerator GetEnumerator();
}

class WeirdFibonacciGenerator : Generator
{
    public WeirdFibonacciGenerator(IFormula? formula=null) : base(formula) { }
    public override IEnumerator GetEnumerator()
    {
        double a = 2, b = 1, c = 3;
        for (int i = 0; ; i++)
        {
            if (i == 0) { yield return formula.Calculate(a); }
            else if (i == 1) { yield return formula.Calculate(b); }
            else if (i == 2) { yield return formula.Calculate(c); }
            else
            {
                double tmp = a * 2 + b * 3 + c;
                a = b;
                b = c;
                c = tmp;

                yield return formula.Calculate(tmp);

            }
        }
    }
}

class FibonacciGenerator : Generator
{
    public FibonacciGenerator(IFormula? formula=null) : base(formula) { }
    public override IEnumerator GetEnumerator()
    {
        double a = 0, b = 1;
        for(int i = 0; ; i++)
        {
            if (i == 0) { yield return formula.Calculate(a); }
            else if (i == 1) { yield return formula.Calculate(b); }
            else
            {
                double tmp = a + b;
                a = b;
                b = tmp;

                yield return formula.Calculate(tmp);

            }
        }
    }
}