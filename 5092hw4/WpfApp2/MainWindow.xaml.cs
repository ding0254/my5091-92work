using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
   
    public partial class MainWindow : Window
    {
        Stopwatch watch = new Stopwatch();
        int progress = 0;
        public delegate void incrementprogress();
        public incrementprogress mydelegate;
       
        private void dowork()
        {
            while(progress<100)
            {
                Thread.Sleep(50);
                progress++;
                progressbar.Dispatcher.Invoke(mydelegate);
            }
        }
        private void updateui()
        {
            progressbar.Value= progress;
        }
        
        public MainWindow()
        {
            
            InitializeComponent();
            mydelegate = new incrementprogress(updateui);
        }
        
        private void button_Click(object sender, RoutedEventArgs e)
        {
            try     //error handle
            {
                progress = 0;
                watch.Reset();
                watch.Start();
                Thread t1 = new Thread(new ThreadStart(dowork));t1.Start();
                double stockprice = Convert.ToDouble(textBox_S.Text); //get stock price from textbox
                double strike = Convert.ToDouble(textBox_K.Text); // get strike price from textbox
                double time = Convert.ToDouble(textBox_T.Text);  //get tenor from textbox
                double sigma = Convert.ToDouble(textBox_sigma.Text); //get volitality from textbox
                double r = Convert.ToDouble(textBox_r.Text);  //get interest rate from textbox
                int step = Convert.ToInt32(textBox_step.Text); //get num of steps of one simulation
                int trial = Convert.ToInt32(textBox_trial.Text); //get num of trials
                int core = System.Environment.ProcessorCount; //get num of core
                textBox_time_core.Text = Convert.ToString(core);
                option a = new option();
                randomnumber b = new randomnumber();
                a.Strike = strike;
                a.Stockprice = stockprice;
                a.Step = step;
                a.Trial = trial;
                a.Time = time;
                a.Vol = sigma;
                a.Rate = r;
                double[,] rmatrix = new double[step, trial];
                if (mutiple.IsChecked == true) //use mutiple thread
                {
                    mutiplethread thread = new mutiplethread();
                    thread.Strike = strike;
                    thread.Stockprice = stockprice;
                    thread.Step = step;
                    thread.Trial = trial;
                    thread.Time = time;
                    thread.Vol = sigma;
                    thread.Rate = r;              
                    thread.getlength();
                    bool boolantithetic = (antithetic.IsChecked==true);
                    bool boolcvmethod = (control_variate.IsChecked == true);
                    bool boolcall=true;
                    if(callButton.IsChecked==true)
                    {  boolcall = (callButton.IsChecked == true); }
                    else if(putButton1.IsChecked==true)
                    {  boolcall = (putButton1.IsChecked == false); }
                    else
                    { MessageBox.Show("optiontype can't be null"); }
                    //create a list1 to save output
                    double[] list1 = thread.threadpractice(strike, stockprice, r, sigma, step, trial, core, time, boolantithetic, boolcvmethod, boolcall);
                    textBox_delta.Text = Convert.ToString(list1[0]);
                    textBox_gamma.Text = Convert.ToString(list1[1]);
                    textBox_vega.Text = Convert.ToString(list1[2]);
                    textBox_rho.Text = Convert.ToString(list1[3]);
                    textBox_theta.Text = Convert.ToString(list1[4]);
                    textBox_Price.Text = Convert.ToString(list1[5]);
                    textBox_se.Text = Convert.ToString(list1[6]);
                }
                else //not use mutiple thread
                {
                    if (antithetic.IsChecked == true)
                    { rmatrix = b.antitheticgenerator(step, trial); }//use antithetic method to get random matrix
                    else
                    { rmatrix = b.randommatrixgenerator(step, trial); }//get random matrix
                    a.Randommatrix = rmatrix;
                    if (callButton.IsChecked == true)  //get the option type if option is a call                    
                    {
                        if (control_variate.IsChecked == false)//if not use cv method
                        {
                            double[] list1 = a.callgreek();  //get result 
                                                             //output result to GUI
                            textBox_delta.Text = Convert.ToString(list1[0]);
                            textBox_gamma.Text = Convert.ToString(list1[1]);
                            textBox_vega.Text = Convert.ToString(list1[2]);
                            textBox_rho.Text = Convert.ToString(list1[3]);
                            textBox_theta.Text = Convert.ToString(list1[4]);
                            textBox_Price.Text = Convert.ToString(list1[5]);
                            if (antithetic.IsChecked == true) //if use antithetic variance reduction
                                textBox_se.Text = Convert.ToString(list1[7]);
                            else
                                textBox_se.Text = Convert.ToString(list1[6]);//if use traditional way to produce random number
                        }
                        else //use the cv method
                        {
                            double[] list1 = a.CVcallgreek();  //get result 
                                                               //output result to GUI
                            textBox_delta.Text = Convert.ToString(list1[0]);
                            textBox_gamma.Text = Convert.ToString(list1[1]);
                            textBox_vega.Text = Convert.ToString(list1[2]);
                            textBox_rho.Text = Convert.ToString(list1[3]);
                            textBox_theta.Text = Convert.ToString(list1[4]);
                            textBox_Price.Text = Convert.ToString(list1[5]);
                            if (antithetic.IsChecked == true) //if use antithetic variance reduction
                                textBox_se.Text = Convert.ToString(list1[7]);
                            else
                                textBox_se.Text = Convert.ToString(list1[6]);//if use traditional way to produce random number
                        }
                    }
                    else if (putButton1.IsChecked == true)  //if option is a put
                    {
                        if (control_variate.IsChecked == false) //if not use the cv method
                        {
                            double[] list1 = a.putgreek();
                            //output result to GUI
                            textBox_delta.Text = Convert.ToString(list1[0]);
                            textBox_gamma.Text = Convert.ToString(list1[1]);
                            textBox_vega.Text = Convert.ToString(list1[2]);
                            textBox_rho.Text = Convert.ToString(list1[3]);
                            textBox_theta.Text = Convert.ToString(list1[4]);
                            textBox_Price.Text = Convert.ToString(list1[5]);
                            if (antithetic.IsChecked == true)
                                textBox_se.Text = Convert.ToString(list1[7]);
                            else
                                textBox_se.Text = Convert.ToString(list1[6]);
                        }
                        else  //if use the cv method
                        {
                            double[] list1 = a.CVputgreek();
                            //output result to GUI
                            textBox_delta.Text = Convert.ToString(list1[0]);
                            textBox_gamma.Text = Convert.ToString(list1[1]);
                            textBox_vega.Text = Convert.ToString(list1[2]);
                            textBox_rho.Text = Convert.ToString(list1[3]);
                            textBox_theta.Text = Convert.ToString(list1[4]);
                            textBox_Price.Text = Convert.ToString(list1[5]);
                            if (antithetic.IsChecked == true)
                                textBox_se.Text = Convert.ToString(list1[7]);
                            else
                                textBox_se.Text = Convert.ToString(list1[6]);
                        }
                    }
                    else  //if the optiontype is not determined
                    {
                        MessageBox.Show("optiontype can't be null");
                    }
                }
            }
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x);
            }
            watch.Stop(); //add timer
            textBox_time.Text = watch.Elapsed.Hours.ToString() + ":" + watch.Elapsed.Minutes.ToString() + ":" + watch.Elapsed.Seconds.ToString()+ ":" + watch.Elapsed.Milliseconds.ToString();

        }

        private void textBox_S_TextChanged(object sender, TextChangedEventArgs e) //error handle of stock price texbox
        {
            double a;
            if (!double.TryParse(textBox_S.Text, out a))
                textBox_S.Background = Brushes.Pink;
            else
                textBox_S.Background = Brushes.White;
            try
            {
                Convert.ToDouble(textBox_S.Text);
            }
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x);
            }
        }

        private void textBox_K_TextChanged(object sender, TextChangedEventArgs e) //error handle of strike textbox
        {
            double a;
            if (!double.TryParse(textBox_K.Text, out a))
                textBox_K.Background = Brushes.Pink;
            else
                textBox_K.Background = Brushes.White;
            try
            {
                Convert.ToDouble(textBox_K.Text);
            }
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x);
            }
        }

        private void textBox_r_TextChanged(object sender, TextChangedEventArgs e) //error handle rate textbox
        {
            double a;
            if (!double.TryParse(textBox_r.Text, out a))
                textBox_r.Background = Brushes.Pink;
            else
                textBox_r.Background = Brushes.White;
            try
            {
                Convert.ToDouble(textBox_r.Text);
            }
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x);
            }
        }

        private void textBox_sigma_TextChanged(object sender, TextChangedEventArgs e) //error handle volitatility textbox
        {
            double a;
            if (!double.TryParse(textBox_sigma.Text, out a))
                textBox_sigma.Background = Brushes.Pink;
            else
                textBox_sigma.Background = Brushes.White;
            try
            {
                Convert.ToDouble(textBox_sigma.Text);
            }
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x);
            }
        }

        private void textBox_T_TextChanged(object sender, TextChangedEventArgs e) //error handle tenor textbox
        {
            double a;
            if (!double.TryParse(textBox_T.Text, out a))
                textBox_T.Background = Brushes.Pink;
            else
                textBox_T.Background = Brushes.White;
            try
            {
                Convert.ToDouble(textBox_T.Text);
            }
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x);
            }
        }

        private void textBox_step_TextChanged(object sender, TextChangedEventArgs e) //error handle step textbox
        {
            Int32 a;
            if (!Int32.TryParse(textBox_step.Text, out a))
                textBox_step.Background = Brushes.Pink;
            else
                textBox_step.Background = Brushes.White;
            try
            {
                Convert.ToInt32(textBox_step.Text);
            }
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x);
            }
        }

        private void textBox_trial_TextChanged(object sender, TextChangedEventArgs e) //error handle trial textbox
        {
            Int32 a;
            if (!Int32.TryParse(textBox_step.Text, out a))
                textBox_trial.Background = Brushes.Pink;
            else
                textBox_trial.Background = Brushes.White;
            try
            {
                Convert.ToInt32(textBox_trial.Text);
            }
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x);
            }
        }

        private void mutiple_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void progressbar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
    }

