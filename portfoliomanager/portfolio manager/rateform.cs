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
    public partial class rateform : Form
    {
        static MyEntityModelContainer container = new MyEntityModelContainer();
        public rateform()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void rateform_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) //cancel
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e) //add interest rate
        {
            try
            {
                container.Entity_rate.Add(new Entity_rate() { Id = Convert.ToInt32(textBox_id.Text), Tenor = Convert.ToDouble(textBox_tenor.Text), Interest_rate = Convert.ToDouble(textBox_rate.Text) });
                container.SaveChanges();
                MessageBox.Show("You add a new interest rate!");
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

        private void textBox_rate_TextChanged(object sender, EventArgs e)
        {
            double a;
            if (!double.TryParse(textBox_rate.Text, out a))
                textBox_rate.BackColor = Color.Pink;
            else
                textBox_rate.BackColor = Color.White;
            try
            {
                Convert.ToDouble(textBox_rate.Text);
            }
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x);
            }
        }
    }
}
