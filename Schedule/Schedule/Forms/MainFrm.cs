using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CCWin;
using Schedule.Forms;
using Schedule.ControlExtend;
using System.Threading;
using System.IO;

namespace Schedule
{
    public partial class MainFrm : Skin_Metro
    {
        public MainFrm()
        {
            InitializeComponent();
        }
        private void MainFrm_Load(object sender, EventArgs e)
        {
            this.statusLblSchoolYear.Text = Status.WaitToImport.ToString();
            //窗体初始化时，订阅窗体状态变化的事件
            this.StatusChange += MainFrm_StatusChange;
            //窗体状态改变为等待导入
            StatusChange(Status.WaitToImport);
            
        }

        #region 窗体状态处理模块
        //以枚举量声明窗体的状态
        private enum Status
        {
            WaitToImport,
            WaitToCalc,
            Processing,
            WaitToOutput,
        }
        //声明控制窗体状态的委托
        private delegate void StatusHandler(Status st);
        //声明窗体状态改变的事件
        private event StatusHandler StatusChange;

        private Status status;
        
        //窗体状态改变的事件处理程序
        private void MainFrm_StatusChange(Status st)
        {
            this.status = st;
            switch (st)
            {
                case Status.WaitToImport:
                    {
                        //根据状态控制标签
                        this.statusLblStatus.Text = "等待导入";
                        this.statusLblSchoolYear.Text = "就绪";
                        //根据状态控制按钮的可用性
                        this.tbtnIn.Enabled = true;
                        this.tbtnCalc.Enabled = false;
                        this.tbtnReset.Enabled = false;
                        this.tbtnOut.Enabled = false;
                        this.tbtnOutSeveralCol.Enabled = false;
                        this.tbtnSearch.Enabled = true;
                        this.tbtnSaveToDb.Enabled = false;
                    }
                    break;
                case Status.WaitToCalc:
                    {
                        this.statusLblStatus.Text = "等待计算";
                        this.statusLblSchoolYear.Text = rawInfo.SchYear + "学年 " + rawInfo.Semester + "学期";
                        this.tbtnIn.Enabled = true;
                        this.tbtnCalc.Enabled = true;
                        this.tbtnReset.Enabled = false;
                        this.tbtnOut.Enabled = false;
                        this.tbtnOutSeveralCol.Enabled = false;
                        this.tbtnSearch.Enabled = true;
                        this.tbtnSaveToDb.Enabled = false;
                    }
                    break;
                case Status.Processing:
                    {
                        //计算过程，导出过程开始时，使两个dgv的Enable状态变为不可用
                        //提交其脏数据
                        this.dgvArrange.Enabled = false;
                        this.dgvStatistic.Enabled = false;
                        this.statusLblStatus.Text = "正在执行...";
                    }
                    break;
                case Status.WaitToOutput:
                    {
                        this.dgvArrange.Enabled = true;
                        this.dgvStatistic.Enabled = true;
                        this.tbtnIn.Enabled = true;
                        if (_origionRawDtArrange != null && _origionRawDtStatistics != null)
                            this.tbtnCalc.Enabled = true;
                        else
                            this.tbtnCalc.Enabled = false;
                        this.tbtnReset.Enabled = true;
                        this.tbtnOut.Enabled = true;
                        this.tbtnOutSeveralCol.Enabled = true;
                        this.tbtnSearch.Enabled = true;
                        this.tbtnSaveToDb.Enabled = true;
                        this.statusLblStatus.Text = "等待导出";
                        this.statusLblStatus.ForeColor = Color.Black;
                    }
                    break;
            }

        }

