
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;

namespace TodoList
{
    partial class TodoList
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TodoList));
            this.checkedListBox = new System.Windows.Forms.CheckedListBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.输入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除选中项ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.颜色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.隐藏ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkedListBox
            // 
            this.checkedListBox.BackColor = System.Drawing.Color.Black;
            this.checkedListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBox.CheckOnClick = true;
            this.checkedListBox.ContextMenuStrip = this.contextMenuStrip;
            this.checkedListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBox.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkedListBox.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.checkedListBox.FormattingEnabled = true;
            this.checkedListBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkedListBox.Location = new System.Drawing.Point(0, 0);
            this.checkedListBox.Margin = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.checkedListBox.Name = "checkedListBox";
            this.checkedListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.checkedListBox.Size = new System.Drawing.Size(408, 481);
            this.checkedListBox.TabIndex = 0;
            this.checkedListBox.TabStop = false;
            this.checkedListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.checkedListBox1_MouseDown);
            this.checkedListBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CheckedListBox1_MouseUp);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.输入ToolStripMenuItem,
            this.删除选中项ToolStripMenuItem,
            this.颜色ToolStripMenuItem,
            this.显示ToolStripMenuItem,
            this.隐藏ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(139, 148);
            // 
            // 输入ToolStripMenuItem
            // 
            this.输入ToolStripMenuItem.Name = "输入ToolStripMenuItem";
            this.输入ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.输入ToolStripMenuItem.Text = "插入待办";
            this.输入ToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // 删除选中项ToolStripMenuItem
            // 
            this.删除选中项ToolStripMenuItem.Name = "删除选中项ToolStripMenuItem";
            this.删除选中项ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.删除选中项ToolStripMenuItem.Text = "删除已选";
            this.删除选中项ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // 颜色ToolStripMenuItem
            // 
            this.颜色ToolStripMenuItem.Name = "颜色ToolStripMenuItem";
            this.颜色ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.颜色ToolStripMenuItem.Text = "字体颜色";
            this.颜色ToolStripMenuItem.Click += new System.EventHandler(this.颜色ToolStripMenuItem_Click);
            // 
            // 显示ToolStripMenuItem
            // 
            this.显示ToolStripMenuItem.Name = "显示ToolStripMenuItem";
            this.显示ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.显示ToolStripMenuItem.Text = "显示窗口";
            this.显示ToolStripMenuItem.Click += new System.EventHandler(this.显示ToolStripMenuItem_Click);
            // 
            // 隐藏ToolStripMenuItem
            // 
            this.隐藏ToolStripMenuItem.Name = "隐藏ToolStripMenuItem";
            this.隐藏ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.隐藏ToolStripMenuItem.Text = "隐藏窗口";
            this.隐藏ToolStripMenuItem.Click += new System.EventHandler(this.隐藏ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "TodoList";
            this.notifyIcon.Visible = true;
            this.notifyIcon.Click += new System.EventHandler(this.NotifyIcon_Click);
            // 
            // colorDialog
            // 
            this.colorDialog.AnyColor = true;
            this.colorDialog.Color = System.Drawing.Color.IndianRed;
            this.colorDialog.FullOpen = true;
            // 
            // TodoList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(408, 481);
            this.ControlBox = false;
            this.Controls.Add(this.checkedListBox);
            this.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TodoList";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "TodoList";
            this.TransparencyKey = System.Drawing.Color.Transparent;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem 输入ToolStripMenuItem;
        private ToolStripMenuItem 退出ToolStripMenuItem;
        private ToolStripMenuItem 删除选中项ToolStripMenuItem;
        private ColorDialog colorDialog;
        private ToolStripMenuItem 颜色ToolStripMenuItem;
        private ToolStripMenuItem 显示ToolStripMenuItem;
        private ToolStripMenuItem 隐藏ToolStripMenuItem;




    }
    
}

