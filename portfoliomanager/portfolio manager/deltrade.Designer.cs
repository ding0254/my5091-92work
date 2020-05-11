namespace portfolio_manager
{
    partial class deltrade
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
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox_instid = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_tradeprice = new System.Windows.Forms.TextBox();
            this.textBox_quantity = new System.Windows.Forms.TextBox();
            this.textBox_tradeID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.textBox_direction = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(417, 405);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(117, 52);
            this.button2.TabIndex = 14;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(77, 405);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 52);
            this.button1.TabIndex = 13;
            this.button1.Text = "Update";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox_instid
            // 
            this.textBox_instid.Location = new System.Drawing.Point(328, 289);
            this.textBox_instid.Name = "textBox_instid";
            this.textBox_instid.Size = new System.Drawing.Size(142, 31);
            this.textBox_instid.TabIndex = 35;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(120, 292);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 25);
            this.label3.TabIndex = 34;
            this.label3.Text = "instrument id";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(120, 345);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(92, 25);
            this.label10.TabIndex = 32;
            this.label10.Text = "Quantity";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(120, 207);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 25);
            this.label9.TabIndex = 31;
            this.label9.Text = "Direction";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // textBox_tradeprice
            // 
            this.textBox_tradeprice.Location = new System.Drawing.Point(328, 148);
            this.textBox_tradeprice.Name = "textBox_tradeprice";
            this.textBox_tradeprice.Size = new System.Drawing.Size(142, 31);
            this.textBox_tradeprice.TabIndex = 30;
            this.textBox_tradeprice.TextChanged += new System.EventHandler(this.textBox_tradeprice_TextChanged);
            // 
            // textBox_quantity
            // 
            this.textBox_quantity.Location = new System.Drawing.Point(328, 345);
            this.textBox_quantity.Name = "textBox_quantity";
            this.textBox_quantity.Size = new System.Drawing.Size(142, 31);
            this.textBox_quantity.TabIndex = 28;
            // 
            // textBox_tradeID
            // 
            this.textBox_tradeID.Location = new System.Drawing.Point(328, 41);
            this.textBox_tradeID.Name = "textBox_tradeID";
            this.textBox_tradeID.Size = new System.Drawing.Size(142, 31);
            this.textBox_tradeID.TabIndex = 27;
            this.textBox_tradeID.TextChanged += new System.EventHandler(this.textBox_tradeID_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(120, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 25);
            this.label5.TabIndex = 26;
            this.label5.Text = "Tradeprice";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(120, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 25);
            this.label4.TabIndex = 25;
            this.label4.Text = "TradeID";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(246, 405);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(114, 52);
            this.button3.TabIndex = 36;
            this.button3.Text = "Delete";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(346, 90);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(114, 52);
            this.button4.TabIndex = 37;
            this.button4.Text = "Search";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // textBox_direction
            // 
            this.textBox_direction.Location = new System.Drawing.Point(328, 207);
            this.textBox_direction.Name = "textBox_direction";
            this.textBox_direction.Size = new System.Drawing.Size(142, 31);
            this.textBox_direction.TabIndex = 38;
            this.textBox_direction.TextChanged += new System.EventHandler(this.textBox_direction_TextChanged);
            // 
            // deltrade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 469);
            this.Controls.Add(this.textBox_direction);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBox_instid);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBox_tradeprice);
            this.Controls.Add(this.textBox_quantity);
            this.Controls.Add(this.textBox_tradeID);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "deltrade";
            this.Text = "delete trade";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox_instid;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_tradeprice;
        private System.Windows.Forms.TextBox textBox_quantity;
        private System.Windows.Forms.TextBox textBox_tradeID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBox_direction;
    }
}