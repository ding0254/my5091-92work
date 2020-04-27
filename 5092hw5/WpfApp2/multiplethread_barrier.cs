using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    class multiplethread_barrier:multiplethread
    {
       private double bar; private bool isdown; private bool isout;
        public double Bar { get { return bar; } set { bar = value; } }   //get barrier
        public bool IsDown { get { return isdown; } set { isdown = value; } }   //get condition
        public bool IsOut { get { return isout; } set { isout = value; } }   //get condition
        public override double[] call(double strike, double stockprice, double rate, double vol, int step, int trial, double time, double[,] matrix, int position)
        {
            double[] call = new double[trial / 8];
            int j = 0;
            if (isdown == true)
            {
                if (isout == true)//option value is 0 when price is less than bar
                {
                    for (int i = position; i < trial; i = i + 8)
                    {
                        call[j] = Math.Max(matrix[i, step - 1] - strike, 0) * Math.Exp(-rate * time); //compute option value at t=0 in every simulation
                        for (int k = 0; k < step; k++)
                        { if (matrix[i, k] < bar) { call[j] = 0; break; } }
                        j = j + 1;
                    } }
                else //option value is not zero once price is higher than bar
                {
                    for (int i = position; i < trial; i = i + 8)
                    {
                        call[j] = 0;
                        for (int k = 0; k < step; k++)
                        { if (matrix[i, k] < bar) { call[j] = Math.Max(matrix[i, step - 1] - strike, 0) * Math.Exp(-rate * time); break; } } //compute option value at t=0 in every simulation
                        j = j + 1;
                    }
                } }
            else
            {
                if (isout == true)//option value is 0 when price is higher than bar
                {
                    for (int i = position; i < trial; i = i + 8)
                    {
                        call[j] = Math.Max(matrix[i, step - 1] - strike, 0) * Math.Exp(-rate * time); //compute option value at t=0 in every simulation
                        for (int k = 0; k < step; k++)
                        { if (matrix[i, k] > bar) { call[j] = 0; break; } }
                        j = j + 1;
                    }
                }
                else //option value is not zero once price is higher than bar
                {
                    for (int i = position; i < trial; i = i + 8)
                    {
                        call[j] = 0;
                        for (int k = 0; k < step; k++)
                        { if (matrix[i, k] > bar) { call[j] = Math.Max(matrix[i, step - 1] - strike, 0) * Math.Exp(-rate * time); break; } }//compute option value at t=0 in every simulation
                        j = j + 1;
                    }
                }
            }            
            return call;
        }
        public override double[] put(double strike, double stockprice, double rate, double vol, int step, int trial, double time, double[,] matrix, int position)
        {
            double[] put = new double[trial / 8];
            int j = 0;
            if (isdown == true)
            {
                if (isout == true)//option value is 0 when price is less than bar
                {
                    for (int i = position; i < trial; i = i + 8)
                    {
                        put[j] = Math.Max(-matrix[i, step - 1] + strike, 0) * Math.Exp(-rate * time); //compute option value at t=0 in every simulation
                        for (int k = 0; k < step; k++)
                        { if (matrix[i, k] < bar) { put[j] = 0; break; } }
                        j = j + 1;
                    }
                }
                else //option value is not zero once price is higher than bar
                {
                    for (int i = position; i < trial; i = i + 8)
                    {
                        put[j] = 0;
                        for (int k = 0; k < step; k++)
                        { if (matrix[i, k] < bar) { put[j] = Math.Max(-matrix[i, step - 1] + strike, 0) * Math.Exp(-rate * time); break; } } //compute option value at t=0 in every simulation
                        j = j + 1;
                    }
                }
            }
            else
            {
                if (isout == true)//option value is 0 when price is higher than bar
                {
                    for (int i = position; i < trial; i = i + 8)
                    {
                        put[j] = Math.Max(-matrix[i, step - 1] + strike, 0) * Math.Exp(-rate * time); //compute option value at t=0 in every simulation
                        for (int k = 0; k < step; k++)
                        { if (matrix[i, k] > bar) { put[j] = 0; break; } }
                        j = j + 1;
                    }
                }
                else //option value is not zero once price is higher than bar
                {
                    for (int i = position; i < trial; i = i + 8)
                    {
                        put[j] = 0;
                        for (int k = 0; k < step; k++)
                        { if (matrix[i, k] > bar) { put[j] = Math.Max(-matrix[i, step - 1] + strike, 0) * Math.Exp(-rate * time); break; } }//compute option value at t=0 in every simulation
                        j = j + 1;
                    }
                }
            }
            return put;
        }
        //use CV method to get call and put in control variate
        public override double[] CVmethod(double strike, double stockprice, double rate, double vol, int step, int trial, double time, double[,] pricematrix, bool calloption)
        {
            double deltas = 0.001 * stockprice;
            double deltavol = 0.001 * vol;
            double deltar = 0.001 * rate;
            double dt = time / step; double t;
            CDFmethod cdf = new CDFmethod();  //compute cdf 
            double[] ct = new double[8 * trial];//define a list to save option price 
            if (isdown == true)
            {
                if (isout == true)//option value is 0 when price is less than bar
                {
                    bool[] iszero = new bool[8];
                    for (int i = 0; i < 8 * trial; i = i + 8)
                    {
                        for (int k = i; k < 8 + i; k++) iszero[k-i] = false;//define whether satisfy conditions
                        double cv1 = 0;//define delta hedge
                        double cv2 = 0; double cv3 = 0; double cv4 = 0; double cv5 = 0; double cv6 = 0; double cv7 = 0; double cv8 = 0;
                        for (int j = 1; j < step; j++)
                        {
                            if (pricematrix[i, j] < bar) { iszero[0] = true; break; }
                            if (pricematrix[i + 1, j] < bar) { iszero[1] = true; break; }
                            if (pricematrix[i + 2, j] < bar) { iszero[2] = true; break; }
                            if (pricematrix[i + 3, j] < bar) { iszero[3] = true; break; }
                            if (pricematrix[i + 4, j] < bar) { iszero[4] = true; break; }
                            if (pricematrix[i + 5, j] < bar) { iszero[5] = true; break; }
                            if (pricematrix[i + 6, j] < bar) { iszero[6] = true; break; }
                            if (pricematrix[i + 7, j] < bar) { iszero[7] = true; break; }//determine whether meet the condition
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
                        if (calloption) //get optionprice
                        {
                            ct[i] = (Math.Max(0, pricematrix[i, step - 1] - strike)) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[0])) - cv1 * Math.Exp(-rate * time);
                            ct[i + 1] = (Math.Max(0, pricematrix[i + 1, step - 1] - strike)) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[1])) - cv2 * Math.Exp(-rate * time);
                            ct[i + 2] = (Math.Max(0, pricematrix[i + 2, step - 1] - strike)) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[2])) - cv3 * Math.Exp(-rate * time);
                            ct[i + 3] = (Math.Max(0, pricematrix[i + 3, step - 1] - strike)) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[3])) - cv4 * Math.Exp(-rate * time);
                            ct[i + 4] = (Math.Max(0, pricematrix[i + 4, step - 1] - strike)) * Math.Exp(-(rate + deltar) * time) * (1 - Convert.ToInt32(iszero[4])) - cv5 * Math.Exp(-rate * time);
                            ct[i + 5] = (Math.Max(0, pricematrix[i + 5, step - 1] - strike)) * Math.Exp(-rate * (time + dt)) * (1 - Convert.ToInt32(iszero[5])) - cv6 * Math.Exp(-rate * time);
                            ct[i + 6] = (Math.Max(0, pricematrix[i + 6, step - 1] - strike)) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[6])) - cv7 * Math.Exp(-rate * time);
                            ct[i + 7] = (Math.Max(0, pricematrix[i + 7, step - 1] - strike)) * Math.Exp(-(rate - deltar) * time) * (1 - Convert.ToInt32(iszero[7])) - cv8 * Math.Exp(-rate * time);
                        }
                        else
                        {
                            ct[i] = (Math.Max(0, -pricematrix[i, step - 1] + strike)) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[0])) - cv1 * Math.Exp(-rate * time);
                            ct[i + 1] = (Math.Max(0, -pricematrix[i + 1, step - 1] + strike)) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[1])) - cv2 * Math.Exp(-rate * time);
                            ct[i + 2] = (Math.Max(0, -pricematrix[i + 2, step - 1] + strike)) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[2])) - cv3 * Math.Exp(-rate * time);
                            ct[i + 3] = (Math.Max(0, -pricematrix[i + 3, step - 1] + strike)) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[3])) - cv4 * Math.Exp(-rate * time);
                            ct[i + 4] = (Math.Max(0, -pricematrix[i + 4, step - 1] + strike)) * Math.Exp(-(rate + deltar) * time) * (1 - Convert.ToInt32(iszero[4])) - cv5 * Math.Exp(-rate * time);
                            ct[i + 5] = (Math.Max(0, -pricematrix[i + 5, step - 1] + strike)) * Math.Exp(-rate * (time + dt)) * (1 - Convert.ToInt32(iszero[5])) - cv6 * Math.Exp(-rate * time);
                            ct[i + 6] = (Math.Max(0, -pricematrix[i + 6, step - 1] + strike)) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[6])) - cv7 * Math.Exp(-rate * time);
                            ct[i + 7] = (Math.Max(0, -pricematrix[i + 7, step - 1] + strike)) * Math.Exp(-(rate - deltar) * time) * (1 - Convert.ToInt32(iszero[7])) - cv8 * Math.Exp(-rate * time);
                        }
                    }
                }
                else //option value is not 0 once price is less than bar
                { bool[] iszero = new bool[8];
                for (int i = 0; i < 8 * trial; i = i + 8)
                {
                    for (int k = i; k < 8 + i; k++) iszero[k-i] = true;//define whether satisfy conditions
                    double cv1 = 0;//define delta hedge
                    double cv2 = 0; double cv3 = 0; double cv4 = 0; double cv5 = 0; double cv6 = 0; double cv7 = 0; double cv8 = 0;
                    for (int j = 1; j < step; j++)
                    {
                        if (pricematrix[i, j] < bar) { iszero[0] = false; break; }
                        if (pricematrix[i + 1, j] < bar) { iszero[1] = false; break; }
                        if (pricematrix[i + 2, j] < bar) { iszero[2] = false; break; }
                        if (pricematrix[i + 3, j] < bar) { iszero[3] = false; break; }
                        if (pricematrix[i + 4, j] < bar) { iszero[4] = false; break; }
                        if (pricematrix[i + 5, j] < bar) { iszero[5] = false; break; }
                        if (pricematrix[i + 6, j] < bar) { iszero[6] = false; break; }
                        if (pricematrix[i + 7, j] < bar) { iszero[7] = false; break; }//determine whether meet the condition
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
                        if (calloption) //get optionprice
                        {
                            ct[i] = (Math.Max(0, pricematrix[i, step - 1] - strike)) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[0])) - cv1 * Math.Exp(-rate * time);
                            ct[i + 1] = (Math.Max(0, pricematrix[i + 1, step - 1] - strike)) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[1])) - cv2 * Math.Exp(-rate * time);
                            ct[i + 2] = (Math.Max(0, pricematrix[i + 2, step - 1] - strike)) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[2])) - cv3 * Math.Exp(-rate * time);
                            ct[i + 3] = (Math.Max(0, pricematrix[i + 3, step - 1] - strike)) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[3])) - cv4 * Math.Exp(-rate * time);
                            ct[i + 4] = (Math.Max(0, pricematrix[i + 4, step - 1] - strike)) * Math.Exp(-(rate + deltar) * time) * (1 - Convert.ToInt32(iszero[4])) - cv5 * Math.Exp(-rate * time);
                            ct[i + 5] = (Math.Max(0, pricematrix[i + 5, step - 1] - strike)) * Math.Exp(-rate * (time + dt)) * (1 - Convert.ToInt32(iszero[5])) - cv6 * Math.Exp(-rate * time);
                            ct[i + 6] = (Math.Max(0, pricematrix[i + 6, step - 1] - strike)) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[6])) - cv7 * Math.Exp(-rate * time);
                            ct[i + 7] = (Math.Max(0, pricematrix[i + 7, step - 1] - strike)) * Math.Exp(-(rate - deltar) * time) * (1 - Convert.ToInt32(iszero[7])) - cv8 * Math.Exp(-rate * time);
                        }
                        else
                        {
                            ct[i] = (Math.Max(0, -pricematrix[i, step - 1] + strike)) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[0])) - cv1 * Math.Exp(-rate * time);
                            ct[i + 1] = (Math.Max(0, -pricematrix[i + 1, step - 1] + strike)) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[1])) - cv2 * Math.Exp(-rate * time);
                            ct[i + 2] = (Math.Max(0, -pricematrix[i + 2, step - 1] + strike)) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[2])) - cv3 * Math.Exp(-rate * time);
                            ct[i + 3] = (Math.Max(0, -pricematrix[i + 3, step - 1] + strike)) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[3])) - cv4 * Math.Exp(-rate * time);
                            ct[i + 4] = (Math.Max(0, -pricematrix[i + 4, step - 1] + strike)) * Math.Exp(-(rate + deltar) * time) * (1 - Convert.ToInt32(iszero[4])) - cv5 * Math.Exp(-rate * time);
                            ct[i + 5] = (Math.Max(0, -pricematrix[i + 5, step - 1] + strike)) * Math.Exp(-rate * (time + dt)) * (1 - Convert.ToInt32(iszero[5])) - cv6 * Math.Exp(-rate * time);
                            ct[i + 6] = (Math.Max(0, -pricematrix[i + 6, step - 1] + strike)) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[6])) - cv7 * Math.Exp(-rate * time);
                            ct[i + 7] = (Math.Max(0, -pricematrix[i + 7, step - 1] + strike)) * Math.Exp(-(rate - deltar) * time) * (1 - Convert.ToInt32(iszero[7])) - cv8 * Math.Exp(-rate * time);
                        }
                    }
            }
        }
         else
           {
              if (isout == true)//option value is 0 when price is higher than bar
                {
                    bool[] iszero = new bool[8];
                    for (int i = 0; i < 8 * trial; i = i + 8)
                    {
                        for (int k = i; k < 8 + i; k++) iszero[k-i] = false;//define whether satisfy conditions
                        double cv1 = 0;//define delta hedge
                        double cv2 = 0; double cv3 = 0; double cv4 = 0; double cv5 = 0; double cv6 = 0; double cv7 = 0; double cv8 = 0;
                        for (int j = 1; j < step; j++)
                        {
                            if (pricematrix[i, j] > bar) { iszero[0] = true; break; }
                            if (pricematrix[i + 1, j] > bar) { iszero[1] = true; break; }
                            if (pricematrix[i + 2, j] > bar) { iszero[2] = true; break; }
                            if (pricematrix[i + 3, j] > bar) { iszero[3] = true; break; }
                            if (pricematrix[i + 4, j] > bar) { iszero[4] = true; break; }
                            if (pricematrix[i + 5, j] > bar) { iszero[5] = true; break; }
                            if (pricematrix[i + 6, j] > bar) { iszero[6] = true; break; }
                            if (pricematrix[i + 7, j] > bar) { iszero[7] = true; break; }//determine whether meet the condition
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
                        if (calloption) //get optionprice
                        {
                            ct[i] = (Math.Max(0, pricematrix[i, step - 1] - strike) ) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[0]))-cv1 * Math.Exp(-rate * time);
                            ct[i + 1] = (Math.Max(0, pricematrix[i + 1, step - 1] - strike) - cv2) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[1])) - cv2 * Math.Exp(-rate * time);
                            ct[i + 2] = (Math.Max(0, pricematrix[i + 2, step - 1] - strike) - cv3) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[2])) - cv3 * Math.Exp(-rate * time);
                            ct[i + 3] = (Math.Max(0, pricematrix[i + 3, step - 1] - strike) - cv4) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[3])) - cv4 * Math.Exp(-rate * time);
                            ct[i + 4] = (Math.Max(0, pricematrix[i + 4, step - 1] - strike) - cv5) * Math.Exp(-(rate + deltar) * time) * (1 - Convert.ToInt32(iszero[4])) - cv5 * Math.Exp(-rate * time);
                            ct[i + 5] = (Math.Max(0, pricematrix[i + 5, step - 1] - strike) - cv6) * Math.Exp(-rate * (time + dt)) * (1 - Convert.ToInt32(iszero[5])) - cv6 * Math.Exp(-rate * time);
                            ct[i + 6] = (Math.Max(0, pricematrix[i + 6, step - 1] - strike) - cv7) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[6])) - cv7 * Math.Exp(-rate * time);
                            ct[i + 7] = (Math.Max(0, pricematrix[i + 7, step - 1] - strike) - cv8) * Math.Exp(-(rate - deltar) * time) * (1 - Convert.ToInt32(iszero[7])) - cv8 * Math.Exp(-rate * time);
                        }
                        else
                        {
                            ct[i] = (Math.Max(0, -pricematrix[i, step - 1] + strike) ) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[0])) - cv1 * Math.Exp(-rate * time);
                            ct[i + 1] = (Math.Max(0, -pricematrix[i + 1, step - 1] - +strike) ) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[1])) - cv2 * Math.Exp(-rate * time);
                            ct[i + 2] = (Math.Max(0, -pricematrix[i + 2, step - 1] + strike) ) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[2])) - cv3 * Math.Exp(-rate * time);
                            ct[i + 3] = (Math.Max(0, -pricematrix[i + 3, step - 1] + strike) ) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[3])) - cv4 * Math.Exp(-rate * time);
                            ct[i + 4] = (Math.Max(0, -pricematrix[i + 4, step - 1] + strike) ) * Math.Exp(-(rate + deltar) * time) * (1 - Convert.ToInt32(iszero[4])) - cv5 * Math.Exp(-rate * time);
                            ct[i + 5] = (Math.Max(0, -pricematrix[i + 5, step - 1] + strike) ) * Math.Exp(-rate * (time + dt)) * (1 - Convert.ToInt32(iszero[5])) - cv6 * Math.Exp(-rate * time);
                            ct[i + 6] = (Math.Max(0, -pricematrix[i + 6, step - 1] + strike) ) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[6])) - cv7 * Math.Exp(-rate * time);
                            ct[i + 7] = (Math.Max(0, -pricematrix[i + 7, step - 1] + strike) ) * Math.Exp(-(rate - deltar) * time) * (1 - Convert.ToInt32(iszero[7])) - cv8 * Math.Exp(-rate * time);
                        }
                    }
                }
                else//option value is not 0 once price is high than bar
                {
                    bool[] iszero = new bool[8];
                    for (int i = 0; i < 8 * trial; i = i + 8)
                    {
                        for (int k = i; k < 8 + i; k++) iszero[k-i] = true;//define whether satisfy conditions
                        double cv1 = 0;//define delta hedge
                        double cv2 = 0; double cv3 = 0; double cv4 = 0; double cv5 = 0; double cv6 = 0; double cv7 = 0; double cv8 = 0;
                        for (int j = 1; j < step; j++)
                        {
                            if (pricematrix[i, j] > bar) { iszero[0] = false; break; }
                            if (pricematrix[i + 1, j] > bar) { iszero[1] = false; break; }
                            if (pricematrix[i + 2, j] > bar) { iszero[2] = false; break; }
                            if (pricematrix[i + 3, j] > bar) { iszero[3] = false; break; }
                            if (pricematrix[i + 4, j] > bar) { iszero[4] = false; break; }
                            if (pricematrix[i + 5, j] > bar) { iszero[5] = false; break; }
                            if (pricematrix[i + 6, j] > bar) { iszero[6] = false; break; }
                            if (pricematrix[i + 7, j] > bar) { iszero[7] = false; break; }//determine whether meet the condition
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
                        if (calloption) //get optionprice
                        {
                            ct[i] = (Math.Max(0, pricematrix[i, step - 1] - strike) ) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[0])) - cv1 * Math.Exp(-rate * time);
                            ct[i + 1] = (Math.Max(0, pricematrix[i + 1, step - 1] - strike) ) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[1]))-cv2 *Math.Exp(-rate * time);
                            ct[i + 2] = (Math.Max(0, pricematrix[i + 2, step - 1] - strike)) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[2])) - cv3 * Math.Exp(-rate * time);
                            ct[i + 3] = (Math.Max(0, pricematrix[i + 3, step - 1] - strike) ) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[3])) - cv4 * Math.Exp(-rate * time);
                            ct[i + 4] = (Math.Max(0, pricematrix[i + 4, step - 1] - strike) ) * Math.Exp(-(rate + deltar) * time) * (1 - Convert.ToInt32(iszero[4])) - cv5 * Math.Exp(-rate * time);
                            ct[i + 5] = (Math.Max(0, pricematrix[i + 5, step - 1] - strike) ) * Math.Exp(-rate * (time + dt)) * (1 - Convert.ToInt32(iszero[5])) - cv6 * Math.Exp(-rate * time);
                            ct[i + 6] = (Math.Max(0, pricematrix[i + 6, step - 1] - strike) ) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[6])) - cv7 * Math.Exp(-rate * time);
                            ct[i + 7] = (Math.Max(0, pricematrix[i + 7, step - 1] - strike) ) * Math.Exp(-(rate - deltar) * time) * (1 - Convert.ToInt32(iszero[7])) - cv8 * Math.Exp(-rate * time);
                        }
                        else
                        {
                            ct[i] = (Math.Max(0, -pricematrix[i, step - 1] + strike)) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[0])) - cv1 * Math.Exp(-rate * time);
                            ct[i + 1] = (Math.Max(0, -pricematrix[i + 1, step - 1] + strike)) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[1])) - cv2 * Math.Exp(-rate * time);
                            ct[i + 2] = (Math.Max(0, -pricematrix[i + 2, step - 1] + strike)) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[2])) - cv3 * Math.Exp(-rate * time);
                            ct[i + 3] = (Math.Max(0,- pricematrix[i + 3, step - 1] + strike)) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[3])) - cv4 * Math.Exp(-rate * time);
                            ct[i + 4] = (Math.Max(0,- pricematrix[i + 4, step - 1] + strike)) * Math.Exp(-(rate + deltar) * time) * (1 - Convert.ToInt32(iszero[4])) - cv5 * Math.Exp(-rate * time);
                            ct[i + 5] = (Math.Max(0,- pricematrix[i + 5, step - 1] +strike)) * Math.Exp(-rate * (time + dt)) * (1 - Convert.ToInt32(iszero[5])) - cv6 * Math.Exp(-rate * time);
                            ct[i + 6] = (Math.Max(0,- pricematrix[i + 6, step - 1] + strike)) * Math.Exp(-rate * time) * (1 - Convert.ToInt32(iszero[6])) - cv7 * Math.Exp(-rate * time);
                            ct[i + 7] = (Math.Max(0,- pricematrix[i + 7, step - 1] + strike)) * Math.Exp(-(rate - deltar) * time) * (1 - Convert.ToInt32(iszero[7])) - cv8 * Math.Exp(-rate * time);
                        }
                    }
                }
            }
            return ct;
        }
    }
}
