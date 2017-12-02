using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace Schedule
{
    //构建字典所用的属性类
    public class MembersInRoom 
    {
        /// <summary>
        /// 第一场考试所需人数
        /// </summary>
        private int room1Num=0;
        public int MemNumInRoom1 
        {
            get { return room1Num; }
            set { room1Num = value; }
        }

        /// <summary>
        /// 第二场考试所需人数
        /// </summary>
        private int room2Num=0;
        public int MemNumInRoom2
        {
            get { return room2Num; }
            set { room2Num = value; }
        }

        /// <summary>
        /// 第三场考试所需人数
        /// </summary>
        private int room3Num=0;
        public int MemNumInRoom3
        {
            get { return room3Num; }
            set { room3Num = value; }
        }

        /// <summary>
        /// 第四场考试所需人数
        /// </summary>
        private int room4Num=0;
        public int MemNumInRoom4
        {
            get { return room4Num; }
            set { room4Num = value; }
        }

        //每一个场次开始index
        private int startIndexRoom1 = -1;
        public int StartIndexRoom1
        {
            get { return startIndexRoom1; }
            set { startIndexRoom1 = value; }
        }

        private int startIndexRoom2 = -1;
        public int StartIndexRoom2
        {
            get { return startIndexRoom2; }
            set { startIndexRoom2 = value; }
        }
        private int startIndexRoom3 = -1;
        public int StartIndexRoom3
        {
            get { return startIndexRoom3; }
            set { startIndexRoom3 = value; }
        }
        private int startIndexRoom4 = -1;
        public int StartIndexRoom4
        {
            get { return startIndexRoom4; }
            set { startIndexRoom4 = value; }
        }

        //每一场次结束index
        private int finishIndexRoom1 = -1;
        public int FinishIndexRoom1
        {
            get { return finishIndexRoom2; }
            set { finishIndexRoom2 = value; }
        }

        private int finishIndexRoom2 = -1;
        public int FinishIndexRoom2
        {
            get { return finishIndexRoom2; }
            set { finishIndexRoom2 = value; }
        }

        private int finishIndexRoom3 = -1;
        public int FinishIndexRoom3
        {
            get { return finishIndexRoom3; }
            set { finishIndexRoom3 = value; }
        }

        private int finishIndexRoom4 = -1;
        public int FinishIndexRoom4
        {
            get { return finishIndexRoom4; }
            set { finishIndexRoom4 = value; }
        }

        //该场次的老师
        private List<string> occupyTeacherRoom1 = new List<string>();
        public List<string> OccupyTeacherRoom1
        {
            get { return occupyTeacherRoom1; }
            set { occupyTeacherRoom1 = value; }
        }

        private List<string> occupyTeacherRoom2 = new List<string>();
        public List<string> OccupyTeacherRoom2
        {
            get { return occupyTeacherRoom2; }
            set { occupyTeacherRoom2 = value; }
        }

        private List<string> occupyTeacherRoom3 = new List<string>();
        public List<string> OccupyTeacherRoom3
        {
            get { return occupyTeacherRoom3; }
            set { occupyTeacherRoom3 = value; }
        }

        private List<string> occupyTeacherRoom4 = new List<string>();
        public List<string> OccupyTeacherRoom4
        {
            get { return occupyTeacherRoom4; }
            set { occupyTeacherRoom4 = value; }
        }



        
    }
    public class DataLayer
    {
        public DataLayer() 
        {
            statisticsFromTB(0);
        }
        #region 数据获取与清洗
        static DataTable dt1;
        static DataTable dt2;
        Dictionary<string, MembersInRoom> members = new Dictionary<string, MembersInRoom>();
        MembersInRoom mir;
        int[] indicator = new int[dt1.Rows.Count];
        List<string> roomList = new List<string>();
        //从数据源获得两个数据表格
        public static bool getDataFromFile(string filePath) 
        {
            DataSet ds = new DataSet();
            ds = ExcelOLEDB.ReadExcelToTable(filePath);
            if (ds != null) 
            {
                getDataTables(ds);
                return true;
            }
            else 
            {
                return false;
            }
        }       
        //清洗数据
        private static void getDataTables(DataSet ds) 
        {
            //tb1在最后添加一行
            dt1 = ds.Tables["dt1"];
            DataRow dr = dt1.NewRow();
            dt1.Rows.Add(dr);
            
            //tb2删除前两行
            dt2 = ds.Tables["dt2"];
            for (int i = 0; i < 2; i++)
            {
                dt2.Rows.Remove(dt2.Rows[0]);
            } 
            
        }
       
        //统计数据（每天：每一个场次需要的人数；每个场次起止index，每个场次已分配的人名单）
        public void statisticsFromTB(int startIndex ,string beforedate="") 
        {
            int dtLength = dt1.Rows.Count-1;
            HashSet<string> rooms = new HashSet<string>();
            //考虑使用list辅助          
            string flagString = dt1.Rows[startIndex]["考试时间"].ToString();
            string todayDate = flagString.Substring(0, 5);
            string time = flagString.Substring(13);
            int roomsNum=0;
            int occupiedNum = 0;
            int i = 0;
            List<string> occupylist = new List<string>();
            //判断是否为同一天
            if (!String.Equals(beforedate,todayDate))
            {
                mir = new MembersInRoom();
            }
            //某一考场人数统计
            for (i=startIndex; i < dtLength; i++)
            {
                
                string foreString = dt1.Rows[i]["考试时间"].ToString();
                string backString = dt1.Rows[i+1]["考试时间"].ToString();
                
                if (rooms.Add(dt1.Rows[i]["考场"].ToString()))
                {
                    roomList.Add(dt1.Rows[i]["考场"].ToString());
                    if (dt1.Rows[i]["主监考"].ToString() != "")
                    {
                        occupiedNum++;
                        string occupyname = dt1.Rows[i]["主监考"].ToString();
                        if (occupyname.Substring(occupyname.Length - 2, 2) != "学院")
                        {
                            occupylist.Add(occupyname);
                        }
                    }
                }
                else 
                {                   
                    indicator[roomList.LastIndexOf(dt1.Rows[i]["考场"].ToString())]=i;
                    roomList.Add(dt1.Rows[i]["考场"].ToString());
                }
                if (!String.Equals(foreString,backString))
                {
                    roomsNum = rooms.Count * 2 - occupiedNum;
                    break;
                }
            }
            //字典赋值           
            switch (time)
            {
                case "8:00-10:00":
                case ":00-10:00":
                case ":00-9:40":
                case "8:00-9:40":
                case "午8:00-9:40":
                case "午8:00-10:00":
                    {
                        mir.MemNumInRoom1 = roomsNum;
                        mir.StartIndexRoom1 = startIndex;
                        mir.FinishIndexRoom1 = i;
                        mir.OccupyTeacherRoom1 = occupylist;
                    }
                    break;
                case "10:20-12:00":
                case "午10:20-12:00":
                case "0:20-12:00":
                    {
                        mir.MemNumInRoom2 = roomsNum;
                        mir.StartIndexRoom2 = startIndex;
                        mir.FinishIndexRoom2 = i;
                        mir.OccupyTeacherRoom2 = occupylist;
                    }
                    break;
                case "午2:00-3:40":
                case "2:00-3:40":
                case ":00-3:40":
                    {
                        mir.MemNumInRoom3 = roomsNum;
                        mir.StartIndexRoom3 = startIndex;
                        mir.FinishIndexRoom3 = i;
                        mir.OccupyTeacherRoom3 = occupylist;
                    }
                    break;
                case "午4:20-6:00":
                case "4:20-6:00":
                case ":20-6:00":
                    {
                        mir.MemNumInRoom4 = roomsNum;
                        mir.StartIndexRoom4 = startIndex;
                        mir.FinishIndexRoom4 = i;
                        mir.OccupyTeacherRoom4 = occupylist;
                    }
                    break;
            }
            if (members.ContainsKey(todayDate))
            {
                members[todayDate] = mir;
            }
            else 
            {
                members.Add(todayDate, mir);
            }
            //递归调用
            if (i+1<dtLength)
            {
                statisticsFromTB(i + 1,todayDate);
            }
            
        }

        //监考老师名单
        public List<string> getStaffList() 
        {
            List<string> staffList = new List<string>();
            if (dt2!=null)
            {
                int dtLength = dt2.Rows.Count;
                for (int i = 0; i < dtLength; i++)
                {
                    staffList.Add(dt2.Rows[i]["姓名"].ToString());
                }
                
            }
            return staffList;
        }

        //产生考场统计数据
        public Dictionary<string, MembersInRoom> getDictFromStatistics() 
        {
            if (members==null)
            {
                statisticsFromTB(0);
            }
            return members;
        }

        #endregion

        #region 数据插入
        //数据插入指示器
        public int[] InsertDataIndicator() 
        {
            return indicator;
        }

        //数据区域替换(实现整体写入excel)
        public DataTable dataAreaBuffer() 
        {
            if (dt1 != null)
            {
                DataTable dtBuffer = dt1.DefaultView.ToTable(false, new string[] { "主监考", "副监考" });
                return dtBuffer;
            }
            else 
            {
                return null;
            }
        }

        #endregion

    }
}
