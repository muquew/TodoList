using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using System.Collections.Specialized;
using System.Reflection.Emit;

namespace TodoList
{
    public partial class TodoList : Form
    {

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
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None; // 去掉窗口边框
            this.TransparencyKey = Color.FromArgb(0, 0, 0); // 设置窗口透明色为黑色
            this.ShowInTaskbar = false; // 任务栏图标


            this.fontDialog.Apply += fontDialog_Apply;  // apply按钮


            // 注册Windows关机事件
            SystemEvents.SessionEnding += new SessionEndingEventHandler(SystemEvents_SessionEnding);
            // 注册异常事件
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

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
        // Form1_Load 方法是一个事件处理程序，是通过窗体设计器生成的，并在窗体加载时由框架自动调用
        private void Form1_Load(object sender, EventArgs e)
        {

            // 读取属性值
            Properties.Settings.Default.Reload();

            checkedListBox.Font = Properties.Settings.Default.Font;
            checkedListBox.ForeColor = Properties.Settings.Default.Forecolor;

            if (Properties.Settings.Default.Formsize != null)
            {
                this.Size = Properties.Settings.Default.Formsize;
                checkedListBox.Size = Properties.Settings.Default.Formsize;
            }

            this.Location = new Point(Properties.Settings.Default.FormLocationX, Properties.Settings.Default.FormLocationY);


            // 检索字符串数组
            if (Properties.Settings.Default.ItemsTostring != null)
            {
                string[] items1 = new string[Properties.Settings.Default.ItemsTostring.Count];
                Properties.Settings.Default.ItemsTostring.CopyTo(items1, 0);
                if (items1 != null)
                {
                    foreach (string item in items1)
                    {
                        checkedListBox.Items.Add(item);
                    }
                }
            }
            if (Properties.Settings.Default.ItemsChecked != null)
            {
                string[] items2 = new string[Properties.Settings.Default.ItemsChecked.Count];
                Properties.Settings.Default.ItemsChecked.CopyTo(items2, 0);
                if (items2 != null)
                {
                    int index = 0;
                    foreach (string item in items2)
                    {
                        if (items2[index] == "True")
                            checkedListBox.SetItemChecked(index, true);
                        else
                            checkedListBox.SetItemChecked(index, false);
                        index++;
                    }
                }
            }
        }


        // 在关机或注销事件发生时执行你想要的操作
        private void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
        {
            save();
        }
        // 处理UI线程异常
        private void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            save();
        }
        // 处理非UI线程异常
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            if (ex != null)
            {
                save();
                // 显示错误信息
                // MessageBox.Show("发生未处理的异常：" + ex.Message);
            }
        }


        private void 插入toolStripMenuItem1_Click(object sender, EventArgs e)
        {

            Text textForm = new Text();
            textForm.ValueSelected += TextForm_ValueSelected; // 订阅事件
            textForm.ShowDialog();

        }
        private void TextForm_ValueSelected(object sender, ValueSelectedEventArgs e)
        {
            // linesFromSecondForm 变量将包含传回的值
            string[] linesFromSecondForm = e.SelectedLines; // 获取从第二个窗体传回的值

            foreach (string line in linesFromSecondForm)
            {
                checkedListBox.Items.Add(line, false);
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
        private void 字体颜色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog.Color = checkedListBox.ForeColor;
            if (Properties.Settings.Default.CustomColors != null)
            {
                string customColorsString = Properties.Settings.Default.CustomColors;
                string[] customColorsStrings = customColorsString.Split(',');
                int[] customColors = Array.ConvertAll(customColorsStrings, int.Parse);
                colorDialog.CustomColors = customColors;
            }

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                System.Drawing.Color a = colorDialog.Color;
                checkedListBox.ForeColor = a;
            }
        }

        private void fontDialog_Apply(object sender, EventArgs e)
        {
            System.Drawing.Font a = fontDialog.Font;
            checkedListBox.Font = a;
        }
        private void 字体样式ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            fontDialog.Font = checkedListBox.Font;
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                fontDialog_Apply(sender, e);
            }

        }
        private void 自定义字体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // ProcessColor(sender, e);
            
