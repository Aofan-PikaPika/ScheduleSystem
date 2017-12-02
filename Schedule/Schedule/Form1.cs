using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CCWin;
using System.IO;

namespace Schedule
{
    public partial class Form1 : Skin_DevExpress
    {
        public Form1()
        {
            InitializeComponent();
            

        }
        /// <summary>
        /// 定义excel文件路径
        /// </summary>
        string filePath = "";

        private void importButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "EXCEL文件(*.xls)|*.xls;*.xlsx";
            opf.RestoreDirectory = true;
            if (opf.ShowDialog()==DialogResult.OK)
            {
                filePath = opf.FileName;
                label1.Text = filePath;
              
                if (DataLayer.getDataFromFile(filePath)) 
                {
                    MessageBox.Show("导入成功！");
                }
                
            }

        }

        private void workButton_Click(object sender, EventArgs e)
        {
            DataLayer dl = new DataLayer();     
            AlgorithmLayer al = new AlgorithmLayer(dl.getDictFromStatistics(), dl.InsertDataIndicator(),dl.dataAreaBuffer(),dl.getStaffList());
            

        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            DataLayer dl = new DataLayer();
            DataTable dt = dl.dataAreaBuffer();
            dt.Rows[0][1] = "esdfsas";
            ExcelHelper sh = new ExcelHelper();
            if (sh.Open(filePath))
            {
                sh.InsertTable(dt,"整班",2,7);
            }
            string newName = Path.GetDirectoryName(filePath) + "\\测试结果.xlsx";
            sh.SaveAs(newName);
            sh.Close();
        }
    }
}
