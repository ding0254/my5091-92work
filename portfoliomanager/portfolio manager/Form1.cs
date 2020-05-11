using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WpfApp2;


namespace portfolio_manager
{
    public partial class Form1 : Form
    {
        static MyEntityModelContainer container = new MyEntityModelContainer();
        public Form1()
        {
            InitializeComponent();
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

             

        private void menustrip_Opening(object sender, CancelEventArgs e)
        {

        }

        private void instrumentToolStripMenuItem_Click(object sender, EventArgs e)//new instrument
        {
            forminstrument form2 = new forminstrument();
            form2.ShowDialog();
        }

        private void historyPriceToolStripMenuItem_Click(object sender, EventArgs e) //new history price
        {
            historyprice form3 = new historyprice();
            form3.ShowDialog();
        }

        private void interestRateToolStripMenuItem_Click(object sender, EventArgs e) //new interest rate
        {
            rateform form4 = new rateform();
            form4.ShowDialog();
        }

        private void tradeToolStripMenuItem_Click(object sender, EventArgs e) //new trade
        {
            formtrade form5 = new formtrade();           
            form5.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void totals_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void Alltrades_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

            private void instrumentToolStripMenuItem1_Click(object sender, EventArgs e) //
        {
            delinstrument form6 = new delinstrument();
            form6.ShowDialog();
        }

        private void tradeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            deltrade form7 = new deltrade();
            form7.ShowDialog();
        } //edit trade

        private void interestRateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            delrate form6 = new delrate();
            form6.ShowDialog();
        } //edit interest rate

        private void historyPriceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            deleteprice form6 = new deleteprice();
            form6.ShowDialog();
        } //edit history price

