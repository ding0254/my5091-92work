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

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try     //error handle
            {
                double stockprice = Convert.ToDouble(textBox_S.Text); //get stock price from textbox
                double strike = Convert.ToDouble(textBox_K.Text); // get strike price from textbox
                double time = Convert.ToDouble(textBox_T.Text);  //get tenor from textbox
                double sigma = Convert.ToDouble(textBox_sigma.Text); //get volitality from textbox
                double r = Convert.ToDouble(textBox_r.Text);  //get interest rate from textbox
                int step = Convert.ToInt32(textBox_step.Text); //get num of steps of one simulation
                int trial = Convert.ToInt32(textBox_trial.Text); //get num of trials
                option a = new option();
                randomnumber b = new randomnumber();
                a.Strike = strike;
                a.Stockprice = stockprice;
                a.Step = step;
                a.Trial = trial;
                a.Time = time;
                a.Vol = sigma;
                a.Rate = r;
                double[,] rmatrix = b.randommatrixgenerator(step, trial);//get random matrix
                a.Randommatrix = rmatrix;
                if (radioButton.IsChecked == true)  //get the option type if option is a call
                {
                    double[] list1 = a.callgreek();  //get result 
                    //output result to GUI
                    textBox_delta.Text = Convert.ToString(list1[0]);
                    textBox_gamma.Text = Convert.ToString(list1[1]);
                    textBox_vega.Text = Convert.ToString(list1[2]);
                    textBox_rho.Text = Convert.ToString(list1[3]);
                    textBox_theta.Text = Convert.ToString(list1[4]);
                    textBox_Price.Text = Convert.ToString(list1[5]);
                    textBox_std.Text = Convert.ToString(list1[6]);
                }
                else if (radioButton1.IsChecked == true)  //if option is a put
                {
                    double[] list1 = a.putgreek();
                    //output result to GUI
                    textBox_delta.Text = Convert.ToString(list1[0]);
                    textBox_gamma.Text = Convert.ToString(list1[1]);
                    textBox_vega.Text = Convert.ToString(list1[2]);
                    textBox_rho.Text = Convert.ToString(list1[3]);
                    textBox_theta.Text = Convert.ToString(list1[4]);
                    textBox_Price.Text = Convert.ToString(list1[5]);
                    textBox_std.Text = Convert.ToString(list1[6]);
                }
                else  //if the optiontype is not determined
                {
                    MessageBox.Show("optiontype can't be null");
                }
            }
            catch(Exception x)
            {
                MessageBox.Show("Error:" + x);
            }

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
    }
}
