using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{

    class randomnumber   //define a class to generate randomnumber
    {
        static int GetRandomSeed()  //define a method to avoid producing same random number caused by short time interval  
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
        static double boxmuller()   //box-muller method to produce two numbers
        {
            double randn1;
            double randn2;
            Random rnd = new Random(GetRandomSeed());
            //produce two uniformed random variable
            randn1 = rnd.NextDouble();
            randn2 = rnd.NextDouble();
            //using boxmuller method to produce numbers in standard normal distribution
            double z1 = Math.Sqrt(-2 * Math.Log(randn1)) * Math.Cos(2 * Math.PI * randn2);
            double z2 = Math.Sqrt(-2 * Math.Log(randn1)) * Math.Sin(2 * Math.PI * randn2);
            double[] result1 = { z1, z2 };
            return result1[0];
        }
        public double[,] randommatrixgenerator(int step, int trial) //create a random matrix, num of rows is trial and num of cols is step
        {
            double[,] matrix = new double[trial, step];
            for (int i = 0; i < trial; i++)
            {
                for (int j = 0; j < step; j++)
                {
                    matrix[i, j] = boxmuller();
                }
            }
            rerturn matrix;
        }
        public double[,] antitheticgenerator(int step, int trial)
        {
            if (trial % 2 == 0)
            {
                trial1 = 0.5 * trial;
                double[,] matrix = new double[2 * trial1, step];
                for (int i = 0; i < trial1; i++)
                {
                    for (int j = 0; j < step; j++)
                    {
                        matrix[i, j] = boxmuller();
                        matrix[i + trial1, j] = -matrix[i, j];
                    }
                }
            }
            else
            {
                double matrix = new double[trial, step];
                for (int i = 0; i < 0.5 * (trial - 1); i++)
                {
                    for (int j = 0; j < step; j++)
                    {
                        matrix[i, j] = boxmuller();
                        matrix[i + 0.5 * (trial + 1), j] = -matrix[i, j];
                    }
                }
                for (int j = 0; j < step; j++)
                    matrix[0.5 * (trial - 1), j] = boxmuller();
            }
            return matrix;
        }
    }
}
