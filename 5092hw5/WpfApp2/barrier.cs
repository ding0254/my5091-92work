using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    class barrier:baseoption
    {
        private double bar;private bool isdown;private bool isout;
        public double Bar { get { return bar; } set { bar = value; } }   //get barrier
        public bool IsDown { get { return isdown; } set { isdown  = value; } }   //get condition
        public bool IsOut { get { return isout; } set { isout = value; } }   //get condition
        public override double[] call(double strike, double stockprice, double rate, double vol, int step, int trial, double time, double[,] randommatrix)
        {
            double[,] price = pricematrix(strike, stockprice, rate, vol, step, trial, time, randommatrix);
            double[] call = new double[trial];
            if (isdown == true)
            {
                if (isout == true)//option value is 0 when price is less than bar
                {
                    for (int i = 0; i < trial; i++)
                    {
                        call[i] = Math.Max(price[i, step - 1] - strike, 0) * Math.Exp(-rate * time); //compute option value at t=0 in every simulation
                        for (int j = 0; j < step; j++)
                        { if (price[i, j] < bar) { call[i] = 0; break; } }
                    }
                }
                else//option value is not zero when price is less than bar
                {
                    for (int i = 0; i < trial; i++)
                    {
                        call[i] = 0; //compute option value at t=0 in every simulation
                        for (int j = 0; j < step; j++)
                        { if (price[i, j] < bar) { call[i] = Math.Max(price[i, step - 1] - strike, 0) * Math.Exp(-rate * time); break; } }
                    }
                }
            }
            else
            {
                if (isout == true)//option value is 0 when price is more than bar
                {
                    for (int i = 0; i < trial; i++)
                    {
                        call[i] = Math.Max(price[i, step - 1] - strike, 0) * Math.Exp(-rate * time); //compute option value at t=0 in every simulation
                        for (int j = 0; j < step; j++)
                        { if (price[i, j] > bar) { call[i] = 0; break; } }
                    }
                }
                else//option value is not zero when price is more than bar
                {
                    for (int i = 0; i < trial; i++)
                    {
                        call[i] = 0; //compute option value at t=0 in every simulation
                        for (int j = 0; j < step; j++)
                        { if (price[i, j] > bar) { call[i] = Math.Max(price[i, step - 1] - strike, 0) * Math.Exp(-rate * time); break; } }
                    }
                }
            }
            return call;
        }
        //method to get barrier put option value list
        public override double[] put(double strike, double stockprice, double rate, double vol, int step, int trial, double time, double[,] randommatrix)
        {
            double[,] price = pricematrix(strike, stockprice, rate, vol, step, trial, time, randommatrix);
            double[] put = new double[trial];
            if (isdown == true)
            {
                if (isout == true)//option value is 0 when price is less than bar
                {
                    for (int i = 0; i < trial; i++)
                    {
                        put[i] = Math.Max(-price[i, step - 1] + strike, 0) * Math.Exp(-rate * time); //compute option value at t=0 in every simulation
                        for (int j = 0; j < step; j++)
                        { if (price[i, j] < bar) { put[i] = 0; break; } }
                    }
                }
                else//option value is not zero when price is less than bar
                {
                    for (int i = 0; i < trial; i++)
                    {
                        put[i] = 0; //compute option value at t=0 in every simulation
                        for (int j = 0; j < step; j++)
                        { if (price[i, j] < bar) { put[i] = Math.Max(-price[i, step - 1] + strike, 0) * Math.Exp(-rate * time); break; } }
                    }
                }
            }
            else
            {
                if (isout == true)//option value is 0 when price is more than bar
                {
                    for (int i = 0; i < trial; i++)
                    {
                        put[i] = Math.Max(-price[i, step - 1] + strike, 0) * Math.Exp(-rate * time); //compute option value at t=0 in every simulation
                        for (int j = 0; j < step; j++)
                        { if (price[i, j] > bar) { put[i] = 0; break; } }
                    }
                }
                else//option value is not zero when price is more than bar
                {
                    for (int i = 0; i < trial; i++)
                    {
                        put[i] = 0; //compute option value at t=0 in every simulation
                        for (int j = 0; j < step; j++)
                        { if (price[i, j] > bar) { put[i] = Math.Max(-price[i, step - 1] + strike, 0) * Math.Exp(-rate * time); break; } }
                    }
                }
            }
            return put;
        }
        public override double[] CVcall(double strike, double stockprice, double rate, double vol, int step, int trial, double time, double[,] randommatrix)
        {
            double[,] price = pricematrix(strike, stockprice, rate, vol, step, trial, time, randommatrix);

            double dt = time / step; double t;
            CDFmethod cdf = new CDFmethod();  //compute cdf 
            double[] ct = new double[trial];//define a list to save option price and standard error 
            if (isdown == true)
            {
                if (isout == true)//option value is 0 when price is less than bar
                {
                    for (int i = 0; i < trial; i++)
                    {
                        bool iszero = false;//define whether satisfy conditions
                        ct[i] = 0;
                        double cv = 0;//define delta hedge                       
                        for (int j = 1; j < step; j++)
                        {
                            if (price[i, j] < bar) { iszero = true; break; }
                            t = dt * (step - j);
                            double d1 = (Math.Log(price[i, j - 1] / strike) + (rate + Math.Pow(vol, 2) / 2) * t) / (vol * Math.Pow(t, 0.5));
                            double delta = cdf.CumDensity(d1);
                            cv = cv + delta * (price[i, j] - price[i, j - 1] * Math.Exp(rate * dt));
                        }
                        if(iszero==false) ct[i] = (Math.Max(0, price[i, step - 1] - strike) - cv) * Math.Exp(-rate * time);
                        else ct[i] = -cv * Math.Exp(-rate * time);
                    }
                }
                else//option value is not zero when price is less than bar
                {
                    for (int i = 0; i < trial; i++)
                    {
                        ct[i] = 0;
                        bool iszero = true;//define whether satisfy conditions
                        double cv = 0;//define delta hedge
                        for (int j = 1; j < step; j++)
                        {
                            if (price[i, j] < bar) iszero = false;
                            t = dt * (step - j);
                            double d1 = (Math.Log(price[i, j - 1] / strike) + (rate + Math.Pow(vol, 2) / 2) * t) / (vol * Math.Pow(t, 0.5));
                            double delta = cdf.CumDensity(d1);
                            cv = cv + delta * (price[i, j] - price[i, j - 1] * Math.Exp(rate * dt));
                        }
                        if(iszero==false) ct[i] = (Math.Max(0, price[i, step - 1] - strike) - cv) * Math.Exp(-rate * time);
                        else ct[i] = -cv * Math.Exp(-rate * time);
                    }
                }
            }
            else
            {
                if (isout == true)//option value is 0 when price is more than bar
                {
                    for (int i = 0; i < trial; i++)
                    {
                        bool iszero = false;//define whether satisfy conditions
                        ct[i] = 0;
                        double cv = 0;//define delta hedge                       
                        for (int j = 1; j < step; j++)
                        {
                            if (price[i, j] > bar) { iszero = true; break; }
                            t = dt * (step - j);
                            double d1 = (Math.Log(price[i, j - 1] / strike) + (rate + Math.Pow(vol, 2) / 2) * t) / (vol * Math.Pow(t, 0.5));
                            double delta = cdf.CumDensity(d1) ;
                            cv = cv + delta * (price[i, j] - price[i, j - 1] * Math.Exp(rate * dt));
                        }
                        if (iszero == false)
                            ct[i] = (Math.Max(0, price[i, step - 1] -strike) - cv) * Math.Exp(-rate * time);
                        else ct[i] = -cv * Math.Exp(-rate * time);
                    }
                }
                else//option value is not zero when price is more than bar
                {
                    for (int i = 0; i < trial; i++)
                    {
                        ct[i] = 0;
                        bool iszero = true;//define whether satisfy conditions
                        double cv = 0;//define delta hedge
                        for (int j = 1; j < step; j++)
                        {
                            if (price[i, j] > bar) iszero = false;
                            t = dt * (step - j);
                            double d1 = (Math.Log(price[i, j - 1] / strike) + (rate + Math.Pow(vol, 2) / 2) * t) / (vol * Math.Pow(t, 0.5));
                            double delta = cdf.CumDensity(d1);
                            cv = cv + delta * (price[i, j] - price[i, j - 1] * Math.Exp(rate * dt));
                        }
                        if (iszero == false) ct[i] = (Math.Max(0, price[i, step - 1] - strike) - cv) * Math.Exp(-rate * time);
                        else ct[i] = -cv * Math.Exp(-rate * time);
                    }
                }
            }           
            return ct;
        }
        public override double[] CVput(double strike, double stockprice, double rate, double vol, int step, int trial, double time, double[,] randommatrix)
        {
            double[,] price = pricematrix(strike, stockprice, rate, vol, step, trial, time, randommatrix);
            double dt = time / step; double t;
            CDFmethod cdf = new CDFmethod();
            double[] ct = new double[trial];//define a list to save option price and standard error
            if (isdown == true)
            {
                if (isout == true)//option value is 0 when price is less than bar
                {
                    for (int i = 0; i < trial; i++)
                    {
                        bool iszero = false;//define whether satisfy conditions
                        ct[i] = 0;
                        double cv = 0;//define delta hedge                       
                        for (int j = 1; j < step; j++)
                        {
                            if (price[i, j] < bar) { iszero = true; break; }
                            t = dt * (step - j);
                            double d1 = (Math.Log(price[i, j - 1] / strike) + (rate + Math.Pow(vol, 2) / 2) * t) / (vol * Math.Pow(t, 0.5));
                            double delta = cdf.CumDensity(d1)-1;
                            cv = cv + delta * (price[i, j] - price[i, j - 1] * Math.Exp(rate * dt));
                        }
                        if (iszero == false) ct[i] = (Math.Max(0, -price[i, step - 1] + strike) - cv) * Math.Exp(-rate * time);
                        else ct[i] = -cv * Math.Exp(-rate * time);
                    }
                }
                else//option value is not zero when price is less than bar
                {
                    for (int i = 0; i < trial; i++)
                    {
                        ct[i] = 0;
                        bool iszero = true;//define whether satisfy conditions
                        double cv = 0;//define delta hedge
                        for (int j = 1; j < step; j++)
                        {
                            if (price[i, j] < bar) iszero = false;
                            t = dt * (step - j);
                            double d1 = (Math.Log(price[i, j - 1] / strike) + (rate + Math.Pow(vol, 2) / 2) * t) / (vol * Math.Pow(t, 0.5));
                            double delta = cdf.CumDensity(d1)-1;
                            cv = cv + delta * (price[i, j] - price[i, j - 1] * Math.Exp(rate * dt));
                        }
                        if (iszero == false) ct[i] = (Math.Max(0, -price[i, step - 1] + strike) - cv) * Math.Exp(-rate * time);
                        else ct[i]=-cv* Math.Exp(-rate * time);
                    }
                }
            }
            else
            {
                if (isout == true)//option value is 0 when price is more than bar
                {
                    for (int i = 0; i < trial; i++)
                    {
                        bool iszero = false;//define whether satisfy conditions
                        ct[i] = 0;
                        double cv = 0;//define delta hedge                       
                        for (int j = 1; j < step; j++)
                        {
                            if (price[i, j] > bar) { iszero = true; break; }
                            t = dt * (step - j);
                            double d1 = (Math.Log(price[i, j - 1] / strike) + (rate + Math.Pow(vol, 2) / 2) * t) / (vol * Math.Pow(t, 0.5));
                            double delta = cdf.CumDensity(d1)-1;
                            cv = cv + delta * (price[i, j] - price[i, j - 1] * Math.Exp(rate * dt));
                        }
                        if (iszero == false) 
                        ct[i] = (Math.Max(0, -price[i, step - 1] + strike) - cv) * Math.Exp(-rate * time);
                        else ct[i] = -cv * Math.Exp(-rate * time);
                    }
                }
                else//option value is not zero when price is more than bar
                {
                    for (int i = 0; i < trial; i++)
                    {
                        ct[i] = 0;
                        bool iszero = true;//define whether satisfy conditions
                        double cv = 0;//define delta hedge
                        for (int j = 1; j < step; j++)
                        {
                            if (price[i, j] > bar) iszero = false;
                            t = dt * (step - j);
                            double d1 = (Math.Log(price[i, j - 1] / strike) + (rate + Math.Pow(vol, 2) / 2) * t) / (vol * Math.Pow(t, 0.5));
                            double delta = cdf.CumDensity(d1)-1;
                            cv = cv + delta * (price[i, j] - price[i, j - 1] * Math.Exp(rate * dt));
                        }
                        if (iszero == false) ct[i] = (Math.Max(0, -price[i, step - 1] + strike) - cv) * Math.Exp(-rate * time);
                        else ct[i] = -cv * Math.Exp(-rate * time);
                    }
                }
            }
            return ct;
        }
    }
}
