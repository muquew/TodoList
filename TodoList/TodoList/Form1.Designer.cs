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
            this.ItemsMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.测试ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.修改ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.输入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除选中项ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.字体样式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.自定义字体ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.字体颜色ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.隐藏ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.ItemsMenuStrip.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkedListBox
            // 
            this.checkedListBox.BackColor = System.Drawing.Color.Black;
            this.checkedListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBox.ContextMenuStrip = this.ItemsMenuStrip;
            resources.ApplyResources(this.checkedListBox, "checkedListBox");
            this.checkedListBox.ForeColor = System.Drawing.Color.Cyan;
            this.checkedListBox.FormattingEnabled = true;
            this.checkedListBox.Name = "checkedListBox";
            this.checkedListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.checkedListBox.TabStop = false;
            this.checkedListBox.UseCompatibleTextRendering = true;
            this.checkedListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.checkedListBoxItem_MouseDoubleClick);
            this.checkedListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.checkedListBoxItem_MouseDown);
            // 
            // ItemsMenuStrip
            // 
            this.ItemsMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ItemsMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.测试ToolStripMenuItem1,
            this.修改ToolStripMenuItem,
            this.删除ToolStripMenuItem});
            this.ItemsMenuStrip.Name = "ItemsMenuStrip";
            resources.ApplyResources(this.ItemsMenuStrip, "ItemsMenuStrip");
            // 
            // 测试ToolStripMenuItem1
            // 
            this.测试ToolStripMenuItem1.Name = "测试ToolStripMenuItem1";
            resources.ApplyResources(this.测试ToolStripMenuItem1, "测试ToolStripMenuItem1");
            this.测试ToolStripMenuItem1.Click += new System.EventHandler(this.测试ToolStripMenuItem1_Click);
            // 
            // 修改ToolStripMenuItem
            // 
            this.修改ToolStripMenuItem.Name = "修改ToolStripMenuItem";
            resources.ApplyResources(this.修改ToolStripMenuItem, "修改ToolStripMenuItem");
            this.修改ToolStripMenuItem.Click += new System.EventHandler(this.修改ToolStripMenuItem_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            resources.ApplyResources(this.删除ToolStripMenuItem, "删除ToolStripMenuItem");
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click_1);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.输入ToolStripMenuItem,
            this.删除选中项ToolStripMenuItem,
            this.字体样式ToolStripMenuItem,
            this.自定义字体ToolStripMenuItem,
            this.字体颜色ToolStripMenuItem,
            this.显示ToolStripMenuItem,
            this.隐藏ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            // 
            // 输入ToolStripMenuItem
            // 
            this.输入ToolStripMenuItem.Name = "输入ToolStripMenuItem";
            resources.ApplyResources(this.输入ToolStripMenuItem, "输入ToolStripMenuItem");
            this.输入ToolStripMenuItem.Click += new System.EventHandler(this.插入toolStripMenuItem1_Click);
            // 
            // 删除选中项ToolStripMenuItem
            // 
            this.删除选中项ToolStripMenuItem.Name = "删除选中项ToolStripMenuItem";
            resources.ApplyResources(this.删除选中项ToolStripMenuItem, "删除选中项ToolStripMenuItem");
            this.删除选中项ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // 字体样式ToolStripMenuItem
            // 
            this.字体样式ToolStripMenuItem.Name = "字体样式ToolStripMenuItem";
            resources.ApplyResources(this.字体样式ToolStripMenuItem, "字体样式ToolStripMenuItem");
            this.字体样式ToolStripMenuItem.Click += new System.EventHandler(this.字体样式ToolStripMenuItem_Click);
            // 
            // 自定义字体ToolStripMenuItem
            // 
            this.自定义字体ToolStripMenuItem.Name = "自定义字体ToolStripMenuItem";
            resources.ApplyResources(this.自定义字体ToolStripMenuItem, "自定义字体ToolStripMenuItem");
            this.自定义字体ToolStripMenuItem.Click += new System.EventHandler(this.自定义字体ToolStripMenuItem_Click);
            // 
            // 字体颜色ToolStripMenuItem
            // 
            this.字体颜色ToolStripMenuItem.Name = "字体颜色ToolStripMenuItem";
            resources.ApplyResources(this.字体颜色ToolStripMenuItem, "字体颜色ToolStripMenuItem");
            this.字体颜色ToolStripMenuItem.Click += new System.EventHandler(this.字体颜色ToolStripMenuItem_Click);
            // 
            // 显示ToolStripMenuItem
            // 
            this.显示ToolStripMenuItem.Name = "显示ToolStripMenuItem";
            resources.ApplyResources(this.显示ToolStripMenuItem, "显示ToolStripMenuItem");
            this.显示ToolStripMenuItem.Click += new System.EventHandler(this.显示ToolStripMenuItem_Click);
            // 
            // 隐藏ToolStripMenuItem
            // 
            this.隐藏ToolStripMenuItem.Name = "隐藏ToolStripMenuItem";
            resources.ApplyResources(this.隐藏ToolStripMenuItem, "隐藏ToolStripMenuItem");
            this.隐藏ToolStripMenuItem.Click += new System.EventHandler(this.隐藏ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            resources.ApplyResources(this.退出ToolStripMenuItem, "退出ToolStripMenuItem");
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            resources.ApplyResources(this.notifyIcon, "notifyIcon");
            this.notifyIcon.Click += new System.EventHandler(this.NotifyIcon_Click);
            // 
            // colorDialog
            // 
            this.colorDialog.AnyColor = true;
            this.colorDialog.Color = System.Drawing.Color.IndianRed;
            this.colorDialog.FullOpen = true;
            // 
            // fontDialog
            // 
            this.fontDialog.ShowApply = true;
            // 
            // TodoList
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ControlBox = false;
            this.Controls.Add(this.checkedListBox);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.MediumAquamarine;
            this.Name = "TodoList";
            this.ShowInTaskbar = false;
            this.TransparencyKey = System.Drawing.Color.Transparent;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ItemsMenuStrip.ResumeLayout(false);
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
        private ToolStripMenuItem 字体颜色ToolStripMenuItem;
        private ToolStripMenuItem 显示ToolStripMenuItem;
        private ToolStripMenuItem 隐藏ToolStripMenuItem;
        private ContextMenuStrip ItemsMenuStrip;
        private ToolStripMenuItem 测试ToolStripMenuItem1;
        private ToolStripMenuItem 修改ToolStripMenuItem;
        private ToolStripMenuItem 字体样式ToolStripMenuItem;
        private FontDialog fontDialog;
        private ToolStripMenuItem 自定义字体ToolStripMenuItem;
        private ToolStripMenuItem 删除ToolStripMenuItem;
    }
    
}

