using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw5
{
    class Program
    {
        static void Main(string[] args)   //main function to ouptput result
        {
            Console.WriteLine("fisrt method:"+straight()); //output radom number using straightforward way
            double[] a = boxmuller();        //output random number using boxmuller method
            double[] b = polarrejection();    //output random number using polar rejection method
            Console.WriteLine("box-muller method:");
            for(int i=0;i<a.Length;i++)
            {
                Console.WriteLine(a[i]);
            }
            Console.WriteLine("polar rejection method:");
            for (int i = 0; i < b.Length; i++)
            {
                Console.WriteLine(b[i]);
            }
            Console.WriteLine("input a correlation value:");  //let user to choose the correlation value
            string rho=Console.ReadLine();
            double[] c= jointnormal(Convert.ToDouble(rho));   //produce jointly normal random number 
            Console.WriteLine("first method for jointly normal:");  
            Console.WriteLine(c[0]); Console.WriteLine(c[1]);  //output using the straightforward method
            Console.WriteLine("box-muller method for jointly normal:");
            Console.WriteLine(c[2]); Console.WriteLine(c[3]);  //output using box-muller method
            Console.WriteLine("polar rejection method for jointly normal:");
            Console.WriteLine(c[4]); Console.WriteLine(c[5]); //output using polar rejection method
            Console.ReadLine();

        }
        static int GetRandomSeed()  //define a method to avoid producing same random number caused by short time interval  
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
        static double straight()   //the straightforward method
        {
            double randn; Random rnd = new Random(GetRandomSeed());
            double a = 0;
            for (int i = 1; i <= 12; i++)
            {
                randn = rnd.NextDouble();   //produce 12 uniformed random numbers in [0,1]
                a = randn + a;               //add numbers together
            }
            a = a - 6;    //subtract 6 to produce standard normal random number.
            return a;
        }
        static double[] boxmuller()   //box-muller method to produce two numbers
        {
            double randn1;
            double randn2;
            Random rnd = new Random(GetRandomSeed());
            //produce two uniformed random variable
            randn1 = rnd.NextDouble();
            randn2= rnd.NextDouble();
            //using boxmuller method to produce numbers in standard normal distribution
            double z1 = Math.Sqrt(-2 * Math.Log(randn1)) * Math.Cos(2 * Math.PI * randn2);
            double z2 = Math.Sqrt(-2 * Math.Log(randn1)) * Math.Sin(2 * Math.PI * randn2);
            double[] result1 = { z1, z2 };
            return result1;
        }
        static double[] polarrejection()  //using polarrejection method
        {
            Random rnd = new Random(GetRandomSeed());  //produce two uniformed random in [0,1]
            double randn1 = rnd.NextDouble();
            double randn2 = rnd.NextDouble();
            randn1 = 2 * randn1 - 1;    //expand the interval from [0,1] to [-1,1]
            randn2 = 2 * randn2 - 1;    
            double w = Math.Pow(randn1, 2) + Math.Pow(randn2, 2);  
            while (w>1)     //to satisfy the condition w<1
            {
                randn1 = rnd.NextDouble();
                randn2= rnd.NextDouble();
                randn1 = 2 * randn1 - 1;
                randn2 = 2 * randn2 - 1;
                w = Math.Pow(randn1, 2) + Math.Pow(randn2, 2);
            }
            double z1=randn1*Math.Sqrt(-2 * Math.Log(w) / w);
            double z2 = randn2 * Math.Sqrt(-2 * Math.Log(w) / w);
            double[] a = { z1, z2 };
            return a;
        }

        static double[] jointnormal(double rho)    //produce random jonitly normal numbers
        {
            double a1 = straight();
            double a2 = straight();
            double[] b = boxmuller();
            double[] c = polarrejection();
            double z1, z2, z3, z4, z5, z6;      
            z1 = a1;z2 = rho * a1 + Math.Sqrt(1 - rho * rho) * a2; //produce jointly normal random numbers using straightforward method
            z3 = b[0]; z4= rho * b[0] + Math.Sqrt(1 - rho * rho) * b[1];//produce jointly normal random numbers using box-muller method
            z5 = c[0]; z6 = rho * c[0] + Math.Sqrt(1 - rho * rho) * c[1]; //produce jointly normal random numbers using polar rejection method
            double[] result = { z1, z2, z3, z4, z5, z6 };
            return result;
        }
}
    }
    

