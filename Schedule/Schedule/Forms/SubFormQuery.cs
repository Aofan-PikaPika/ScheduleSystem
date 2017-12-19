using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CCWin;
using System.Configuration;
using Shedule.Store;
using System.Data.SQLite;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Schedule.Forms
{
    //使用委托将子窗体查询到的datatable传给主窗体
    public delegate void getTableFromDBHandle(DataTable dt1, DataTable dt2);
    public partial class SubFormQuery : Skin_Metro
    {
       
        public getTableFromDBHandle getTableFromDBFunction;
        public SubFormQuery()
        {
            InitializeComponent();
        }


        private void btnSure_Click(object sender, EventArgs e)
        {
            //查询第一个datatable
            string sqlSearch = @"select dtArrage from tb_calcRecord "
                 + "where schYear=" + this.cboSchoolYear.SelectedItem.ToString() + " and " + " semester='" + this.cboSemester.SelectedItem.ToString() + "'";
            string connectionStr = @"Data Source= |DataDirectory|\ScheduleDB.db;Pooling=true;FailIfMissing=false";
            SQLiteCommand cmd = SQLiteHelper.CreateCommand(connectionStr, sqlSearch);
            DataTable dt = SQLiteHelper.ExecuteDataset(cmd).Tables[0];
            //查询第二个datatable
            string sqlSearch2 = @"select dtStatistic from tb_calcRecord "
              + "where schYear=" + this.cboSchoolYear.SelectedItem.ToString() + " and " + " semester='" + this.cboSemester.SelectedItem.ToString() + "'";
            SQLiteCommand cmd2 = SQLiteHelper.CreateCommand(connectionStr, sqlSearch2);
            DataTable dt0 = SQLiteHelper.ExecuteDataset(cmd2).Tables[0];
            if (dt.Rows.Count != 0 && dt0.Rows.Count != 0)
            {
                byte[] buffer = (byte[])dt.Rows[0][0];
                DataTable dt1 = Deserilize<DataTable>(buffer);
                byte[] buffer2 = (byte[])dt0.Rows[0][0];
                DataTable dt2 = Deserilize<DataTable>(buffer2);
                getTableFromDBFunction(dt1, dt2);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("该表不存在");
            }
           
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private static T Deserilize<T>(byte[] buffer)
        {
            BinaryFormatter bFmter = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(buffer);
            Object obj = bFmter.Deserialize(ms);
            return (T)obj;//强转在内部进行
        }

        private void SubFormQuery_Load(object sender, EventArgs e)
        {
            string sqlSearch = @"select schYear from tb_calcRecord group by schYear";
            string connectionStr = @"Data Source= |DataDirectory|\ScheduleDB.db;Pooling=true;FailIfMissing=false";
            SQLiteCommand cmd = SQLiteHelper.CreateCommand(connectionStr, sqlSearch);
            DataTable dt1 = SQLiteHelper.ExecuteDataset(cmd).Tables[0];
            if (dt1.Rows.Count != 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    this.cboSchoolYear.Items.Add(dt1.Rows[i][0].ToString());
                }
                this.cboSchoolYear.SelectedIndex = 0;
            }
           

        }

        private void cboSchoolYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cboSemester.Items.Clear();
            string sqlSearch2 = @"select semester from tb_calcRecord"
               + " where schYear=" + this.cboSchoolYear.SelectedItem.ToString();
            string connectionStr2 = @"Data Source= |DataDirectory|\ScheduleDB.db;Pooling=true;FailIfMissing=false";
            SQLiteCommand cmd2 = SQLiteHelper.CreateCommand(connectionStr2, sqlSearch2);
            DataTable dt2 = SQLiteHelper.ExecuteDataset(cmd2).Tables[0];
            if (dt2.Rows.Count != 0)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    this.cboSemester.Items.Add(dt2.Rows[i][0].ToString());
                }
                this.cboSemester.SelectedIndex = 0;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