        #endregion

       
        #region 导入模块
        //窗体引用导入信息的类变量
        RawInfo rawInfo = null;
        private void tbtnIn_Click(object sender, EventArgs e)
        {
            bool reInSuccess = false;
            //生成子窗体，获取需要处理的表格信息以及学年，学期信息
            SubFormIn frmIn = new SubFormIn();
            frmIn.ShowDialog();
            if (frmIn.RInfo != null)
            {
                rawInfo = frmIn.RInfo;
                reInSuccess = true;
            }
            frmIn.Dispose();
            //先尝试导入，导入不成功，不将状态转变
            //其他状态要重新转变到这个状态，到这儿肯定是需要导入，则启动导入程序
            if(rawInfo == null) return;
            //这个说明子窗体按了取消键
            if (!reInSuccess) return;
            //到这里说明真正进了窗体
            bool fileProblemFlag = false;
            bool exceptionFlag = false;
            try
            {
                fileProblemFlag = !DataLayer.getDataFromFile(rawInfo.FilePath);
            }
            catch
            {
                exceptionFlag = true;
            }
            finally
            {
                if (fileProblemFlag)
                    MessageBoxEx.Show("导入不成功，请关闭正在打开的Excel，或者尝试安装ACCESS数据库驱动", "错误");
                else if (exceptionFlag)
                    MessageBoxEx.Show("数据格式不符合要求", "错误");
                else
                {
                    this.dgvArrange.DataSource = DataLayer.DtRawTchArrange;
                    this.dgvStatistic.DataSource = DataLayer.DtRawStatistics;
                    try
                    {
                        CorrectTwoDgv();
                        //能走到这里说明窗体已经做好了计算准备，转变窗体的状态
                        StatusChange(Status.WaitToCalc);
                    }
                    catch (Exception ex)
                    {
                        MessageBoxEx.Show(ex.Message);
                        this.dgvArrange.DataSource = null;
                        this.dgvStatistic.DataSource = null;
                        StatusChange(Status.WaitToImport);
                    }
                }
            }
        }

        private void CorrectTwoDgv()
        {
            string[] firstStrArr = new string[] { "一", "二", "三", "四" };
            string[] secondStrArr = new string[] { "主监", "副监" };
            List<string> expectStrList = new List<string>();
            foreach (string stro in firstStrArr)
            {
                foreach (string strm in secondStrArr)
                {
                    expectStrList.Add("第" + stro + "大节" + strm + "场");
                }
            }
            expectStrList.Add("主监考场次总计");
            expectStrList.Add("副监考场次总计");
            expectStrList.Add("监考场次总计");
            this.dgvStatistic.ChangeMultyHeadValue(2, expectStrList.ToArray());
            this.dgvArrange.DisallowColSort();
            this.dgvStatistic.DisallowColSort();
        }
        #endregion




        private DataTable _origionRawDtArrange;
        private DataTable _origionRawDtStatistics;

        // _teacherArrangeSolve = null;//窗体保存结果的引用
        private void tbtnCalc_Click(object sender, EventArgs e)
        {
            
            //计算不能写死直接在窗体dgv上面取数据，有可能是二次计算
            //计算部分需要干两件事，一个是判断状态，存数据到内存,以供撤回
            //另一个是利用数据调用模块来计算

            //自等待计算过来,说明是根据导入，第一次计算
            if (status.Equals(Status.WaitToCalc))
            {
                StatusChange(Status.Processing);
                this._origionRawDtArrange = ((DataTable)this.dgvArrange.DataSource).Copy();
                this._origionRawDtStatistics = ((DataTable)this.dgvStatistic.DataSource).Copy();
            }
            StatusChange(Status.Processing);

            System.Diagnostics.Stopwatch swForUser = new System.Diagnostics.Stopwatch();
            double loopInterval = -1;
            swForUser.Start();
            //UI主线程执行运算，子线程弹出等待界面
            Thread t = new Thread(() => new FrmWait().ShowDialog());
            t.Start();
            try
            {
                //这三个参数用来接收ForceCalc返回的结果
                DataTable finalDt;
                DataTable finalStatistic;
                int range = 0;
                //将计算提取到Algorithm中的一个函数，循环求解的函数都在AlgorithmLayer中查看
                AlgorithmLayer.ForceCalc(this._origionRawDtArrange, this._origionRawDtStatistics, out finalDt, out finalStatistic, out range);
                //添加一些代码将计算完成的表添加到dgv中
                this.dgvArrange.FillDt(6, finalDt);
                this.dgvStatistic.FillDt(2, finalStatistic);
                //关闭前不让主线程sleep可能会出现不稳定的情况
                Thread.Sleep(500);
                swForUser.Stop();
                loopInterval = Math.Round(double.Parse(swForUser.ElapsedMilliseconds.ToString()) / 1000, 2);
                t.Abort();
                MessageBoxEx.Show("耗时" + loopInterval + "秒，监考次数极差为" + range + "\n导出后对结果不满意，可再点击排班", "执行结果");
                StatusChange(Status.WaitToOutput);
            }
            catch(Exception ex)
            {
                Thread.Sleep(500);//关闭前不让主线程sleep可能会出现不稳定的情况
                t.Abort();
                swForUser.Stop();
                MessageBoxEx.Show(ex.Message, "错误");
                StatusChange(Status.WaitToCalc);
                //让用户无法进行导出,计算，验证
            }
        }

 

