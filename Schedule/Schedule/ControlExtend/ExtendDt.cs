using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Schedule.ControlExtend
{
    public static class ExtendDt
    {
        public static DataTable GetCols(this DataTable dt,int startCol ,int num)
        {
            if (dt.Columns.Count < startCol || dt.Columns.Count < (startCol + num - 1)) return null;
            DataTable newDt =null;
            List<string> colsName = new List<string>();
            while (num-- != 0)
            {
                string oneColName = dt.Columns[startCol].ColumnName;
                colsName.Add(oneColName);
                startCol++;
            }
            newDt = dt.DefaultView.ToTable(false, colsName.ToArray());
            return newDt;
        }
    }
}
