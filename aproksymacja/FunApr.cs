using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aproksymacja
{
    class FunApr<T>
    {
        private T distance;
        private T height;

        public T Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
            }
        }

        public T Distance
        {
            get
            {
                return distance;
            }

            set
            {
                distance = value;
            }
        }
    }
}
