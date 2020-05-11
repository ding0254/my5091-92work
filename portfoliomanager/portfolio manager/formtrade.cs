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
    public partial class formtrade : Form
    {
        static MyEntityModelContainer container = new MyEntityModelContainer();
        public formtrade()
        {
            InitializeComponent();
           
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox_Type_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox_instType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textBox_tradeID.Text);
                short direction = Convert.ToInt16(comboBox_direction.SelectedIndex == 0 ? 1 : 0);
                int quantity = Convert.ToInt32(textBox_quantity.Text);
                double tradeprice = Convert.ToDouble(textBox_tradeprice.Text);
                // string insttype =Convert.ToString(comboBox_instType.SelectedItem);
                //string type = Convert.ToString(comboBox_Type.SelectedItem);
                container.Entity_Trade.Add(new Entity_Trade()
                {
                    Id = id,
                    Direction = direction,
                    Quantity = quantity,
                    Tradeprice = tradeprice,
                    Entity_instrumentId = Convert.ToInt32(textBox_instid.Text)
                });
                container.SaveChanges();
                MessageBox.Show("You add a new trade!");
                Dispose();
            }
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x);
            }
        }
        }
}
