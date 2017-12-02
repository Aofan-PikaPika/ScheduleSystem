using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Schedule
{
    public class AlgorithmLayer
    {
        private Dictionary<string, Schedule.MembersInRoom> dicInfo;
        private int[] insertIndicator;
        private DataTable dtBuffer;
        private List<string> staffList;

        //构造器，获得所需参数
        public AlgorithmLayer(Dictionary<string, Schedule.MembersInRoom> Dic, int[] Indicator, DataTable Buffer, List<string> Staff) 
        {
            dicInfo = Dic;
            insertIndicator = Indicator;
            dtBuffer = Buffer;
            staffList = Staff;
        }






    }
}
