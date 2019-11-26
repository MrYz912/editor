using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace editor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            richTextBox1.AllowDrop = true;
            richTextBox1.DragDrop += RichTextBox_DragDrop;
        }

        private void OpenFile(string path, RichTextBox richTextBox)
        {
            if (Path.GetExtension(path) == ".txt")  //判断文件类型，只接受txt文件
            {
                StreamReader sr = new StreamReader(path, System.Text.Encoding.UTF8);

                //使用StreamReader类来读取文件
                sr.BaseStream.Seek(0, SeekOrigin.Begin);
                // 从数据流中读取每一行，直到文件的最后一行，并在richTextBox1中显示出内容
                richTextBox.Text = "";
                string strLine = sr.ReadLine();
                while (strLine != null)
                {
                    richTextBox.Text += strLine + "\n";
                    strLine = sr.ReadLine();
                }
                //关闭此StreamReader对象
                sr.Close();
            }
            else if (Path.GetExtension(path) == ".rtf")
            {
                richTextBox.LoadFile(path);
            }
            //richTextBox.Name = path;
            TabPage tabPage = richTextBox.Parent as TabPage;
            tabPage.Text = path;
        }

        private void RichTextBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                Control.ControlCollection controls = tabControl1.TabPages[0].Controls;
                RichTextBox richTextBox = controls[0] as RichTextBox;
                if (tabControl1.TabPages.Count == 1 && richTextBox.Text.Trim() == "")
                {
                    OpenFile(file, richTextBox);
                }
                else
                {
                    // new tabPage
                    TabPage tabPageTemp = new TabPage();
                    RichTextBox rich = new RichTextBox();
                    rich.Dock = DockStyle.Fill;
                    rich.AllowDrop = true;
                    rich.DragDrop += RichTextBox_DragDrop;
                    tabPageTemp.Controls.Add(rich);
                    tabControl1.TabPages.Add(tabPageTemp);
                    OpenFile(file, rich);
                    RichTextBox richTextBox2 = tabControl1.SelectedTab.Controls[0] as RichTextBox;
                }
            }
            TabPage tabPage = _ = tabControl1.TabPages[0];
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void 查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            RichTextBox richTextBox = tabControl1.SelectedTab.Controls[0] as RichTextBox;

            DialogResult dr = fontDialog1.ShowDialog();
            if(dr == DialogResult.OK)
            {
                richTextBox.SelectionFont = fontDialog1.Font;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RichTextBox richTextBox = tabControl1.SelectedTab.Controls[0] as RichTextBox;

            DialogResult di = colorDialog1.ShowDialog();
            if(di == DialogResult.OK)
            {
                richTextBox.SelectionColor = colorDialog1.Color;
            }

        }
        
        private void findAndSearch()
        {
            RichTextBox richTextBox = tabControl1.SelectedTab.Controls[0] as RichTextBox;

            Form2 from2 = new Form2(richTextBox);
            from2.Show();
        }

        private void 查找ToolStripMenuItem1_Click(object sender, EventArgs e)//工具栏查找
        {
            findAndSearch();
        }

        private void 替换ToolStripMenuItem1_Click(object sender, EventArgs e)//工具栏替换
        {
            findAndSearch();
        }

        private void 全选ToolStripMenuItem1_Click(object sender, EventArgs e)//全选按钮
        {
            RichTextBox richTextBox = tabControl1.SelectedTab.Controls[0] as RichTextBox;
            richTextBox.SelectAll();
        }

        private void 替换ToolStripMenuItem_Click(object sender, EventArgs e)//粘贴按钮
        {
            RichTextBox richTextBox = tabControl1.SelectedTab.Controls[0] as RichTextBox;
            richTextBox.Paste();
        }

        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)//剪切按钮
        {
            RichTextBox richTextBox = tabControl1.SelectedTab.Controls[0] as RichTextBox;
            richTextBox.Cut();
        }

        private void 查找ToolStripMenuItem_Click(object sender, EventArgs e)//复制按钮
        {
            RichTextBox richTextBox = tabControl1.SelectedTab.Controls[0] as RichTextBox;
            richTextBox.Copy();
        }
    }
}