/*            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "TrueType Fonts (*.ttf)|*.ttf";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fontFile = openFileDialog.FileName;

                    string targetDirectory = Path.GetDirectoryName(Application.ExecutablePath); // 获取应用程序的同级目录
                    string targetFileName = Path.GetFileName(fontFile); // 获取文件名
                    Console.WriteLine(targetDirectory);
                    string targetFilePath = Path.Combine(targetDirectory, targetFileName); // 合并目标文件路径

                    // 将选择的文件复制到同级目录
                    File.Copy(fontFile, targetFilePath, true);

                    // 在代码中引用复制的.ttf文件
                    PrivateFontCollection privateFontCollection = new PrivateFontCollection();
                    privateFontCollection.AddFontFile(targetFilePath);

                    using (FontDialog fontDialog = new FontDialog())
                    {
                        fontDialog.Font = new Font(privateFontCollection.Families[0], 12f);

                        if (fontDialog.ShowDialog() == DialogResult.OK)
                        {
                            Font selectedFont = fontDialog.Font;
                            // 处理选中的字体
                            // ...
                        }
                        else
                        {
                            // 处理取消操作
                            // ...
                        }
                    }
                }
            }*/
        }

        private void 显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;
        }
        private void 隐藏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
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
            Properties.Settings.Default.Font = checkedListBox.Font;
            Properties.Settings.Default.Forecolor = checkedListBox.ForeColor;
            Properties.Settings.Default.Formsize = this.Size;
            Properties.Settings.Default.FormLocationX = this.Location.X;
            Properties.Settings.Default.FormLocationY = this.Location.Y;
            // Create a string array to store the items
            string[] itemsTostring = new string[checkedListBox.Items.Count];
            string[] itemsChecked = new string[checkedListBox.Items.Count];
            // Copy the items from the CheckedListBox to the array
            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                itemsTostring[i] = checkedListBox.Items[i].ToString();
                itemsChecked[i] = checkedListBox.GetItemChecked(i).ToString();
            }

            // Save the string to the application settings
            if (Properties.Settings.Default.ItemsTostring == null)
            {
                Properties.Settings.Default.ItemsTostring = new StringCollection();
            }
            Properties.Settings.Default.ItemsTostring.Clear();
            Properties.Settings.Default.ItemsTostring.AddRange(itemsTostring);
            if (Properties.Settings.Default.ItemsChecked == null)
            {
                Properties.Settings.Default.ItemsChecked = new StringCollection();
            }
            Properties.Settings.Default.ItemsChecked.Clear();
            Properties.Settings.Default.ItemsChecked.AddRange(itemsChecked);

            string customColors = string.Join(", ", colorDialog.CustomColors);
            Properties.Settings.Default.CustomColors = customColors;

            Properties.Settings.Default.Save();
        }

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            // 将窗口提于顶层
            this.BringToFront();
        }

        private void checkedListBoxItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int indexDoubleClick = checkedListBox.IndexFromPoint(e.Location);
            if (indexDoubleClick != ListBox.NoMatches)
            {
                checkedListBox.SetItemChecked(indexDoubleClick, !checkedListBox.GetItemChecked(indexDoubleClick));
            }
        }
        private int itemIndex;
        private void checkedListBoxItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // 获取鼠标指针处的项的索引
                itemIndex = checkedListBox.IndexFromPoint(checkedListBox.PointToClient(Cursor.Position));
            }

        }

        private void processColor(object sender, EventArgs e)
        {

            string 字体颜色16进制 = ColorTranslator.ToHtml(Color.FromArgb(checkedListBox.ForeColor.ToArgb())).ToString().Substring(1);
            Console.WriteLine(字体颜色16进制);

            Color 透明色 = HexToColor相近(字体颜色16进制);
            Console.WriteLine(透明色);
            Console.WriteLine(ColorTranslator.ToHtml(Color.FromArgb(透明色.ToArgb())).ToString().Substring(1));
            this.BackColor = 透明色;
            this.TransparencyKey = 透明色;

        }


        public Color HexToColor相近(string hex)
        {
            int br = int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            int bg = int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            int bb = int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);


            if (br > 20)
            {
                br -= 5;
            }

            if (bg > 20)
            {
                bg -= 5;
            }
            if (bb > 20)
            {
                bb -= 5;
            }

            // MessageBox.Show(br.ToString());
            Color c = Color.FromArgb(br, bg, bb);
            return c;
        }

        private void 测试ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // 如果存在项，则执行相应的操作
            if (itemIndex >= 0)
            {
                string selectedItem = checkedListBox.Items[itemIndex].ToString();

                // 在这里可以执行关于所选项的操作
                // MessageBox.Show("您点击了 " + selectedItem);

                Console.WriteLine(selectedItem);
                Console.WriteLine(itemIndex);
                Console.WriteLine(checkedListBox.Items.Count);
            }
        }
        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 如果存在项，则执行相应的操作
            if (itemIndex >= 0)
            {
                string selectedItem = checkedListBox.Items[itemIndex].ToString();
                String s = Interaction.InputBox("修改事件", "标题", selectedItem, - 1, -1);

                if (s.Length == 0)
                {
                    return;
                }
                else
                {
                    checkedListBox.Items[itemIndex] = s;
                }
            }
        }

        private void 删除ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if(itemIndex >= 0)
            {
                checkedListBox.Items.RemoveAt(itemIndex);
            }
        }

    }
}
