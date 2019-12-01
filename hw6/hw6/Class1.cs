using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw6
{
    class option
    {
        private double strike, stockprice, rate, vol, dividend, time,deltavol,deltar;int num;
        public double Strike { get { return strike; } set { strike = value; } }  //get strike value
        public double Deltar { get { return deltar; } set { deltar = value; } }  //get the diff interest rate
        public double Deltavol { get { return deltavol; } set { deltavol = value; } }  //get the diff volitality
        public double Stockprice { get { return stockprice; } set { stockprice = value; } } //get stockprice value
        public double Rate { get { return rate; } set { rate = value; } } //get interest rate value
        public double Vol { get { return vol; } set { vol = value; } }  //get volitality value
        public double Dividend { get { return dividend; } set { dividend = value; } }  //get dividend value
        public int Num { get { return num; } set { num = value; } }    //get number of process
        public double Time { get { return time; } set { time = value; } }   //get tenor value
        public static double[] prob(double strike, double stockprice, double rate, double vol, double dividend, int num, double time) 
            //function to get risk-neutral measure
        { double v = rate - dividend - 0.5 * vol * vol; double deltat = time / num;
            double deltax = vol * Math.Sqrt(3 * deltat );
            //compute upper probability pu
            double pu = 0.5 * ((vol * vol * deltat + v * v * deltat * deltat) / (deltax * deltax) + v * deltat / deltax); 
            //compute medium probability pm
            double pm = 1 - (vol * vol * deltat + v * v * deltat * deltat) / (deltax * deltax);
            //compute lower probability pd
            double pd = 0.5 * ((vol * vol * deltat + v * v * deltat * deltat) / (deltax * deltax) - v * deltat / deltax);
            double[] p = { pu, pm, pd };
            return p;
        }
        
       //method to get european call option value , delta, gamma, theta
        public static double[] eco(double strike, double stockprice, double rate, double vol, double dividend, int num, double time)
        {
            double deltat = time / num;
            //compute stockprice's increase factor u or decrease factor d
            double u = Math.Exp(vol * Math.Sqrt(3 * time / num));
            double d = Math.Exp(-vol * Math.Sqrt(3 * time / num));
            //use array to save option price
            double[] optlist1 = new double[2 * num + 1];
            double[] optlist2 = new double[2 * num + 1];
            double[] optlist3 = new double[3];
            //caculate the risk-neutral measure
            double[] problist = prob(strike, stockprice, rate, vol, dividend, num, time);
            double pu = problist[0];
            double pm = problist[1];
            double pd = problist[2];
            //get option prices at T
            for (int i = 0; i < 2 * num + 1; i++)
            {
                optlist1[i] = Math.Max(0, Math.Pow(u, (num - i))*stockprice-strike);
            }

            for (int j = 1; j < num - 1; j++)     //caculate backward to the last three process
            {
                optlist1.CopyTo(optlist2, 0);
                for (int i = 0; i < 2 * num + 1 - 2 * j; i++)
                {
                    optlist1[i] = Math.Exp(-rate * deltat) * (pu * optlist2[i] + pm * optlist2[i + 1] + pd * optlist2[i + 2]);
                }
            }
            //caculate the process before last step to get c10,c11,c12;
            for (int i = 0; i < 3; i++)
            {
                optlist3[i] = Math.Exp(-rate * deltat) * (pu * optlist1[i] + pm * optlist1[i + 1] + pd * optlist1[i + 2]);
            }
            //get the option price at time 0
            double optprice = Math.Exp(-rate * deltat) * (pu * optlist3[0] + pm * optlist3[1] + pd * optlist3[2]);
            //get delta,gamma,theta
            double delta = (optlist3[0] - optlist3[2]) / (u * stockprice - d * stockprice);
            double gamma = ((optlist3[0] - optlist3[1]) / (u * stockprice - stockprice) - (optlist3[1] - optlist3[2]) / (stockprice - d * stockprice)) / (0.5 * u * stockprice - 0.5 * d * stockprice);
            double theta = (optlist3[1] - optprice) / deltat;
            double[] result = { optprice, delta, gamma, theta };
            return result;
        }
        //get european put option, delta,gamma,theta
        public static double[] epo(double strike, double stockprice, double rate, double vol, double dividend, int num, double time)
        {
            double deltat = time / num;
            //compute stockprice's increase factor u or decrease factor d
            double u = Math.Exp(vol * Math.Sqrt(3 * deltat));
            double d = Math.Exp(-vol * Math.Sqrt(3 * deltat));
            //use array to save option price
            double[] optlist1 = new double[2 * num + 1];
            double[] optlist2 = new double[2 * num + 1];
            double[] optlist3 = new double[3];
            //caculate the risk-neutral measure
            double[] problist = prob(strike, stockprice, rate, vol, dividend, num, time);
            double pu = problist[0];
            double pm = problist[1];
            double pd = problist[2];
            //get option price at T
            for (int i = 0; i < 2 * num + 1; i++)
            {
                optlist1[i] = Math.Max(0, -Math.Pow(u, (num - i))*stockprice +strike);
            }

            for (int j = 1; j < num - 1; j++)     //caculate to the last three process
            {
                optlist1.CopyTo(optlist2, 0);
                for (int i = 0; i < 2 * num + 1 - 2 * j; i++)
                {
                    optlist1[i] = Math.Exp(-rate * deltat) * (pu * optlist2[i] + pm * optlist2[i + 1] + pd * optlist2[i + 2]);
                }
            }
            //caculate the process before last step to get c10,c11,c12;
            for (int i = 0; i < 3; i++)
            {
                optlist3[i] = Math.Exp(-rate * deltat) * (pu * optlist1[i] + pm * optlist1[i + 1] + pd * optlist1[i + 2]);
            }
            double optprice = Math.Exp(-rate * deltat) * (pu * optlist3[0] + pm * optlist3[1] + pd * optlist3[2]);
            //caculate delta,gamma,theta
            double delta = (optlist3[0] - optlist3[2]) / (u * stockprice - d * stockprice);
            double gamma = ((optlist3[0] - optlist3[1]) / (u * stockprice - stockprice) - (optlist3[1] - optlist3[2]) / (stockprice - d * stockprice)) / (0.5 * u * stockprice - 0.5 * d * stockprice);
            double theta = (optlist3[1] - optprice) / deltat;
            double[] result = { optprice, delta, gamma, theta };
            return result;
        }
        //caculate the american call option,delta,gamma,theta
        public static double[] aco(double strike, double stockprice, double rate, double vol, double dividend, int num, double time)
        {
            double deltat = time / num;
            //compute stockprice's increase factor u or decrease factor d
            double u = Math.Exp(vol * Math.Sqrt(3 * time / num));
            double d = Math.Exp(-vol * Math.Sqrt(3 * time / num));
            //use array to save option price
            double[] optlist1 = new double[2 * num + 1];
            double[] optlist2 = new double[2 * num + 1];
            double[] optlist3 = new double[3];
            //caculate the risk-neutral measure
            double[] problist = prob(strike, stockprice, rate, vol, dividend, num, time);
            double pu = problist[0];
            double pm = problist[1];
            double pd = problist[2];
            //get option price at time T
            for (int i = 0; i < 2 * num + 1; i++)
            {
                optlist1[i] = Math.Max(0, stockprice* Math.Pow(u, (num - i)) - strike);
            }
            for (int j = 1; j < num - 1; j++)     //caculate to the last three process
            {
                optlist1.CopyTo(optlist2, 0);
                for (int i = 0; i < 2 * num + 1 - 2 * j; i++)
                {
                    optlist1[i] = Math.Max( Math.Exp(-rate * deltat) * (pu * optlist2[i] + pm * optlist2[i + 1] + pd * optlist2[i + 2]), stockprice*Math.Pow(u, (num - i-j)) - strike);
                }
            }
            //caculate the process before last step to get c10,c11,c12;
            for (int i = 0; i < 3; i++)
            {
                optlist3[i] =Math.Max( Math.Exp(-rate * deltat) * (pu * optlist1[i] + pm * optlist1[i + 1] + pd * optlist1[i + 2]),stockprice*Math.Pow(u,1-i)-strike);
            }
            double optprice = Math.Max(Math.Exp(-rate * deltat) * (pu * optlist3[0] + pm * optlist3[1] + pd * optlist3[2]), stockprice - strike);
            
            double delta = (optlist3[0] - optlist3[2]) / (u * stockprice - d * stockprice);
            double gamma = ((optlist3[0] - optlist3[1]) / (u * stockprice - stockprice) - (optlist3[1] - optlist3[2]) / (stockprice - d * stockprice)) / (0.5 * u * stockprice - 0.5 * d * stockprice);
            double theta = (optlist3[1] - optprice) / deltat;
            double[] result = { optprice, delta, gamma, theta };
            return result;
        }
        //get american put option ,delta,gamma,theta
        public static double[] apo(double strike, double stockprice, double rate, double vol, double dividend, int num, double time)
        {
            double deltat = time / num;
            //compute stockprice's increase factor u or decrease factor d
            double u = Math.Exp(vol * Math.Sqrt(3 * time / num));
            double d = Math.Exp(-vol * Math.Sqrt(3 * time / num));
            //use array to save option prices
            double[] optlist1 = new double[2 * num + 1];
            double[] optlist2 = new double[2 * num + 1];
            double[] optlist3 = new double[3];
            //get risk-neutral measure
            double[] problist = prob(strike, stockprice, rate, vol, dividend, num, time);
            double pu = problist[0];
            double pm = problist[1];
            double pd = problist[2];
            //get final price at T
            for (int i = 0; i < 2 * num + 1; i++)
            {
                optlist1[i] = Math.Max(0, -stockprice * Math.Pow(u, (num - i)) + strike);
            }
            for (int j = 1; j < num - 1; j++)     //caculate to the last three process
            {
                optlist1.CopyTo(optlist2, 0);
                for (int i = 0; i < 2 * num + 1 - 2 * j; i++)
                {
                    optlist1[i] = Math.Max(Math.Exp(-rate * deltat) * (pu * optlist2[i] + pm * optlist2[i + 1] + pd * optlist2[i + 2]), -stockprice * Math.Pow(u, (num - i - j)) + strike);
                }
            }
            //caculate the process before last step to get c10,c11,c12;
            for (int i = 0; i < 3; i++)
            {
                optlist3[i] = Math.Max(Math.Exp(-rate * deltat) * (pu * optlist1[i] + pm * optlist1[i + 1] + pd * optlist1[i + 2]), stockprice * Math.Pow(u, 1 - i) - strike);
            }
            double optprice = Math.Max(Math.Exp(-rate * deltat) * (pu * optlist3[0] + pm * optlist3[1] + pd * optlist3[2]), stockprice - strike);
            //get delta,gamma,theta
            double delta = (optlist3[0] - optlist3[2]) / (u * stockprice - d * stockprice);
            double gamma = ((optlist3[0] - optlist3[1]) / (u * stockprice - stockprice) - (optlist3[1] - optlist3[2]) / (stockprice - d * stockprice)) / (0.5 * u * stockprice - 0.5 * d * stockprice);
            double theta = (optlist3[1] - optprice) / deltat;
            double[] result = { optprice, delta, gamma, theta };
            return result;
        }       
        public static double rho(double c1, double c2,double deltar) //method to get rho
        {
            double r = (c1 - c2) / (2*deltar);
            return r;
        }
        public static double vega(double c1, double c2, double deltavol)  //method to get vega
        {
            double v = (c1 - c2) / (2*deltavol);
            return v;
        }
        public double[] outeco()   
            //method to get value of european call option price, delta,vega,gamma, rho, theta
        {
            double[] a = eco(strike, stockprice, rate, vol, dividend, num, time);
            double option1 = eco(strike, stockprice, rate, vol + deltavol, dividend, num, time)[0];
            double option2 = eco(strike, stockprice, rate, vol - deltavol, dividend, num, time)[0];
            double v = vega(option1, option2, deltavol);
            double option3 = eco(strike, stockprice, rate + deltar, vol, dividend, num, time)[0];
            double option4 = eco(strike, stockprice, rate - deltar, vol, dividend, num, time)[0];
            double r = rho(option3, option4, deltar);
            double[] result = new double[6];
            for (int i = 0; i < a.Length; i++)
            {
                result[i] = a[i];
            }
            result[4] = v; result[5] = r;
            return result;
        }
        //method to get value of european put option price, delta,vega,gamma, rho, theta
        public double[] outepo()
        {
            double[] a = epo(strike, stockprice, rate, vol, dividend, num, time);
            double option1 = epo(strike, stockprice, rate, vol + deltavol, dividend, num, time)[0];
            double option2 = epo(strike, stockprice, rate, vol - deltavol, dividend, num, time)[0];
            double v = vega(option1, option2, deltavol);
            double option3 = epo(strike, stockprice, rate + deltar, vol, dividend, num, time)[0];
            double option4 = epo(strike, stockprice, rate - deltar, vol, dividend, num, time)[0];
            double r = rho(option3, option4, deltar);
            double[] result = new double[6];
            for (int i = 0; i < a.Length; i++)
            {
                result[i] = a[i];
            }
            result[4] = v; result[5] = r;
            return result;
        }
        //method to get value of american call option price, delta,vega,gamma, rho, theta
        public double[] outaco()
        {
            double[] a= aco(strike, stockprice, rate, vol, dividend, num, time);
            double option1 = aco(strike, stockprice, rate, vol+deltavol, dividend, num, time)[0];
            double option2 = aco(strike, stockprice, rate, vol-deltavol, dividend, num, time)[0];
            double v = vega(option1, option2, deltavol);
            double option3= aco(strike, stockprice, rate+deltar, vol , dividend, num, time)[0];
            double option4 = aco(strike, stockprice, rate - deltar, vol, dividend, num, time)[0];
            double r = rho(option3, option4, deltar);
            double[] result=new double[6];
            for(int i=0;i<a.Length;i++)
            {
                result[i] = a[i];
            }
            result[4] = v;result[5] = r;
            return result;
        }
        //method to get value of american put option price, delta,vega,gamma, rho, theta
        public double[] outapo()
        {
            double[] a = apo(strike, stockprice, rate, vol, dividend, num, time);
            double option1 = apo(strike, stockprice, rate, vol + deltavol, dividend, num, time)[0];
            double option2 = apo(strike, stockprice, rate, vol - deltavol, dividend, num, time)[0];
            double v = vega(option1, option2, deltavol);
            double option3 = apo(strike, stockprice, rate + deltar, vol, dividend, num, time)[0];
            double option4 = apo(strike, stockprice, rate - deltar, vol, dividend, num, time)[0];
            double r = rho(option3, option4, deltar);
            double[] result = new double[6];
            for (int i = 0; i < a.Length; i++)
            {
                result[i] = a[i];
            }
            result[4] = v; result[5] = r;
            return result;
        }
    }
}

