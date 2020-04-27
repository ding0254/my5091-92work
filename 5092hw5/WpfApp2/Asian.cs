using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    class Asian:baseoption
    {
        //private double strike, stockprice, rate, vol, time; int trial, step; double[,] randommatrix;
        
        public override double[] call(double strike, double stockprice, double rate, double vol, int step, int trial, double time, double[,] randommatrix)
        {
            double[,] price = pricematrix(strike, stockprice, rate, vol, step, trial, time, randommatrix);
            double[] call = new double[trial];            
            double[] avgprice = new double[trial];
            for (int i = 0; i < trial; i++)
            {  avgprice[i] = 0;
                for (int j = 0; j < step; j++)
                { avgprice[i] = avgprice[i] + price[i, j]; }
                avgprice[i] = avgprice[i] / step;
                call[i] = Math.Max(avgprice[i] - strike, 0) * Math.Exp(-rate * time); //compute option value at t=0 in every simulation
            }
            return call;
        }
        //method to get asian put option value list
        public override double[] put(double strike, double stockprice, double rate, double vol, int step, int trial, double time, double[,] randommatrix)
        {
            double[,] price = pricematrix(strike, stockprice, rate, vol, step, trial, time, randommatrix);
            double[] put = new double[trial];
            double[] avgprice = new double[trial];
            for (int i = 0; i < trial; i++)
            {
                avgprice[i] = 0;
                for (int j = 0; j < step; j++)
                { avgprice[i] = avgprice[i] + price[i, j]; }
                avgprice[i] = avgprice[i] / step;
                put[i] = Math.Max(strike - avgprice[i], 0) * Math.Exp(-rate * time);//compute option at 0 in every simulation
            }          
            return put;
        }
        public override double[] CVcall(double strike, double stockprice, double rate, double vol, int step, int trial, double time, double[,] randommatrix)
        {
            double[,] price = pricematrix(strike, stockprice, rate, vol, step, trial, time, randommatrix);

            double dt = time / step; double t;
            CDFmethod cdf = new CDFmethod();  //compute cdf 
            double[] ct = new double[trial];//define a list to save option price and standard error
            double[] avgprice = new double[trial];            
            for (int i = 0; i < trial; i++)
            {
                double cv = 0;//define delta hedge
                for (int j = 1; j < step; j++)
                {
                    avgprice[i] = avgprice[i] + price[i, j];
                    t = dt * (step - j);
                    double d1 = (Math.Log(price[i, j - 1] / strike) + (rate + Math.Pow(vol, 2) / 2) * t) / (vol * Math.Pow(t, 0.5));
                    double delta = cdf.CumDensity(d1);
                    cv = cv + delta * (price[i, j] - price[i, j - 1] * Math.Exp(rate * dt));
                }
                avgprice[i] = avgprice[i] / step; //find the mean stockprice of every trial
                ct[i] = (Math.Max(0, avgprice[i] - strike) - cv) * Math.Exp(-rate * time);
            }
            return ct;
        }
        public override double[] CVput(double strike, double stockprice, double rate, double vol, int step, int trial, double time, double[,] randommatrix)
        {
            double[,] price = pricematrix(strike, stockprice, rate, vol, step, trial, time, randommatrix);

            double dt = time / step; double t;
            CDFmethod cdf = new CDFmethod();  //compute cdf 
            double[] ct = new double[trial];//define a list to save option price and standard error
            double[] avgprice = new double[trial];
            for (int i = 0; i < trial; i++)
            {
                double cv = 0;//define delta hedge
                for (int j = 1; j < step; j++)
                {
                    avgprice[i] = avgprice[i] + price[i, j];
                    t = dt * (step - j);
                    double d1 = (Math.Log(price[i, j - 1] / strike) + (rate + Math.Pow(vol, 2) / 2) * t) / (vol * Math.Pow(t, 0.5));
                    double delta = cdf.CumDensity(d1)-1;
                    cv = cv + delta * (price[i, j] - price[i, j - 1] * Math.Exp(rate * dt));
                }
                avgprice[i] = avgprice[i] / step; //find the mean stockprice of every trial
                ct[i] = (Math.Max(0, -avgprice[i] + strike) - cv) * Math.Exp(-rate * time);
            }
            return ct;
        }
        
    }
}
