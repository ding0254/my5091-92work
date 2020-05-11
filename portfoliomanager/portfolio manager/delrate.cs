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
    public partial class delrate : Form
    {
        MyEntityModelContainer container = new MyEntityModelContainer();
        public delrate()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e) //search a selected rate
        {
            try
            {
                int id = Convert.ToInt32(textBox_rateID.Text);
                using (var db = new MyEntityModelContainer())
                {
                    var data = (from tt in db.Entity_rate
                                where tt.Id == id
                                select new
                                {
                                    id = tt.Id,
                                    tenor = tt.Tenor,
                                    insterest_rate = tt.Interest_rate
                                }).ToList();
                    if (data.FirstOrDefault() == null) MessageBox.Show("Selected ID does not exist!");//if not return result
                    else
                    {
                        foreach (var item in data)
                        {

                            textBox_rateID.Text = item.id.ToString();
                            textBox_tenor.Text = item.tenor.ToString();
                            textBox_rate.Text = item.insterest_rate.ToString();
                        }
                    }
                }
            }
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x);
            }
        }

            private void button3_Click(object sender, EventArgs e)//remove selected rate
        {
            int id = Convert.ToInt32(textBox_rateID.Text);

            container.Entity_rate.Remove((from i in container.Entity_rate
                                           where i.Id == id
                                           select i).First());
            container.SaveChanges();
            MessageBox.Show("Delete a Rate Successfully! ");
            Dispose();
        }

        private void button1_Click(object sender, EventArgs e) //upgrade selected rate id
        {
            try
            {
                int id = Convert.ToInt32(textBox_rateID.Text);
                container.Entity_rate.Remove((from i in container.Entity_rate
                                              where i.Id == id
                                              select i).First());
                container.SaveChanges();
                double tenor = Convert.ToDouble(textBox_tenor.Text);
                double rate = Convert.ToDouble(textBox_rate.Text);

                container.Entity_rate.Add(new Entity_rate()
                {
                    Id = id,
                    Tenor = tenor,
                    Interest_rate = rate
                });
                container.SaveChanges();
                MessageBox.Show("Update a Trade Successfully! ");
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

        private void textBox_rateID_TextChanged(object sender, EventArgs e)
        {
            int a;
            if (!int.TryParse(textBox_rateID.Text, out a))
                textBox_rateID.BackColor = Color.Pink;
            else
                textBox_rateID.BackColor = Color.White;
            try
            {
                Convert.ToInt32(textBox_rateID.Text);
            }
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x);
            }
        }
    }
}
