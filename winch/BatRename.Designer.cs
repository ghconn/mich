namespace winch
{
    partial class BatRename
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
            this.txtpath = new System.Windows.Forms.TextBox();
            this.txtformat = new System.Windows.Forms.TextBox();
            this.txtstart = new System.Windows.Forms.TextBox();
            this.txtstep = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnsltfold = new System.Windows.Forms.Button();
            this.btngo = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.GroupBox();
            this.rdoback = new System.Windows.Forms.RadioButton();
            this.rdopre = new System.Windows.Forms.RadioButton();
            this.chb = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtpath
            // 
            this.txtpath.Location = new System.Drawing.Point(12, 18);
            this.txtpath.Name = "txtpath";
            this.txtpath.Size = new System.Drawing.Size(452, 21);
            this.txtpath.TabIndex = 0;
            // 
            // txtformat
            // 
            this.txtformat.Location = new System.Drawing.Point(98, 30);
            this.txtformat.Name = "txtformat";
            this.txtformat.Size = new System.Drawing.Size(147, 21);
            this.txtformat.TabIndex = 1;
            // 
            // txtstart
            // 
            this.txtstart.Location = new System.Drawing.Point(61, 101);
            this.txtstart.Name = "txtstart";
            this.txtstart.Size = new System.Drawing.Size(75, 21);
            this.txtstart.TabIndex = 3;
            this.txtstart.Text = "1";
            // 
            // txtstep
            // 
            this.txtstep.Location = new System.Drawing.Point(222, 101);
            this.txtstep.Name = "txtstep";
            this.txtstep.Size = new System.Drawing.Size(75, 21);
            this.txtstep.TabIndex = 4;
            this.txtstep.Text = "1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "格式";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "初";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(166, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "步";
            // 
            // btnsltfold
            // 
            this.btnsltfold.Location = new System.Drawing.Point(507, 15);
            this.btnsltfold.Name = "btnsltfold";
            this.btnsltfold.Size = new System.Drawing.Size(75, 23);
            this.btnsltfold.TabIndex = 11;
            this.btnsltfold.Text = "选择文件夹";
            this.btnsltfold.UseVisualStyleBackColor = true;
            this.btnsltfold.Click += new System.EventHandler(this.btnsltfold_Click);
            // 
            // btngo
            // 
            this.btngo.Location = new System.Drawing.Point(506, 99);
            this.btngo.Name = "btngo";
            this.btngo.Size = new System.Drawing.Size(75, 23);
            this.btngo.TabIndex = 12;
            this.btngo.Text = "执行";
            this.btngo.UseVisualStyleBackColor = true;
            this.btngo.Click += new System.EventHandler(this.btngo_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.txtpath);
            this.panel1.Controls.Add(this.btnsltfold);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(624, 412);
            this.panel1.TabIndex = 13;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 267);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "撤销";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rdoback);
            this.panel2.Controls.Add(this.rdopre);
            this.panel2.Controls.Add(this.chb);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtstep);
            this.panel2.Controls.Add(this.btngo);
            this.panel2.Controls.Add(this.txtstart);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txtformat);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(12, 60);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(600, 170);
            this.panel2.TabIndex = 13;
            // 
            // rdoback
            // 
            this.rdoback.AutoSize = true;
            this.rdoback.Checked = true;
            this.rdoback.Location = new System.Drawing.Point(383, 106);
            this.rdoback.Name = "rdoback";
            this.rdoback.Size = new System.Drawing.Size(35, 16);
            this.rdoback.TabIndex = 15;
            this.rdoback.TabStop = true;
            this.rdoback.Text = "后";
            this.rdoback.UseVisualStyleBackColor = true;
            // 
            // rdopre
            // 
            this.rdopre.AutoSize = true;
            this.rdopre.Location = new System.Drawing.Point(318, 105);
            this.rdopre.Name = "rdopre";
            this.rdopre.Size = new System.Drawing.Size(35, 16);
            this.rdopre.TabIndex = 14;
            this.rdopre.Text = "前";
            this.rdopre.UseVisualStyleBackColor = true;
            // 
            // chb
            // 
            this.chb.AutoSize = true;
            this.chb.Location = new System.Drawing.Point(297, 34);
            this.chb.Name = "chb";
            this.chb.Size = new System.Drawing.Size(72, 16);
            this.chb.TabIndex = 13;
            this.chb.Text = "原文件名";
            this.chb.UseVisualStyleBackColor = true;
            this.chb.CheckedChanged += new System.EventHandler(this.chb_CheckedChanged);
            // 
            // BatRename
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 412);
            this.Controls.Add(this.panel1);
            this.Name = "BatRename";
            this.Text = "BatRename";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtpath;
        private System.Windows.Forms.TextBox txtformat;
        private System.Windows.Forms.TextBox txtstart;
        private System.Windows.Forms.TextBox txtstep;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnsltfold;
        private System.Windows.Forms.Button btngo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox panel2;
        private System.Windows.Forms.CheckBox chb;
        private System.Windows.Forms.RadioButton rdoback;
        private System.Windows.Forms.RadioButton rdopre;
        private System.Windows.Forms.Button button1;
    }
}