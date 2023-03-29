using System;
using System.Collections.Generic;
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
                    this[i, j] = z[i, j];
                }
            }
        }

        public Matrix(double[,] z)
        {
            m_ = z.GetLength(0);
            n_ = z.GetLength(1);
            data = new double[m_, n_];
            for (int i = 0; i < m_; ++i)
            {
                for (int j = 0; j < n_; ++j)
                {
                    this[i, j] = z[i, j];
                }
            }
        }
        
        public double this[System.Index i, System.Index j]
        {
            get { return data[i.Value, j.Value]; }
            set { data[i.Value, j.Value] = value; }
        }

        public Matrix this[System.Range i_range, System.Range j_range]
        {
            get {
                System.Console.WriteLine(i_range.ToString(), j_range.ToString());

                //if (!(i_range.Start.Value>=0 && i_range.End.Value<M) || !(j_range.Start.Value>=0 && j_range.End.Value<N))
                //{
                //     throw new IndexOutOfRangeException();
                //}

                //System.Console.WriteLine(i_range.ToString(), j_range.ToString());

                var (i_start, i_length) = i_range.GetOffsetAndLength(M);
                var (j_start, j_length) = j_range.GetOffsetAndLength(N);


                Matrix sub_mat = new Matrix(i_length, j_length);

                for (var i = i_start; i < i_start + i_length; ++i)
                {
                    for (var j = j_start; j < j_start + j_length; ++j)
                    {
                        sub_mat[i - i_start, j - j_start] = this[i, j];
                    }
                }

                return sub_mat;

            }
            set
            {
                var (i_start, i_length) = i_range.GetOffsetAndLength(M);
                var (j_start, j_length) = j_range.GetOffsetAndLength(N);

                for (var i = i_start; i < i_start + i_length; ++i)
                {
                    for (var j = j_start; j < j_start + j_length; ++j)
                    {
                        this[i, j] = value[i - i_start, j - j_start];
                    }
                }
            }
        }

        public Matrix this[System.Range i_range, System.Index j]
        {
            get
            {
                var (i_start, i_length) = i_range.GetOffsetAndLength(M);


                Matrix sub_mat = new Matrix(i_length, 1);

                for (var i = i_start; i < i_start + i_length; ++i)
                {
                    
                        sub_mat[i - i_start, 0] = this[i, j];
                  
                }

                return sub_mat;
            }
            set
            {
                var (i_start, i_length) = i_range.GetOffsetAndLength(M);



                for (var i = i_start; i < i_start + i_length; ++i)
                {

                    //sub_mat[i - i_start, 0] = this[i, j];
                    this[i, j] = value[i - i_start, 0];

                }


            }
        }


        public Matrix this[System.Index i, System.Range j_range]
        {
            get
            {
                var (j_start, j_length) = j_range.GetOffsetAndLength(M);


                Matrix sub_mat = new Matrix(1, j_length);

                for (var j = j_start; j < j_start + j_length; ++j)
                {

                    sub_mat[ 0, j-j_start] = this[i, j];

                }

                return sub_mat;
            }
            set
            {
                var (j_start, j_length) = j_range.GetOffsetAndLength(N);


                Matrix sub_mat = new Matrix(1, j_length);

                for (var j = j_start; j < j_start + j_length; ++j)
                {

                    this[i, j] = value[i, 0] ;
                }

            }
        }


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
        
    }
}