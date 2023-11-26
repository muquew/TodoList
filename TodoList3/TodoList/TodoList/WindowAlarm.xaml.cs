using System.Windows;

namespace TodoList
{
    /// <summary>
    /// WindowAlarm.xaml 的交互逻辑
    /// </summary>
    public partial class WindowAlarm : Window
    {
        public WindowAlarm()
        {
            InitializeComponent();
        }
        public bool Extended { get; private set; }

        private void ExtendTimeButton_Click(object sender, RoutedEventArgs e)
        {
            // 将返回的值赋给属性 Extended
            Extended = true;

            // 关闭当前窗口并将延长的时间值传递给主窗口
            this.Close();  // 关闭当前窗口
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            // 将返回的值赋给属性 Extended
            Extended = false;
            // 关闭当前窗口
            this.Close();
        }
    }
}
