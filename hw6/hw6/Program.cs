using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw6
{
    class Program
    {
        static void Main(string[] args)
        {
            option a = new option();  //define an option class variable
            Console.WriteLine("input stockprice");  //input stockprice value
            double stockprice=Convert.ToDouble( Console.ReadLine());
            Console.WriteLine("input strike");  //input strike value
            double strike = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("input interest rate"); //input interst rate value
            double rate = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("input volitality");   //input volitality value
            double vol = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("input time");  //input tenor value
            double time = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("input interest rate difference");  //input diff interest rate
            double deltar = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("input dividend of stock");   //input dividend of stock
            double dividend = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("input volitality difference");  //input diff volitality 
            double deltavol = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("input number of process");  //input number of process in trinomial tree
            int num = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("input option type(aco  -american call option,\r\n apo -american put option," +
                "\r\n eco  -european call option, \r\n epo -european put option)");
            string type = Console.ReadLine();  //input type of option
            //use method in option class to get value of strike,stockprice, deltar,deltavol,num,time,rate,vol,dividend
            a.Strike = strike;
            a.Stockprice = stockprice;
            a.Deltar = deltar;
            a.Deltavol = deltavol;
            a.Num = num;
            a.Time = time;
            a.Vol = vol;
            a.Rate = rate;
            a.Dividend = dividend;
            double[] optionresult=new double[6];
            switch(type) //determine the type of option
            {
                case "eco":
                    optionresult = a.outeco();
                    break;
                case "epo":
                    optionresult = a.outepo();
                    break;
                case "aco":
                    optionresult = a.outaco();
                    break;
                case "apo":
                    optionresult = a.outapo();
                    break;
                 default:
                    Console.WriteLine("wrong option type!");
                    break;
            }
            //output the optionprice and greek
            Console.WriteLine("optionprice:" + optionresult[0] +"\r\n delta:" + optionresult[1]+"\r\n gamma:"+optionresult[2]
                +"\r\n theta:"+optionresult[3]+"\r\n vega: "+optionresult[4]+"\r\n rho:"+optionresult[5]);
            Console.ReadLine();

        }
       
    }
}
