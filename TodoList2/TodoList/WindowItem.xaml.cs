using DevExpress.Xpf.Editors;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using FontDialogSample;
using Dsafa.WpfColorPicker;

namespace TodoList
{
    /// <summary>
    /// WindowItem.xaml 的交互逻辑
    /// </summary>
    public partial class WindowItem : UserControl
    {

        public string TextContent { get; set; }

        private bool isFirstImageShown = true;

        public bool IsFirstImageShown
        {
            get { return isFirstImageShown; }
            set
            {
                isFirstImageShown = value;
                UpdateImage();
            }
        }


        public WindowItem()
        {

            InitializeComponent();

        }

        public WindowItem(string textContent)
        {
            if (textContent != null)
            {
                InitializeComponent();
                text.Text = textContent;
            }
        }

        private void Svg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Change();
        }
        private void Change()
        {
            // 根据 IsFirstImageShown 的值来显示或隐藏 dateEdit 控件
            if (IsFirstImageShown)
                // 显示 dateEdit 控件
                date.Visibility = Visibility.Visible;
            else
                // 隐藏 dateEdit 控件
                date.Visibility = Visibility.Collapsed;

            // 在点击事件处理程序中切换 IsFirstImageShown 属性的值
            IsFirstImageShown = !IsFirstImageShown;
        }

        private void UpdateImage()
        {
            if (IsFirstImageShown)
                svg.Source = (DrawingImage)FindResource("FirstImage");
            else
                svg.Source = (DrawingImage)FindResource("SecondImage");

        }




        private void SetFont(object sender, System.EventArgs e)
        {
            FontChooser fontChooser = new FontChooser();

            fontChooser.SetPropertiesFromObject(text);
            fontChooser.PreviewSampleText = text.Text;

            if (fontChooser.ShowDialog().Value)
            {
                fontChooser.ApplyPropertiesToObject(text);
            }
        }


        private void SetColor(object sender, System.EventArgs e)
        {
            ColorPickerDialog colorPickerDialog = new ColorPickerDialog();
            var result = colorPickerDialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                var newColor = colorPickerDialog.Color;
                text.Foreground = new SolidColorBrush(newColor);
            }
        }

        private void ChangeText(object sender, RoutedEventArgs e)
        {
            WindowText changeWindow = new WindowText();
            changeWindow.Title = "修改待办";
            changeWindow.TextBlock.Text = "修改待办";
            changeWindow.text.Text = text.Text;
            changeWindow.RemoveResetButton();

            if (changeWindow.ShowDialog() == true)
            {
                string textBoxContent = changeWindow.TextBoxContent;
                if (!string.IsNullOrWhiteSpace(textBoxContent))
                {
                    text.Text = textBoxContent;
                }
            }
        }

        public void RemoveSelf(object sender, RoutedEventArgs e)
        {
            if (Parent is Panel parentPanel)
            {
                parentPanel.Children.Remove(this);
                OnUserControlDeleted();
            }
        }

        // 自身删除告诉主窗口
        public event EventHandler UserControlDeleted;
        protected virtual void OnUserControlDeleted()
        {
            UserControlDeleted?.Invoke(this, EventArgs.Empty);
        }


        private void MoveFirst_Click(object sender, RoutedEventArgs e)
        {
            // 执行第一项操作
            OnUserControlMove(1);
        }

        private void MoveUp_Click(object sender, RoutedEventArgs e)
        {
            // 执行上移操作
            OnUserControlMove(2);
        }

        private void MoveDown_Click(object sender, RoutedEventArgs e)
        {
            // 执行下移操作
            OnUserControlMove(3);
        }

        private void MoveLast_Click(object sender, RoutedEventArgs e)
        {
            // 执行最后一项操作
            OnUserControlMove(4);
        }

        public event EventHandler<int> UserControlMove;
        protected virtual void OnUserControlMove(int value)
        {
            UserControlMove?.Invoke(this, value);
        }



        private void UpdateSelectedTime(object sender, EditValueChangedEventArgs e)
        {
            DateTime selectedTime = date.DateTime;
            DateTime dateTime = DateTime.Now;
            int comparisonResult = selectedTime.CompareTo(dateTime);

            if (comparisonResult <= 0)
            {
                Change();
            }

        }
    }
}
