using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Forms;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;
using Excel = Microsoft.Office.Interop.Excel; //Библиотека для выгрузки в excel в visual studio 2010

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

        private void button1_Click(object sender, EventArgs e) //авторизоваться и получить список групп пользователя
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
                System.Data.DataTable comments = vk.getGroupComents(groupId, (long)post.Id);
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
        //До  RUN app добавить ссылку в Project - MS Office Object.. Подробнее тут в коментах http://www.cyberforum.ru/windows-forms/thread1026783.html
        // или тут перед "simple code" https://www.c-sharpcorner.com/UploadFile/hrojasara/export-datagridview-to-excel-in-C-Sharp/
        private void ButtonExport_Click(object sender, EventArgs e)
		{
            // creating Excel Application  
            Excel.Application app = new Excel.Application();
            // see the excel sheet behind the program  
            app.Visible = false;
            // prompt the user to save changes didn't override
            app.DisplayAlerts = true;
            //app.SheetsInNewWorkbook = 1;
            // creating new WorkBook within Excel application  
            app.Workbooks.Add(Type.Missing);
            Excel.Workbook workbook = app.Workbooks[1];
            workbook.Saved = true;
            app.DefaultSaveFormat = Excel.XlFileFormat.xlWorkbookNormal;
            // save excel doc with file dialog for user to touch path
            string path = string.Empty;
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    path = saveDialog.FileName;
                    workbook.SaveAs(path);
                    saveToFile(path, dataGridViewComments, app, workbook, app.Workbooks);
                }
            }
        }

        private void saveToFile(string pathToFile, DataGridView dbg,
     Excel.Application excelApp, Excel.Workbook excelWorkbook, Excel.Workbooks excelWorkbooks)
        {
            try
            {
                Excel.Sheets excelSheets = excelWorkbook.Worksheets;
                Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelSheets.get_Item(1);
                /*// creating new Excelsheet in workbook  
                Excel.Worksheet worksheet = null;
                // get the reference of first sheet. By default its name is Sheet1.  
                // store its reference to worksheet  
                worksheet = workbook.Sheets["Лист1"];
                worksheet = workbook.ActiveSheet;*/
                // changing the name of active sheet  
                excelWorksheet.Name = "Exported from gridview";
                int i, j;

                // storing header part in Excel  
                for (i = 1; i < dbg.Columns.Count + 1; i++)
                {
                    excelWorksheet.Cells[1, i] = dataGridViewComments.Columns[i - 1].HeaderText;
                }
                // storing Each row and column value to excel sheet
                for (i = 0; i < dbg.Rows.Count; i++)
                    for (j = 0; j < dbg.Columns.Count; j++)
                    {
                        excelWorksheet.Cells[i + 1, j + 1] = dataGridViewComments.Rows[i].Cells[j].Value.ToString();
                        //Excel.Range excelRange = (Excel.Range)excelWorksheet.Cells[i, j];
                        //excelRange.Value2 = dbg.Rows[i].Cells[j].Value.ToString();
                    }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Ошибка записи",
                      MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                excelApp.Quit();
            }
        }
	}
}
