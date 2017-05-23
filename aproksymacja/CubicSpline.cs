using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aproksymacja
{
    class CubicSpline<T>
    {
        private T[] spline;
        private T[] h;
        private T[] F;
        private T a;
        private T b;
        private T c;
        private T d;

        public CubicSpline(int amount)
        {
            spline = new T[amount];
            F = new T[amount];
            h = new T[amount];
        }

        public T[] Spline
        {
            get
            {
                return spline;
            }

            set
            {
                this.spline = value;
            }
        }

        public T[] H
        {
            get
            {
                return h;
            }

            set
            {
                this.h = value;
            }
        }

        public T[] Fun
        {
            get
            {
                return F;
            }

            set
            {
                this.F = value;
            }
        }

        public T A
        {
            get
            {
                return a;
            }

            set
            {
                this.a = value;
            }
        }

        public T B
        {
            get
            {
                return b;
            }

            set
            {
                this.b = value;
            }
        }

        public T C
        {
            get
            {
                return c;
            }

            set
            {
                this.c = value;
            }
        }

        public T D
        {
            get
            {
                return d;
            }

            set
            {
                this.d = value;
            }
        }
    }
}
