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

            tabControl1.TabPages[0].Text = "new file";
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
                FileToRich(file);
            }
        }

        private void FileToRich(string file)
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
            }
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

        private void button1_Click(object sender, EventArgs e)//改变字体
        {
            RichTextBox richTextBox = tabControl1.SelectedTab.Controls[0] as RichTextBox;
            DialogResult dr = fontDialog1.ShowDialog();
            if(dr == DialogResult.OK)
            {
                richTextBox.SelectionFont = fontDialog1.Font;
            }
        }

        private void button2_Click(object sender, EventArgs e)//改变颜色
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
            Form2 from2 = new Form2(tabControl1);
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

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox richTextBox = tabControl1.SelectedTab.Controls[0] as RichTextBox;
            richTextBox.Copy();
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox richTextBox = tabControl1.SelectedTab.Controls[0] as RichTextBox;
            richTextBox.Paste();
        }

        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox richTextBox = tabControl1.SelectedTab.Controls[0] as RichTextBox;
            richTextBox.Cut();
        }

        private void 查找和替换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            findAndSearch();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage tabPageTemp = new TabPage("new file");
            RichTextBox rich = new RichTextBox();
            rich.Dock = DockStyle.Fill;
            rich.AllowDrop = true;
            rich.DragDrop += RichTextBox_DragDrop;
            tabPageTemp.Controls.Add(rich);
            tabControl1.TabPages.Add(tabPageTemp);
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = GetOpenFilePath();
            if (path == "") return;
            FileToRich(path);
        }

        private string GetOpenFilePath()
        {
            string strFileName = "";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "文本文件(*.txt)|*.txt|rtf文件(*.rtf)|*.rtf|所有文件|*.*";
            ofd.ValidateNames = true; // 验证用户输入是否是一个有效的Windows文件名
            ofd.CheckFileExists = true; //验证路径的有效性
            ofd.CheckPathExists = true;//验证路径的有效性
            if (ofd.ShowDialog() == DialogResult.OK) //用户点击确认按钮，发送确认消息
            {
                strFileName = ofd.FileName;//获取在文件对话框中选定的路径或者字符串

            }
            return strFileName;
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = "";
            if (tabControl1.SelectedTab.Text == "new file")
            {
                path = GetSaveFilePath();
            }
            else
            {
                path = tabControl1.SelectedTab.Text;
            }
            if (path == "") return;
            RichTextBox richTextBox = tabControl1.SelectedTab.Controls[0] as RichTextBox;
            SaveFile(path, richTextBox);
        }

        private void SaveFile(string path, RichTextBox richTextBox)
        {

            if (Path.GetExtension(path) == ".txt")  //判断文件类型，只接受txt文件
            {
                StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8);

                sw.Write(richTextBox.Text);

                sw.Flush();
                sw.Close();

            }
            else if (Path.GetExtension(path) == ".rtf")
            {
                richTextBox.SaveFile(path);
            }
            TabPage tabPage = richTextBox.Parent as TabPage;
            tabPage.Text = path;
        }

        private string GetSaveFilePath()
        {
            string strFileName = "";
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "文本文件(*.txt)|*.txt|rtf文件(*.rtf)|*.rtf";
            sfd.ValidateNames = true;
            //sfd.CheckFileExists = true;
            sfd.CheckPathExists = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                strFileName = sfd.FileName;
            }
            return strFileName;
        }

        private void tabControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TabPage selectedTab = tabControl1.SelectedTab;
            tabControl1.TabPages.Remove(selectedTab);
        }
    }
}
