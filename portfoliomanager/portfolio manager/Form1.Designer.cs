namespace portfolio_manager
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Listview_totals = new System.Windows.Forms.ListView();
            this.columnHeaderPL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDELTA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderGAMMA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderVEGA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTHETA = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderRHO = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.instrumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tradeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.historyPriceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.interestRateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshTradesFromDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.priceBookUsingSimulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.instrumentToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tradeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.interestRateToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.historyPriceToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.sampleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSampleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox_vol = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Listview_Alltrades = new System.Windows.Forms.ListView();
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_DIRECTION = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Listview_totals
            // 
            this.Listview_totals.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderPL,
            this.columnHeaderDELTA,
            this.columnHeaderGAMMA,
            this.columnHeaderVEGA,
            this.columnHeaderTHETA,
            this.columnHeaderRHO});
            this.Listview_totals.HideSelection = false;
            this.Listview_totals.Location = new System.Drawing.Point(24, 146);
            this.Listview_totals.Name = "Listview_totals";
            this.Listview_totals.Size = new System.Drawing.Size(1122, 135);
            this.Listview_totals.TabIndex = 2;
            this.Listview_totals.UseCompatibleStateImageBehavior = false;
            this.Listview_totals.View = System.Windows.Forms.View.Details;
            this.Listview_totals.SelectedIndexChanged += new System.EventHandler(this.totals_SelectedIndexChanged);
            // 
            // columnHeaderPL
            // 
            this.columnHeaderPL.Text = "P&L";
            this.columnHeaderPL.Width = 180;
            // 
            // columnHeaderDELTA
            // 
            this.columnHeaderDELTA.Text = "DELTA";
            this.columnHeaderDELTA.Width = 180;
            // 
            // columnHeaderGAMMA
            // 
            this.columnHeaderGAMMA.Text = "GAMMA";
            this.columnHeaderGAMMA.Width = 180;
            // 
            // columnHeaderVEGA
            // 
            this.columnHeaderVEGA.Text = "VEGA";
            this.columnHeaderVEGA.Width = 180;
            // 
            // columnHeaderTHETA
            // 
            this.columnHeaderTHETA.Text = "THETA";
            this.columnHeaderTHETA.Width = 180;
            // 
            // columnHeaderRHO
            // 
            this.columnHeaderRHO.Text = "RHO";
            this.columnHeaderRHO.Width = 180;
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.sampleToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1227, 42);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fToolStripMenuItem,
            this.refreshTradesFromDatabaseToolStripMenuItem,
            this.priceBookUsingSimulationToolStripMenuItem,
            this.exitToolStripMenuItem,
            this.exitToolStripMenuItem1});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(72, 38);
            this.newToolStripMenuItem.Text = "File";
            // 
            // fToolStripMenuItem
            // 
            this.fToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.instrumentToolStripMenuItem,
            this.tradeToolStripMenuItem,
            this.historyPriceToolStripMenuItem,
            this.interestRateToolStripMenuItem});
            this.fToolStripMenuItem.Name = "fToolStripMenuItem";
            this.fToolStripMenuItem.Size = new System.Drawing.Size(460, 44);
            this.fToolStripMenuItem.Text = "New";
            // 
            // instrumentToolStripMenuItem
            // 
            this.instrumentToolStripMenuItem.Name = "instrumentToolStripMenuItem";
            this.instrumentToolStripMenuItem.Size = new System.Drawing.Size(283, 44);
            this.instrumentToolStripMenuItem.Text = "Instrument";
            this.instrumentToolStripMenuItem.Click += new System.EventHandler(this.instrumentToolStripMenuItem_Click);
            // 
            // tradeToolStripMenuItem
            // 
            this.tradeToolStripMenuItem.Name = "tradeToolStripMenuItem";
            this.tradeToolStripMenuItem.Size = new System.Drawing.Size(283, 44);
            this.tradeToolStripMenuItem.Text = "Trade";
            this.tradeToolStripMenuItem.Click += new System.EventHandler(this.tradeToolStripMenuItem_Click);
            // 
            // historyPriceToolStripMenuItem
            // 
            this.historyPriceToolStripMenuItem.Name = "historyPriceToolStripMenuItem";
            this.historyPriceToolStripMenuItem.Size = new System.Drawing.Size(283, 44);
            this.historyPriceToolStripMenuItem.Text = "History Price";
            this.historyPriceToolStripMenuItem.Click += new System.EventHandler(this.historyPriceToolStripMenuItem_Click);
            // 
            // interestRateToolStripMenuItem
            // 
            this.interestRateToolStripMenuItem.Name = "interestRateToolStripMenuItem";
            this.interestRateToolStripMenuItem.Size = new System.Drawing.Size(283, 44);
            this.interestRateToolStripMenuItem.Text = "Interest Rate";
            this.interestRateToolStripMenuItem.Click += new System.EventHandler(this.interestRateToolStripMenuItem_Click);
            // 
            // refreshTradesFromDatabaseToolStripMenuItem
            // 
            this.refreshTradesFromDatabaseToolStripMenuItem.Name = "refreshTradesFromDatabaseToolStripMenuItem";
            this.refreshTradesFromDatabaseToolStripMenuItem.Size = new System.Drawing.Size(460, 44);
            this.refreshTradesFromDatabaseToolStripMenuItem.Text = "Refresh trades from database";
            this.refreshTradesFromDatabaseToolStripMenuItem.Click += new System.EventHandler(this.refreshTradesFromDatabaseToolStripMenuItem_Click);
            // 
            // priceBookUsingSimulationToolStripMenuItem
            // 
            this.priceBookUsingSimulationToolStripMenuItem.Name = "priceBookUsingSimulationToolStripMenuItem";
            this.priceBookUsingSimulationToolStripMenuItem.Size = new System.Drawing.Size(460, 44);
            this.priceBookUsingSimulationToolStripMenuItem.Text = "Price book using simulation";
            this.priceBookUsingSimulationToolStripMenuItem.Click += new System.EventHandler(this.priceBookUsingSimulationToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.instrumentToolStripMenuItem1,
            this.tradeToolStripMenuItem1,
            this.interestRateToolStripMenuItem1,
            this.historyPriceToolStripMenuItem1});
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(460, 44);
            this.exitToolStripMenuItem.Text = "Edit";
            // 
            // instrumentToolStripMenuItem1
            // 
            this.instrumentToolStripMenuItem1.Name = "instrumentToolStripMenuItem1";
            this.instrumentToolStripMenuItem1.Size = new System.Drawing.Size(283, 44);
            this.instrumentToolStripMenuItem1.Text = "Instrument";
            this.instrumentToolStripMenuItem1.Click += new System.EventHandler(this.instrumentToolStripMenuItem1_Click);
            // 
            // tradeToolStripMenuItem1
            // 
            this.tradeToolStripMenuItem1.Name = "tradeToolStripMenuItem1";
            this.tradeToolStripMenuItem1.Size = new System.Drawing.Size(283, 44);
            this.tradeToolStripMenuItem1.Text = "Trade";
            this.tradeToolStripMenuItem1.Click += new System.EventHandler(this.tradeToolStripMenuItem1_Click);
            // 
            // interestRateToolStripMenuItem1
            // 
            this.interestRateToolStripMenuItem1.Name = "interestRateToolStripMenuItem1";
            this.interestRateToolStripMenuItem1.Size = new System.Drawing.Size(283, 44);
            this.interestRateToolStripMenuItem1.Text = "Interest Rate";
            this.interestRateToolStripMenuItem1.Click += new System.EventHandler(this.interestRateToolStripMenuItem1_Click);
            // 
            // historyPriceToolStripMenuItem1
            // 
            this.historyPriceToolStripMenuItem1.Name = "historyPriceToolStripMenuItem1";
            this.historyPriceToolStripMenuItem1.Size = new System.Drawing.Size(283, 44);
            this.historyPriceToolStripMenuItem1.Text = "History Price";
            this.historyPriceToolStripMenuItem1.Click += new System.EventHandler(this.historyPriceToolStripMenuItem1_Click);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(460, 44);
            this.exitToolStripMenuItem1.Text = "Exit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // sampleToolStripMenuItem
            // 
            this.sampleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSampleToolStripMenuItem});
            this.sampleToolStripMenuItem.Name = "sampleToolStripMenuItem";
            this.sampleToolStripMenuItem.Size = new System.Drawing.Size(114, 38);
            this.sampleToolStripMenuItem.Text = "Sample";
            this.sampleToolStripMenuItem.Click += new System.EventHandler(this.sampleToolStripMenuItem_Click);
            // 
            // addSampleToolStripMenuItem
            // 
            this.addSampleToolStripMenuItem.Name = "addSampleToolStripMenuItem";
            this.addSampleToolStripMenuItem.Size = new System.Drawing.Size(275, 44);
            this.addSampleToolStripMenuItem.Text = "Add sample";
            this.addSampleToolStripMenuItem.Click += new System.EventHandler(this.addSampleToolStripMenuItem_Click);
            // 
            // textBox_vol
            // 
            this.textBox_vol.Location = new System.Drawing.Point(230, 61);
            this.textBox_vol.Name = "textBox_vol";
            this.textBox_vol.Size = new System.Drawing.Size(100, 31);
            this.textBox_vol.TabIndex = 5;
            this.textBox_vol.TextChanged += new System.EventHandler(this.textBox_vol_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(168, 25);
            this.label4.TabIndex = 6;
            this.label4.Text = "Pricing volatility:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "Totals:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 315);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "All Trades:";
            // 
            // Listview_Alltrades
            // 
            this.Listview_Alltrades.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID,
            this.columnHeader_DIRECTION,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader12});
            this.Listview_Alltrades.FullRowSelect = true;
            this.Listview_Alltrades.HideSelection = false;
            this.Listview_Alltrades.Location = new System.Drawing.Point(24, 351);
            this.Listview_Alltrades.Name = "Listview_Alltrades";
            this.Listview_Alltrades.Size = new System.Drawing.Size(1122, 154);
            this.Listview_Alltrades.TabIndex = 9;
            this.Listview_Alltrades.UseCompatibleStateImageBehavior = false;
            this.Listview_Alltrades.View = System.Windows.Forms.View.Details;
            this.Listview_Alltrades.SelectedIndexChanged += new System.EventHandler(this.Listview_Alltrades_SelectedIndexChanged);
            // 
            // ID
            // 
            this.ID.Text = "ID";
            this.ID.Width = 76;
            // 
            // columnHeader_DIRECTION
            // 
            this.columnHeader_DIRECTION.Text = "Direction";
            this.columnHeader_DIRECTION.Width = 127;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Quantity";
            this.columnHeader3.Width = 126;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "InstrumentId";
            this.columnHeader4.Width = 130;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Type";
            this.columnHeader5.Width = 99;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "InstrumentType";
            this.columnHeader6.Width = 169;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "TradePrice";
            this.columnHeader7.Width = 135;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Mark Price";
            this.columnHeader8.Width = 138;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "P&L";
            this.columnHeader9.Width = 108;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Delta";
            this.columnHeader10.Width = 180;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Gamma";
            this.columnHeader11.Width = 180;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "vega";
            this.columnHeader1.Width = 180;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Theta";
            this.columnHeader2.Width = 180;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Rho";
            this.columnHeader12.Width = 180;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1227, 568);
            this.Controls.Add(this.Listview_Alltrades);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_vol);
            this.Controls.Add(this.Listview_totals);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "portfolio manager";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView Listview_totals;
        private System.Windows.Forms.ColumnHeader columnHeaderPL;
        private System.Windows.Forms.ColumnHeader columnHeaderDELTA;
        private System.Windows.Forms.ColumnHeader columnHeaderGAMMA;
        private System.Windows.Forms.ColumnHeader columnHeaderVEGA;
        private System.Windows.Forms.ColumnHeader columnHeaderTHETA;
        private System.Windows.Forms.ColumnHeader columnHeaderRHO;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem instrumentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tradeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem historyPriceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem interestRateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshTradesFromDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem priceBookUsingSimulationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox_vol;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem instrumentToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tradeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem interestRateToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem historyPriceToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ListView Listview_Alltrades;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader columnHeader_DIRECTION;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ToolStripMenuItem sampleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSampleToolStripMenuItem;
    }
}

