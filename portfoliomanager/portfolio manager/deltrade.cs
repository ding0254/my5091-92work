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
    
    public partial class deltrade : Form
    {
        static MyEntityModelContainer container = new MyEntityModelContainer();
        public deltrade()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e) //search selected trade
        {
            try
            {
                int id = Convert.ToInt32(textBox_tradeID.Text);
                using (var db = new MyEntityModelContainer())
                {
                    var data = (from tt in db.Entity_Trade  //search trade with selected id
                                where tt.Id == id
                                select new
                                {
                                    id = tt.Id,
                                    direction = tt.Direction,
                                    quantity = tt.Quantity,
                                    instrumentid = tt.Entity_instrumentId,
                                    tradeprice = tt.Tradeprice
                                }).ToList();
                    if (data.FirstOrDefault() == null) MessageBox.Show("Selected ID does not exist!");//if not return result
                    else
                    {
                        foreach (var item in data) //display data
                        {
                            textBox_quantity.Text = item.quantity.ToString();
                            textBox_instid.Text = item.instrumentid.ToString();
                            textBox_tradeprice.Text = item.tradeprice.ToString();
                            if (item.direction == 0)
                                textBox_direction.Text = "Sell";
                            else textBox_direction.Text = "Buy";
                        }
                    }

                }
            }
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x);
            }
        }

            private void button3_Click(object sender, EventArgs e) //remove selected trade
        {
            int id = Convert.ToInt32(textBox_tradeID.Text);
           
                container.Entity_Trade.Remove((from i in container.Entity_Trade
                                              where i.Id == id
                                              select i).First());
            container.SaveChanges();
            MessageBox.Show("Delete a Trade Successfully! ");
            Dispose();
        }

        private void button1_Click(object sender, EventArgs e) //update selected trade
        {
            try
            {
                int id = Convert.ToInt32(textBox_tradeID.Text);
                container.Entity_Trade.Remove((from i in container.Entity_Trade
                                               where i.Id == id
                                               select i).First());
                container.SaveChanges();
                int quantity = Convert.ToInt32(textBox_quantity.Text);
                double tradeprice = Convert.ToDouble(textBox_tradeprice.Text);
                if (textBox_direction.Text == "Buy")//if direction is buy
                    container.Entity_Trade.Add(new Entity_Trade()
                {
                    Id = id,
                    Direction = 1,
                    Quantity = quantity,
                    Tradeprice = tradeprice,
                    Entity_instrumentId = Convert.ToInt32(textBox_instid.Text)
                });
                else
                    container.Entity_Trade.Add(new Entity_Trade()
                    {
                        Id = id,
                        Direction = 0,
                        Quantity = quantity,
                        Tradeprice = tradeprice,
                        Entity_instrumentId = Convert.ToInt32(textBox_instid.Text)
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

        private void textBox_direction_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox_tradeprice_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_tradeID_TextChanged(object sender, EventArgs e)
        {
            int a;
            if (!int.TryParse(textBox_tradeID.Text, out a))
                textBox_tradeID.BackColor = Color.Pink;
            else
                textBox_tradeID.BackColor = Color.White;
            try
            {
                Convert.ToInt32(textBox_tradeID.Text);
            }
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
