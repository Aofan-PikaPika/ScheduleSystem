using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace Schedule
{
    public class ExcelOLEDB
    {
        /// <summary>
        /// 根据excel的路径把第一个sheet中的内容放入datatable
        /// </summary>
        /// <param name="path">excel存放的路径</param>
        /// <returns></returns>
        public static DataSet ReadExcelToTable(string path) 
        {
            try
            {
               //连接字符串
                string connstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1';";
                using (OleDbConnection conn = new OleDbConnection(connstring)) 
                {
                    conn.Open();
                    //得到所有sheet的名字
                    DataTable sheetsName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });
                    string firstSheetName = sheetsName.Rows[0][2].ToString();
                    string secondSheetName = sheetsName.Rows[1][2].ToString();
                    string sql=string.Format("SELECT * FROM [{0}]", firstSheetName);
                    string sql2 = string.Format("SELECT * FROM [{0}]", secondSheetName);
                    OleDbDataAdapter ada = new OleDbDataAdapter(sql, connstring);
                    OleDbDataAdapter ada2 = new OleDbDataAdapter(sql2, connstring);
                    DataTable dt1 = new DataTable();
                    ada.Fill(dt1);
                    DataTable dt2 = new DataTable();
                    ada2.Fill(dt2);
                    DataSet ds = new DataSet();
                    dt1.TableName = "dt1";
                    dt2.TableName = "dt2";
                    ds.Tables.Add(dt1);
                    ds.Tables.Add(dt2);
                    conn.Close();
                    return ds;
                }
            }
            catch (Exception )
            {
                return null;
            }
        }
    }
}
