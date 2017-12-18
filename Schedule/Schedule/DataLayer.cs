using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using Entity;

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
            get { return finishIndexRoom1; }
            set { finishIndexRoom1 = value; }
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
        }

        public void GetInfoFromRaw()
        {
            DataRow dr = dtRawTchArrange.NewRow();
            dtRawTchArrange.Rows.Add(dr);
            statisticsFromTB(0);
            //为了算法加上的一列，统计完了必须去掉，否则会出
            dtRawTchArrange.Rows.Remove(dr);
        }

        #region 数据获取与清洗
        //静态方法必须用静态变量
        static DataTable dtRawTchArrange;
        public static DataTable DtRawTchArrange
        {
            get { return dtRawTchArrange; }
            set { dtRawTchArrange = value; }
        }

        //为sheet中的教师统计表
        static DataTable dtRawStatistics;
        public static DataTable DtRawStatistics
        {
            get { return dtRawStatistics; }
            set { dtRawStatistics = value; }
        }

        Dictionary<string, MembersInRoom> members = new Dictionary<string, MembersInRoom>();
        MembersInRoom mir;
        int[] indicator = new int[dtRawTchArrange.Rows.Count];
        List<string> roomList = new List<string>();
        //从数据源获得两个数据表格
        public static bool getDataFromFile(string filePath) 
        {
            DataSet ds = new DataSet();
            ds = ExcelOLEDB.ReadExcelToTable(filePath);
            if (ds != null) 
            {
               //获取教师列表的前两列
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

            dtRawTchArrange = ds.Tables["dt1"];
            //RawTchArrange在最后添加一行
            //为了在循环中逐行按时间和教室比较，区分时段

            //tb2删除前两行
            dtRawStatistics = ds.Tables["dt2"];
            for (int i = 0; i < 2; i++)
            {
                dtRawStatistics.Rows.Remove(dtRawStatistics.Rows[0]);
            }
            //这里的作用是将sheet2中老师的姓名去除空格
            for (int i = 0; i < dtRawStatistics.Rows.Count; i++)
            {
                dtRawStatistics.Rows[i]["姓名"] = dtRawStatistics.Rows[i]["姓名"].ToString().Replace(" ", "");
            }
        }

        

        //统计数据（每天：每一个场次需要的人数；每个场次起止index，每个场次已分配的人名单）
        private void statisticsFromTB(int startIndex ,string beforedate="") 
        {
            int dtLength = dtRawTchArrange.Rows.Count-1;
            HashSet<string> rooms = new HashSet<string>();
            //考虑使用list辅助          
            string flagString = dtRawTchArrange.Rows[startIndex]["考试时间"].ToString();
            //匹配出今天的日期
            Regex rDate = new Regex(@".*月.*日");
            string todayDate = rDate.Matches(flagString)[0].ToString();
            int roomsNum=0;
            int occupiedNum = 0;
            int i = 0;
            List<string> occupylist = new List<string>();
            //判断是否为同一天
            if (!String.Equals(beforedate,todayDate))
            {
                mir = new MembersInRoom();
            }
            int rowCnt = 0;//统计每个时段的行数
            //某一考场人数统计
            for (i=startIndex; i < dtLength; i++)
            {
                //利用哈希表的集合唯一性,插入的同时判断
                //如果加进去了，就不是同一间屋，就不是同一考场
                if (rooms.Add(dtRawTchArrange.Rows[i]["考场"].ToString()))/**/
                {
                    rowCnt++;
                    //房间列表里面存储考场信息
                    roomList.Add(dtRawTchArrange.Rows[i]["考场"].ToString());
                    if (dtRawTchArrange.Rows[i]["主监考"].ToString() != "")
                    {
                        //统计预定过的主监考的个数
                        occupiedNum++;
                        string occupyname = dtRawTchArrange.Rows[i]["主监考"].ToString();
                        if (occupyname.Substring(occupyname.Length - 2, 2) != "学院")
                        {
                            occupylist.Add(occupyname);
                        }
                    }
                }
                else 
                {   
                    //这句是干啥用的？构造一个指示器？
                    //删除这句程序会无限循环
                    if(int.Parse(dtRawTchArrange.Rows[i]["人数"].ToString()) < 10)
                        indicator[roomList.LastIndexOf(dtRawTchArrange.Rows[i]["考场"].ToString())]=i;
                    else
                    {
                        rowCnt++;
                        if (dtRawTchArrange.Rows[i]["主监考"].ToString() != "")
                        {
                            //统计预定过的主监考的个数
                            occupiedNum++;
                            string occupyname = dtRawTchArrange.Rows[i]["主监考"].ToString();
                            if (occupyname.Substring(occupyname.Length - 2, 2) != "学院")
                            {
                                occupylist.Add(occupyname);
                            }
                        }
                    }
                    roomList.Add(dtRawTchArrange.Rows[i]["考场"].ToString());
                }
                //这里没有将空格替换掉，不够健壮
                string foreString = dtRawTchArrange.Rows[i]["考试时间"].ToString();
                string backString = dtRawTchArrange.Rows[i + 1]["考试时间"].ToString();
                //这里好像检查到了边界的考试时间
                if (!String.Equals(foreString,backString))
                {
                    //这个关系到以后的运算
                    roomsNum = rowCnt * 2 - occupiedNum;
                    break;//到了边界直接break出去，记录一个时段的信息
                    //这个for虽然会在i < dtLength条件下结束，但实质上仅仅能处理一个时段的数据
                }
            }

            //字典赋值,一个字典是一个时间段的东西
            //根据time字符串辨别时间段
            //先匹配大时间段:例如：8:00 - 9:30
            Regex rTimeIntv = new Regex(@"[0-9][0-9]?\:[0-9][0-9]\-[0-9][0-9]?\:[0-9][0-9]");
            string timeIntv = rTimeIntv.Matches(flagString)[0].ToString();
            //再匹配第一个小时数字
            Regex rTimeHour = new Regex(@"[0-9][0-9]?");
            string timehour = rTimeHour.Match(timeIntv).ToString();
            switch (timehour)/*改成if*/
            {
                case "8":
                case "9":
                    {
                        mir.MemNumInRoom1 = roomsNum;
                        mir.StartIndexRoom1 = startIndex;
                        mir.FinishIndexRoom1 = i;
                        mir.OccupyTeacherRoom1 = occupylist;
                    }
                    break;
                case "10":
                case"11":
                    {
                        mir.MemNumInRoom2 = roomsNum;
                        mir.StartIndexRoom2 = startIndex;
                        mir.FinishIndexRoom2 = i;
                        mir.OccupyTeacherRoom2 = occupylist;
                    }
                    break;  
                case"2":
                case"3":
                    {
                        mir.MemNumInRoom3 = roomsNum;
                        mir.StartIndexRoom3 = startIndex;
                        mir.FinishIndexRoom3 = i;
                        mir.OccupyTeacherRoom3 = occupylist;
                    }
                    break;
                case"4":
                case"5":
                    {
                        mir.MemNumInRoom4 = roomsNum;
                        mir.StartIndexRoom4 = startIndex;
                        mir.FinishIndexRoom4 = i;
                        mir.OccupyTeacherRoom4 = occupylist;
                    }
                    break;
            }
            //直接替换掉引用有风险啊，如果上一次第1,2,3时段写好了，这一次递归下，第四个时段检测到todayDate的键已经有了
            //替换掉引用，123不就都没了？
            if (members.ContainsKey(todayDate))
            {
                members[todayDate] = mir;
            }
            else 
            {
                members.Add(todayDate, mir);
            }
            //递归调用
            //for循环中的break决定了for仅仅能处理一个时段的数据
            //这个递归不包含任何[分治法]的思想,仅仅是要保证程序处理完一个时段的数据之后还能继续进行
            //顺便传递一个今天的参数而已
            if (i+1<dtLength)
            {
                statisticsFromTB(i + 1,todayDate);
            }
            
        }

        //监考老师名单
        public List<string> getStaffList() 
        {
            List<string> staffList = new List<string>();
            if (dtRawStatistics!=null)
            {
                int dtLength = dtRawStatistics.Rows.Count;
                for (int i = 0; i < dtLength; i++)
                {
                    staffList.Add(dtRawStatistics.Rows[i]["姓名"].ToString().Replace(" ",""));
                }
                
            }
            return staffList;
        }

        //产生考场统计数据
        public Dictionary<string, MembersInRoom> getDictFromStatistics() 
        {
            if (members==null || members.Count == 0)
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

        public DataTable CreateTchArrangeDt( List<List<List<Teacher>>> teacherArrangeSolve ,Dictionary<string,MembersInRoom> dic) 
        {
            DataTable tchArrangeDt = GetEmptyTeacherArrangeDt();
            int rCnt = 0;
            int cCnt = 0;
            for (int i = 0; i < teacherArrangeSolve.Count; i++)
            {
                for (int j = 0; j < teacherArrangeSolve[i].Count; j++)
                {
                    //打乱一个时段的教师，准备排序
                    teacherArrangeSolve[i][j] = AlgorithmLayer.UpsetTeacherList(teacherArrangeSolve[i][j]);
                    for (int k = 0; k < teacherArrangeSolve[i][j].Count; k++)
                    {
                        
                        Teacher teacher = teacherArrangeSolve[i][j][k];
                        if (tchArrangeDt.Rows[rCnt][cCnt].ToString().Equals(""))
                        {
                            tchArrangeDt.Rows[rCnt][cCnt] = teacher.Name;
                            //指示器是看哪行和哪行一样的数组
                            //下一步的需求变化则面临不要指示器或者根据情况修正指示器的显示
                            if (indicator[rCnt] != 0)
                            {
                                tchArrangeDt.Rows[indicator[rCnt]][cCnt] = teacher.Name;
                            }
                        }
                        else//当前遍历dt所在的位置不为空
                        {
                            k--;
                        }
                        //实现z字形遍历
                        cCnt++;
                        if (cCnt > 1)
                        {
                            rCnt++;
                            cCnt = 0;
                        }
                    }
                }
            }
            return tchArrangeDt;
        }

        #endregion

        //取出两列，分别为主监考，副监考
        public DataTable GetEmptyTeacherArrangeDt()
        {
            if (dtRawTchArrange != null)
            {
                DataTable dtBuffer = dtRawTchArrange.DefaultView.ToTable(false, new string[] { "主监考", "副监考" });
                return dtBuffer;
            }
            else
            {
                return null;
            }
        }

        
        private static DataTable GetStatisticTable(int staffNumber)
        {
            DataTable dtstatisticData = new DataTable();
            DataColumn dc1 = new DataColumn("Y1", Type.GetType("System.Int16"));
            DataColumn dc2 = new DataColumn("Y2", Type.GetType("System.Int16"));
            DataColumn dc3 = new DataColumn("E1", Type.GetType("System.Int16"));
            DataColumn dc4 = new DataColumn("E2", Type.GetType("System.Int16"));
            DataColumn dc5 = new DataColumn("T1", Type.GetType("System.Int16"));
            DataColumn dc6 = new DataColumn("T2", Type.GetType("System.Int16"));
            DataColumn dc7 = new DataColumn("S1", Type.GetType("System.Int16"));
            DataColumn dc8 = new DataColumn("S2", Type.GetType("System.Int16"));
            DataColumn dc9 = new DataColumn("Total1", Type.GetType("System.Int16"));
            DataColumn dc10 = new DataColumn("Total2", Type.GetType("System.Int16"));
            DataColumn dc11 = new DataColumn("Total3", Type.GetType("System.Int16"));
            dtstatisticData.Columns.Add(dc1);
            dtstatisticData.Columns.Add(dc2);
            dtstatisticData.Columns.Add(dc3);
            dtstatisticData.Columns.Add(dc4);
            dtstatisticData.Columns.Add(dc5);
            dtstatisticData.Columns.Add(dc6);
            dtstatisticData.Columns.Add(dc7);
            dtstatisticData.Columns.Add(dc8);
            dtstatisticData.Columns.Add(dc9);
            dtstatisticData.Columns.Add(dc10);
            dtstatisticData.Columns.Add(dc11);
            for (int i = 0; i < staffNumber;i++ )
            {
                DataRow dr = dtstatisticData.NewRow();
                dtstatisticData.Rows.Add(dr);
            }


            for (int i = 0; i < dtstatisticData.Rows.Count; i++)
            {
                for (int j = 0; j < dtstatisticData.Columns.Count; j++)
                {
                    dtstatisticData.Rows[i][j] = 0;
                }
            }
            return dtstatisticData;
        }


        public int[,] totalIndex = null;
        /// <summary>
        /// 计算一周主副监考的人数
        /// </summary>
        public DataTable Total(DataTable dtDataSource)
        {
            //string[] name = new string[dt2.Rows.Count];
            List<string> name = getStaffList();//获取人员的名单
            DataTable dtStatisticData = GetStatisticTable(name.Count);//根据人员名单获取最后的统计表格
            Dictionary<string, Schedule.MembersInRoom> dic = getDictFromStatistics();//获取数据dictionary

            #region 生成某一时段开始结束的二维表
            int daysNumber=dic.Count*2;
            totalIndex=new int[4,daysNumber];//记录时间段的二维数组
            int m = 0;
            foreach (KeyValuePair<string, Schedule.MembersInRoom> k in dic)
            {
                totalIndex[0,m] = k.Value.StartIndexRoom1;
                totalIndex[0, m + 1] = k.Value.FinishIndexRoom1;
                totalIndex[1, m] = k.Value.StartIndexRoom2;
                totalIndex[1, m+1] = k.Value.FinishIndexRoom2;
                totalIndex[2, m] = k.Value.StartIndexRoom3;
                totalIndex[2, m + 1] = k.Value.FinishIndexRoom3;
                totalIndex[3, m] = k.Value.StartIndexRoom4;
                totalIndex[3, m + 1] = k.Value.FinishIndexRoom4;
                m = m + 2;
            }
            #endregion
            for(int i=0;i<4;i++)
            {
                for(int j=0;j<daysNumber;j+=2)
                {
                    if(totalIndex[i,j]!=-1&&totalIndex[i,j+1]!=-1)
                    {
                        InsertData(totalIndex[i,j],totalIndex[i, j + 1], i, dtDataSource, ref dtStatisticData);
                    }
                   
                }
            }
            //开始根据表总计教考次数总和
            for(int i=0;i<name.Count;i++)
            {
                dtStatisticData.Rows[i]["Total1"] = Convert.ToInt16(dtStatisticData.Rows[i]["Y1"]) + Convert.ToInt16(dtStatisticData.Rows[i]["E1"]) + Convert.ToInt16(dtStatisticData.Rows[i]["T1"]) + Convert.ToInt16(dtStatisticData.Rows[i]["S1"]);
                dtStatisticData.Rows[i]["Total2"] = Convert.ToInt16(dtStatisticData.Rows[i]["Y2"]) + Convert.ToInt16(dtStatisticData.Rows[i]["E2"]) + Convert.ToInt16(dtStatisticData.Rows[i]["T2"]) + Convert.ToInt16(dtStatisticData.Rows[i]["S2"]);
                dtStatisticData.Rows[i]["Total3"] = Convert.ToInt16(dtStatisticData.Rows[i]["Total1"]) + Convert.ToInt16(dtStatisticData.Rows[i]["Total2"]);
            }

            return dtStatisticData;

        }

        /// <summary>
        /// 统计数据算法
        /// </summary>
        /// <param name="startIndex">某一时段开始的编号</param>
        /// <param name="finalIndex">某一时段结束的编号</param>
        /// <param name="room">某一时段的编号</param>
        /// <param name="DataSource">王怡峥给的表</param>
        /// <param name="dtStatisticTable">自己建的表</param>
        private void InsertData(int startIndex, int finalIndex, int room, DataTable DataSource, ref DataTable dtStatisticTable)
        {
            DataTable dtClass = dtRawTchArrange.DefaultView.ToTable(false, new string[] { "考场" });
            DataTable dtStuCnt = dtRawTchArrange.DefaultView.ToTable(false, new string[] { "人数" });
            List<string> name = getStaffList();
            HashSet<string> unique = new HashSet<string>();
            for (int i = startIndex; i <= finalIndex; i++)
            {
                //对于考场的重复性检查
                //为了防止分身的老师被加两次
                if (unique.Add(dtClass.Rows[i][0].ToString()))
                {
                    for (int j = 0; j < 2; j++)
                    {
                        string staffName = DataSource.Rows[i][j].ToString();//获得老师的名字
                        int staffID = name.IndexOf(staffName);
                        if (staffID != -1)
                        {
                            switch (room)
                            {
                                case 0:
                                    {
                                        if (j == 0)
                                        {
                                            dtStatisticTable.Rows[staffID]["Y1"] = int.Parse(dtStatisticTable.Rows[staffID]["Y1"].ToString()) + 1;
                                        }
                                        else
                                        {
                                            dtStatisticTable.Rows[staffID]["Y2"] = int.Parse(dtStatisticTable.Rows[staffID]["Y2"].ToString()) + 1;
                                        }
                                    } break;
                                case 1:
                                    {

                                        if (j == 0)
                                        {
                                            dtStatisticTable.Rows[staffID]["E1"] = int.Parse(dtStatisticTable.Rows[staffID]["E1"].ToString()) + 1;
                                        }
                                        else
                                        {
                                            dtStatisticTable.Rows[staffID]["E2"] = int.Parse(dtStatisticTable.Rows[staffID]["E2"].ToString()) + 1;
                                        }
                                    } break;
                                case 2:
                                    {

                                        if (j == 0)
                                        {
                                            dtStatisticTable.Rows[staffID]["T1"] = int.Parse(dtStatisticTable.Rows[staffID]["T1"].ToString()) + 1;
                                        }
                                        else
                                        {
                                            dtStatisticTable.Rows[staffID]["T2"] = int.Parse(dtStatisticTable.Rows[staffID]["T2"].ToString()) + 1;
                                        }
                                    } break;
                                case 3:
                                    {

                                        if (j == 0)
                                        {
                                            dtStatisticTable.Rows[staffID]["S1"] = int.Parse(dtStatisticTable.Rows[staffID]["S1"].ToString()) + 1;
                                        }
                                        else
                                        {
                                            dtStatisticTable.Rows[staffID]["S2"] = int.Parse(dtStatisticTable.Rows[staffID]["S2"].ToString()) + 1;
                                        }
                                    } break;
                            }
                        }
                    }

                }
                else
                {
                    //考场重复，看人数是是否大于十个人
                    //大于十个人也需要按老师统计
                    //这里设置阈值
                    if (int.Parse(dtStuCnt.Rows[i][0].ToString()) < 10)
                        continue;
                    else
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            string staffName = DataSource.Rows[i][j].ToString();//获得老师的名字
                            int staffID = name.IndexOf(staffName);
                            if (staffID != -1)
                            {
                                switch (room)
                                {
                                    case 0:
                                        {
                                            if (j == 0)
                                            {
                                                dtStatisticTable.Rows[staffID]["Y1"] = int.Parse(dtStatisticTable.Rows[staffID]["Y1"].ToString()) + 1;
                                            }
                                            else
                                            {
                                                dtStatisticTable.Rows[staffID]["Y2"] = int.Parse(dtStatisticTable.Rows[staffID]["Y2"].ToString()) + 1;
                                            }
                                        } break;
                                    case 1:
                                        {

                                            if (j == 0)
                                            {
                                                dtStatisticTable.Rows[staffID]["E1"] = int.Parse(dtStatisticTable.Rows[staffID]["E1"].ToString()) + 1;
                                            }
                                            else
                                            {
                                                dtStatisticTable.Rows[staffID]["E2"] = int.Parse(dtStatisticTable.Rows[staffID]["E2"].ToString()) + 1;
                                            }
                                        } break;
                                    case 2:
                                        {

                                            if (j == 0)
                                            {
                                                dtStatisticTable.Rows[staffID]["T1"] = int.Parse(dtStatisticTable.Rows[staffID]["T1"].ToString()) + 1;
                                            }
                                            else
                                            {
                                                dtStatisticTable.Rows[staffID]["T2"] = int.Parse(dtStatisticTable.Rows[staffID]["T2"].ToString()) + 1;
                                            }
                                        } break;
                                    case 3:
                                        {

                                            if (j == 0)
                                            {
                                                dtStatisticTable.Rows[staffID]["S1"] = int.Parse(dtStatisticTable.Rows[staffID]["S1"].ToString()) + 1;
                                            }
                                            else
                                            {
                                                dtStatisticTable.Rows[staffID]["S2"] = int.Parse(dtStatisticTable.Rows[staffID]["S2"].ToString()) + 1;
                                            }
                                        } break;
                                }
                            }
                        }
                    }
                }
            }

        }

    }

}