        //重置按钮，撤回到修改以前的状态
        private void tbtnRecall_Click(object sender, EventArgs e)
        {
            this.dgvArrange.DataSource = null;
            this.dgvArrange.DataSource = _origionRawDtArrange.Copy();
            this.dgvStatistic.DataSource = null;
            this.dgvStatistic.DataSource = _origionRawDtStatistics.Copy();
            CorrectTwoDgv();
            StatusChange(Status.WaitToCalc);

        }

        #region 防止编辑dgv不回车出现数据不能录入的问题
        /*
        private void toolStrip_MouseHover_1(object sender, EventArgs e)
        {
            this.dgvArrange.Enabled = false;
            this.dgvStatistic.Enabled = false;
        }

        private void toolStrip_MouseLeave_1(object sender, EventArgs e)
        {
            this.dgvArrange.Enabled = true;
            this.dgvStatistic.Enabled = true;
        }
         */
        #endregion

        private void tbtnOut_Click(object sender, EventArgs e)
        {
            StatusChange(Status.Processing);
            RecountWorkCnt();
            ExcelHelper sh = new ExcelHelper();
            try
            {
                #region Cooy会出错误
                //猜测是copy会出现错乱的问题
                /*
                DataTable dtFinalArrange = ((DataTable)this.dgvArrange.DataSource).Copy();
                DataTable dtFinalStatistic = ((DataTable)this.dgvStatistic.DataSource).Copy();
                 * */
                #endregion
                DataTable dtFinalArrange = ((DataTable)this.dgvArrange.DataSource);
                DataTable dtFinalStatistic = ((DataTable)this.dgvStatistic.DataSource);
                //写入EXCEl
                if (sh.Open(this.rawInfo.FilePath))
                {
                    //整体将人员安排的表格写入
                    sh.InsertTable(dtFinalArrange, "整班", 2, 1);
                    sh.InsertTable(dtFinalStatistic, "监考次数统计", 4, 1);
                }
                ShowDialogAndSave(sh);
                sh.Close();
            }
            catch
            {
                MessageBox.Show("数据格式不符合要求", "错误");
                sh.Close();
            }
            StatusChange(Status.WaitToOutput);
        }

        private void ShowDialogAndSave(ExcelHelper sh)
        {
            string newName = Path.GetDirectoryName(this.rawInfo.FilePath) + "\\安排结果.xlsx";
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
        }

        private void tbtnSaveToDb_Click(object sender, EventArgs e)
        {
           
        }

        private void tbtnOutSeveralCol_Click(object sender, EventArgs e)
        {
            StatusChange(Status.Processing);
            RecountWorkCnt();
            ExcelHelper sh = new ExcelHelper();
            try
            {
                DataTable dtFinalArrange = ((DataTable)this.dgvArrange.DataSource);
                DataTable dtFinalStatistic = ((DataTable)this.dgvStatistic.DataSource);
                DataTable dtFA2Col = dtFinalArrange.GetCols(6, 2);
                DataTable dtFS11Col = dtFinalStatistic.GetCols(2, 11);
                if (sh.Open(this.rawInfo.FilePath))
                {
                    //整体将人员安排的表格写入
                    sh.InsertTable(dtFA2Col, "整班", 2, 7);
                    sh.InsertTable(dtFS11Col, "监考次数统计", 4, 3);
                }
                ShowDialogAndSave(sh);
                sh.Close();
            }
            catch
            {
                MessageBox.Show("数据格式不符合要求", "错误");
                sh.Close();
            }
            StatusChange(Status.WaitToOutput);
        }

        //这个事件处理程序确保程序一经变动就将变动更新到数据源
        //不过将dgv强制转换成数据源再Copy貌似会出很大问题
        private void dgvArrange_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (this.dgvArrange.IsCurrentCellDirty)
            {
                this.dgvArrange.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        //这个保证切换表格时重新统计一遍
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.status.Equals(Status.WaitToOutput)) return;
            RecountWorkCnt();
        }

        //这个是界面用来调用，统计函数
        private void RecountWorkCnt()
        {
            //获取数据源的两列
            DataTable dt = this.dgvArrange.DataSource as DataTable;
            DataTable newTeacher = dt.GetCols(6, 2);
            //统计教师监考数据
            DataLayer ReCounter = new DataLayer();
            DataTable dtRecountedStatistic = ReCounter.Total(newTeacher);
            //再次填充DGV
            this.dgvStatistic.FillDt(2, dtRecountedStatistic);
        }



    }
}
