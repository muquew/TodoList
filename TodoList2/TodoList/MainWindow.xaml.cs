using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media;
using FontDialogSample;
using System.Linq;
using Dsafa.WpfColorPicker;
using System.Diagnostics;
using System.Windows.Threading;
using Microsoft.Win32;
using ItemObject;


namespace TodoList
{ 
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Windows.Forms.NotifyIcon? _notifyIcon;

        private System.Windows.Forms.ContextMenuStrip _TroyIconMenu = new();

        private List<WindowItem> ItemWindowList = new List<WindowItem>();

        private ObjectManager objectManager = new ObjectManager();

        private bool IsBorder
        {
            get
            {
                if (customBorder.Visibility == Visibility.Visible)
                    return true;
                else if (customBorder.Visibility == Visibility.Collapsed)
                    return false;
                else
                    return true;
            }
            set
            {
                if (value)
                {
                    customBorder.Visibility = Visibility.Visible;
                    moveThumb.Visibility = Visibility.Visible;
                    resizeThumb.Visibility = Visibility.Visible;
                }
                else
                {
                    customBorder.Visibility = Visibility.Collapsed;
                    moveThumb.Visibility = Visibility.Collapsed;
                    resizeThumb.Visibility = Visibility.Collapsed;
                }

            }

        }

        public MainWindow()
        {
            InitializeComponent();
            InitializeTrayIcon();
            Load_This();
            Timer();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // 隐藏任务视图预览
            new WindowInteropHelper(this).HideTaskViewPreview();

            // 注册全局异常处理事件
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            // 注册关机事件
            SystemEvents.SessionEnding += SystemEvents_SessionEnding;

            // 注册重启事件
            SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;

        }

