using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aproksymacja
{
    class Matrix<T>
    {
        private T value;
        private double[][] tab;
        int amount;

        public Matrix(int amount)
        {
            this.amount = amount;
            this.tab = new double[amount][];
            for (int i = 0; i < amount; i++)
            {
                tab[i] = new double[amount];
            }
        }

        public T Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }

        public int Amount
        {
            get
            {
                return amount;
            }

            set
            {
                amount = value;
            }
        }

        private void setMatrix(FunApr<double>[] funApr, double[] F, double[] h)
        {
            //setting  h and F 
            for (int i = amount - 1; i > 0; i--)
            {
                F[i] = (funApr[i].Height - funApr[i - 1].Height) / (funApr[i].Distance - funApr[i - 1].Distance);
                h[i - 1] = funApr[i].Distance - funApr[i - 1].Distance;
            }

            // setting matrix
            for (int i = 1; i < amount - 1; i++)
            {
                tab[i][i] = 2 * (h[i - 1] + h[i]);
                if (i != 1)
                {
                    tab[i][i - 1] = h[i - 1];
                    tab[i - 1][i] = h[i - 1];
                }
                tab[i][amount - 1] = 6 * (F[i + 1] - F[i]);
            }
        }

        public void gauss(FunApr<double>[] funApr, double[] spline, double[] F, double[] h)
        {
            setMatrix(funApr, F, h);

            //forward elimination 
            for (int i = 1; i < amount - 2; i++)
            {
                double temp = (tab[i + 1][i] / tab[i][i]);
                for (int j = 1; j <= amount - 1; j++)
                    tab[i + 1][j] -= temp * tab[i][j];
            }

           // back ward substitution
            for (int i = amount - 2; i > 0; i--)
            {
                double sum = 0;
                for (int j = i; j <= amount - 2; j++)
                    sum += tab[i][j] * spline[j];
                spline[i] = (tab[i][amount - 1] - sum) / tab[i][i];
            }
        }
    }
}
