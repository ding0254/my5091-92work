namespace portfolio_manager
{
    partial class rateform
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_tenor = new System.Windows.Forms.TextBox();
            this.textBox_rate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_id = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Rate:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 203);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 39);
            this.button1.TabIndex = 10;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(275, 203);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(133, 39);
            this.button2.TabIndex = 11;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 25);
            this.label3.TabIndex = 13;
            this.label3.Text = "Tenor:";
            // 
            // textBox_tenor
            // 
            this.textBox_tenor.Location = new System.Drawing.Point(224, 80);
            this.textBox_tenor.Name = "textBox_tenor";
            this.textBox_tenor.Size = new System.Drawing.Size(157, 31);
            this.textBox_tenor.TabIndex = 19;
            this.textBox_tenor.TextChanged += new System.EventHandler(this.textBox_tenor_TextChanged);
            // 
            // textBox_rate
            // 
            this.textBox_rate.Location = new System.Drawing.Point(224, 140);
            this.textBox_rate.Name = "textBox_rate";
            this.textBox_rate.Size = new System.Drawing.Size(157, 31);
            this.textBox_rate.TabIndex = 20;
            this.textBox_rate.TextChanged += new System.EventHandler(this.textBox_rate_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 25);
            this.label2.TabIndex = 21;
            this.label2.Text = "Id:";
            // 
            // textBox_id
            // 
            this.textBox_id.Location = new System.Drawing.Point(224, 18);
            this.textBox_id.Name = "textBox_id";
            this.textBox_id.Size = new System.Drawing.Size(157, 31);
            this.textBox_id.TabIndex = 22;
            this.textBox_id.TextChanged += new System.EventHandler(this.textBox_id_TextChanged);
            // 
            // rateform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 301);
            this.Controls.Add(this.textBox_id);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_rate);
            this.Controls.Add(this.textBox_tenor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Name = "rateform";
            this.Text = "Interest rate";
            this.Load += new System.EventHandler(this.rateform_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_tenor;
        private System.Windows.Forms.TextBox textBox_rate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_id;
    }
}