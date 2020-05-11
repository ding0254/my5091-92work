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
    public partial class historyprice : Form
    {
        static MyEntityModelContainer container = new MyEntityModelContainer();
        public historyprice()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double closeprice = Convert.ToDouble(textBox_histprice.Text);//Input element to database
                string ticker = textBox_companyticker.Text;
                int id = Convert.ToInt32(textBox_id.Text);
                string companyname = textBox_name.Text;
                DateTime pricedate = Convert.ToDateTime(textBox_date.Text);
                container.Entity_Historyprice.Add(new Entity_Historyprice() //add to database
                {
                    ClosePrice = closeprice,
                    Date = pricedate,
                    Ticker = ticker,
                    Id = Convert.ToInt32(textBox_id.Text),
                    CompanyName = textBox_name.Text
                });
                container.Entity_Historyprice.Add(new Entity_Historyprice() { Id = id, CompanyName = companyname, Ticker = ticker, ClosePrice = closeprice, Date = pricedate, });
                container.SaveChanges();
                MessageBox.Show("You add a new history price!"); //finish adding
                Dispose();

            }
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x);
            }
        }

        private void textBox_id_TextChanged(object sender, EventArgs e)
        {
            int  a;
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

        private void textBox_histprice_TextChanged(object sender, EventArgs e)
        {
            double a;
            if (!double.TryParse(textBox_histprice.Text, out a))
                textBox_histprice.BackColor = Color.Pink;
            else
                textBox_histprice.BackColor = Color.White;
            try
            {
                Convert.ToDouble(textBox_histprice.Text);
            }
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x);
            }
        }
    }
}
