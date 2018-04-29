namespace winch
{
    partial class webdoc
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btngo = new System.Windows.Forms.Button();
            this.txturl = new System.Windows.Forms.TextBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.btnget = new System.Windows.Forms.Button();
            this.txtSelector = new System.Windows.Forms.TextBox();
            this.txtText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVal = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHtml = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtHtml);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtVal);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtText);
            this.panel1.Controls.Add(this.txtSelector);
            this.panel1.Controls.Add(this.btnget);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(806, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 697);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txturl);
            this.panel2.Controls.Add(this.btngo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(806, 71);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.webBrowser1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 71);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(806, 626);
            this.panel3.TabIndex = 2;
            // 
            // btngo
            // 
            this.btngo.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btngo.Location = new System.Drawing.Point(588, 14);
            this.btngo.Name = "btngo";
            this.btngo.Size = new System.Drawing.Size(130, 29);
            this.btngo.TabIndex = 0;
            this.btngo.Text = "go";
            this.btngo.UseVisualStyleBackColor = true;
            this.btngo.Click += new System.EventHandler(this.btngo_Click);
            // 
            // txturl
            // 
            this.txturl.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txturl.Location = new System.Drawing.Point(24, 15);
            this.txturl.Name = "txturl";
            this.txturl.Size = new System.Drawing.Size(544, 31);
            this.txturl.TabIndex = 1;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(806, 626);
            this.webBrowser1.TabIndex = 0;
            // 
            // btnget
            // 
            this.btnget.Location = new System.Drawing.Point(16, 80);
            this.btnget.Name = "btnget";
            this.btnget.Size = new System.Drawing.Size(75, 23);
            this.btnget.TabIndex = 0;
            this.btnget.Text = "get";
            this.btnget.UseVisualStyleBackColor = true;
            this.btnget.Click += new System.EventHandler(this.btnget_Click);
            // 
            // txtSelector
            // 
            this.txtSelector.Location = new System.Drawing.Point(16, 37);
            this.txtSelector.Name = "txtSelector";
            this.txtSelector.Size = new System.Drawing.Size(162, 21);
            this.txtSelector.TabIndex = 1;
            // 
            // txtText
            // 
            this.txtText.Location = new System.Drawing.Point(16, 176);
            this.txtText.Multiline = true;
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(162, 82);
            this.txtText.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 156);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "text";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 308);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "val";
            // 
            // txtVal
            // 
            this.txtVal.Location = new System.Drawing.Point(18, 328);
            this.txtVal.Multiline = true;
            this.txtVal.Name = "txtVal";
            this.txtVal.Size = new System.Drawing.Size(162, 82);
            this.txtVal.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 469);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "html";
            // 
            // txtHtml
            // 
            this.txtHtml.Location = new System.Drawing.Point(16, 489);
            this.txtHtml.Multiline = true;
            this.txtHtml.Name = "txtHtml";
            this.txtHtml.Size = new System.Drawing.Size(162, 82);
            this.txtHtml.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "selector";
            // 
            // webdoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 697);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "webdoc";
            this.Text = "webdoc";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txturl;
        private System.Windows.Forms.Button btngo;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtText;
        private System.Windows.Forms.TextBox txtSelector;
        private System.Windows.Forms.Button btnget;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtHtml;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtVal;
        private System.Windows.Forms.Label label4;

    }
}