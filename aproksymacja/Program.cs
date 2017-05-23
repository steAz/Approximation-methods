using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Program do obliczenia wysokości punktów pośrednich profilu wysokościowego
//znając tylko część z nich, wykorzystujący metody:
//interpolacji wielomianowej Lagrange'a i funkcji sklejanych 3 stopnia
//~MICHAŁ KAZANOWSKI 160512, 14.05.2017

namespace aproksymacja
{
    class Program
    {
        static void Main(string[] args)
        {
            Fixer fixer = new Fixer();
        }
    }
}
