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
using System.Windows.Threading;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
   
    public partial class MainWindow : Window
    {
        Stopwatch watch = new Stopwatch();               
        
        public MainWindow()
        {
            
            InitializeComponent();
            
        }
        public void updateProgressBar(int i)//progressbar
        {                     
                Action action = () => { setProgress(i); };
                progressbar.Dispatcher.BeginInvoke(action);
                
        }
        private void setProgress (int i)
        {
            progressbar.Value = i;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try     //error handle
            {

                progressbar.Value = 20;              
                watch.Reset();
                watch.Start();
                disable();//disable user to revise input
                double stockprice = Convert.ToDouble(textBox_S.Text); //get stock price from textbox
                double strike;
                if (ComboBoxItem_range.IsSelected == false) strike = Convert.ToDouble(textBox_K.Text); // get strike price from textbox
                else strike =stockprice;//because range option does not need strike so we can give strike any value
                double time = Convert.ToDouble(textBox_T.Text);  //get tenor from textbox
                double sigma = Convert.ToDouble(textBox_sigma.Text); //get volitality from textbox
                double r = Convert.ToDouble(textBox_r.Text);  //get interest rate from textbox
                int step = Convert.ToInt32(textBox_step.Text); //get num of steps of one simulation
                int trial = Convert.ToInt32(textBox_trial.Text); //get num of trials
                int core = System.Environment.ProcessorCount; //get num of core
                randomnumber b = new randomnumber();
                textBox_time_core.Text = Convert.ToString(core);
                updateProgressBar(40);
                if (ComboBoxItem_digital.IsSelected == true)//if option type is digital option
                {
                    digital a = new digital(); a.Strike = strike; a.Stockprice = stockprice; a.Time = time; a.Step = step; a.Trial = trial; a.Vol = sigma; a.Rate = r;
                    textBox_rebate.IsEnabled = true; a.Rebate = Convert.ToDouble(textBox_rebate.Text);
                    double[,] rmatrix = new double[step, trial];
                    if (mutiple.IsChecked == true) //use mutiple thread
                    {
                        multiplethread_digital thread = new multiplethread_digital();
                        thread.Strike = strike; thread.Stockprice = stockprice; thread.Time = time; thread.Step = step; thread.Trial = trial; thread.Vol = sigma; thread.Rate = r;
                        thread.getlength();
                        thread.Rebate = Convert.ToDouble(textBox_rebate.Text);
                        bool boolantithetic = (antithetic.IsChecked == true);
                        bool boolcvmethod = (control_variate.IsChecked == true);
                        bool boolcall = true;
                        if (callButton.IsChecked == true)
                        { boolcall = (callButton.IsChecked == true); }
                        else if (putButton1.IsChecked == true)
                        { boolcall = (putButton1.IsChecked == false); }
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
                                progressbar.Value = 80;                         
                                //output result to GUI
                                textBox_delta.Text = Convert.ToString(list1[0]);
                                textBox_gamma.Text = Convert.ToString(list1[1]);
                                textBox_vega.Text = Convert.ToString(list1[2]);
                                textBox_rho.Text = Convert.ToString(list1[3]);
                                //inprogress(50);
                                textBox_theta.Text = Convert.ToString(list1[4]);
                                textBox_Price.Text = Convert.ToString(list1[5]);
                                //inprogress(70);
                                if (antithetic.IsChecked == true) //if use antithetic variance reduction
                                    textBox_se.Text = Convert.ToString(list1[7]);
                                else
                                    textBox_se.Text = Convert.ToString(list1[6]);//if use traditional way to produce random number
                                progressbar.Value = 100;
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
                else if (ComboBoxItem_lookback.IsSelected == true) //if option type is lookback option
                { 
                    lookback a = new lookback();                    
                        double[,] rmatrix = new double[step, trial];
                        if (mutiple.IsChecked == true) //use mutiple thread
                        {
                            multiplethread_lookback thread = new multiplethread_lookback();
                            thread.Strike = strike; thread.Stockprice = stockprice; thread.Time = time; thread.Step = step; thread.Trial = trial; thread.Vol = sigma; thread.Rate = r;
                            thread.getlength();
                            bool boolantithetic = (antithetic.IsChecked == true);
                            bool boolcvmethod = (control_variate.IsChecked == true);
                            bool boolcall = true;
                            if (callButton.IsChecked == true)
                            { boolcall = (callButton.IsChecked == true); }
                            else if (putButton1.IsChecked == true)
                            { boolcall = (putButton1.IsChecked == false); }
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
                        a.Strike = strike; a.Stockprice = stockprice; a.Time = time; a.Step = step; a.Trial = trial; a.Vol = sigma; a.Rate = r;
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
                else if (ComboBoxItem_asian.IsSelected == true) 
                { 
                    Asian a = new Asian();
                        double[,] rmatrix = new double[step, trial];
                        if (mutiple.IsChecked == true) //use mutiple thread
                        {
                            multiplethread_asian thread = new multiplethread_asian();
                        thread.Strike = strike; thread.Stockprice = stockprice; thread.Time = time; thread.Step = step; thread.Trial = trial; thread.Vol = sigma; thread.Rate = r;
                        thread.getlength();
                            bool boolantithetic = (antithetic.IsChecked == true);
                            bool boolcvmethod = (control_variate.IsChecked == true);
                            bool boolcall = true;
                            if (callButton.IsChecked == true)
                            { boolcall = (callButton.IsChecked == true); }
                            else if (putButton1.IsChecked == true)
                            { boolcall = (putButton1.IsChecked == false); }
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
                        a.Strike = strike; a.Stockprice = stockprice; a.Time = time; a.Step = step; a.Trial = trial; a.Vol = sigma; a.Rate = r;
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
                else if (ComboBoxItem_range.IsSelected == true) //option type is range option
                {
                    strike = stockprice;//because range option does not need a strike we can give strike an arbitrary value including stockprice 
                    range a = new range();
                        double[,] rmatrix = new double[step, trial];
                        if (mutiple.IsChecked == true) //use mutiple thread
                        {
                            multiplethread_range thread = new multiplethread_range();
                        thread.Strike = strike; thread.Stockprice = stockprice; thread.Time = time; thread.Step = step; thread.Trial = trial; thread.Vol = sigma; thread.Rate = r;
                        thread.getlength();
                            bool boolantithetic = (antithetic.IsChecked == true);
                            bool boolcvmethod = (control_variate.IsChecked == true);
                            bool boolcall = true;
                            if (callButton.IsChecked == true)
                            { boolcall = (callButton.IsChecked == true); }
                            else if (putButton1.IsChecked == true)
                            { boolcall = (putButton1.IsChecked == false); }
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
                        a.Strike = strike; a.Stockprice = stockprice; a.Time = time; a.Step = step; a.Trial = trial; a.Vol = sigma; a.Rate = r;
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
                else if (ComboBoxItem_barrier_downin.IsSelected == true || ComboBoxItem_barrier_downout.IsSelected == true ||
                    ComboBoxItem_barrier_upin.IsSelected == true || ComboBoxItem_barrier_upout.IsSelected == true)//option type is barrier option
                {
                    barrier a = new barrier(); bool isdown; bool isout;
                    a.Bar = Convert.ToDouble(textBox_bound.Text);//get bound of carrier option
                    if (ComboBoxItem_barrier_downout.IsSelected == true) { isdown = true; isout = true; }
                    else if(ComboBoxItem_barrier_upin.IsSelected == true) {isdown = false; isout = false; }
                    else if (ComboBoxItem_barrier_upout.IsSelected == true) { isdown = false;isout = true; }
                    else { isdown = true; isout = false; }
                    a.IsDown = isdown;a.IsOut = isout;
                        double[,] rmatrix = new double[step, trial];
                        if (mutiple.IsChecked == true) //use mutiple thread
                        {
                            multiplethread_barrier thread = new multiplethread_barrier();
                            thread.Strike = strike; thread.Stockprice = stockprice; thread.Time = time;thread.Step = step; thread.Trial = trial; thread.Vol = sigma; thread.Rate = r;
                            thread.getlength();
                            thread.Bar= Convert.ToDouble(textBox_bound.Text);
                            thread.IsDown = isdown; thread.IsOut = isout;
                            bool boolantithetic = (antithetic.IsChecked == true);
                            bool boolcvmethod = (control_variate.IsChecked == true);
                            bool boolcall = true;
                            if (callButton.IsChecked == true)
                            { boolcall = (callButton.IsChecked == true); }
                            else if (putButton1.IsChecked == true)
                            { boolcall = (putButton1.IsChecked == false); }
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
                        a.Strike = strike; a.Stockprice = stockprice; a.Time = time; a.Step = step; a.Trial = trial; a.Vol = sigma; a.Rate = r;
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
                
                else
                {
                    option a = new option();
                    double[,] rmatrix = new double[step, trial];
                    if (mutiple.IsChecked == true) //use mutiple thread
                    {
                        multiplethread thread = new multiplethread();
                        thread.Strike = strike;
                        thread.Stockprice = stockprice;
                        thread.Step = step;
                        thread.Trial = trial;
                        thread.Time = time;
                        thread.Vol = sigma;
                        thread.Rate = r;
                        thread.getlength();
                        bool boolantithetic = (antithetic.IsChecked == true);
                        bool boolcvmethod = (control_variate.IsChecked == true);
                        bool boolcall = true;
                        if (callButton.IsChecked == true)
                        { boolcall = (callButton.IsChecked == true); }
                        else if (putButton1.IsChecked == true)
                        { boolcall = (putButton1.IsChecked == false); }
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
                        a.Strike = strike; a.Stockprice = stockprice; a.Time = time; a.Step = step; a.Trial = trial; a.Vol = sigma; a.Rate = r;
                        if (antithetic.IsChecked == true)
                        { rmatrix = b.antitheticgenerator(step, trial); }//use antithetic method to get random matrix
                        else
                        { rmatrix = b.randommatrixgenerator(step, trial); }//get random matrix
                        a.Randommatrix = rmatrix;
                        a.Strike = strike;a.Stockprice = stockprice;a.Time = time;a.Step = step;a.Trial = trial;a.Vol = sigma;a.Rate = r;
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
                updateProgressBar(90);
                updateProgressBar(100);
            }
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x);
            }
            watch.Stop(); //add timer
            textBox_time.Text = watch.Elapsed.Hours.ToString() + ":" + watch.Elapsed.Minutes.ToString() + ":" + watch.Elapsed.Seconds.ToString()+ ":" + watch.Elapsed.Milliseconds.ToString();           
            enable();//allow user to input
            progressbar.Value = 0;
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
            if (!Int32.TryParse(textBox_trial.Text, out a))
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
        private void textBox_rebate_TextChanged(object sender, TextChangedEventArgs e)
        {
            double a;
            if (comboBox.SelectedIndex == 8)
            {
                if (!double.TryParse(textBox_rebate.Text, out a))
                    textBox_rebate.Background = Brushes.Pink;
                else
                    textBox_rebate.Background = Brushes.White;
                try
                {
                    Convert.ToDouble(textBox_rebate.Text);
                }
                catch (Exception x)
                {
                    MessageBox.Show("Error:" + x);
                }
            }
        }

        private void textBox_bound_TextChanged(object sender, TextChangedEventArgs e)
        {
            double a;
            if (comboBox.SelectedIndex == 4 || comboBox.SelectedIndex == 5 || comboBox.SelectedIndex == 6 || comboBox.SelectedIndex == 7)
            {
                if (!double.TryParse(textBox_bound.Text, out a))
                    textBox_bound.Background = Brushes.Pink;
                else
                    textBox_bound.Background = Brushes.White;
                try
                {
                    Convert.ToDouble(textBox_bound.Text);
                }
                catch (Exception x)
                {
                    MessageBox.Show("Error:" + x);
                }
            }
        }
        private void mutiple_Checked(object sender, RoutedEventArgs e)
        {

        }
        private void progressbar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                progressbar.Value = 0;
                if (comboBox.SelectedIndex == 0) { textBox_rebate.Clear(); textBox_rebate.IsEnabled = false; textBox_bound.Clear(); textBox_bound.IsEnabled = false; }
                if (comboBox.SelectedIndex == 1) { textBox_rebate.Clear(); textBox_rebate.IsEnabled = false; textBox_bound.Clear(); textBox_bound.IsEnabled = false; }
                if (comboBox.SelectedIndex == 2) { textBox_rebate.Clear(); textBox_rebate.IsEnabled = false; textBox_bound.Clear(); textBox_bound.IsEnabled = false; }
                if (comboBox.SelectedIndex == 3) { textBox_rebate.Clear(); textBox_rebate.IsEnabled = false; textBox_bound.Clear(); textBox_bound.IsEnabled = false; }
                if (comboBox.SelectedIndex == 4) { textBox_rebate.Clear(); textBox_rebate.IsEnabled = false; textBox_bound.IsEnabled = true; }
                if (comboBox.SelectedIndex == 5) { textBox_rebate.Clear(); textBox_rebate.IsEnabled = false; textBox_bound.IsEnabled = true; }
                if (comboBox.SelectedIndex == 6) { textBox_rebate.Clear(); textBox_rebate.IsEnabled = false; textBox_bound.IsEnabled = true; }
                if (comboBox.SelectedIndex == 7) { textBox_rebate.Clear(); textBox_rebate.IsEnabled = false; textBox_bound.IsEnabled = true; }
                if (comboBox.SelectedIndex == 8) { textBox_rebate.Clear(); textBox_rebate.IsEnabled = true; textBox_bound.Clear(); textBox_bound.IsEnabled = false; }
            }
        } 
        private  void enable()
        {
            textBox_S.IsEnabled = true; textBox_K.IsEnabled = true; textBox_r.IsEnabled = true; textBox_sigma.IsEnabled = true;
            textBox_T.IsEnabled = true; textBox_step.IsEnabled = true; textBox_trial.IsEnabled = true; textBox_rebate.IsEnabled = true;
            textBox_bound.IsEnabled = true;mutiple.IsEnabled = true;control_variate.IsEnabled = true;antithetic.IsEnabled = true;
            callButton.IsEnabled = true;putButton1.IsEnabled = true;
        }
        private void disable()
        {
            textBox_S.IsEnabled = false; textBox_K.IsEnabled = false; textBox_r.IsEnabled = false; textBox_sigma.IsEnabled = false;
            textBox_T.IsEnabled = false; textBox_step.IsEnabled = false; textBox_trial.IsEnabled = false; textBox_rebate.IsEnabled = false;
            textBox_bound.IsEnabled = false; mutiple.IsEnabled = false; control_variate.IsEnabled = false; antithetic.IsEnabled = false;
            callButton.IsEnabled = false; putButton1.IsEnabled = false;
        }
        private void ComboBoxItem_lookback_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBoxItem_european_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBoxItem_range_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBoxItem_asian_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBoxItem_barrier_downout_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBoxItem_barrier_upout_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBoxItem_barrier_downin_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBoxItem_barrier_upin_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBoxItem_digital_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void progressbar_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void antithetic_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
    }

