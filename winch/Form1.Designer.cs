﻿namespace winch
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.设置连接SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.生成语句BToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.直接生成ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选项OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.生成到文本文件TToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.生成到XMLXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.批量更改文件名MToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.表名和列描述DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.网页WToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txt1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置连接SToolStripMenuItem,
            this.生成语句BToolStripMenuItem,
            this.工具ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(789, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 设置连接SToolStripMenuItem
            // 
            this.设置连接SToolStripMenuItem.Name = "设置连接SToolStripMenuItem";
            this.设置连接SToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.设置连接SToolStripMenuItem.Text = "设置连接(&S)";
            this.设置连接SToolStripMenuItem.Click += new System.EventHandler(this.设置连接SToolStripMenuItem_Click);
            // 
            // 生成语句BToolStripMenuItem
            // 
            this.生成语句BToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.直接生成ToolStripMenuItem,
            this.选项OToolStripMenuItem,
            this.生成到文本文件TToolStripMenuItem,
            this.生成到XMLXToolStripMenuItem});
            this.生成语句BToolStripMenuItem.Name = "生成语句BToolStripMenuItem";
            this.生成语句BToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.生成语句BToolStripMenuItem.Text = "构筑(&B)";
            // 
            // 直接生成ToolStripMenuItem
            // 
            this.直接生成ToolStripMenuItem.Name = "直接生成ToolStripMenuItem";
            this.直接生成ToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.直接生成ToolStripMenuItem.Text = "直接生成(&P)";
            this.直接生成ToolStripMenuItem.Click += new System.EventHandler(this.直接生成ToolStripMenuItem_Click);
            // 
            // 选项OToolStripMenuItem
            // 
            this.选项OToolStripMenuItem.Name = "选项OToolStripMenuItem";
            this.选项OToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.选项OToolStripMenuItem.Text = "选项(&O)";
            // 
            // 生成到文本文件TToolStripMenuItem
            // 
            this.生成到文本文件TToolStripMenuItem.Name = "生成到文本文件TToolStripMenuItem";
            this.生成到文本文件TToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.生成到文本文件TToolStripMenuItem.Text = "生成到文本文件(&T)";
            this.生成到文本文件TToolStripMenuItem.Click += new System.EventHandler(this.生成到文本文件TToolStripMenuItem_Click);
            // 
            // 生成到XMLXToolStripMenuItem
            // 
            this.生成到XMLXToolStripMenuItem.Name = "生成到XMLXToolStripMenuItem";
            this.生成到XMLXToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.生成到XMLXToolStripMenuItem.Text = "生成到XML(&X)";
            // 
            // 工具ToolStripMenuItem
            // 
            this.工具ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.批量更改文件名MToolStripMenuItem,
            this.表名和列描述DToolStripMenuItem,
            this.网页WToolStripMenuItem});
            this.工具ToolStripMenuItem.Name = "工具ToolStripMenuItem";
            this.工具ToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.工具ToolStripMenuItem.Text = "工具(&T)";
            // 
            // 批量更改文件名MToolStripMenuItem
            // 
            this.批量更改文件名MToolStripMenuItem.Name = "批量更改文件名MToolStripMenuItem";
            this.批量更改文件名MToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.批量更改文件名MToolStripMenuItem.Text = "批量更改文件名(&M)";
            this.批量更改文件名MToolStripMenuItem.Click += new System.EventHandler(this.批量更改文件名MToolStripMenuItem_Click);
            // 
            // 表名和列描述DToolStripMenuItem
            // 
            this.表名和列描述DToolStripMenuItem.Name = "表名和列描述DToolStripMenuItem";
            this.表名和列描述DToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.表名和列描述DToolStripMenuItem.Text = "表名和列描述(&D)";
            this.表名和列描述DToolStripMenuItem.Click += new System.EventHandler(this.表名和列描述DToolStripMenuItem_Click);
            // 
            // 网页WToolStripMenuItem
            // 
            this.网页WToolStripMenuItem.Name = "网页WToolStripMenuItem";
            this.网页WToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.网页WToolStripMenuItem.Text = "网页(&W)";
            this.网页WToolStripMenuItem.Click += new System.EventHandler(this.网页WToolStripMenuItem_Click);
            // 
            // txt1
            // 
            this.txt1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt1.Location = new System.Drawing.Point(0, 0);
            this.txt1.Multiline = true;
            this.txt1.Name = "txt1";
            this.txt1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt1.Size = new System.Drawing.Size(789, 527);
            this.txt1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(789, 36);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txt1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 60);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(789, 527);
            this.panel2.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 587);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "CH";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设置连接SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 生成语句BToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 选项OToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 直接生成ToolStripMenuItem;
        private System.Windows.Forms.TextBox txt1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem 生成到文本文件TToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 生成到XMLXToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripMenuItem 工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 批量更改文件名MToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 表名和列描述DToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 网页WToolStripMenuItem;
    }
}

