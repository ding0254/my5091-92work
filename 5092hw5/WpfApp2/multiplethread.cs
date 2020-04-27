using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace WpfApp2
{
    class multiplethread
    {
        private double strike, stockprice, rate, vol, time; int trial, step, core; 
        private double[,] _matrix;
        public double[,] Matrix { get { return this._matrix; } }//define _matrix to save price data
        public void getlength()
        {
            _matrix = new double[8 * trial, step];
        }
        //public static double[,] threadMatrix;
        public double Strike { get { return strike; } set { strike = value; } }  //get strike value

        public double Stockprice { get { return stockprice; } set { stockprice = value; } } //get stockprice value
        public double Rate { get { return rate; } set { rate = value; } } //get interest rate value
        public double Vol { get { return vol; } set { vol = value; } }  //get volitality value
        public int Step { get { return step; } set { step = value; } }    //get number of process
        public int Trial { get { return trial; } set { trial = value; } }    //get number of trial
        public double Time { get { return time; } set { time = value; } }   //get tenor value
        public int Core { get { return core; } set { core = value; } }   //get core

        //use threads method to generate every part of thread to compute stock price process
        public void threads(double strike, double stockprice, double rate, double vol, int step, int trial, int beginnumber, double time, bool antithetic, bool cvmethod, bool calloption)
        {
            randomnumber r = new randomnumber();
            option o = new option();
            double[,] randommatrix = new double[trial, step];
            //use bool antithetic to decide random matrix
            if (antithetic)
            {
                randommatrix = r.antitheticgenerator(step, trial);
            }
            else
            {
                randommatrix = r.randommatrixgenerator(step, trial);
            }

            if (cvmethod)
            {
                double[,] p1 = pricematrix(strike, stockprice, rate, vol, step, trial, time, randommatrix);
                //get option value using cvmethod
                double[] optionlist = CVmethod(strike, stockprice, rate, vol, step, trial, time, p1, calloption);
                for (int i = 0; i < p1.GetLength(0); i++)
                {
                    for (int j = 0; j < p1.GetLength(1) - 1; j++)
                    {
                        _matrix[beginnumber + i, j] = p1[i, j];
                    }
                    _matrix[beginnumber + i, p1.GetLength(1) - 1] = optionlist[i];
                }

            }
            else //if not control variate
            {
                double[,] p1 = pricematrix(strike, stockprice, rate, vol, step, trial, time, randommatrix);

                for (int i = 0; i < p1.GetLength(0); i++)
                {
                    for (int j = 0; j < p1.GetLength(1); j++)
                    {

                        _matrix[beginnumber + i, j] = p1[i, j];

                    }
                }
            }

        }
        // use threadpractice method to generate final mutiple thread result
        public double[] threadpractice(double strike, double stockprice, double rate, double vol, int step, int trial, int corenumber, double time, bool antithetic, bool cvmethod, bool calloption)
        {
            Thread[] tt = new Thread[corenumber];//define thread list
            int avgtrial = trial / (corenumber);//ditribute trial number to every thread
            int lastthreadtrial = avgtrial + trial % corenumber;
            for (int i = 0; i < corenumber - 1; i++)
            {
                int beginnumber = i * avgtrial * 8;
                tt[i] = new Thread(new ThreadStart(() => threads(strike, stockprice, rate, vol, step, avgtrial, beginnumber, time, antithetic, cvmethod, calloption)));
                tt[i].Start();                
            }
            tt[corenumber - 1] = new Thread(new ThreadStart(() => threads(strike, stockprice, rate, vol, step, trial - avgtrial * (corenumber - 1), 8 * (corenumber - 1) * avgtrial, time, antithetic, cvmethod, calloption)));
            tt[corenumber - 1].Start();
            for (int i = 0; i < corenumber; i++) { tt[i].Join(); }
            double deltas = 0.001 * stockprice; //difference of stockprice
            double deltavol = 0.001 * vol; //difference of volitality
            double deltat = time / step;//difference of t
            double deltar = 0.001 * rate;//difference of rate
            if (cvmethod) //use control variate method
            {
                double o1 = 0, o2 = 0, o3 = 0, o4 = 0, o5 = 0, o6 = 0, o7 = 0, o8 = 0;
                double[] option = new double[trial]; int k = 0;
                //get sum of option values under different parameters
                for (int i = 0; i < 8 * trial; i = i + 8)
                {
                    option[k] = _matrix[i, step - 1];
                    o2 = o2 + _matrix[i + 1, step - 1]; o3 = o3 + _matrix[i + 2, step - 1];
                    o4 = o4 + _matrix[i + 3, step - 1]; o5 = o5 + _matrix[i + 4, step - 1]; o6 = o6 + _matrix[i + 5, step - 1];
                    o7 = o7 + _matrix[i + 6, step - 1]; o8 = o8 + _matrix[i + 7, step - 1];
                    k++;
                }
                //get mean of option values
                o1 = option.Average(); o2 = o2 / trial; o3 = o3 / trial; o4 = o4 / trial; o5 = o5 / trial; o6 = o6 / trial; o7 = o7 / trial; o8 = o8 / trial;
                double delta = (o2 - o3) / (2 * deltas); //get delta
                double gamma = (o2 + o3 - o1 * 2) / (deltas * deltas); //get gamma
                double vega = (o4 - o7) / (2 * deltavol);//get vega
                double rho = (o5 - o8) / (2 * deltar);//get rho
                double theta = (o6 - o1) / deltat;//get theta
                if (antithetic)//use antithetic method
                {
                    double ase = antise(option, corenumber);
                    double[] a = { delta, gamma, vega, rho, theta, o1, ase };
                    return a;
                }
                else//not use antithetic method
                {
                    double se = SE(option);
                    double[] a = { delta, gamma, vega, rho, theta, o1, se };
                    return a;
                }
            }

            else//not cv method
            {
                if (calloption)
                {
                    double[] option = call(strike, stockprice, rate, vol, step, 8 * trial, time, _matrix, 0);
                    //get options under different parameters
                    double o1 = option.Average();
                    double o2 = call(strike, stockprice + deltas, rate, vol, step, 8 * trial, time, _matrix, 1).Average();
                    double o3 = call(strike, stockprice - deltas, rate, vol, step, 8 * trial, time, _matrix, 2).Average();
                    double o4 = call(strike, stockprice, rate, vol + deltavol, step, 8 * trial, time, _matrix, 3).Average();
                    double o5 = call(strike, stockprice, rate + deltar, vol, step, 8 * trial, time, _matrix, 4).Average();
                    double o6 = call(strike, stockprice, rate, vol, step, 8 * trial, time + deltat, _matrix, 5).Average();
                    double o7 = call(strike, stockprice, rate, vol - deltavol, step, 8 * trial, time, _matrix, 6).Average();
                    double o8 = call(strike, stockprice, rate - deltar, vol, step, 8 * trial, time, _matrix, 7).Average();
                    double delta = (o2 - o3) / (2 * deltas); //get delta
                    double gamma = (o2 + o3 - o1 * 2) / (deltas * deltas); //get gamma
                    double vega = (o4 - o7) / (2 * deltavol);//get vega
                    double rho = (o5 - o8) / (2 * deltar);//get rho
                    double theta = (o6 - o1) / deltat;//get theta
                    if (antithetic)//use antithetic method
                    {
                        double ase = antise(option, corenumber);
                        double[] a = { delta, gamma, vega, rho, theta, o1, ase };
                        return a;
                    }
                    else//not use antithetic method
                    {
                        double se = SE(option);
                        double[] a = { delta, gamma, vega, rho, theta, o1, se };
                        return a;
                    }
                }
                else //if option is put
                {
                    //get options under different parameters
                    double[] option = put(strike, stockprice, rate, vol, step, 8 * trial, time, _matrix, 0);
                    double o1 = option.Average();
                    double o2 = put(strike, stockprice + deltas, rate, vol, step, 8 * trial, time, _matrix, 1).Average();
                    double o3 = put(strike, stockprice - deltas, rate, vol, step, 8 * trial, time, _matrix, 2).Average();
                    double o4 = put(strike, stockprice, rate, vol + deltavol, step, 8 * trial, time, _matrix, 3).Average();
                    double o5 = put(strike, stockprice, rate + deltar, vol, step, 8 * trial, time, _matrix, 4).Average();
                    double o6 = put(strike, stockprice, rate, vol, step, 8 * trial, time + deltat, _matrix, 5).Average();
                    double o7 = put(strike, stockprice, rate, vol - deltavol, step, 8 * trial, time, _matrix, 6).Average();
                    double o8 = put(strike, stockprice, rate - deltar, vol, step, 8 * trial, time, _matrix, 7).Average();
                    double delta = (o2 - o3) / (2 * deltas); //get delta
                    double gamma = (o2 + o3 - o1 * 2) / (deltas * deltas); //get gamma
                    double vega = (o4 - o7) / (2 * deltavol);//get vega
                    double rho = (o5 - o8) / (2 * deltar);//get rho
                    double theta = (o6 - o1) / deltat;//get theta
                    if (antithetic) //use antithetic method
                    {
                        double ase = antise(option, corenumber);
                        double[] a = { delta, gamma, vega, rho, theta, o1, ase };
                        return a;
                    }
                    else //not use antithetic method
                    {
                        double se = SE(option);
                        double[] a = { delta, gamma, vega, rho, theta, o1, se };
                        return a;
                    }
                }//put option
            }//not use control variate method
        }

        public static double[,] pricematrix(double strike, double stockprice, double rate, double vol, int step, int trial, double time, double[,] randommatrix)

        {
            double deltas = 0.001 * stockprice;
            double deltavol = 0.001 * vol;
            //double deltat = time / step;
            double deltar = 0.001 * rate;
            double[,] price = new double[8 * trial, step];
            double deltat = time / step; int k = 0;
            for (int i = 0; i < 8 * trial; i = i + 8)
            {
                price[i, 0] = stockprice;
                price[i + 1, 0] = stockprice + deltas;
                price[i + 2, 0] = stockprice - deltas;
                price[i + 3, 0] = stockprice; price[i + 4, 0] = stockprice; price[i + 5, 0] = stockprice; price[i + 6, 0] = stockprice; price[i + 7, 0] = stockprice;
                //use MC method to simulate the last stock price
                for (int j = 1; j < step; j++)
                {
                    price[i, j] = price[i, j - 1] * Math.Exp((rate - 0.5 * vol * vol) * deltat + vol * Math.Sqrt(deltat) * randommatrix[k, j]);
                    price[i + 1, j] = price[i + 1, j - 1] * Math.Exp((rate - 0.5 * vol * vol) * deltat + vol * Math.Sqrt(deltat) * randommatrix[k, j]);
                    price[i + 2, j] = price[i + 2, j - 1] * Math.Exp((rate - 0.5 * vol * vol) * deltat + vol * Math.Sqrt(deltat) * randommatrix[k, j]);
                    price[i + 3, j] = price[i + 3, j - 1] * Math.Exp((rate - 0.5 * (vol + deltavol) * (vol + deltavol)) * deltat + (vol + deltavol) * Math.Sqrt(deltat) * randommatrix[k, j]);
                    price[i + 4, j] = price[i + 4, j - 1] * Math.Exp((rate + deltar - 0.5 * vol * vol) * deltat + vol * Math.Sqrt(deltat) * randommatrix[k, j]);
                    price[i + 5, j] = price[i + 5, j - 1] * Math.Exp((rate - 0.5 * vol * vol) * (time + deltat) / step + vol * Math.Sqrt((time + deltat) / step) * randommatrix[k, j]);
                    price[i + 6, j] = price[i + 6, j - 1] * Math.Exp((rate - 0.5 * (vol - deltavol) * (vol - deltavol)) * deltat + (vol - deltavol) * Math.Sqrt(deltat) * randommatrix[k, j]);
                    price[i + 7, j] = price[i + 7, j - 1] * Math.Exp((rate - deltar - 0.5 * vol * vol) * deltat + vol * Math.Sqrt(deltat) * randommatrix[k, j]);

                }
                k = k + 1;
            }
            return price;
        }
        public double SE(double[] option) //method to compute standard error
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
        public double antise(double[] option, int corenumber) //get standard error using antithetic method
        {
            int len = option.Length;

            if (len % 2 == 0) //if trial number is even
            {
                int lastnumber = len / corenumber + len % corenumber;//different thread's paired option prices' indexes are different
                double[] list = new double[len / 2];
                for (int j = 0; j < corenumber - 1; j++)
                {
                    for (int i = 0; i < (len / corenumber) / 2; i++)
                    {
                        list[i + j * corenumber / 2] = 0.5 * (option[i + j * corenumber] + option[i + len / corenumber / 2 + j * corenumber]); //get mean of paired random numbers
                    }
                }
                for (int i = (len - lastnumber) / 2; i < len / 2; i++)
                {
                    list[i] = 0.5 * (option[i] + option[i + lastnumber / 2]);
                }
                double mean = list.Average();
                double sum = 0;
                for (int i = 0; i < list.Length; i++)
                {
                    sum = sum + Math.Pow(list[i] - mean, 2);
                }
                double se = Math.Sqrt(sum / (list.Length * list.Length)); //get standard error
                return se;
            }
            else  //if trial number is odd
            {
                if (len % corenumber == 1) //len%corenumber is odd
                {
                    int lastnumber = len / corenumber + len % corenumber; //different thread's paired option prices' indexes are different
                    double[] list = new double[(len + 1) / 2];
                    for (int j = 0; j < corenumber - 1; j++)
                    {
                        for (int i = 0; i < (len / corenumber - 1) / 2; i++)
                        {
                            list[i + j * corenumber / 2] = 0.5 * (option[i + j * corenumber] + option[i + (1 + len / corenumber) / 2 + j * corenumber]);//get mean of paired random numbers
                        }
                        list[(len / corenumber - 1) / 2 + j * len / corenumber] = option[(len / corenumber - 1) / 2 + j * len / corenumber];
                    }
                    for (int i = (len - lastnumber) / 2; i < len / 2; i++)
                    {
                        list[i] = 0.5 * (option[i] + option[i + lastnumber / 2]);
                    }

                    double mean = list.Average();
                    double sum = 0;
                    for (int i = 0; i < list.Length; i++)
                    {
                        sum = sum + Math.Pow(list[i] - mean, 2);
                    }
                    double se = Math.Sqrt(sum / (list.Length * list.Length));
                    return se;
                }
                else // len%corenumber is even
                {
                    int lastnumber = len / corenumber + len % corenumber; //different thread's paired option prices' indexes are different
                    double[] list = new double[(len + 1) / 2];
                    for (int j = 0; j < corenumber - 1; j++)
                    {
                        for (int i = 0; i < (len / corenumber) / 2; i++)
                        {
                            list[i + j * corenumber / 2] = 0.5 * (option[i + j * corenumber] + option[i + len / corenumber / 2 + j * corenumber]); //get mean of paired random numbers
                        }
                    }
                    for (int i = (len - lastnumber) / 2; i < len / 2; i++)
                    { list[i] = 0.5 * (option[i] + option[i + (1 + len / corenumber) / 2]); }//get mean of paired random numbers}
                    list[(len - lastnumber) / 2 + lastnumber / 2 + 1] = option[(len - lastnumber) + lastnumber / 2];

                    double mean = list.Average();
                    double sum = 0;
                    for (int i = 0; i < list.Length; i++)
                    {
                        sum = sum + Math.Pow(list[i] - mean, 2);
                    }
                    double se = Math.Sqrt(sum / (list.Length * list.Length));
                    return se;
                }
            }
        }
        //use method eco and epo to get option price under different parameters by changing position.
        public virtual double[] call(double strike, double stockprice, double rate, double vol, int step, int trial, double time, double[,] matrix, int position)
        {
            double[] call = new double[trial / 8];
            int j = 0;
            for (int i = position; i < trial; i = i + 8)
            {
                call[j] = Math.Max(matrix[i, step - 1] - strike, 0) * Math.Exp(-rate * time); //compute option value at t=0 in every simulation
                j = j + 1;
            }
            return call;
        }
        public virtual double[] put(double strike, double stockprice, double rate, double vol, int step, int trial, double time, double[,] matrix, int position)
        {
            double[] put = new double[trial / 8];
            int j = 0;
            for (int i = position; i < trial; i = i + 8)
            {
                put[j] = Math.Max(-matrix[i, step - 1] + strike, 0) * Math.Exp(-rate * time); //compute option value at t=0 in every simulation
                j = j + 1;
            }
            return put;
        }
        //use CV method to get call and put in control variate
        public virtual double[] CVmethod(double strike, double stockprice, double rate, double vol, int step, int trial, double time, double[,] pricematrix, bool calloption)
        {
            double deltas = 0.001 * stockprice;
            double deltavol = 0.001 * vol;
            double deltar = 0.001 * rate;
            double dt = time / step; double t;
            CDFmethod cdf = new CDFmethod();  //compute cdf 
            double[] ct = new double[8 * trial];//define a list to save option price 
            for (int i = 0; i < 8 * trial; i = i + 8)
            {
                double cv1 = 0;//define delta hedge
                double cv2 = 0; double cv3 = 0; double cv4 = 0; double cv5 = 0; double cv6 = 0; double cv7 = 0; double cv8 = 0;
                for (int j = 1; j < step; j++)
                {
                    t = dt * (step - j);
                    //get d1 of bs formula
                    double d11 = (Math.Log(pricematrix[i, j - 1] / strike) + (rate + Math.Pow(vol, 2) / 2) * t) / (vol * Math.Pow(t, 0.5));
                    double d12 = (Math.Log(pricematrix[i + 1, j - 1] / strike) + (rate + Math.Pow(vol, 2) / 2) * t) / (vol * Math.Pow(t, 0.5));
                    double d13 = (Math.Log(pricematrix[i + 2, j - 1] / strike) + (rate + Math.Pow(vol, 2) / 2) * t) / (vol * Math.Pow(t, 0.5));
                    double d14 = (Math.Log(pricematrix[i + 3, j - 1] / strike) + (rate + Math.Pow(vol + deltavol, 2) / 2) * t) / ((vol + deltavol) * Math.Pow(t, 0.5));
                    double d15 = (Math.Log(pricematrix[i + 4, j - 1] / strike) + (rate + deltar + Math.Pow(vol, 2) / 2) * t) / (vol * Math.Pow(t, 0.5));
                    double d16 = (Math.Log(pricematrix[i + 5, j - 1] / strike) + (rate + Math.Pow(vol, 2) / 2) * (t * step + dt) / step) / (vol * Math.Pow((t * step + dt) / step, 0.5));
                    double d17 = (Math.Log(pricematrix[i + 6, j - 1] / strike) + (rate + Math.Pow(vol - deltavol, 2) / 2) * t) / ((vol - deltavol) * Math.Pow(t, 0.5));
                    double d18 = (Math.Log(pricematrix[i + 7, j - 1] / strike) + (rate - deltar + Math.Pow(vol, 2) / 2) * t) / (vol * Math.Pow(t, 0.5));
                    //get delta
                    if (calloption)
                    {
                        double delta1 = cdf.CumDensity(d11); double delta2 = cdf.CumDensity(d12);
                        double delta3 = cdf.CumDensity(d13); double delta4 = cdf.CumDensity(d14);
                        double delta5 = cdf.CumDensity(d15); double delta6 = cdf.CumDensity(d16);
                        double delta7 = cdf.CumDensity(d17); double delta8 = cdf.CumDensity(d18);
                        //get delta hedge
                        cv1 = cv1 + delta1 * (pricematrix[i, j] - pricematrix[i, j - 1] * Math.Exp(rate * dt));
                        cv2 = cv2 + delta2 * (pricematrix[i + 1, j] - pricematrix[i + 1, j - 1] * Math.Exp(rate * dt));
                        cv3 = cv3 + delta3 * (pricematrix[i + 2, j] - pricematrix[i + 2, j - 1] * Math.Exp(rate * dt));
                        cv4 = cv4 + delta4 * (pricematrix[i + 3, j] - pricematrix[i + 3, j - 1] * Math.Exp(rate * dt));
                        cv5 = cv5 + delta5 * (pricematrix[i + 4, j] - pricematrix[i + 4, j - 1] * Math.Exp((rate + deltar) * dt));
                        cv6 = cv6 + delta6 * (pricematrix[i + 5, j] - pricematrix[i + 5, j - 1] * Math.Exp(rate * (dt + dt / step)));
                        cv7 = cv7 + delta7 * (pricematrix[i + 6, j] - pricematrix[i + 6, j - 1] * Math.Exp(rate * dt));
                        cv8 = cv8 + delta8 * (pricematrix[i + 7, j] - pricematrix[i + 7, j - 1] * Math.Exp((rate - deltar) * dt));
                    }
                    else
                    {
                        double delta1 = cdf.CumDensity(d11) - 1; double delta2 = cdf.CumDensity(d12) - 1;
                        double delta3 = cdf.CumDensity(d13) - 1; double delta4 = cdf.CumDensity(d14) - 1;
                        double delta5 = cdf.CumDensity(d15) - 1; double delta6 = cdf.CumDensity(d16) - 1;
                        double delta7 = cdf.CumDensity(d17) - 1; double delta8 = cdf.CumDensity(d18) - 1;
                        //get delta hedge
                        cv1 = cv1 + delta1 * (pricematrix[i, j] - pricematrix[i, j - 1] * Math.Exp(rate * dt));
                        cv2 = cv2 + delta2 * (pricematrix[i + 1, j] - pricematrix[i + 1, j - 1] * Math.Exp(rate * dt));
                        cv3 = cv3 + delta3 * (pricematrix[i + 2, j] - pricematrix[i + 2, j - 1] * Math.Exp(rate * dt));
                        cv4 = cv4 + delta4 * (pricematrix[i + 3, j] - pricematrix[i + 3, j - 1] * Math.Exp(rate * dt));
                        cv5 = cv5 + delta5 * (pricematrix[i + 4, j] - pricematrix[i + 4, j - 1] * Math.Exp((rate + deltar) * dt));
                        cv6 = cv6 + delta6 * (pricematrix[i + 5, j] - pricematrix[i + 5, j - 1] * Math.Exp(rate * (dt + dt / step)));
                        cv7 = cv7 + delta7 * (pricematrix[i + 6, j] - pricematrix[i + 6, j - 1] * Math.Exp(rate * dt));
                        cv8 = cv8 + delta8 * (pricematrix[i + 7, j] - pricematrix[i + 7, j - 1] * Math.Exp((rate - deltar) * dt));
                    }
                }
                if(calloption) //get optionprice
                { 
                        ct[i] = (Math.Max(0, pricematrix[i, step - 1] - strike) - cv1) * Math.Exp(-rate * time);
                        ct[i + 1] = (Math.Max(0, pricematrix[i + 1, step - 1] - strike) - cv2) * Math.Exp(-rate * time);
                        ct[i + 2] = (Math.Max(0, pricematrix[i + 2, step - 1] - strike) - cv3) * Math.Exp(-rate * time);
                        ct[i + 3] = (Math.Max(0, pricematrix[i + 3, step - 1] - strike) - cv4) * Math.Exp(-rate * time);
                        ct[i + 4] = (Math.Max(0, pricematrix[i + 4, step - 1] - strike) - cv5) * Math.Exp(-(rate + deltar) * time);
                        ct[i + 5] = (Math.Max(0, pricematrix[i + 5, step - 1] - strike) - cv6) * Math.Exp(-rate * (time + dt));
                        ct[i + 6] = (Math.Max(0, pricematrix[i + 6, step - 1] - strike) - cv7) * Math.Exp(-rate * time);
                        ct[i + 7] = (Math.Max(0, pricematrix[i + 7, step - 1] - strike) - cv8) * Math.Exp(-(rate - deltar) * time);
                }
                else
                {     
                        ct[i] = (Math.Max(0, -pricematrix[i, step - 1] + strike) - cv1) * Math.Exp(-rate * time);
                        ct[i + 1] = (Math.Max(0, -pricematrix[i + 1, step - 1] + strike) - cv2) * Math.Exp(-rate * time);
                        ct[i + 2] = (Math.Max(0, -pricematrix[i + 2, step - 1] + strike) - cv3) * Math.Exp(-rate * time);
                        ct[i + 3] = (Math.Max(0, -pricematrix[i + 3, step - 1] + strike) - cv4) * Math.Exp(-rate * time);
                        ct[i + 4] = (Math.Max(0, -pricematrix[i + 4, step - 1] + strike) - cv5) * Math.Exp(-(rate + deltar) * time);
                        ct[i + 5] = (Math.Max(0, -pricematrix[i + 5, step - 1] + strike) - cv6) * Math.Exp(-rate * (time + dt));
                        ct[i + 6] = (Math.Max(0, -pricematrix[i + 6, step - 1] + strike) - cv7) * Math.Exp(-rate * time);
                        ct[i + 7] = (Math.Max(0, -pricematrix[i + 7, step - 1] + strike) - cv8) * Math.Exp(-(rate - deltar) * time);
                }
            }
            return ct;
            }
            
        }
    }

        
