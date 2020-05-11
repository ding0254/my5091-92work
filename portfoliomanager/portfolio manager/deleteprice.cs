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
    public partial class deleteprice : Form
    {
        MyEntityModelContainer container = new MyEntityModelContainer();
        public deleteprice()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e) //search selected history price
        {
            try
            {
                int id = Convert.ToInt32(textBox_id.Text);
                using (var db = new MyEntityModelContainer())
                {
                    var data = (from tt in db.Entity_Historyprice
                                where tt.Id == id
                                select new
                                {
                                    id = tt.Id,
                                    companyname = tt.CompanyName,
                                    ticker = tt.Ticker,
                                    closeprice = tt.ClosePrice,
                                    date = tt.Date
                                }).ToList();
                    if (data.FirstOrDefault() == null) MessageBox.Show("Selected ID does not exist!");//if not return result
                    else
                    {
                        foreach (var item in data)
                        {
                            textBox_name.Text = item.companyname.ToString();
                            textBox_companyticker.Text = item.ticker.ToString();
                            textBox_date.Text = item.date.ToString();
                            textBox_histprice.Text = item.closeprice.ToString();
                        }
                    }
                }
            }
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x);
            }


        }

        private void button1_Click(object sender, EventArgs e)//update selected history price
        {
            try {
                int id = Convert.ToInt32(textBox_id.Text);
                container.Entity_Historyprice.Remove((from i in container.Entity_Historyprice
                                                      where i.Id == id
                                                      select i).First());
                container.SaveChanges();
                double closeprice = Convert.ToDouble(textBox_histprice.Text);
                string ticker = textBox_companyticker.Text;
                string companyname = textBox_name.Text;
                DateTime pricedate = Convert.ToDateTime(textBox_date.Text);
                container.Entity_Historyprice.Add(new Entity_Historyprice()
                {
                    ClosePrice = closeprice,
                    Date = pricedate,
                    Ticker = ticker,
                    Id = Convert.ToInt32(textBox_id.Text),
                    CompanyName = textBox_name.Text
                });
                container.Entity_Historyprice.Add(new Entity_Historyprice() { Id = id, CompanyName = companyname, Ticker = ticker, ClosePrice = closeprice, Date = pricedate, });
                container.SaveChanges();
                MessageBox.Show("You update a new history price!");
                Dispose();
            }
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textBox_id.Text);

                container.Entity_Historyprice.Remove((from i in container.Entity_Historyprice
                                                      where i.Id == id
                                                      select i).First());
                container.SaveChanges();
                MessageBox.Show("Delete a price history Successfully! ");
                Dispose();
            }
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
