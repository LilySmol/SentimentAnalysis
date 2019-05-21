using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Forms;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace SentimentAnalysis
{
    public partial class Form1 : Form
    {
        private int dataGridViewGroupsSelectedRowIndex;
        private VK vk = new VK();
        private ulong countPosts = 10;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //авторизоваться и получить список грусс пользователя
        {            
            vk.authorization(textBox1.Text, textBox2.Text);
            dataGridViewGroups.DataSource = vk.getGroups();
        }

        private void dataGridViewGroups_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewGroups.SelectedCells.Count > 0)
            {
                dataGridViewGroupsSelectedRowIndex = dataGridViewGroups.SelectedCells[0].RowIndex;
            }
        }

        private void button2_Click(object sender, EventArgs e) //получить список комментариев выбранной группы
        {
            List<string> commentsList = new List<string>();           
            string s = dataGridViewGroups.Rows[dataGridViewGroupsSelectedRowIndex].Cells[0].Value.ToString();
            long groupId = Convert.ToInt64(dataGridViewGroups.Rows[dataGridViewGroupsSelectedRowIndex].Cells[0].Value);
            var posts = vk.getGroupPosts(groupId, countPosts);
            foreach (var post in posts)
            {
                DataTable comments = vk.getGroupComents(groupId, (long)post.Id);
                foreach (DataRow row in comments.Rows)
                {
                    commentsList.Add(row.ItemArray[0].ToString());
                    //dataGridViewComments.Rows.Add(row.ItemArray);
                }
            }
            dataGridViewComments.Rows.Clear();
            dataGridViewComments.Columns.Clear();
            dataGridViewComments.Columns.Add(new DataGridViewTextBoxColumn());
            dataGridViewComments.Columns.Add(new DataGridViewTextBoxColumn());
            foreach (string comment in commentsList)
            {
                SentimentAnalysis sentimentAnalysis = new SentimentAnalysis();   
                dataGridViewComments.Rows.Add(comment, sentimentAnalysis.getEmotion(comment));
            }
        }
    }
}