        private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            Save();
        }

        private void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
        {
            Save();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Save();
        }

        private void InitializeTrayIcon()
        {
            _notifyIcon = new System.Windows.Forms.NotifyIcon
            {
                Icon = new System.Drawing.Icon("../../../Resources/Crocodile.ico"), // 设置托盘图标的图标文件
                Text = "TodoList" // 设置鼠标悬停在托盘图标上显示的文本
            };


            _notifyIcon.Click += (sender, e) =>
            {
                // 将窗口提至顶层
                Topmost = true;
                Topmost = false;
            };


            // 创建托盘菜单

            // 添加菜单项
            System.Windows.Forms.ToolStripMenuItem menuItemAdd = new();
            menuItemAdd.Text = "Add";
            menuItemAdd.Click += (s, e) =>
            {
                WindowText addWindow = new WindowText();
                if (addWindow.ShowDialog() == true)
                {
                    string textBoxContent = addWindow.TextBoxContent;
                    if (!string.IsNullOrWhiteSpace(textBoxContent))
                    {
                        // 在这里可以使用获取到的 textBoxContent，例如将其设置到主窗口的某个文本框中

                        // 创建实例
                        WindowItem itemWindow = new WindowItem(textBoxContent);

                        // 绑定删除事件
                        itemWindow.UserControlDeleted += UserControl_Deleted;
                        itemWindow.UserControlMove += UserControl_Move;
                        // userControl.SelectedTimeChanged += UserControl_SelectedTimeChanged;

                        int before = ItemWindowList.Count - 1;

                        if (before != -1)
                        {
                            itemWindow.text.FontFamily = ItemWindowList[before].text.FontFamily;
                            itemWindow.text.FontWeight = ItemWindowList[before].text.FontWeight;
                            itemWindow.text.FontStyle = ItemWindowList[before].text.FontStyle;
                            itemWindow.text.FontSize = ItemWindowList[before].text.FontSize;
                            itemWindow.text.Foreground = ItemWindowList[before].text.Foreground;
                        }

                        // 将UserControl1添加到Grid或StackPanel中
                        list.Children.Add(itemWindow);
                        // 将UserControl1实例添加到列表中
                        ItemWindowList.Add(itemWindow);
                    }
                }

                addWindow.Close();
                addWindow = null;
                GC.Collect();

            };
            _TroyIconMenu.Items.Add(menuItemAdd);

            System.Windows.Forms.ToolStripMenuItem menuItemListSelectAll = new();
            menuItemListSelectAll.Text = "选择全部";
            menuItemListSelectAll.Click += (s, e) =>
            {
                foreach (WindowItem itemWindow in ItemWindowList)
                {
                    itemWindow.check.IsChecked = true;
                }
            };
            _TroyIconMenu.Items.Add(menuItemListSelectAll);

            System.Windows.Forms.ToolStripMenuItem menuItemListDelete = new();
            menuItemListDelete.Text = "删除已选";
            menuItemListDelete.Click += (s, e) =>
            {
                ItemWindowList = ItemWindowList.Where(item => item.check.IsChecked == false).ToList();
                Update();
            };
            _TroyIconMenu.Items.Add(menuItemListDelete);

            System.Windows.Forms.ToolStripMenuItem menuItemListDeleteAll = new();
            menuItemListDeleteAll.Text = "删除全部";
            menuItemListDeleteAll.Click += (s, e) =>
            {
                ItemWindowList.Clear();
                Update();
            };
            _TroyIconMenu.Items.Add(menuItemListDeleteAll);

            _TroyIconMenu.Items.Add(new ToolStripSeparator()); // 添加分割线

            System.Windows.Forms.ToolStripMenuItem menuItemListColor = new();
            menuItemListColor.Text = "颜色";
            menuItemListColor.Click += (s, e) =>
            {

                ColorPickerDialog colorPickerDialog = new ColorPickerDialog();
                var result = colorPickerDialog.ShowDialog();
                if (result.HasValue && result.Value)
                {
                    var newColor = colorPickerDialog.Color;
                    foreach (WindowItem item in ItemWindowList)
                    {
                        item.text.Foreground = new SolidColorBrush(newColor);
                    }
                }

            };
            _TroyIconMenu.Items.Add(menuItemListColor);

            System.Windows.Forms.ToolStripMenuItem menuItemListFont = new();
            menuItemListFont.Text = "字体";
            menuItemListFont.Click += (s, e) =>
            {
                FontChooser fontChooser = new FontChooser();
                fontChooser.SetSize(new System.Windows.Size(1000, 750));
                fontChooser.SetPropertiesFromObject(ItemWindowList[0].text);
                fontChooser.PreviewSampleText = ItemWindowList[0].text.Text;

                if (fontChooser.ShowDialog().Value)
                {
                    foreach (WindowItem item in ItemWindowList)
                    {
                        fontChooser.ApplyPropertiesToObject(item.text);
                    }

                }
            };
            _TroyIconMenu.Items.Add(menuItemListFont);

            _TroyIconMenu.Items.Add(new ToolStripSeparator()); // 添加分割线

            System.Windows.Forms.ToolStripMenuItem menuItemOn = new();
            menuItemOn.Text = "show";
            menuItemOn.Click += (s, e) =>
            {
                IsBorder = true;
            };
            _TroyIconMenu.Items.Add(menuItemOn);

            System.Windows.Forms.ToolStripMenuItem menuItemOff = new();
            menuItemOff.Text = "hide";
            menuItemOff.Click += (s, e) =>
            {
                IsBorder = false;
            };
            _TroyIconMenu.Items.Add(menuItemOff);

            _TroyIconMenu.Items.Add(new ToolStripSeparator()); // 添加分割线

            System.Windows.Forms.ToolStripMenuItem menuItemThumb = new();
            menuItemThumb.Text = "修改边框";
            menuItemThumb.Click += (s, e) =>
            {
                Timer();
            };
            _TroyIconMenu.Items.Add(menuItemThumb);

            _TroyIconMenu.Items.Add(new ToolStripSeparator()); // 添加分割线

            System.Windows.Forms.ToolStripMenuItem menuItemExit = new();
            menuItemExit.Text = "Exit";
            menuItemExit.Click += (s, e) =>
            {
                Save();
                Close();
                GC.Collect();
            };
            _TroyIconMenu.Items.Add(menuItemExit);

            // 设置托盘菜单
            _notifyIcon.ContextMenuStrip = _TroyIconMenu;

            // 显示托盘图标
            _notifyIcon.Visible = true;
        }
        private void UserControl_Deleted(object sender, EventArgs e)
        {
            WindowItem deletedItem = sender as WindowItem;
            // 执行删除UserControl的操作

            // 更新列表或进行其他必要的操作
            ItemWindowList.Remove(deletedItem);
            list.Children.Remove(deletedItem);
        }

        private void UserControl_Move(object sender, int value)
        {
            // 假设userControlList是List<UserControl1>类型的列表
            int index = ItemWindowList.FindIndex(u => u == sender);

            WindowItem? temp = new WindowItem();

            switch (value)
            {
                // 第一
                case 1:
                    if (index != 0)
                    {
                        temp = ItemWindowList[index];
                        ItemWindowList.RemoveAt(index);
                        ItemWindowList.Insert(0, temp);
                    }
                    break;
                // 上
                case 2:
                    if (index != 0)
                        Move_Swap(temp, index, index - 1);
                    break;
                // 下
                case 3:
                    if (index != ItemWindowList.Count - 1)
                        Move_Swap(temp, index, index + 1);
                    break;
                // 最后
                case 4:
                    if (index != ItemWindowList.Count - 1)
                    {
                        Debug.WriteLine(66);
                        temp = ItemWindowList[index];
                        ItemWindowList.RemoveAt(index);
                        ItemWindowList.Insert(ItemWindowList.Count, temp);
                    }
                    break;
                default:
                    break;
            }
            temp = null;
            Update();

        }
        private void Move_Swap(WindowItem temp, int index1, int index2)
        {
            temp = ItemWindowList[index1];
            ItemWindowList[index1] = ItemWindowList[index2];
            ItemWindowList[index2] = temp;
        }

        private void ResizeThumb(object sender, DragDeltaEventArgs e)
        {
            double width = window.Width + e.HorizontalChange;
            double height = window.Height + e.VerticalChange;

            if (width > 0 && height > 0)
            {
                window.Width = width;
                window.Height = height;
            }
        }
        private void MoveThumb(object sender, DragDeltaEventArgs e)
        {
            double left = Canvas.GetLeft(window);
            double top = Canvas.GetTop(window);

            left += e.HorizontalChange;
            top += e.VerticalChange;

            Canvas.SetLeft(window, left);
            Canvas.SetTop(window, top);
        }

        private void Update()
        {
            list.Children.Clear();
            foreach (WindowItem item in ItemWindowList)
            {
                list.Children.Add(item);
            }
        }


        private void Load_This()
        {
            Properties.Settings.Default.Reload();

            this.Left = Properties.Settings.Default.XPosition;
            this.Top = Properties.Settings.Default.YPosition;
            this.Width = Properties.Settings.Default.Width;
            this.Height = Properties.Settings.Default.Height;
            IsBorder = Properties.Settings.Default.Border;

            List<Item> obj = objectManager.LoadObjects();
            if (obj != null)
            {
                foreach (Item item in obj)
                {
                    // 创建实例
                    WindowItem userControl = new WindowItem();
                    userControl.check.IsChecked = item.IsChecked;
                    userControl.IsFirstImageShown = item.IsFirstSvg;
                    userControl.text.Text = item.Text;
                    userControl.text.FontFamily = item.FontFamily;
                    userControl.text.FontWeight = item.FontWeight;
                    userControl.text.FontStyle = item.FontStyle;
                    userControl.text.FontSize = item.FontSize;
                    userControl.text.Foreground = item.FontBrush;
                    if (item.IsFirstSvg == false)
                    {
                        userControl.date.DateTime = (DateTime)item.DateTime;
                        userControl.date.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        userControl.date.Visibility = Visibility.Collapsed;
                    }

                    userControl.UserControlDeleted += UserControl_Deleted;
                    userControl.UserControlMove += UserControl_Move;
                    // 在创建UserControl实例并将其添加到列表中的代码处


                    list.Children.Add(userControl);
                    ItemWindowList.Add(userControl);
                }
                obj.Clear();
                obj = null;
            }

            GC.Collect();
        }


        private void Save()
        {

            Properties.Settings.Default.XPosition = this.Left;
            Properties.Settings.Default.YPosition = this.Top;
            Properties.Settings.Default.Width = this.Width;
            Properties.Settings.Default.Height = this.Height;
            Properties.Settings.Default.Border = IsBorder;

            Properties.Settings.Default.Save();

            foreach (WindowItem itemWindow in ItemWindowList)
            {
                Item item = new Item
                {
                    IsChecked = (bool)itemWindow.check.IsChecked,
                    IsFirstSvg = itemWindow.IsFirstImageShown,
                    Text = itemWindow.text.Text,
                    FontFamily = itemWindow.text.FontFamily,
                    FontWeight = itemWindow.text.FontWeight,
                    FontStyle = itemWindow.text.FontStyle,
                    FontSize = itemWindow.text.FontSize,
                    FontBrush = itemWindow.text.Foreground

                };
                if (item.IsFirstSvg == false)
                    item.DateTime = itemWindow.date.DateTime;

                objectManager.AddItem(item);
            }

            objectManager.SaveObjects();
        }

        private DispatcherTimer timer;
        private void Timer()
        {

            // 创建并配置 DispatcherTimer
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // 每隔一秒触发一次
            timer.Tick += Timer_Tick;

            // 启动定时器
            timer.Start();


        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // 获取当前时间
            DateTime currentTime = DateTime.Now;

            // foreach (UserControl1 userControl in userControlList.Where(userControl => userControl.date.DateTime.CompareTo(DateTime.Now) > 0))
            foreach (WindowItem userControl in ItemWindowList.
                Where(userControl =>
                    ((userControl.date.DateTime - currentTime).TotalMinutes < 1) &&
                    (userControl.IsFirstImageShown == false)))
            {
                // 获取用户控件的选定时间
                DateTime selectedDateTime = userControl.date.DateTime;

                // 检查当前时间是否与选定时间匹配（以分钟为精度）
                if (currentTime.Year == selectedDateTime.Year &&
                    currentTime.Month == selectedDateTime.Month &&
                    currentTime.Day == selectedDateTime.Day &&
                    currentTime.Hour == selectedDateTime.Hour &&
                    currentTime.Minute == selectedDateTime.Minute)
                {
                    userControl.IsFirstImageShown = true;
                    userControl.date.Visibility = Visibility.Collapsed;
                    // 触发闹钟操作
                    PlayAlarm(userControl);


                }
            }
        }
        private void PlayAlarm(WindowItem userControl)
        {

            // 执行闹钟操作，例如弹出消息框、播放声音等
            Debug.WriteLine("闹钟时间到了！");


            // 创建SoundPlayer对象并指定声音文件的路径
            // SoundPlayer player = new SoundPlayer("path/to/sound/file.wav");
            // 播放声音
            // player.Play();

            WindowAlarm alarmWindow = new WindowAlarm();
            alarmWindow.ShowDialog();  // 显示窗口，并等待窗口关闭

            // 在窗口关闭后，检查属性 Extended 的值
            if (alarmWindow.Extended)
            {
                // 如果 Extended 为 true，表示延长了闹钟时间
                // 在这里执行相应的逻辑
                Debug.WriteLine("闹钟时间已延长！");
                userControl.IsFirstImageShown = false;
                userControl.date.DateTime = userControl.date.DateTime.AddMinutes(5);
                userControl.date.Visibility = Visibility.Visible;
            }
            else
            {
                // 如果 Extended 为 false，表示未延长闹钟时间
                // 在这里执行相应的逻辑
                Debug.WriteLine("闹钟时间未延长！");
            }
            // player.Stop();
        }


    }
}