        private void refreshTradesFromDatabaseToolStripMenuItem_Click(object sender, EventArgs e) //display all trades in database
        {
            try
            {
                using (var db = new MyEntityModelContainer())
                {
                    var data = (from trade in db.Entity_Trade
                                join inst in db.Entity_instrument on trade.Entity_instrumentId equals inst.Id
                                select new
                                {
                                    id = trade.Id,
                                    direction = trade.Direction,
                                    quantity = trade.Quantity,
                                    instrumentid =inst.Id,                                 
                                    type = inst.Type,
                                    instrumenttype = inst.Instype,
                                    tradeprice = trade.Tradeprice
                                }).ToList();
                    Listview_Alltrades.Items.Clear(); //clear listview
                    foreach (var q in data) //display listview in listview_alltrades
                    {
                        ListViewItem lv = new ListViewItem(q.id.ToString());
                        if(q.direction==0)lv.SubItems.Add("Sell");
                        else  lv.SubItems.Add("Buy");
                        lv.SubItems.Add(q.quantity.ToString());
                        lv.SubItems.Add(q.instrumentid.ToString());
                        if (q.type == null) lv.SubItems.Add("Neither call nor put");
                        else lv.SubItems.Add(q.type.ToString());
                        lv.SubItems.Add(q.instrumenttype.ToString());
                        lv.SubItems.Add(q.tradeprice.ToString());
                        Listview_Alltrades.Items.Add(lv);
                    }
                    MessageBox.Show("Refresh successfully!");
                }
            }
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x);
            }
        }
        //compute greek and mark price in very trade
        private void priceBookUsingSimulationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox_vol.Text == "") MessageBox.Show("You need input a volatility!");
                using (var db = new MyEntityModelContainer())
                {
                    double vol = Convert.ToDouble(textBox_vol.Text);//get vol
                    int core =System.Environment.ProcessorCount; //get num of core
                    var data = (from trade in db.Entity_Trade //get data from database

                                join inst in db.Entity_instrument on trade.Entity_instrumentId equals inst.Id
                                join r in db.Entity_rate on inst.Entity_rateId equals r.Id
                                join price in db.Entity_Historyprice on inst.Ticker equals price.Ticker
                                select new //get enough parameter to compute greek and simulation price
                                {
                                    id = trade.Id,
                                    direction = trade.Direction,
                                    quantity = trade.Quantity,
                                    instrumentid = trade.Entity_instrumentId,
                                    type = inst.Type,
                                    instrumenttype = inst.Instype,
                                    tradeprice = trade.Tradeprice,
                                    underlying = inst.Underlying,
                                    strike = inst.Strike,
                                    tenor = inst.Tenor,
                                    rate = r.Interest_rate,
                                    rebate = inst.Rebate,
                                    barrier = inst.Barrier,
                                    histprice = price.ClosePrice
                                }).ToList();
                    Listview_Alltrades.Items.Clear();
                    Listview_totals.Items.Clear();
                    foreach (var q in data)  //output data in listview
                    {
                        string instrumentype = q.instrumenttype; double delta; double rho; double gamma;
                        double vega; double theta; double PL;
                        if (instrumentype == "Stock") //if instrument is stock
                        {
                            ListViewItem lv = new ListViewItem(q.id.ToString()); //get an instrument line
                            if (q.direction == 0) lv.SubItems.Add("Sell");
                            else lv.SubItems.Add("Buy");
                            lv.SubItems.Add(q.quantity.ToString());
                            lv.SubItems.Add(q.instrumentid.ToString());
                            lv.SubItems.Add("neither call nor put");
                            lv.SubItems.Add(q.instrumenttype.ToString());
                            lv.SubItems.Add(q.tradeprice.ToString());
                            lv.SubItems.Add(q.histprice.ToString());//close price as mark price
                            PL = (q.histprice - q.tradeprice) * (q.direction - 0.5) * 2 * q.quantity;//compute p&l
                            delta = (q.direction - 0.5) * 2 * q.quantity;
                            vega = 0; rho = 0; theta = 0; gamma = 0;
                            lv.SubItems.Add(PL.ToString());
                            lv.SubItems.Add(delta.ToString());
                            lv.SubItems.Add(gamma.ToString());
                            lv.SubItems.Add(vega.ToString());
                            lv.SubItems.Add(theta.ToString());
                            lv.SubItems.Add(rho.ToString());
                            Listview_Alltrades.Items.Add(lv);
                        }
                        else if (instrumentype == "European") //if instrument type is european option
                        {
                            ListViewItem lv = new ListViewItem(q.id.ToString());
                            if (q.direction == 0) lv.SubItems.Add("Sell");
                            else lv.SubItems.Add("Buy");
                            lv.SubItems.Add(q.quantity.ToString());
                            lv.SubItems.Add(q.instrumentid.ToString());
                            lv.SubItems.Add(q.type.ToString());
                            lv.SubItems.Add(q.instrumenttype.ToString());
                            lv.SubItems.Add(q.tradeprice.ToString());
                            multiplethread thread = new multiplethread();
                            
                            double strike = Convert.ToDouble(q.strike);
                            bool iscall = q.type == "Call";
                            double tenor = Convert.ToDouble(q.tenor);
                            thread.Strike = strike; thread.Stockprice = q.underlying; thread.Time =1; thread.Step = 200; thread.Trial = 10000; thread.Vol = vol; thread.Rate = q.rate;
                            thread.getlength();
                            double[] list1 = thread.threadpractice(strike, q.underlying, q.rate, vol, 200, 10000, core, tenor, false, false, iscall);
                            PL = (list1[5] - q.tradeprice) * (q.direction - 0.5) * 2 * q.quantity;
                            delta = (q.direction - 0.5) * 2 * q.quantity * list1[0];
                            vega = list1[2] * (q.direction - 0.5) * 2 * q.quantity;
                            rho = list1[3] * (q.direction - 0.5) * 2 * q.quantity;
                            theta = list1[4] * (q.direction - 0.5) * 2 * q.quantity;
                            gamma = list1[1] * (q.direction - 0.5) * 2 * q.quantity;
                            lv.SubItems.Add(list1[5].ToString()); //simulation price
                            lv.SubItems.Add(PL.ToString());//P and L
                            lv.SubItems.Add(delta.ToString()); //delta
                            lv.SubItems.Add(gamma.ToString()); //gamma
                            lv.SubItems.Add(vega.ToString()); //vega
                            lv.SubItems.Add(theta.ToString());//theta
                            lv.SubItems.Add(rho.ToString());//rho
                            Listview_Alltrades.Items.Add(lv);
                        }
                        else if (instrumentype == "Asian") //if instrument type is asian option
                        {
                            ListViewItem lv = new ListViewItem(q.id.ToString());
                            if (q.direction == 0) lv.SubItems.Add("Sell");
                            else lv.SubItems.Add("Buy");
                            lv.SubItems.Add(q.quantity.ToString());
                            lv.SubItems.Add(q.instrumentid.ToString());
                            lv.SubItems.Add(q.type.ToString());
                            lv.SubItems.Add(q.instrumenttype.ToString());
                            lv.SubItems.Add(q.tradeprice.ToString());
                            multiplethread_asian thread = new multiplethread_asian();
                           
                            double strike = Convert.ToDouble(q.strike);
                            bool iscall = q.type == "Call";
                            double tenor = Convert.ToDouble(q.tenor);
                            thread.Strike = strike; thread.Stockprice = q.underlying; thread.Time = tenor; thread.Step = 200; thread.Trial = 10000; thread.Vol = vol; thread.Rate = q.rate;
                            thread.getlength();
                            double[] list1 = thread.threadpractice(strike, q.underlying, q.rate, vol, 200, 10000, core, tenor, false, false, iscall);
                            PL = (list1[5] - q.tradeprice) * (q.direction - 0.5) * 2 * q.quantity;
                            delta = (q.direction - 0.5) * 2 * q.quantity * list1[0];
                            vega = list1[2] * (q.direction - 0.5) * 2 * q.quantity;
                            rho = list1[3] * (q.direction - 0.5) * 2 * q.quantity;
                            theta = list1[4] * (q.direction - 0.5) * 2 * q.quantity;
                            gamma = list1[1] * (q.direction - 0.5) * 2 * q.quantity;
                            lv.SubItems.Add(list1[5].ToString()); //simulation price
                            lv.SubItems.Add(PL.ToString());//P and L
                            lv.SubItems.Add(delta.ToString()); //delta
                            lv.SubItems.Add(gamma.ToString()); //gamma
                            lv.SubItems.Add(vega.ToString()); //vega
                            lv.SubItems.Add(theta.ToString());//theta
                            lv.SubItems.Add(rho.ToString());//rho
                            Listview_Alltrades.Items.Add(lv);
                        }
                        else if (instrumentype == "Lookback")//if instrument type is lookback option
                        {
                            ListViewItem lv = new ListViewItem(q.id.ToString());
                            if (q.direction == 0) lv.SubItems.Add("Sell");
                            else lv.SubItems.Add("Buy");
                            lv.SubItems.Add(q.quantity.ToString());
                            lv.SubItems.Add(q.instrumentid.ToString());
                            lv.SubItems.Add(q.type.ToString());
                            lv.SubItems.Add(q.instrumenttype.ToString());
                            lv.SubItems.Add(q.tradeprice.ToString());
                            multiplethread_lookback thread = new multiplethread_lookback();
                            
                            double strike = Convert.ToDouble(q.strike);
                            bool iscall = q.type == "Call";
                            double tenor = Convert.ToDouble(q.tenor);
                            thread.Strike = strike; thread.Stockprice = q.underlying; thread.Time = tenor; thread.Step = 200; thread.Trial = 10000; thread.Vol = vol; thread.Rate = q.rate;
                            thread.getlength();
                            double[] list1 = thread.threadpractice(strike, q.underlying, q.rate, vol, 200, 10000, core, tenor, false, false, iscall);
                            PL = (list1[5] - q.tradeprice) * (q.direction - 0.5) * 2 * q.quantity;
                            delta = (q.direction - 0.5) * 2 * q.quantity * list1[0];
                            vega = list1[2] * (q.direction - 0.5) * 2 * q.quantity;
                            rho = list1[3] * (q.direction - 0.5) * 2 * q.quantity;
                            theta = list1[4] * (q.direction - 0.5) * 2 * q.quantity;
                            gamma = list1[1] * (q.direction - 0.5) * 2 * q.quantity;
                            lv.SubItems.Add(list1[5].ToString()); //simulation price
                            lv.SubItems.Add(PL.ToString());//P and L
                            lv.SubItems.Add(delta.ToString()); //delta
                            lv.SubItems.Add(gamma.ToString()); //gamma
                            lv.SubItems.Add(vega.ToString()); //vega
                            lv.SubItems.Add(theta.ToString());//theta
                            lv.SubItems.Add(rho.ToString());//rho
                            Listview_Alltrades.Items.Add(lv);
                        }
                        else if (instrumentype == "Digital")//if instrument type is digital option
                        {
                            ListViewItem lv = new ListViewItem(q.id.ToString());
                            if (q.direction == 0) lv.SubItems.Add("Sell");
                            else lv.SubItems.Add("Buy");
                            lv.SubItems.Add(q.quantity.ToString());
                            lv.SubItems.Add(q.instrumentid.ToString());
                            lv.SubItems.Add(q.type.ToString());
                            lv.SubItems.Add(q.instrumenttype.ToString());
                            lv.SubItems.Add(q.tradeprice.ToString());
                            multiplethread_digital thread = new multiplethread_digital();
                           
                            double strike = Convert.ToDouble(q.strike);
                            double rebate = Convert.ToDouble(q.rebate);
                            thread.Rebate = rebate;
                            bool iscall = q.type == "Call";
                            double tenor = Convert.ToDouble(q.tenor);
                            thread.Strike = strike; thread.Stockprice = q.underlying; thread.Time = tenor; thread.Step = 200; thread.Trial = 10000; thread.Vol = vol; thread.Rate = q.rate;
                            thread.getlength();
                            double[] list1 = thread.threadpractice(strike, q.underlying, q.rate, vol, 200, 10000, core, tenor, false, false, iscall);
                            PL = (list1[5] - q.tradeprice) * (q.direction - 0.5) * 2 * q.quantity;
                            delta = (q.direction - 0.5) * 2 * q.quantity * list1[0];
                            vega = list1[2] * (q.direction - 0.5) * 2 * q.quantity;
                            rho = list1[3] * (q.direction - 0.5) * 2 * q.quantity;
                            theta = list1[4] * (q.direction - 0.5) * 2 * q.quantity;
                            gamma = list1[1] * (q.direction - 0.5) * 2 * q.quantity;
                            lv.SubItems.Add(list1[5].ToString()); //simulation price
                            lv.SubItems.Add(PL.ToString());//P and L
                            lv.SubItems.Add(delta.ToString()); //delta
                            lv.SubItems.Add(gamma.ToString()); //gamma
                            lv.SubItems.Add(vega.ToString()); //vega
                            lv.SubItems.Add(theta.ToString());//theta
                            lv.SubItems.Add(rho.ToString());//rho
                            Listview_Alltrades.Items.Add(lv);
                        }
                        else if (instrumentype == "Range")//if instrument type is range option
                        {
                            ListViewItem lv = new ListViewItem(q.id.ToString());
                            if (q.direction == 0) lv.SubItems.Add("Sell");
                            else lv.SubItems.Add("Buy");
                            lv.SubItems.Add(q.quantity.ToString());
                            lv.SubItems.Add(q.instrumentid.ToString());
                            lv.SubItems.Add(q.type.ToString());
                            lv.SubItems.Add(q.instrumenttype.ToString());
                            lv.SubItems.Add(q.tradeprice.ToString());
                            multiplethread_range thread = new multiplethread_range(); 
                            double strike = 0;
                            double rebate = Convert.ToDouble(q.rebate);
                            bool iscall = q.type == "Call";
                            double tenor = Convert.ToDouble(q.tenor);
                            thread.Strike = strike; thread.Stockprice = q.underlying; thread.Time = tenor; thread.Step = 200; thread.Trial = 10000; thread.Vol = vol; thread.Rate = q.rate;
                            thread.getlength();
                            double[] list1 = thread.threadpractice(strike, q.underlying, q.rate, vol, 200, 10000, core, tenor, false, false, iscall);
                            PL = (list1[5] - q.tradeprice) * (q.direction - 0.5) * 2 * q.quantity;
                            delta = (q.direction - 0.5) * 2 * q.quantity * list1[0];
                            vega = list1[2] * (q.direction - 0.5) * 2 * q.quantity;
                            rho = list1[3] * (q.direction - 0.5) * 2 * q.quantity;
                            theta = list1[4] * (q.direction - 0.5) * 2 * q.quantity;
                            gamma = list1[1] * (q.direction - 0.5) * 2 * q.quantity;
                            lv.SubItems.Add(list1[5].ToString()); //simulation price
                            lv.SubItems.Add(PL.ToString());//P and L
                            lv.SubItems.Add(delta.ToString()); //delta
                            lv.SubItems.Add(gamma.ToString()); //gamma
                            lv.SubItems.Add(vega.ToString()); //vega
                            lv.SubItems.Add(theta.ToString());//theta
                            lv.SubItems.Add(rho.ToString());//rho
                            Listview_Alltrades.Items.Add(lv);
                        }
                        else //if instrument type is barrier option
                        {
                            ListViewItem lv = new ListViewItem(q.id.ToString());
                            if (q.direction == 0) lv.SubItems.Add("Sell");
                            else lv.SubItems.Add("Buy");
                            lv.SubItems.Add(q.quantity.ToString());
                            lv.SubItems.Add(q.instrumentid.ToString());
                            lv.SubItems.Add(q.type.ToString());
                            lv.SubItems.Add(q.instrumenttype.ToString());
                            lv.SubItems.Add(q.tradeprice.ToString());
                            multiplethread_barrier thread = new multiplethread_barrier(); 
                            double strike = 0;
                            double rebate = Convert.ToDouble(q.rebate);
                            bool iscall = q.type == "Call";
                            double barrier = Convert.ToDouble(q.barrier);
                            thread.Bar = barrier;//input barrier
                            thread.IsOut = q.instrumenttype == "Barrier(Down out)" || q.instrumenttype == "Barrier(Up out)";
                            thread.IsDown = q.instrumenttype == "Barrier(Down out)" || q.instrumenttype == "Barrier(Down in)";
                            double tenor = Convert.ToDouble(q.tenor);
                            thread.Strike = strike; thread.Stockprice = q.underlying; thread.Time = tenor; thread.Step = 200; thread.Trial = 10000; thread.Vol = vol; thread.Rate = q.rate;
                            thread.getlength();
                            double[] list1 = thread.threadpractice(strike, q.underlying, q.rate, vol, 200, 10000, core, tenor, false, false, iscall);
                            PL = (list1[5] - q.tradeprice) * (q.direction - 0.5) * 2 * q.quantity;
                            delta = (q.direction - 0.5) * 2 * q.quantity * list1[0];
                            vega = list1[2] * (q.direction - 0.5) * 2 * q.quantity;
                            rho = list1[3] * (q.direction - 0.5) * 2 * q.quantity;
                            theta = list1[4] * (q.direction - 0.5) * 2 * q.quantity;
                            gamma = list1[1] * (q.direction - 0.5) * 2 * q.quantity;
                            lv.SubItems.Add(list1[5].ToString()); //simulation price
                            lv.SubItems.Add(PL.ToString());//P and L
                            lv.SubItems.Add(delta.ToString()); //delta
                            lv.SubItems.Add(gamma.ToString()); //gamma
                            lv.SubItems.Add(vega.ToString()); //vega
                            lv.SubItems.Add(theta.ToString());//theta
                            lv.SubItems.Add(rho.ToString());//rho
                            Listview_Alltrades.Items.Add(lv);
                        }
                    }
                } MessageBox.Show("Simulation has been done!");//finish simulation
            }
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x);
            }
        }

       private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void textBox_vol_TextChanged(object sender, EventArgs e)
        {
            double a;
            if (!double.TryParse(textBox_vol.Text, out a))
                textBox_vol.BackColor = Color.Pink;
            else
                textBox_vol.BackColor = Color.White;
            try
            {
                Convert.ToDouble(textBox_vol.Text);
            }
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x);
            }
        }

        private void Listview_Alltrades_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            { //error handling
              //clear total list before use
                Listview_totals.Items.Clear();
                ListViewItem db = new ListViewItem();
                //find number of selected items
                int selectnum = Listview_Alltrades.SelectedItems.Count;
                int[] selectindex = new int[selectnum];
                for (int i = 0; i < selectnum; i++)
                {
                    selectindex[i] = Listview_Alltrades.SelectedIndices[i];
                }
                //get total result
                double PL = 0; double delta = 0; double gamma = 0; double vega = 0; double theta = 0; double rho = 0;
                foreach (int i in selectindex)//GET sum p&l and greek
                {
                    PL = PL + Convert.ToDouble(Listview_Alltrades.Items[i].SubItems[8].Text);
                    delta = delta + Convert.ToDouble(Listview_Alltrades.Items[i].SubItems[9].Text);
                    gamma = gamma + Convert.ToDouble(Listview_Alltrades.Items[i].SubItems[10].Text);
                    vega = vega + Convert.ToDouble(Listview_Alltrades.Items[i].SubItems[11].Text);
                    theta = theta + Convert.ToDouble(Listview_Alltrades.Items[i].SubItems[12].Text);
                    rho = rho + Convert.ToDouble(Listview_Alltrades.Items[i].SubItems[13].Text);
                }
                db.Text = PL.ToString();
                db.SubItems.Add(delta.ToString());
                db.SubItems.Add(gamma.ToString());
                db.SubItems.Add(vega.ToString());
                db.SubItems.Add(theta.ToString());
                db.SubItems.Add(rho.ToString());
                Listview_totals.Items.Add(db); //display in total listview
            }
            catch (Exception x)
            {
                MessageBox.Show("Error:" + x);
            }
        }

        private void addSampleToolStripMenuItem_Click(object sender, EventArgs e) //add sample data to database
        {if (!container.Entity_Historyprice.Any()) //if there is no data in database
            {//insert basic element to database before users using
                container.Entity_rate.Add(new Entity_rate() { Id = 1, Tenor = 1, Interest_rate = 0.05 });
                container.Entity_rate.Add(new Entity_rate() { Id = 2, Tenor = 2, Interest_rate = 0.08 });
                container.Entity_rate.Add(new Entity_rate() { Id = 3, Tenor = 0.5, Interest_rate = 0.03 });
                DateTime date = new DateTime(2020, 05, 08);
                container.Entity_Historyprice.Add(new Entity_Historyprice() { Id = 1, Ticker = "MSFT", CompanyName = "Microsoft", ClosePrice = 184, Date = date });
                container.Entity_Historyprice.Add(new Entity_Historyprice() { Id = 2, Ticker = "AAPL", CompanyName = "APPLE.Inc", ClosePrice = 310.13, Date = date });
                container.Entity_Historyprice.Add(new Entity_Historyprice() { Id = 3, Ticker = "HPQ", CompanyName = "HP.Inc", ClosePrice = 15.66, Date = date });
                container.Entity_Historyprice.Add(new Entity_Historyprice() { Id = 4, Ticker = "AMZN", CompanyName = "Amazon", ClosePrice = 2379.61, Date = date });
                container.Entity_instrument.Add(new Entity_instrument() { Id = 1, Tenor = 1, Entity_rateId = 1, CompanyName = "Microsoft", Ticker = "MSFT", Instype = "Stock", Exchange = "NASDAQ", Underlying = 184 });
                container.Entity_instrument.Add(new Entity_instrument() { Id = 2, Tenor = 1, Entity_rateId = 1, CompanyName = "Apple", Ticker = "AAPL", Instype = "Stock", Exchange = "NASDAQ", Underlying = 310.13 });
                container.Entity_instrument.Add(new Entity_instrument() { Id = 3, Tenor = 1, Entity_rateId = 1, CompanyName = "HP.Inc", Ticker = "HPQ", Instype = "Stock", Exchange = "NASDAQ", Underlying = 15.66 });
                container.Entity_instrument.Add(new Entity_instrument() { Id = 4, Tenor = 1, Entity_rateId = 1, CompanyName = "Amazon", Ticker = "AMZN", Instype = "Stock", Exchange = "NASDAQ", Underlying = 2379.61 });
                container.Entity_instrument.Add(new Entity_instrument() { Id = 5, Tenor = 1, Entity_rateId = 1, CompanyName = "Microsoft", Ticker = "MSFT", Instype = "European", Exchange = "NASDAQ", Underlying = 184, Strike = 180.ToString(), Type = "Call" });
                container.Entity_instrument.Add(new Entity_instrument() { Id = 6, Tenor = 1, Entity_rateId = 1, CompanyName = "Microsoft", Ticker = "MSFT", Instype = "Asian", Exchange = "NASDAQ", Underlying = 184, Strike = 180.ToString(), Type = "Call" });
                container.Entity_instrument.Add(new Entity_instrument() { Id = 7, Tenor = 1, Entity_rateId = 1, CompanyName = "Microsoft", Ticker = "MSFT", Instype = "Lookback", Exchange = "NASDAQ", Underlying = 184, Strike = 180.ToString(), Type = "Call" });
                container.Entity_instrument.Add(new Entity_instrument() { Id = 8, Tenor = 1, Entity_rateId = 1, CompanyName = "Microsoft", Ticker = "MSFT", Instype = "Digital", Exchange = "NASDAQ", Underlying = 184, Strike = 180.ToString(), Type = "Call", Rebate = 10 });
                container.Entity_instrument.Add(new Entity_instrument() { Id = 9, Tenor = 1, Entity_rateId = 1, CompanyName = "Microsoft", Ticker = "MSFT", Instype = "Barrier", Exchange = "NASDAQ", Underlying = 184, Strike = 180.ToString(), Type = "Call", Barrier = 190 });
                container.Entity_instrument.Add(new Entity_instrument() { Id = 10, Tenor = 1, Entity_rateId = 1, CompanyName = "Microsoft", Ticker = "MSFT", Instype = "Range", Exchange = "NASDAQ", Underlying = 184 });
                container.Entity_Trade.Add(new Entity_Trade() { Id = 1, Quantity = 100, Direction = 0, Entity_instrumentId = 1, Tradeprice = 178 });
                container.Entity_Trade.Add(new Entity_Trade() { Id = 2, Quantity = 100, Direction = 1, Entity_instrumentId = 5, Tradeprice = 20 });
                container.Entity_Trade.Add(new Entity_Trade() { Id = 3, Quantity = 100, Direction = 1, Entity_instrumentId = 6, Tradeprice = 15 });
                container.Entity_Trade.Add(new Entity_Trade() { Id = 4, Quantity = 100, Direction = 0, Entity_instrumentId = 7, Tradeprice = 30 });
                container.SaveChanges();
                MessageBox.Show("You add sample successfully!");
            }
        else
            { MessageBox.Show("Add sample failed: Sample already exists!"); }
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sampleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
