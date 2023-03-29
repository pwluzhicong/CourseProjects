using System;
using System.Text;

namespace Lab07
{
    /**
     * Class representing matrix mxn, ie. m-rows and n-columns
     */
    public class Matrix
    {

        private double[,] data;
        private int m_;
        private int n_;

        public Matrix(int m, int n)
        {
            m_ = m;
            n_ = n;
            data = new double[m, n];
        }

        public int M
        {
            get { return m_; }

        }

        public int N
        {
            get { return n_; }
        }

        public Matrix(Matrix z)
        {
            m_ = z.M;
            n_ = z.N;
            data = new double[z.M, z.N];
            for(int i=0; i<z.M; ++i)
            {
                for(int j=0; j < z.N; ++j)
                {
                    data[i, j] = z.data[i, j];
                }
            }
        }
        
        public double this[System.Index x]
        {
            get { }
        }

        /*
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < M; i++)
            {
                builder.Append('|');
                for (int j = 0; j < N; j++)
                {
                    builder.Append($" {this[i, j],5:0.0} ");
                }
                builder.Append('|');
                if (i != M - 1) builder.Append('\n');
            }

            return builder.ToString();
        }
        */
    }
}