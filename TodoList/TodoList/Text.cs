using System;
using System.IO;
using System.Windows.Forms;

namespace TodoList
{
    public partial class Text : Form
    {

        public Text()
        {
            InitializeComponent();
        }


        public event EventHandler<ValueSelectedEventArgs> ValueSelected;
        private void button1_Click(object sender, EventArgs e)
        {
            string x = textBox1.Text;

            string[] lines = x.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            if (lines.Length > 0)
            {
                OnValueSelected(lines);
            }

            this.Close();
        }



        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }


        private string draggedFilepath;
        private void TextBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                draggedFilepath = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];

                // 检查文件是否是 TXT 文件
                if (Path.GetExtension(draggedFilepath).ToLower() == ".txt")
                {
                    // 检查文件大小，限制为 3000 字节（3 KB）
                    if (new FileInfo(draggedFilepath).Length <= 3000)
                    {
                        e.Effect = DragDropEffects.Copy;
                    }
                    else
                    {
                        label2.Text = "文件大小超过限制！";
                    }
                }
                else
                {
                    label2.Text = "文件类型不为txt！";
                }
            }
        }
        private void TextBox1_DragDrop(object sender, DragEventArgs e)
        {
            string fileContent = File.ReadAllText(draggedFilepath);
            textBox1.AppendText(fileContent);
            textBox1.AppendText(Environment.NewLine);
        }


        protected virtual void OnValueSelected(string[] selectedLines)
        {
            ValueSelected?.Invoke(this, new ValueSelectedEventArgs(selectedLines));
        }
    }


    public class ValueSelectedEventArgs : EventArgs
    {
        public string[] SelectedLines { get; }

        public ValueSelectedEventArgs(string[] selectedLines)
        {
            SelectedLines = selectedLines;
        }
    }
}
