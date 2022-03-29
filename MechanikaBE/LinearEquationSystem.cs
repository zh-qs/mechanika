using System;
using MathNet.Numerics.LinearAlgebra;

namespace Mechanika
{

    public class LinearEquationSystem
    {
        double[,] A;
        double[] b;
        int m, n;
        public LinearEquationSystem(int m, int n)
        {
            this.n = n;
            this.m = m;
            A = new double[m,n];
            b = new double[m];
            for (int i = 0; i < m; ++i) b[i] = 0;
        }
        public void SetAB(int i, double[,] vals)
        {
            if (i >= 1 && i + vals.GetUpperBound(0) <= m && vals.GetUpperBound(1) == n)
                for (int iv = 0; iv <= vals.GetUpperBound(0); ++iv)
                {
                    for (int jv = 0; jv < n; ++jv)
                        A[i + iv - 1, jv] = vals[iv, jv];
                    AddB(iv + i, vals[iv, n]);
                }
            else throw new ArgumentOutOfRangeException($"{i}");
        }
        public void SetA(int i, int j, double val)
        {
            if (i >= 1 && i <= m && j >= 1 && j <= n) A[i - 1, j - 1] = val;
            else throw new ArgumentOutOfRangeException($"{i}, {j}");
        }
        public void SetARange(int i, int jstart, int jend, double val)
        {
            if (i >= 1 && i <= m && jstart >= 1 && jend < n)
                for (int j = jstart; j < jend; ++j)
                    A[i - 1, j - 1] = val;
            else throw new ArgumentOutOfRangeException($"{i}, {jstart}, {jend}");
        }
        public void AddB(int i, double val)
        {
            if (i >= 1 && i <= m) b[i - 1] += val;
            else throw new ArgumentOutOfRangeException($"{i}");
        }

        public double[] SolveQR()
        {
            Vector<double> x;

            var AA = Matrix<double>.Build.DenseOfArray(A);
            var bb = Vector<double>.Build.Dense(b);
            x = AA.Solve(bb);

            return x.ToArray();
        }
        public double[] Solve()
        {
            return SolveQR();
        }
    }
}
