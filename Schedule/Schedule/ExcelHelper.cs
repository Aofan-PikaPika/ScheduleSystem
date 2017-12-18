using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;

namespace Schedule
{
    public class ExcelHelper
    {
        public string mFilename;
        public Microsoft.Office.Interop.Excel.Application app;
        public Microsoft.Office.Interop.Excel.Workbooks wbs;
        public Microsoft.Office.Interop.Excel.Workbook wb;
        public Microsoft.Office.Interop.Excel.Worksheets wss;
        public Microsoft.Office.Interop.Excel.Worksheet ws;

        public ExcelHelper() 
        {

        }

        //打开一个EXCEL文件
        public bool Open(string FileName) 
        {
            app = new Application();
            wbs = app.Workbooks;
            wb = wbs.Add(FileName);
            mFilename = FileName;
            if (mFilename!="")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

         //获取一个工作表
        public Microsoft.Office.Interop.Excel.Worksheet GetSheet(string SheetName) 
        {
            Microsoft.Office.Interop.Excel.Worksheet s=(Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[SheetName];
            return s;
        }


        
        
        /// <summary>
        /// 给工作表的某个单元赋值
        /// </summary>
        /// <param name="ws">工作表名称</param>
        /// <param name="x">行</param>
        /// <param name="y">列</param>
        /// <param name="value">值</param>
        public void SetCellValue(string ws, int x, int y, object value) 
        {
            GetSheet(ws).Cells[x, y] = value;
        }
        /// <summary>
        /// 将内存中的datatable插入到Excel工作表的指定位置
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="ws"></param>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        public void InsertTable(System.Data.DataTable dt, string ws, int startX, int startY) 
        {
            for (int i = 0; i <dt.Rows.Count; i++)
            {
                for (int j = 0; j <dt.Columns.Count; j++)
                {
                    GetSheet(ws).Cells[startX + i, startY + j] = dt.Rows[i][j].ToString();
                }
                
            }
        }

        /// <summary>
        /// 保存文档
        /// </summary>
        /// <returns></returns>
        public bool save() 
        {
            if (mFilename=="")
            {
                return false;
            }
            else
            {
                try
                {
                    wb.Save();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 关闭并销毁一个EXCEL对象
        /// </summary>
        public void Close() 
        {
            try
            {
                wb.Close(Type.Missing, Type.Missing, Type.Missing);
                wbs.Close();
                app.Quit();
            }
            catch { }//在点击取消时可能在wbs中抛出异常，这个层给他处理掉
            wb = null;
            wbs = null;
            app = null;
            GC.Collect();
            
        }

        public bool SaveAs(object FileName)
        //文档另存为
        {
            try
            {
                wb.SaveAs(FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                return true;
            }

            catch (Exception ex)
            {
                return false;

            }
        }


    }
}
