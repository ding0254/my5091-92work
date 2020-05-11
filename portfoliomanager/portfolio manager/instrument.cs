using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace portfolio_manager
{
    public partial class forminstrument : Form
    {
        static MyEntityModelContainer container = new MyEntityModelContainer();
        public forminstrument()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox_neithercallnorput_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                int id = Convert.ToInt32(textBox_id.Text);
                string companyname = textBox_companyname.Text;
                string ticker = textBox_Ticker.Text;
                string exchange = textBox_exchange.Text;
                string instype = comboBox_istype.SelectedItem.ToString();
                double underlying = Convert.ToDouble(textBox_Underlying.Text);
                double barrier; double rebate; string type; double tenor;
                if (comboBox_istype.SelectedIndex == 0) //if stock selected
                { type = "Neither call nor put";
                    container.Entity_instrument.Add(new Entity_instrument()
                    {
                        Id = id,
                        CompanyName = companyname,
                        Ticker = ticker,
                        Exchange = exchange,
                        Underlying = underlying,                       
                        Type = type,
                        Instype = instype,
                        Entity_rateId = 1
                    });
                }
                else if (comboBox_istype.SelectedIndex == 1 || comboBox_istype.SelectedIndex == 3 || comboBox_istype.SelectedIndex == 4)
                //european option or aisan or lookback option selected
                {
                    tenor = Convert.ToDouble(textBox_tenor.Text);
                    type = Convert.ToString(comboBox_type.SelectedItem);
                    string strike = textBox_strike.Text;
                    container.Entity_instrument.Add(new Entity_instrument
                    {
                        Id = id,
                        CompanyName = companyname,
                        Ticker = ticker,
                        Exchange = exchange,
                        Underlying = underlying,
                        Strike = strike,
                        Type = type,
                        Instype = instype,
                        Tenor = tenor,
                        Entity_rateId = Convert.ToInt32(textBox_rateid.Text)
                    }); 
                }
                //if range option selected
                else if (comboBox_istype.SelectedIndex == 2)
                {
                    tenor = Convert.ToDouble(textBox_tenor.Text);
                    container.Entity_instrument.Add(new Entity_instrument
                    {
                        Id = id,
                        CompanyName = companyname,
                        Ticker = ticker,
                        Exchange = exchange,
                        Underlying = underlying,
                        Instype = instype,
                        Tenor = tenor,
                        Entity_rateId = Convert.ToInt32(textBox_rateid.Text)
                    });
                }
                //if digital option selected
                else if (comboBox_istype.SelectedIndex == 9)
                {
                    rebate = Convert.ToDouble(textBox_rebate.Text);
                    tenor = Convert.ToDouble(textBox_tenor.Text);
                    type = Convert.ToString(comboBox_type.SelectedItem);
                    string strike = textBox_strike.Text;
                        container.Entity_instrument.Add(new Entity_instrument
                        {
                            Id = id,
                            CompanyName = companyname,
                            Ticker = ticker,
                            Exchange = exchange,
                            Underlying = underlying,
                            Strike = strike,
                            Rebate = rebate,
                            Type = type,
                            Instype = instype,
                            Tenor = tenor,
                            Entity_rateId = Convert.ToInt32(textBox_rateid.Text)
                        });
                    }
                    //if barrier option selected
                 else  //(comboBox_istype.SelectedIndex == 5 || comboBox_istype.SelectedIndex == 6 || comboBox_istype.SelectedIndex == 7 || comboBox_istype.SelectedIndex == 8)
                 {
                    barrier = Convert.ToDouble(textBox_barrier.Text);
                    tenor = Convert.ToDouble(textBox_tenor.Text);
                    type = Convert.ToString(comboBox_type.SelectedItem);
                    string strike= textBox_strike.Text;
                    container.Entity_instrument.Add(new Entity_instrument
                        {
                            Id = id,
                            CompanyName = companyname,
                            Ticker = ticker,
                            Exchange = exchange,
                            Underlying = underlying,
                            Strike = strike,
                            Barrier = barrier,
                            Type = type,
                            Instype = instype,
                            Tenor = tenor,
                            Entity_rateId = Convert.ToInt32(textBox_rateid.Text)
                        });
                    }
                        container.SaveChanges();
                        MessageBox.Show("You add a new instrument!");
                        Dispose();
                    } 
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x);
            }
        }

        private void textBox_id_TextChanged(object sender, EventArgs e)
        {
            int a;
            if (!int.TryParse(textBox_id.Text, out a))
                textBox_id.BackColor = Color.Pink;
            else
                textBox_id.BackColor = Color.White;
            try
            {
                Convert.ToInt32(textBox_id.Text);
            }
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x);
            }
        }

        private void textBox_Underlying_TextChanged(object sender, EventArgs e)
        {
            double a;
            if (!double.TryParse(textBox_Underlying.Text, out a))
                textBox_Underlying.BackColor = Color.Pink;
            else
                textBox_Underlying.BackColor = Color.White;
            try
            {
                Convert.ToDouble(textBox_Underlying.Text);
            }
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x);
            }
        }

        private void textBox_strike_TextChanged(object sender, EventArgs e)
        {
            double a;
            if (!double.TryParse(textBox_strike.Text, out a))
                textBox_strike.BackColor = Color.Pink;
            else
                textBox_strike.BackColor = Color.White;
            try
            {
                Convert.ToDouble(textBox_strike.Text);
            }
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x);
            }
        }

        private void textBox_rateid_TextChanged(object sender, EventArgs e)
        {
            int a;
            if (!int.TryParse(textBox_rateid.Text, out a))
                textBox_rateid.BackColor = Color.Pink;
            else
                textBox_rateid.BackColor = Color.White;
            try
            {
                Convert.ToInt32(textBox_rateid.Text);
            }
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x);
            }
        }

        private void textBox_tenor_TextChanged(object sender, EventArgs e)
        {
            double a;
            if (!double.TryParse(textBox_tenor.Text, out a))
                textBox_tenor.BackColor = Color.Pink;
            else
                textBox_tenor.BackColor = Color.White;
            try
            {
                Convert.ToDouble(textBox_tenor.Text);
            }
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x);
            }
        }

        private void textBox_rebate_TextChanged(object sender, EventArgs e)
        {
            double a;
            if (!double.TryParse(textBox_rebate.Text, out a))
                textBox_rebate.BackColor = Color.Pink;
            else
                textBox_rebate.BackColor = Color.White;
            try
            {
                Convert.ToDouble(textBox_rebate.Text);
            }
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x);
            }
        }

        private void textBox_barrier_TextChanged(object sender, EventArgs e)
        {
            double a;
            if (!double.TryParse(textBox_barrier.Text, out a))
                textBox_barrier.BackColor = Color.Pink;
            else
                textBox_barrier.BackColor = Color.White;
            try
            {
                Convert.ToDouble(textBox_barrier.Text);
            }
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x);
            }
        }
    }
}
