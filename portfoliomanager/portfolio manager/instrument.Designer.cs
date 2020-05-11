namespace portfolio_manager
{
    partial class forminstrument
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.comboBox_istype = new System.Windows.Forms.ComboBox();
            this.comboBox_type = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_companyname = new System.Windows.Forms.TextBox();
            this.textBox_rebate = new System.Windows.Forms.TextBox();
            this.textBox_tenor = new System.Windows.Forms.TextBox();
            this.textBox_strike = new System.Windows.Forms.TextBox();
            this.textBox_Underlying = new System.Windows.Forms.TextBox();
            this.textBox_Ticker = new System.Windows.Forms.TextBox();
            this.textBox_exchange = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox_id = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox_barrier = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox_rateid = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Company name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 543);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Type:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 337);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tenor:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 244);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Strike:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 192);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 25);
            this.label5.TabIndex = 4;
            this.label5.Text = "Underlying:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(37, 142);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 25);
            this.label6.TabIndex = 5;
            this.label6.Text = "Ticker:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(114, 25);
            this.label7.TabIndex = 6;
            this.label7.Text = "Exchange:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 392);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 25);
            this.label8.TabIndex = 7;
            this.label8.Text = "Rebate:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(27, 494);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 25);
            this.label12.TabIndex = 11;
            this.label12.Text = "InstType:";
            // 
            // comboBox_istype
            // 
            this.comboBox_istype.FormattingEnabled = true;
            this.comboBox_istype.Items.AddRange(new object[] {
            "Stock",
            "European",
            "Range",
            "Lookback",
            "Asian",
            "Barrier(Up in)",
            "Barrier(Up out)",
            "Barrier(Down in)",
            "Barrier(Down out)",
            "Digital"});
            this.comboBox_istype.Location = new System.Drawing.Point(219, 486);
            this.comboBox_istype.Name = "comboBox_istype";
            this.comboBox_istype.Size = new System.Drawing.Size(249, 33);
            this.comboBox_istype.TabIndex = 17;
            this.comboBox_istype.Text = "Instrument type";
            this.comboBox_istype.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox_type
            // 
            this.comboBox_type.FormattingEnabled = true;
            this.comboBox_type.Items.AddRange(new object[] {
            "Call",
            "Put",
            "Neither call nor put"});
            this.comboBox_type.Location = new System.Drawing.Point(219, 543);
            this.comboBox_type.Name = "comboBox_type";
            this.comboBox_type.Size = new System.Drawing.Size(249, 33);
            this.comboBox_type.TabIndex = 18;
            this.comboBox_type.Text = "Call or put";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(33, 582);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 46);
            this.button1.TabIndex = 19;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(297, 582);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(128, 46);
            this.button2.TabIndex = 22;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(37, 347);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 25);
            this.label9.TabIndex = 23;
            // 
            // textBox_companyname
            // 
            this.textBox_companyname.Location = new System.Drawing.Point(219, 56);
            this.textBox_companyname.Name = "textBox_companyname";
            this.textBox_companyname.Size = new System.Drawing.Size(206, 31);
            this.textBox_companyname.TabIndex = 24;
            // 
            // textBox_rebate
            // 
            this.textBox_rebate.Location = new System.Drawing.Point(219, 386);
            this.textBox_rebate.Name = "textBox_rebate";
            this.textBox_rebate.Size = new System.Drawing.Size(206, 31);
            this.textBox_rebate.TabIndex = 25;
            this.textBox_rebate.TextChanged += new System.EventHandler(this.textBox_rebate_TextChanged);
            // 
            // textBox_tenor
            // 
            this.textBox_tenor.Location = new System.Drawing.Point(219, 331);
            this.textBox_tenor.Name = "textBox_tenor";
            this.textBox_tenor.Size = new System.Drawing.Size(206, 31);
            this.textBox_tenor.TabIndex = 26;
            this.textBox_tenor.TextChanged += new System.EventHandler(this.textBox_tenor_TextChanged);
            // 
            // textBox_strike
            // 
            this.textBox_strike.Location = new System.Drawing.Point(219, 238);
            this.textBox_strike.Name = "textBox_strike";
            this.textBox_strike.Size = new System.Drawing.Size(206, 31);
            this.textBox_strike.TabIndex = 27;
            this.textBox_strike.TextChanged += new System.EventHandler(this.textBox_strike_TextChanged);
            // 
            // textBox_Underlying
            // 
            this.textBox_Underlying.Location = new System.Drawing.Point(219, 192);
            this.textBox_Underlying.Name = "textBox_Underlying";
            this.textBox_Underlying.Size = new System.Drawing.Size(206, 31);
            this.textBox_Underlying.TabIndex = 28;
            this.textBox_Underlying.TextChanged += new System.EventHandler(this.textBox_Underlying_TextChanged);
            // 
            // textBox_Ticker
            // 
            this.textBox_Ticker.Location = new System.Drawing.Point(219, 136);
            this.textBox_Ticker.Name = "textBox_Ticker";
            this.textBox_Ticker.Size = new System.Drawing.Size(206, 31);
            this.textBox_Ticker.TabIndex = 29;
            // 
            // textBox_exchange
            // 
            this.textBox_exchange.Location = new System.Drawing.Point(219, 97);
            this.textBox_exchange.Name = "textBox_exchange";
            this.textBox_exchange.Size = new System.Drawing.Size(206, 31);
            this.textBox_exchange.TabIndex = 30;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 12);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(141, 25);
            this.label10.TabIndex = 31;
            this.label10.Text = "Instrument Id:";
            // 
            // textBox_id
            // 
            this.textBox_id.Location = new System.Drawing.Point(219, 12);
            this.textBox_id.Name = "textBox_id";
            this.textBox_id.Size = new System.Drawing.Size(206, 31);
            this.textBox_id.TabIndex = 32;
            this.textBox_id.TextChanged += new System.EventHandler(this.textBox_id_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(27, 440);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 25);
            this.label11.TabIndex = 33;
            this.label11.Text = "Barrier:";
            // 
            // textBox_barrier
            // 
            this.textBox_barrier.Location = new System.Drawing.Point(219, 434);
            this.textBox_barrier.Name = "textBox_barrier";
            this.textBox_barrier.Size = new System.Drawing.Size(206, 31);
            this.textBox_barrier.TabIndex = 34;
            this.textBox_barrier.TextChanged += new System.EventHandler(this.textBox_barrier_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(30, 297);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(80, 25);
            this.label13.TabIndex = 35;
            this.label13.Text = "Rateid:";
            // 
            // textBox_rateid
            // 
            this.textBox_rateid.Location = new System.Drawing.Point(219, 291);
            this.textBox_rateid.Name = "textBox_rateid";
            this.textBox_rateid.Size = new System.Drawing.Size(206, 31);
            this.textBox_rateid.TabIndex = 36;
            this.textBox_rateid.TextChanged += new System.EventHandler(this.textBox_rateid_TextChanged);
            // 
            // forminstrument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 655);
            this.Controls.Add(this.textBox_rateid);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.textBox_barrier);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBox_id);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBox_exchange);
            this.Controls.Add(this.textBox_Ticker);
            this.Controls.Add(this.textBox_Underlying);
            this.Controls.Add(this.textBox_strike);
            this.Controls.Add(this.textBox_tenor);
            this.Controls.Add(this.textBox_rebate);
            this.Controls.Add(this.textBox_companyname);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox_type);
            this.Controls.Add(this.comboBox_istype);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "forminstrument";
            this.Text = "instrument";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox comboBox_istype;
        private System.Windows.Forms.ComboBox comboBox_type;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_companyname;
        private System.Windows.Forms.TextBox textBox_rebate;
        private System.Windows.Forms.TextBox textBox_tenor;
        private System.Windows.Forms.TextBox textBox_strike;
        private System.Windows.Forms.TextBox textBox_Underlying;
        private System.Windows.Forms.TextBox textBox_Ticker;
        private System.Windows.Forms.TextBox textBox_exchange;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox_id;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox_barrier;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox_rateid;
    }
}