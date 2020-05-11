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
    public partial class delinstrument : Form
    {
        static MyEntityModelContainer container = new MyEntityModelContainer();
        public delinstrument()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textBox_id.Text);
                container.Entity_instrument.Remove((from i in container.Entity_instrument
                                                    where i.Id == id
                                                    select i).First());
                container.SaveChanges();
                string companyname = textBox_companyname.Text;
                string ticker = textBox_Ticker.Text;
                string exchange = textBox_exchange.Text;
                string instype = Convert.ToString(comboBox_istype.SelectedItem);
                double underlying = Convert.ToDouble(textBox_Underlying.Text);
                double strike; double barrier; double rebate; string type; double tenor;
                if (comboBox_istype.SelectedIndex == 0) //if stock selected
                {
                    type = "Neither call nor put";
                    container.Entity_instrument.Add(new Entity_instrument()
                    {
                        Id = id,
                        CompanyName = companyname,
                        Ticker = ticker,
                        Exchange = exchange,
                        Underlying = underlying,
                        Strike = Convert.ToString(0),
                        Barrier = 0,
                        Rebate = 0,
                        Type = type,
                        Instype = instype,
                        Tenor = 0,
                        Entity_rateId = 1
                    });
                }
                else
                {
                    tenor = Convert.ToDouble(textBox_tenor.Text);
                    type = Convert.ToString(comboBox_type.SelectedItem);
                    if (comboBox_istype.SelectedIndex == 2) strike = 0; else strike = Convert.ToDouble(textBox_strike.Text);
                    if (comboBox_istype.SelectedIndex == 9) rebate = Convert.ToDouble(textBox_rebate); else rebate = 0;
                    //if barrier option selected
                    if (comboBox_istype.SelectedIndex == 5 || comboBox_istype.SelectedIndex == 6 || comboBox_istype.SelectedIndex == 7 || comboBox_istype.SelectedIndex == 8)
                        barrier = Convert.ToDouble(textBox_barrier.Text);
                    else barrier = 0;
                    container.Entity_instrument.Add(new Entity_instrument
                    {
                        Id = id,
                        CompanyName = companyname,
                        Ticker = ticker,
                        Exchange = exchange,
                        Underlying = underlying,
                        Strike = Convert.ToString(strike),
                        Barrier = barrier,
                        Rebate = rebate,
                        Type = type,
                        Instype = instype,
                        Tenor = tenor,
                        Entity_rateId = Convert.ToInt32(textBox_rateid.Text)
                    });
                }
                container.SaveChanges();
                MessageBox.Show("You update a new instrument!");
                Dispose();

            }
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x);
            }
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textBox_id.Text);

                using (var db = new MyEntityModelContainer())
                {
                    var data = (from instrument in db.Entity_instrument
                                where instrument.Id == id
                                select new
                                {
                                    id = instrument.Id,
                                    ticker = instrument.Ticker,
                                    exchange = instrument.Exchange,
                                    companyname = instrument.CompanyName,
                                    underlying = instrument.Underlying,
                                    rateid = instrument.Entity_rateId,
                                    strike = instrument.Strike,
                                    tenor = instrument.Tenor,
                                    rebate = instrument.Rebate,
                                    barrier = instrument.Barrier,
                                    type = instrument.Type,
                                    instype = instrument.Instype
                                }).ToList();
                    if (data.FirstOrDefault() == null) MessageBox.Show("Selected ID does not exist!");//if not return result
                    else
                    {
                        foreach (var q in data)
                        {
                            textBox_exchange.Text = q.exchange.ToString();
                            textBox_companyname.Text = q.companyname.ToString();
                            textBox_Ticker.Text = q.ticker.ToString();
                            textBox_strike.Text = q.strike.ToString();
                            textBox_barrier.Text = q.barrier.ToString();
                            textBox_rateid.Text = q.rateid.ToString();
                            textBox_tenor.Text = q.tenor.ToString();
                            textBox_Underlying.Text = q.underlying.ToString();
                            textBox_rebate.Text = q.rebate.ToString();
                            comboBox_istype.SelectedItem = q.instype.ToString();
                            comboBox_type.SelectedItem = q.type.ToString();
                        }
                    }
                }
            }
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try {
                int id = Convert.ToInt32(textBox_id.Text);

                container.Entity_instrument.Remove((from i in container.Entity_instrument
                                                    where i.Id == id
                                                    select i).First());
                container.SaveChanges();
                MessageBox.Show("Delete an instrument Successfully! ");
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
    } }
