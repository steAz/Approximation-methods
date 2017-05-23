using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aproksymacja
{
    class Fixer
    {
        FunApr<double>[] funAprMain;
        Matrix<double> matrix;
        CubicSpline<double> cubicSpline;
        static int amountOfSmaller = 128;

        public Fixer()
        {
            string path = @"f:\Informatyka\trasaZjednymWzniesieniem.txt";
            StreamReader sr = new StreamReader(path);
            funAprMain = new FunApr<double>[512];
            init(funAprMain);

            try
            {
                string ourStr;
                int i = 0;
                while ((ourStr = sr.ReadLine()) != null) /*Dystans(m),Wysokość(m)*/
                {
                    string[] disHeight = ourStr.Split(' ');
                    funAprMain[i].Distance = Double.Parse(disHeight[0]);
                    funAprMain[i].Height = Double.Parse(disHeight[1]);
                    i++;
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not read the file");
            }

            matrix = new Matrix<double>(512 / amountOfSmaller + 1);
            FunApr<double>[] funAprSmaller = new FunApr<double>[512 / amountOfSmaller + 1];
            init(funAprSmaller);
            dividePoints(funAprSmaller);

            cubicSpline = new CubicSpline<double>(512 / amountOfSmaller + 1);
            matrix.gauss(funAprSmaller, cubicSpline.Spline, cubicSpline.Fun, cubicSpline.H);

            FunApr<double>[] funApr = new FunApr<double>[512];
            init(funApr); // tutaj bedziemy mieli nowe wyniki

            laGrangeMethod(funApr, funAprSmaller);
            string path2 = @"f:\Informatyka\LagrangeCo128wezelTrasaZjednymWzniesieniem.txt";
            StreamWriter sw = new StreamWriter(path2);

            for (int j = 0; j < funApr.Length; j++)
            {
                double wartosc = (Math.Abs(funAprMain[j].Height - funApr[j].Height)) / funAprMain[j].Height;
                 sw.WriteLine(wartosc.ToString());
            }
            sw.Close();

            for (int j = 0; j < funApr.Length; j++)
            {
                Console.WriteLine("{0}     {1}", funApr[j].Distance, funApr[j].Height);
            }

            Console.Read();
        }

        void dividePoints(FunApr<double>[] funAprSmaller)
        {
            for (int i = 0; i < funAprSmaller.Length - 1; i++)
            {
                funAprSmaller[i] = funAprMain[i * amountOfSmaller];
            }

            funAprSmaller[funAprSmaller.Length - 1] = funAprMain[funAprMain.Length - 1];
        }

        void init(FunApr<double>[] funApr)
        {
            for (int j = 0; j < funApr.Length; j++)
            {
                funApr[j] = new FunApr<double>();
            }
        }

        void laGrangeMethod(FunApr<double>[] funApr, FunApr<double>[] funAprSmaller)
        {
            for (int i = 0; i < funApr.Length; i++)
            {
                funApr[i].Distance = funAprMain[i].Distance;

                for (int k = 0; k < funAprSmaller.Length; k++)
                {
                    double iloczyn = 1;
                    for (int j = 0; j < funAprSmaller.Length; j++)
                    {
                        if (j != k)
                            iloczyn *= (funApr[i].Distance - funAprSmaller[j].Distance) / (funAprSmaller[k].Distance - funAprSmaller[j].Distance);
                    }

                    funApr[i].Height += (funAprSmaller[k].Height * iloczyn);
                }
            }
        }

        void cubicSplineMethod(FunApr<double>[] funApr, FunApr<double>[] funAprSmaller)
        {
            for (int j = 0; j < funApr.Length; j++)
            {
                funApr[j].Distance = funAprMain[j].Distance;

                for (int i = 0; i < funAprSmaller.Length - 1; i++)
                {
                    if(funAprSmaller[i].Distance <= funApr[j].Distance && funApr[j].Distance <= funAprSmaller[i + 1].Distance)
                    {
                        cubicSpline.A = (cubicSpline.Spline[i + 1] - cubicSpline.Spline[i]) / (6 * cubicSpline.H[i]);
                        cubicSpline.B = cubicSpline.Spline[i] / 2;
                        cubicSpline.C = ((funAprSmaller[i + 1].Height - funAprSmaller[i].Height) / cubicSpline.H[i]) - ((2 * cubicSpline.H[i] * cubicSpline.Spline[i] + cubicSpline.Spline[i + 1] * cubicSpline.H[i]) / 6);
                        cubicSpline.D = funAprSmaller[i].Height;
                        funApr[j].Height = (cubicSpline.A * Math.Pow((funApr[j].Distance - funAprSmaller[i].Distance), 3)) + (cubicSpline.B * Math.Pow((funApr[j].Distance - funAprSmaller[i].Distance), 2)) + (cubicSpline.C * (funApr[j].Distance - funAprSmaller[i].Distance)) + cubicSpline.D;
                        break;
                    }
                }
            }
        }
    }
}


