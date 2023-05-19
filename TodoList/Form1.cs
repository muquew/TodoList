using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.Win32;

namespace TodoList
{
    public partial class TodoList : Form
    {


        //当前线程的上下文
        // static SynchronizationContext synt; //线程切换，异步执行要用到


        // 取消任务视图
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_TOOLWINDOW = 0x00000080;
        private const int WS_EX_APPWINDOW = 0x00040000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);


        public TodoList()
        {

            // synt = SynchronizationContext.Current; //不能在申明时初始化

            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None; // 去掉窗口边框
            this.TransparencyKey = Color.FromArgb(0, 0, 0); // 设置窗口透明色为黑色
            this.ShowInTaskbar = false; // 任务栏图标

            // 注册Windows关机事件
            SystemEvents.SessionEnding += new SessionEndingEventHandler(SystemEvents_SessionEnding);


            

        }
        // 取消任务视图
        // OnLoad 方法是窗体的重写方法，在窗体类中进行重写，并由框架自动调用
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // 在窗体加载时执行的逻辑


            // 获取当前窗口句柄
            IntPtr handle = this.Handle;

            // 获取当前窗口样式
            int style = GetWindowLong(handle, GWL_EXSTYLE);

            // 移除任务视图样式
            style |= WS_EX_TOOLWINDOW;
            style &= ~WS_EX_APPWINDOW;

            // 更新窗口样式
            SetWindowLong(handle, GWL_EXSTYLE, style);
        }


        private void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
        {
            // 在关机或注销事件发生时执行你想要的操作
            // ...

            save(); // 在关机或注销事件发生时调用save()函数保存数据

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

            // 最后两个参数一般为-1
            String s = Interaction.InputBox("添加事件", "标题", "", -1, -1);

            if (s.Length == 0)
            {
                return;
            }
            else
            {
                checkedListBox.Items.Add(s, false);
            }
        }


        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            while (checkedListBox.CheckedItems.Count > 0)
            {
                for (int i = 0; i < checkedListBox.Items.Count; i++)
                {
                    if (checkedListBox.GetItemChecked(i))
                    {
                        checkedListBox.Items.RemoveAt(i);
                    }
                }
            }
        }

        private void 颜色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog.ShowDialog();
            System.Drawing.Color a = colorDialog.Color;
            checkedListBox.ForeColor = a;
        }

        private void 显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;
        }

        private void 隐藏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
        }

        // Form1_Load 方法是一个事件处理程序，是通过窗体设计器生成的，并在窗体加载时由框架自动调用
        private void Form1_Load(object sender, EventArgs e)
        {

            // 读取属性值
            Properties.Settings.Default.Reload();
            checkedListBox.ForeColor = Properties.Settings.Default.forecolor;

            if (Properties.Settings.Default.formsize != null)
            {
                this.Size = Properties.Settings.Default.formsize;
            }
            
            this.Location = new Point(Properties.Settings.Default.FormLocationX, Properties.Settings.Default.FormLocationY);

            if (!string.IsNullOrEmpty(Properties.Settings.Default.todolist))
            {
                // 转换应用程序设置中的字符串值为 CheckedListBox 列表
                string[] items = Properties.Settings.Default.todolist.Split(',');
                foreach (string item in items)
                {
                    checkedListBox.Items.Add(item);
                }
            }

        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            save();
            // 关闭所有的线程
            // 删除ToolStripMenuItem_Click(sender, e);
            this.Dispose();
            this.Close();
        }

        private void save()
        {
            // 保存属性值
            Properties.Settings.Default.forecolor = checkedListBox.ForeColor;
            Properties.Settings.Default.formsize = this.Size;
            Properties.Settings.Default.FormLocationX = this.Location.X;
            Properties.Settings.Default.FormLocationY = this.Location.Y;
            // Create a string array to store the items
            string[] items = new string[checkedListBox.Items.Count];
            // Copy the items from the CheckedListBox to the array
            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                items[i] = checkedListBox.Items[i].ToString();
            }
            // Serialize the array to a string
            string itemsString = string.Join(",", items);
            // Save the string to the application settings
            Properties.Settings.Default.todolist = itemsString;

            Properties.Settings.Default.Save();
        }

        private void checkedListBox1_MouseDown(object sender, MouseEventArgs e)
        {
            // 取消所选项的蓝色背景色
            checkedListBox.ClearSelected();
            // 设置焦点，防止选中项变为未选中
            checkedListBox.Focus();
        }
        private void CheckedListBox1_MouseUp(object sender, MouseEventArgs e)
        {
            int index = checkedListBox.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                checkedListBox.SetItemChecked(index, !checkedListBox.GetItemChecked(index));
            }
        }

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            // 将窗口提于顶层
            this.BringToFront();
        }

    }
}
