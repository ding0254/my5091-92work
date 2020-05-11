using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    class option
    {
        private double strike, stockprice, rate, vol, time; int trial, step; double[,] randommatrix;            
        public double Strike { get { return strike; } set { strike = value; } }  //get strike value
        
        public double Stockprice { get { return stockprice; } set { stockprice = value; } } //get stockprice value
        public double Rate { get { return rate; } set { rate = value; } } //get interest rate value
        public double Vol { get { return vol; } set { vol = value; } }  //get volitality value
        public int Step { get { return step; } set { step = value; } }    //get number of process
        public int Trial { get { return trial; } set { trial = value; } }    //get number of trial
        public double Time { get { return time; } set { time = value; } }   //get tenor value
        public double[,] Randommatrix { get { return randommatrix; } set { randommatrix = value; } } //get random matrix
        
        //get the price matrix to caculate option and greek
        static double[,] pricematrix(double strike, double stockprice, double rate, double vol, int step, int trial, double time, double[,] randommatrix)

        {

            double[,] price = new double[trial, step];
            double deltat = time / step;
            for (int i = 0; i < trial; i++)
            {
                price[i, 0] = stockprice;
                //use MC method to simulate to the last stock price
                for (int j = 1; j < step; j++)
                {
                    price[i, j] = price[i, j - 1] * Math.Exp((rate - 0.5 * vol * vol) * deltat + vol * Math.Sqrt(deltat) * randommatrix[i, j]); 
                }
            }
            return price;
        }

        static double std(double[] option) //method to compute standard deviation
        {
            double mean = option.Average();
            double std = 0;
            for (int i = 0; i < option.Length; i++)
            {
                std = std + Math.Pow((option[i] - mean), 2);
            }
            std = Math.Sqrt(std / (Convert.ToDouble(option.Length) - 1));
            return std;
        }
        //method to get european call option value and standard deviation
        public static double[] eco(double strike, double stockprice, double rate, double vol, int step, int trial, double time, double[,] randommatrix)
        {
            double[,] price = pricematrix(strike, stockprice, rate, vol, step, trial, time, randommatrix);
            double[] call = new double[trial];
            for (int i = 0; i < trial; i++)
            {
                call[i] = Math.Max(price[i, step - 1] - strike, 0) * Math.Exp(-rate * time); //compute option value at t=0 in every simulation
            }
            double callprice = call.Average();
            double se = std(call)/Math.Sqrt(trial);
            double[] a = { callprice, se };
            return a;
        }
        //method to get european put option value and standard deviation
        public static double[] epo(double strike, double stockprice, double rate, double vol, int step, int trial, double time, double[,] randommatrix)
        {
            double[,] price = pricematrix(strike, stockprice, rate, vol, step, trial, time, randommatrix);
            double[] put = new double[trial];
            for (int i = 0; i < trial; i++)
            {
                put[i] = Math.Max(strike - price[i, step - 1], 0) * Math.Exp(-rate * time);//compute option at 0 in every simulation
            }
            double putprice = put.Average();
            double sd = std(put);
            double[] a = { putprice, sd };
            return a;

        }
        public double[] putgreek()  //return put greek,put value and standard deviation
        {
            double deltas = 0.001*stockprice; //difference of stockprice
            double deltavol = 0.001*vol; //difference of volitality
            double deltat = time / step;//difference of t
            double deltar = 0.001*rate;//difference of rate
            double[] option = epo(strike, stockprice, rate, vol, step, trial, time, randommatrix);
            double o1 = option[0];double std = option[1];
            //compute european put at different parameters
            double o2= epo(strike, stockprice+deltas, rate, vol, step, trial, time, randommatrix)[0]; 
            double o3=epo(strike, stockprice-deltas, rate, vol, step, trial, time, randommatrix)[0]; 
            double o4=epo (strike, stockprice, rate, vol+deltavol, step, trial, time, randommatrix)[0];
            double o5=epo(strike, stockprice, rate+deltar, vol, step, trial, time, randommatrix)[0];
            double o6=epo(strike, stockprice, rate, vol, step, trial, time+deltat, randommatrix)[0];
            double o7=epo(strike, stockprice, rate, vol-deltavol, step, trial, time, randommatrix)[0];
            double o8=epo(strike, stockprice, rate-deltar, vol, step, trial, time, randommatrix)[0];
            double delta = (o2 - o3) / (2 * deltas); //get delta
            double gamma = (o2 + o3 - o1 * 2) / (deltas * deltas); //get gamma
            double vega = (o4 - o7) / (2 * deltavol);//get vega
            double rho = (o5 - o8) / (2 * deltar);//get rho
            double theta = (o6 - o1) / deltat;//get theta
            double se = std / Math.Sqrt(trial);
            double[] a = { delta, gamma, vega, rho, theta,o1,se };
            return a;
        }
        public double[] callgreek() //return call value, greek and standard deviation
        {
            double deltas = 0.001 * stockprice;
            double deltavol = 0.001 * vol;
            double deltat = time / step;
            double deltar = 0.001 * rate;
            double[] option = eco(strike, stockprice, rate, vol, step, trial, time, randommatrix);
            //compute call value at diffrent parameters
            double o1 = option[0]; double std = option[1];
            double o2 = eco(strike, stockprice + deltas, rate, vol, step, trial, time, randommatrix)[0];
            double o3 = eco(strike, stockprice - deltas, rate, vol, step, trial, time, randommatrix)[0];
            double o4 = eco(strike, stockprice, rate, vol + deltavol, step, trial, time, randommatrix)[0];
            double o5 = eco(strike, stockprice, rate + deltar, vol, step, trial, time, randommatrix)[0];
            double o6 = eco(strike, stockprice, rate, vol, step, trial, time + deltat, randommatrix)[0];
            double o7 = eco(strike, stockprice, rate, vol - deltavol, step, trial, time, randommatrix)[0];
            double o8 = eco(strike, stockprice, rate - deltar, vol, step, trial, time, randommatrix)[0];
            double delta = (o2 - o3) / (2 * deltas);//get delta
            double gamma = (o2 + o3 - o1 * 2) / (deltas * deltas);//get gamma
            double vega = (o4 - o7) / (2 * deltavol);//get vega
            double rho = (o5 - o8) / (2 * deltar);//get rho
            double theta = (o6 - o1) / deltat;//get theta
            double se = std / Math.Sqrt(trial);//get standard error
            double[] a = { delta, gamma, vega, rho, theta,o1 ,se};
            return a;
        }

    }
}
