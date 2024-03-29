﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace editor
{
    public partial class Form2 : Form
    {
        int start = 0, count = 0;
        RichTextBox richTextBox3;
        TabControl tabControl2;

        public Form2(TabControl tabControl)
        {
            InitializeComponent();
            tabControl2 = tabControl;
            richTextBox3 = tabControl2.SelectedTab.Controls[0] as RichTextBox;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string str1;
            str1 = text_find.Text;
            start = 0;
            count = 0;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)//查找按钮
        {
            string str = text_find.Text;
            richTextBox3 = tabControl2.SelectedTab.Controls[0] as RichTextBox;
            if (start >= richTextBox3.TextLength)
            {
                MessageBox.Show("已寻找到尾部");
                start = 0;
            }
            else
            {
                start = richTextBox3.Find(str, start, RichTextBoxFinds.MatchCase);
                if(start == -1)
                {
                    if(count == 0)
                        MessageBox.Show("没有该字");
                    else
                    {
                        MessageBox.Show("已寻找到尾部");
                        start = 0;
                    }
                }
                else//找到匹配的
                {
                    start += str.Length;
                    count += 1;
                    richTextBox3.Focus();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)//替换按钮
        {
            richTextBox3 = tabControl2.SelectedTab.Controls[0] as RichTextBox;
            richTextBox3.Text = richTextBox3.Text.Replace(text_find.Text, text_replace.Text);
        }

    }
}
