using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CCWin.SkinControl;
using System.Windows.Forms;
using System.Data;

namespace Schedule.ControlExtend
{
    /*面向对象思想扩张Dgv，实现改变列名*/
    public static class ExtendDataGridView
    {
        public static bool ChangeHeadValue(this DataGridView dgv, int col, string expectedStr)
        {
            if (col < 0) throw new Exception("要求了负的列值");
            //有空的表头老师没填
            //这里填了，以后还可能会有问题

            if (col >= dgv.ColumnCount) throw new Exception("输入数据少列");
            //{
            //    dgv.Columns.Add("col" + col, expectedStr);
            //}
            dgv.Columns[col].HeaderText = expectedStr;
            return true;
        }

        public static void DisallowColSort(this DataGridView dgv)
        {
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public static bool ChangeMultyHeadValue(this DataGridView dgv,int startCol,string[] expectedStr)
        {
            if (expectedStr == null) return false;
            int endCol = startCol + expectedStr.Length - 1;
            if (startCol > dgv.ColumnCount 
                || endCol > dgv.ColumnCount 
                || startCol < 0 
                || endCol < 0) return false;
            for (int i = startCol; i <= endCol; i++)
            {
                dgv.ChangeHeadValue(i, expectedStr[i-startCol]);
            }
            return true;
        }


        public static bool FillDt(this DataGridView dgv, int startCol, DataTable dt)
        {
            //((DataTable)dgv.DataSource)暂且不判断行的关系 直接覆盖
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                for (int col = 0; col < dt.Columns.Count; col++)
                {
                    dgv.Rows[row].Cells[startCol + col].Value = dt.Rows[row][col];
                }
            }
            return true;
        } 


    }
}
