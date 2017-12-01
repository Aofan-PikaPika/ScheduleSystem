using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CCWin;

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
            dl.statisticsFromTB(0);
            dl.getStaffList();
        }
    }
}
