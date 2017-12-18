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
using System.Threading;
using Entity;
namespace Schedule
{
    public partial class Form1 : Skin_DevExpress
    {
        public Form1()
        {
            InitializeComponent();
            //skinRollingBar1.StartRolling();
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
                bool fileProblemFlag = false;
                bool exceptionFlag = false;
                try
                {
                    fileProblemFlag = !DataLayer.getDataFromFile(filePath);
                }catch{
                    exceptionFlag = true;
                    workButton.Enabled = false;
                    exportButton.Enabled = false;
                    btnCheck.Enabled = false;
                }
                finally
                {
                    if (fileProblemFlag)
                    {
                        MessageBox.Show("导入不成功，请关闭正在打开的Excel，或者尝试安装ACCESS数据库驱动", "错误");
                    }
                    else if (exceptionFlag)
                    {
                        MessageBox.Show("数据格式不符合要求","错误");
                    }
                    else
                    {
                        workButton.Enabled = true;
                    }
                }
            }

        }

        
 
        private void workButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Stopwatch swForUser = new System.Diagnostics.Stopwatch();
            double loopInterval = -1;
            swForUser.Start();
            AlgorithmLayer al = null;
            //UI主线程执行运算，子线程弹出等待界面
            Thread t = new Thread(() => new FrmWait().ShowDialog());
            t.Start();
            this.Enabled = false;
            try
            {
                _teacherArrangeSolve = null;
                DataLayer dl = new DataLayer();//数据层生成的同时，执行数据分析，数据清洗等操作     
                al = new AlgorithmLayer(dl.getDictFromStatistics(),dl.getStaffList());
                _teacherArrangeSolve = al.CalcTeacherArrange();
                //使理论折腾率与实际折腾率相等
                
                while (true)
                {
                    DataTable dt = dl.GetEmptyTeacherArrangeDt();
                    DataTable dtS = dl.Total(dt);
                    DataChecker dc = new DataChecker(dl.getDictFromStatistics(), dl.getStaffList(), dt, dl.totalIndex);
                    dc.Check();
                    List<double[]> checkSolve = dc.CheckSolve;
                    //实际值与理论值不等就不跳出循环重新运算
                    bool therialEqualsReality = true;
                    foreach (double[] oneDayCheck in checkSolve)
                    {
                        if (oneDayCheck[2] != oneDayCheck[3])
                        {
                            therialEqualsReality = false;
                        }
                    }
                    if (therialEqualsReality) break;
                    al = new AlgorithmLayer(dl.getDictFromStatistics(),dl.getStaffList());
                    _teacherArrangeSolve = al.CalcTeacherArrange();
                }
                
                Thread.Sleep(500);//关闭前不让主线程sleep可能会出现不稳定的情况
                swForUser.Stop();
                loopInterval = Math.Round(double.Parse(swForUser.ElapsedMilliseconds.ToString()) / 1000, 2);
                exportButton.Enabled = true;
                t.Abort();
                MessageBox.Show("耗时" + loopInterval + "秒，监考次数极差为" + al.Range + "\n导出后对结果不满意，可再点击排班", "执行结果");
                btnCheck.Enabled = true;
            }
            catch
            {
                Thread.Sleep(500);//关闭前不让主线程sleep可能会出现不稳定的情况
                t.Abort();
                swForUser.Stop();
                MessageBox.Show("数据格式不符合要求", "错误");
                //让用户无法进行导出,计算，验证
                workButton.Enabled = false;
                exportButton.Enabled = false;
                btnCheck.Enabled = false;
            }
            this.Enabled = true;
        }

        List<List<List<Teacher>>> _teacherArrangeSolve = null;//窗体保存结果的引用

        private void exportButton_Click(object sender, EventArgs e)
        {
            try
            {
                DataLayer dl = new DataLayer();
                DataTable dt = dl.CreateTchArrangeDt(_teacherArrangeSolve,dl.getDictFromStatistics());//这个过程就是自原始表格中获取两个竖列
                DataTable dtS = dl.Total(dt);
                ExcelHelper sh = new ExcelHelper();
                if (sh.Open(filePath))
                {
                    //整体将人员安排的表格写入
                    sh.InsertTable(dt, "整班", 2, 7);
                    sh.InsertTable(dtS, "监考次数统计", 4, 3);
                }
                string newName = Path.GetDirectoryName(filePath) + "\\安排结果.xlsx";
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = newName;
                sfd.Filter = "EXCEL文件(*.xls)|*.xls;*.xlsx";
                DialogResult dr = sfd.ShowDialog();
                //这里如果允许Microsoft Excel弹出对话框，则会给用户过多的信息
                //而且用户如果一路点击取消，Excel进程将不会关闭
                //会一直提示是否保存原文件，给用户的操作带来麻烦
                sh.app.DisplayAlerts = false;//使用DisplayAlerts
                if (dr == DialogResult.OK)
                {
                    sh.SaveAs(sfd.FileName);
                }
                sh.Close();
            }
            catch
            {
                MessageBox.Show("数据格式不符合要求", "错误");
                workButton.Enabled = false;
                exportButton.Enabled = false;
                btnCheck.Enabled = false;
            }
            
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            DataLayer dl = new DataLayer();
            DataTable dt = dl.CreateTchArrangeDt(_teacherArrangeSolve,dl.getDictFromStatistics());//这个过程就是自原始表格中获取两个竖列
            DataTable dtS = dl.Total(dt);
            string checkSolve = "";
            DataChecker dc = new DataChecker(dl.getDictFromStatistics(), dl.getStaffList(), dt, dl.totalIndex);
            dc.Check();
            dc.TextOutput(ref checkSolve);
            FrmChecker checker = new FrmChecker(checkSolve);
            checker.ShowDialog();
        }

   
    }
}
