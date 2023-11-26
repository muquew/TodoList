using System;
using System.Windows;
using System.Windows.Controls;

namespace TodoList
{
    /// <summary>
    /// WindowText.xaml 的交互逻辑
    /// </summary>
    public partial class WindowText : Window
    {

        public string? TextBoxContent { get; private set; }

        public WindowText()
        {
            InitializeComponent();
        }


        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(text.Text))
            {
                TextBoxContent = text.Text;
                DialogResult = true;
            }
            Close();
        }


        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            text.Clear();
        }

        public void RemoveResetButton()
        {
            if (Reset != null && Reset.Parent is Panel parentPanel)
            {
                parentPanel.Children.Remove(Reset);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }



        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // 在子窗口关闭时释放进程内存
            ReleaseProcessMemory();
        }

        private static void ReleaseProcessMemory()
        {
            // 强制进行垃圾回收，释放不再使用的内存
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void FocusTextBox(object sender, RoutedEventArgs e)
        {
            text.Focus();
        }
    }
}

