namespace portfolio_manager
{
    partial class formtrade
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
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_tradeID = new System.Windows.Forms.TextBox();
            this.textBox_quantity = new System.Windows.Forms.TextBox();
            this.textBox_ticker = new System.Windows.Forms.TextBox();
            this.textBox_tradeprice = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBox_direction = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_instid = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 187);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ticker";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(60, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "TradeID";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(60, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 25);
            this.label5.TabIndex = 4;
            this.label5.Text = "Tradeprice";
            // 
            // textBox_tradeID
            // 
            this.textBox_tradeID.Location = new System.Drawing.Point(268, 38);
            this.textBox_tradeID.Name = "textBox_tradeID";
            this.textBox_tradeID.Size = new System.Drawing.Size(142, 31);
            this.textBox_tradeID.TabIndex = 5;
            // 
            // textBox_quantity
            // 
            this.textBox_quantity.Location = new System.Drawing.Point(268, 291);
            this.textBox_quantity.Name = "textBox_quantity";
            this.textBox_quantity.Size = new System.Drawing.Size(142, 31);
            this.textBox_quantity.TabIndex = 6;
            // 
            // textBox_ticker
            // 
            this.textBox_ticker.Location = new System.Drawing.Point(268, 181);
            this.textBox_ticker.Name = "textBox_ticker";
            this.textBox_ticker.Size = new System.Drawing.Size(142, 31);
            this.textBox_ticker.TabIndex = 7;
            // 
            // textBox_tradeprice
            // 
            this.textBox_tradeprice.Location = new System.Drawing.Point(268, 94);
            this.textBox_tradeprice.Name = "textBox_tradeprice";
            this.textBox_tradeprice.Size = new System.Drawing.Size(142, 31);
            this.textBox_tradeprice.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(43, 461);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 52);
            this.button1.TabIndex = 10;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(373, 461);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(117, 52);
            this.button2.TabIndex = 12;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(60, 144);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 25);
            this.label9.TabIndex = 16;
            this.label9.Text = "Direction";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(60, 291);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(92, 25);
            this.label10.TabIndex = 17;
            this.label10.Text = "Quantity";
            // 
            // comboBox_direction
            // 
            this.comboBox_direction.FormattingEnabled = true;
            this.comboBox_direction.Items.AddRange(new object[] {
            "Buy",
            "Sell"});
            this.comboBox_direction.Location = new System.Drawing.Point(268, 136);
            this.comboBox_direction.Name = "comboBox_direction";
            this.comboBox_direction.Size = new System.Drawing.Size(142, 33);
            this.comboBox_direction.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(60, 238);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 25);
            this.label3.TabIndex = 22;
            this.label3.Text = "instrument id";
            // 
            // textBox_instid
            // 
            this.textBox_instid.Location = new System.Drawing.Point(268, 235);
            this.textBox_instid.Name = "textBox_instid";
            this.textBox_instid.Size = new System.Drawing.Size(142, 31);
            this.textBox_instid.TabIndex = 23;
            // 
            // formtrade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 545);
            this.Controls.Add(this.textBox_instid);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox_direction);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox_tradeprice);
            this.Controls.Add(this.textBox_ticker);
            this.Controls.Add(this.textBox_quantity);
            this.Controls.Add(this.textBox_tradeID);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Name = "formtrade";
            this.Text = "formtrade";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_tradeID;
        private System.Windows.Forms.TextBox textBox_quantity;
        private System.Windows.Forms.TextBox textBox_ticker;
        private System.Windows.Forms.TextBox textBox_tradeprice;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBox_direction;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_instid;
    }
}