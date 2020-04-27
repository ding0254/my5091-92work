using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    class digital:baseoption
    {
        private double rebate;
        public double Rebate { get { return rebate; } set { rebate = value; } }   //get rebate
        public override double[] call(double strike, double stockprice, double rate, double vol, int step, int trial, double time, double[,] randommatrix)
        {
            double[,] price = pricematrix(strike, stockprice, rate, vol, step, trial, time, randommatrix);
            double[] call = new double[trial];
            for (int i = 0; i < trial; i++)
            {
                if (Math.Max(price[i, step - 1] - strike, 0) > 0)
                    call[i] = rebate * Math.Exp(-rate * time); //compute option value at t=0 in every simulation
                else
                    call[i] = 0;
            }

            return call;
        }
        //method to get digital put option value list
        public override double[] put(double strike, double stockprice, double rate, double vol, int step, int trial, double time, double[,] randommatrix)
        {
            double[,] price = pricematrix(strike, stockprice, rate, vol, step, trial, time, randommatrix);
            double[] put = new double[trial];
            for (int i = 0; i < trial; i++)
            {
                if (Math.Max(-price[i, step - 1] + strike, 0) > 0)
                    put[i] = rebate * Math.Exp(-rate * time); //compute option value at t=0 in every simulation
                else
                    put[i] = 0;
            }
            return put;
        }
        public override double[] CVcall(double strike, double stockprice, double rate, double vol, int step, int trial, double time, double[,] randommatrix)
        {
            double[,] price = pricematrix(strike, stockprice, rate, vol, step, trial, time, randommatrix);
            double dt = time / step; double t;
            CDFmethod cdf = new CDFmethod();  //compute cdf 
            double[] ct = new double[trial];//define a list to save option price and standard error
            for (int i = 0; i < trial; i++)
            {
                double cv = 0;//define delta hedge
                for (int j = 1; j < step; j++)
                {
                    t = dt * (step - j);
                    double d1 = (Math.Log(price[i, j - 1] / strike) + (rate + Math.Pow(vol, 2) / 2) * t) / (vol * Math.Pow(t, 0.5));
                    double delta = cdf.CumDensity(d1);
                    cv = cv + delta * (price[i, j] - price[i, j - 1] * Math.Exp(rate * dt));
                }
                if(Math.Max(0, price[i, step - 1]-strike)>0)
                    ct[i] = (rebate - cv) * Math.Exp(-rate * time);
                else
                    ct[i] = (0- cv) * Math.Exp(-rate * time);
            }
            return ct;
        }
        public override double[] CVput(double strike, double stockprice, double rate, double vol, int step, int trial, double time, double[,] randommatrix)
        {
            double[,] price = pricematrix(strike, stockprice, rate, vol, step, trial, time, randommatrix);
            double dt = time / step; double t;
            CDFmethod cdf = new CDFmethod();
            double[] ct = new double[trial];//define a list to save option price and standard error          
            for (int i = 0; i < trial; i++)
            {
                double cv = 0;//define delta hedge
                for (int j = 1; j < step; j++)
                {
                    t = dt * (step - j);
                    double d1 = (Math.Log(price[i, j - 1] / strike) + (rate + Math.Pow(vol, 2) / 2) * t) / (vol * Math.Pow(t, 0.5));
                    double delta = cdf.CumDensity(d1) - 1;
                    cv = cv + delta * (price[i, j] - price[i, j - 1] * Math.Exp(rate * dt));
                }
                if (Math.Max(0, strike-price[i, step - 1]) > 0)
                    ct[i] = (rebate - cv) * Math.Exp(-rate * time);
                else
                    ct[i] = (0 - cv) * Math.Exp(-rate * time);
                }
            return ct;
        }

    }
}
