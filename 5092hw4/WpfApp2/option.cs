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
        public static double[,] pricematrix(double strike, double stockprice, double rate, double vol, int step, int trial, double time, double[,] randommatrix)

        {
            double[,] price = new double[trial, step];
            double deltat = time / step;
            for (int i = 0; i < trial; i++)
            {
                price[i, 0] = stockprice;
                //use MC method to simulate the last stock price
                for (int j = 1; j < step; j++)
                {
                    price[i, j] = price[i, j - 1] * Math.Exp((rate - 0.5 * vol * vol) * deltat + vol * Math.Sqrt(deltat) * randommatrix[i, j]);
                }
            }
            return price;
        }

        static double SE(double[] option) //method to compute standard error
        {
            double mean = option.Average();
            double std = 0;
            for (int i = 0; i < option.Length; i++)
            {
                std = std + Math.Pow((option[i] - mean), 2);
            }
            std = Math.Sqrt(std / (Convert.ToDouble(option.Length) - 1));
            double se = std / Math.Sqrt(option.Length);
            return se;
        }
        static double antise(double[] option) //get standard error using antithetic method
         {
            int len = option.Length;
            
            if(len%2==0) //if trial number is even
            {
                double[] list = new double[len/2];
                for (int i=0;i<len/2;i++)
                {
                    list[i] = 0.5*(option[i] + option[i + len/2]); //get mean of paired random numbers
                }
                double mean=list.Average();
                double sum = 0;
                for (int i=0;i<list.Length;i++)
                {
                    sum = sum + Math.Pow(list[i] - mean, 2);
                }
                double se =Math.Sqrt (sum / (list.Length * list.Length)); //get standard error
                return se;
            }
            else  //if trial number is odd
            {
                double[] list = new double[(len+1) / 2];
                for (int i = 0; i < (len-1) / 2; i++)
                {
                    list[i] = 0.5 * (option[i] + option[i + (1+len) / 2]);//get mean of paired random numbers
                }
                list[(len - 1) / 2] = option[(len - 1) / 2];
                double mean = list.Average();
                double sum = 0;
                for (int i = 0; i < list.Length; i++)
                {
                    sum = sum + Math.Pow(list[i] - mean, 2);
                }
                double se =Math.Sqrt(sum / (list.Length * list.Length));
                return se;

            }
        }
        //method to get european call option value list
        public static double[] eco(double strike, double stockprice, double rate, double vol, int step, int trial, double time, double[,] randommatrix)
        {
            double[,] price = pricematrix(strike, stockprice, rate, vol, step, trial, time, randommatrix);
            double[] call = new double[trial];
            for (int i = 0; i < trial; i++)
            {
                call[i] = Math.Max(price[i, step - 1] - strike, 0) * Math.Exp(-rate * time); //compute option value at t=0 in every simulation
            }
            //double callprice = call.Average();
           // double se = std(call) / Math.Sqrt(trial);//get traditional standard error
           // double ase = antise(call); //get antithetic standard error
           // double[] a = { callprice, se,ase };
            return call;
        }
        //method to get european put option value list
        public static double[] epo(double strike, double stockprice, double rate, double vol, int step, int trial, double time, double[,] randommatrix)
        {
            double[,] price = pricematrix(strike, stockprice, rate, vol, step, trial, time, randommatrix);
            double[] put = new double[trial];
            for (int i = 0; i < trial; i++)
            {
                put[i] = Math.Max(strike - price[i, step - 1], 0) * Math.Exp(-rate * time);//compute option at 0 in every simulation
            }
            //double putprice = put.Average();
            //double se = std(put) / Math.Sqrt(trial);//get traditional standard error
            //double ase = antise(put);//get antithetic standard error
            //double[] a = { putprice, se,ase};
            return put;

        }
        //control variate method to get european call option value list
        public static double[] CVeco(double strike, double stockprice, double rate, double vol, int step, int trial, double time, double[,] randommatrix)
        {
            double[,] price = pricematrix(strike, stockprice, rate, vol, step, trial, time, randommatrix);
            
            double dt = time / step;double t;
            CDFmethod cdf = new CDFmethod();  //compute cdf 
            double[] ct = new double[trial];//define a list to save option price and standard error
            
            for (int i = 0; i < trial; i++)
            {
                double cv = 0;//define delta hedge
                for (int j=1;j<step;j++)
                {
                    t=dt*(step-j);
                    double d1 = (Math.Log(price[i,j-1] / strike) + (rate +Math.Pow(vol,2) / 2) * t) / (vol *Math.Pow( t ,0.5));
                    double delta = cdf.CumDensity(d1);
                    cv = cv + delta * (price[i, j] - price[i, j - 1] * Math.Exp(rate * dt));
                }
                ct[i] = (Math.Max(0, price[i, step - 1]-strike) - cv)*Math.Exp(-rate*time);
                               
            }            
            return ct;
        }
        //control variate method to get european put option value list
        public static double[] CVepo(double strike, double stockprice, double rate, double vol, int step, int trial, double time, double[,] randommatrix)
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
                ct[i] = (Math.Max(0, -price[i, step - 1] + strike) - cv)*Math.Exp(-rate*time);
                
            }
            
            return ct;
        }
        public double[] putgreek()  //return put greek,put value and standard deviation
        {
            double deltas = 0.001 * stockprice; //difference of stockprice
            double deltavol = 0.001 * vol; //difference of volitality
            double deltat = time / step;//difference of t
            double deltar = 0.001 * rate;//difference of rate
            double[] option = epo(strike, stockprice, rate, vol, step, trial, time, randommatrix);
            double o1 = option.Average(); double se = SE(option); double ase = antise(option);//get option price and standard error
            //compute european put at different parameters
            double o2 = epo(strike, stockprice + deltas, rate, vol, step, trial, time, randommatrix).Average();
            double o3 = epo(strike, stockprice - deltas, rate, vol, step, trial, time, randommatrix).Average();
            double o4 = epo(strike, stockprice, rate, vol + deltavol, step, trial, time, randommatrix).Average();
            double o5 = epo(strike, stockprice, rate + deltar, vol, step, trial, time, randommatrix).Average();
            double o6 = epo(strike, stockprice, rate, vol, step, trial, time + deltat, randommatrix).Average();
            double o7 = epo(strike, stockprice, rate, vol - deltavol, step, trial, time, randommatrix).Average();
            double o8 = epo(strike, stockprice, rate - deltar, vol, step, trial, time, randommatrix).Average();
            double delta = (o2 - o3) / (2 * deltas); //get delta
            double gamma = (o2 + o3 - o1 * 2) / (deltas * deltas); //get gamma
            double vega = (o4 - o7) / (2 * deltavol);//get vega
            double rho = (o5 - o8) / (2 * deltar);//get rho
            double theta = (o6 - o1) / deltat;//get theta
            double[] a = { delta, gamma, vega, rho, theta, o1, se,ase};
            return a;
        }
        public double[] callgreek()  //return put greek,call value and standard error
        {
            double deltas = 0.001 * stockprice; //difference of stockprice
            double deltavol = 0.001 * vol; //difference of volitality
            double deltat = time / step;//difference of t
            double deltar = 0.001 * rate;//difference of rate
            double[] option = eco(strike, stockprice, rate, vol, step, trial, time, randommatrix);
            double o1 = option.Average(); double se = SE(option); double ase = antise(option);//get option price and standard error
            //compute european call at different parameters
            double o2 = eco(strike, stockprice + deltas, rate, vol, step, trial, time, randommatrix).Average();
            double o3 = eco(strike, stockprice - deltas, rate, vol, step, trial, time, randommatrix).Average();
            double o4 = eco(strike, stockprice, rate, vol + deltavol, step, trial, time, randommatrix).Average();
            double o5 = eco(strike, stockprice, rate + deltar, vol, step, trial, time, randommatrix).Average();
            double o6 = eco(strike, stockprice, rate, vol, step, trial, time + deltat, randommatrix).Average();
            double o7 = eco(strike, stockprice, rate, vol - deltavol, step, trial, time, randommatrix).Average();
            double o8 = eco(strike, stockprice, rate - deltar, vol, step, trial, time, randommatrix).Average();
            double delta = (o2 - o3) / (2 * deltas); //get delta
            double gamma = (o2 + o3 - o1 * 2) / (deltas * deltas); //get gamma
            double vega = (o4 - o7) / (2 * deltavol);//get vega
            double rho = (o5 - o8) / (2 * deltar);//get rho
            double theta = (o6 - o1) / deltat;//get theta
            double[] a = { delta, gamma, vega, rho, theta, o1, se, ase };
            return a;
        }
        public double[] CVcallgreek() //return control variate call value, greek and standard deviation
        {
            double deltas = 0.001 * stockprice;
            double deltavol = 0.001 * vol;
            double deltat = time / step;
            double deltar = 0.001 * rate;
            double[] option = CVeco(strike, stockprice, rate, vol, step, trial, time, randommatrix);
            //compute call value at diffrent parameters
            double o1 = option.Average(); double se =SE(option); double ase = antise(option);//get option price and standard error
            double o2 = CVeco(strike, stockprice + deltas, rate, vol, step, trial, time, randommatrix).Average();
            double o3 = CVeco(strike, stockprice - deltas, rate, vol, step, trial, time, randommatrix).Average();
            double o4 = CVeco(strike, stockprice, rate, vol + deltavol, step, trial, time, randommatrix).Average();
            double o5 = CVeco(strike, stockprice, rate + deltar, vol, step, trial, time, randommatrix).Average();
            double o6 = CVeco(strike, stockprice, rate, vol, step, trial, time + deltat, randommatrix).Average();
            double o7 = CVeco(strike, stockprice, rate, vol - deltavol, step, trial, time, randommatrix).Average();
            double o8 = CVeco(strike, stockprice, rate - deltar, vol, step, trial, time, randommatrix).Average();
            double delta = (o2 - o3) / (2 * deltas);//get delta
            double gamma = (o2 + o3 - o1 * 2) / (deltas * deltas);//get gamma
            double vega = (o4 - o7) / (2 * deltavol);//get vega
            double rho = (o5 - o8) / (2 * deltar);//get rho
            double theta = (o6 - o1) / deltat;//get theta
            double[] a = { delta, gamma, vega, rho, theta, o1, se, ase };
            return a;
        }
        public double[] CVputgreek()  //return control variate put greek,put value and standard deviation
        {
            double deltas = 0.001 * stockprice; //difference of stockprice
            double deltavol = 0.001 * vol; //difference of volitality
            double deltat = time / step;//difference of t
            double deltar = 0.001 * rate;//difference of rate
            double[] option = CVepo(strike, stockprice, rate, vol, step, trial, time, randommatrix);
            double o1 = option.Average(); double se = SE(option); double ase = antise(option);//get option price and standard error
            //compute european put at different parameters
            double o2 = CVepo(strike, stockprice + deltas, rate, vol, step, trial, time, randommatrix).Average();
            double o3 = CVepo(strike, stockprice - deltas, rate, vol, step, trial, time, randommatrix).Average();
            double o4 = CVepo(strike, stockprice, rate, vol + deltavol, step, trial, time, randommatrix).Average();
            double o5 = CVepo(strike, stockprice, rate + deltar, vol, step, trial, time, randommatrix).Average();
            double o6 = CVepo(strike, stockprice, rate, vol, step, trial, time + deltat, randommatrix).Average();
            double o7 = CVepo(strike, stockprice, rate, vol - deltavol, step, trial, time, randommatrix).Average();
            double o8 = CVepo(strike, stockprice, rate - deltar, vol, step, trial, time, randommatrix).Average();
            double delta = (o2 - o3) / (2 * deltas); //get delta
            double gamma = (o2 + o3 - o1 * 2) / (deltas * deltas); //get gamma
            double vega = (o4 - o7) / (2 * deltavol);//get vega
            double rho = (o5 - o8) / (2 * deltar);//get rho
            double theta = (o6 - o1) / deltat;//get theta
            double[] a = { delta, gamma, vega, rho, theta, o1, se, ase };
            return a;
        }
    }
}

