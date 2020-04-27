using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    class range:baseoption
    {
        public override double[] call(double strike, double stockprice, double rate, double vol, int step, int trial, double time, double[,] randommatrix)
        {
            double[,] price = pricematrix(strike, stockprice, rate, vol, step, trial, time, randommatrix);
            double[] call = new double[trial];
            double[] minprice = new double[trial];//define minimum price in every trial
            double[] maxprice = new double[trial];//define maximum price in every trial
            for (int i = 0; i < trial; i++)
            {
                maxprice[i] = 0;minprice[i] = price[i,0];
                for (int j = 0; j < step; j++)
                { 
                    if (price[i, j] < minprice[i]) minprice[i] = price[i, j];
                    if (price[i, j] > maxprice[i]) maxprice[i] = price[i, j];
                }                           
              call[i] =(maxprice[i] - minprice[i]) * Math.Exp(-rate * time); //compute option value at t=0 in every simulation
            }
            return call;
        }
        //method to get asian put option value list
        public override double[] put(double strike, double stockprice, double rate, double vol, int step, int trial, double time, double[,] randommatrix)
        {
            double[,] price = pricematrix(strike, stockprice, rate, vol, step, trial, time, randommatrix);
            double[] put = new double[trial];
            double[] minprice = new double[trial];//define minimum price in every trial
            double[] maxprice = new double[trial];//define maximum price in every trial
            for (int i = 0; i < trial; i++)
            {
                maxprice[i] = 0; minprice[i] = price[i, 0];
                for (int j = 0; j < step; j++)
                {
                    if (price[i, j] < minprice[i]) minprice[i] = price[i, j];
                    if (price[i, j] > maxprice[i]) maxprice[i] = price[i, j];
                }
                put[i] = (maxprice[i] - minprice[i]) * Math.Exp(-rate * time); //compute option value at t=0 in every simulation
            }
            return put;
        }
        public override double[] CVcall(double strike, double stockprice, double rate, double vol, int step, int trial, double time, double[,] randommatrix)
        {
            double[,] price = pricematrix(strike, stockprice, rate, vol, step, trial, time, randommatrix);
            double dt = time / step; double t;
            CDFmethod cdf = new CDFmethod();  //compute cdf 
            double[] ct = new double[trial];//define a list to save option price and standard error
            double[] minprice = new double[trial];//define minimum price in every trial
            double[] maxprice = new double[trial];//define maximum price in every trial            
            for (int i = 0; i < trial; i++)
            {
                double cv = 0;//define delta hedge
                maxprice[i] = 0; minprice[i] = price[i, 0];
                for (int j = 1; j < step; j++)
                {
                    if (price[i, j] < minprice[i]) minprice[i] = price[i, j];
                    if (price[i, j] > maxprice[i]) maxprice[i] = price[i, j];
                    t = dt * (step - j);
                    double d1 = (Math.Log(price[i, j - 1] / strike) + (rate + Math.Pow(vol, 2) / 2) * t) / (vol * Math.Pow(t, 0.5));
                    double delta = cdf.CumDensity(d1);
                    cv = cv + delta * (price[i, j] - price[i, j - 1] * Math.Exp(rate * dt));
                }
                ct[i] = (maxprice[i] -minprice[i]- cv) * Math.Exp(-rate * time);
            }
            return ct;
        }
        public override double[] CVput(double strike, double stockprice, double rate, double vol, int step, int trial, double time, double[,] randommatrix)
        {
            double[,] price = pricematrix(strike, stockprice, rate, vol, step, trial, time, randommatrix);
            double dt = time / step; double t;
            CDFmethod cdf = new CDFmethod();  //compute cdf 
            double[] ct = new double[trial];//define a list to save option price and standard error
            double[] minprice = new double[trial];//define minimum price in every trial
            double[] maxprice = new double[trial];//define maximum price in every trial            
            for (int i = 0; i < trial; i++)
            {
                double cv = 0;//define delta hedge
                maxprice[i] = 0; minprice[i] = price[i, 0];
                for (int j = 1; j < step; j++)
                {
                    if (price[i, j] < minprice[i]) minprice[i] = price[i, j];
                    if (price[i, j] > maxprice[i]) maxprice[i] = price[i, j];
                    t = dt * (step - j);
                    double d1 = (Math.Log(price[i, j - 1] / strike) + (rate + Math.Pow(vol, 2) / 2) * t) / (vol * Math.Pow(t, 0.5));
                    double delta = cdf.CumDensity(d1);
                    cv = cv + delta * (price[i, j] - price[i, j - 1] * Math.Exp(rate * dt));
                }
                ct[i] = (maxprice[i] - minprice[i] - cv) * Math.Exp(-rate * time);
            }
            return ct;
        }
    }
}
