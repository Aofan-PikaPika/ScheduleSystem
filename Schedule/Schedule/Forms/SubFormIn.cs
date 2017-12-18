using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CCWin;

namespace Schedule.Forms
{
    //导入窗体
    public partial class SubFormIn : Skin_Metro
    {
        public SubFormIn()
        {
            InitializeComponent();
        }

        private void SubFormIn_Load(object sender, EventArgs e)
        {
            this.dtpSchYear.CustomFormat = "yyyy年";
            this.dtpSchYear.Value = DateTime.Today;
            this.cboSemester.SelectedIndex = 0;
            this.rawInfo = null;
            this.ofdExcelPath.FileName = null;
            GC.Collect();
        }
        
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            this.ofdExcelPath.ShowDialog();
            string rawFileName = ofdExcelPath.FileName;
            //取出文件名
            string fileName = rawFileName.Substring(rawFileName.LastIndexOf("\\")+1);
            this.txtFileName.Text = fileName;
        }

        //声明一个名为RInfo的属性，用来存储来自此窗体获得的信息
        public RawInfo RInfo
        {
            get
            {
                return rawInfo;
            }
        }
        private RawInfo rawInfo = null;
        private void btnSure_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ofdExcelPath.FileName))
            {
                MessageBoxEx.Show("请选择文件", "提示");
                return;
            }
            //将控件的信息赋值给窗体引用的信息变量
            int schYear = this.dtpSchYear.Value.Year;
            string semester = this.cboSemester.Text;
            string filePath = this.ofdExcelPath.FileName;
            //生成信息对象,外界可以通过属性获取到信息
            //但是不能随意更改
            rawInfo = new RawInfo(schYear, semester, filePath);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    public class RawInfo
    {
        public int SchYear { get; set; }
        public string Semester { get; set; }
        public string FilePath { get; set; }
        public RawInfo(int schYear, string semester, string filePath)
        {
            this.SchYear = schYear;
            this.Semester = semester;
            this.FilePath = filePath;
        }

        public override string ToString()
        {
            return SchYear + "年" + Semester + "学期" + FilePath;
        }
    }
}
