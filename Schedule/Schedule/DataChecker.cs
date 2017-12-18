using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Schedule.ControlExtend;

namespace Schedule
{
    public class DataChecker
    {
        private DataTable dt;//两列已经安排好的dt
        private Dictionary<string, MembersInRoom> dic;//分析天数，时段的字典
        private List<string> memberList;//人员总表
        private int[,] totalIndex;



        public DataChecker(Dictionary<string, MembersInRoom> dictionary, List<string> list, DataTable dt,int[,] totalIndex)
        {
            this.dic = dictionary;
            this.memberList = list;
            this.dt = dt;
            this.totalIndex = totalIndex;
        }

        private List<double[]> checkSolve;//每天上午时段覆盖率，下午时段覆盖率，当天折腾率，当天理论折腾率
        public List<double[]> CheckSolve
        {
            get { return checkSolve; }
        }
        public bool Check()
        {
            checkSolve = calcCover(totalIndex,dt);
            //实际值与理论值不等就不跳出循环重新运算
            bool therialEqualsReality = true;
            foreach (double[] oneDayCheck in checkSolve)
            {
                if (oneDayCheck[2] != oneDayCheck[3])
                {
                    therialEqualsReality = false;
                }
            }
            return therialEqualsReality;

        }

        private List<double[]> calcCover(int[,] totalIndex,DataTable dt)
        {
            List<double[]> solve = new List<double[]>();
            //遍历dt获取每天
            //先列,每次跳两个
            for (int column = 0; column < totalIndex.GetLength(1); column += 2)
            {
                List<List<string>> oneDayNmList = new List<List<string>>();
                for (int row = 0; row < 4; row++)
                {
                    //获取第column天row时段的开始行和结束行
                    int statIndex = totalIndex[row, column];
                    int endIndex = totalIndex[row, column + 1];
                    List<string> rawIntvNmList = ScanUniqNames(dt, statIndex, endIndex);
                    List<string> intvNmList = rawIntvNmList.Intersect(memberList).ToList<string>();//一个时段取出“理学院”等string的NmList
                    oneDayNmList.Add(intvNmList);
                }
                //进行集合的运算，统计
                //统计覆盖率
                //1交2
                double firstIntsSecond = oneDayNmList[0].Intersect(oneDayNmList[1]).ToList<string>().Count;
                //3交4
                double thirdIntsFourth = oneDayNmList[2].Intersect(oneDayNmList[3]).ToList<string>().Count;
                //去除脏数据
                for (int i = 0; i < 4; i++)
                {
                    if (oneDayNmList[i].Count == 0)
                    {
                        if (i > 1) thirdIntsFourth = -1;
                        else firstIntsSecond = -1;
                    }
                }
                //12取最小
                double firstSecondMinCnt = Math.Min(oneDayNmList[0].Count, oneDayNmList[1].Count);
                double thirdFourthMinCnt = Math.Min(oneDayNmList[2].Count, oneDayNmList[3].Count);
                //计算今天上午考试覆盖率
                double amCover = -1;
                double pmCover = -1;
                if (firstSecondMinCnt != 0.0)
                {
                    amCover = firstIntsSecond / firstSecondMinCnt;
                }
                if (thirdFourthMinCnt != 0.0)
                {
                    pmCover = thirdIntsFourth / thirdFourthMinCnt;
                }

               

                //统计折腾率
                //上午交下午/上午下午的最小
                List<string> upList = oneDayNmList[0].Union(oneDayNmList[1]).ToList<string>();
                List<string> downList = oneDayNmList[2].Union(oneDayNmList[3]).ToList<string>();
                List<string> upIntsDown = upList.Intersect(downList).ToList<string>();
                //先写控制折腾率的参数运算
                #region 这里计算理论折腾率
                double theorialOptimumNum = -1.0;
                double amMaxNum = Math.Max(oneDayNmList[0].Count, oneDayNmList[1].Count);
                double pmMaxNum = Math.Max(oneDayNmList[2].Count, oneDayNmList[3].Count);

                double amOffer = memberList.Count - amMaxNum;
                if (pmMaxNum != 0.0)
                {
                    if (amOffer > pmMaxNum) theorialOptimumNum = 0.0;
                    else
                    {
                        double lack = pmMaxNum - amOffer;
                        theorialOptimumNum = lack / pmMaxNum;
                    }
                }

                #endregion 
                double minUpDown = Math.Min(upList.Count, downList.Count);
                double crossUDCover = -1.0;
                if (minUpDown!=0.0)
                {
                    crossUDCover = upIntsDown.Count / minUpDown; 
                }
                //这个加了理论值
                double[] thisDayCover = { amCover, pmCover, crossUDCover, theorialOptimumNum};
                solve.Add(thisDayCover);
            }
            return solve;
        }

        //扫描一个时段的老师
        private List<string> ScanUniqNames(DataTable dt, int statIndex, int endIndex)
        {
            
            HashSet<string> rawIntvNmList = new HashSet<string>();
            if(statIndex < 0 )return rawIntvNmList.ToList<string>();
            for (int i = statIndex; i <= endIndex; i++)
            {
                for(int j = 0; j < 2;j++)
                {
                    rawIntvNmList.Add(dt.Rows[i][j].ToString());
                }
            }
            return rawIntvNmList.ToList<string>();
        }

        internal void TextOutput (ref string s)
        {
            for (int i = 0; i < checkSolve.Count; i++)
            {
                s = s + ("第" + (i + 1) + "天：\t上午覆盖率：" + (checkSolve[i][0] == -1 ? "无" : checkSolve[i][0].ToString("0.##")) + "，下午覆盖率：" + (checkSolve[i][1] == -1 ? "无" : checkSolve[i][1].ToString("0.##")) + "，当天折腾率：" + (checkSolve[i][2] == -1 ? "无" : checkSolve[i][2].ToString("0.##")) + "，当天理论折腾率：" + (checkSolve[i][3] == -1 ? "无" : checkSolve[i][3].ToString("0.##")) + "\r\n");               
            }
        }

        
        
   
        /// <summary>
        /// 传入界面上，用户修改过的安排表和人员统计表
        /// 检查同一时段内是否有教师“分身”
        /// </summary>
        /// <param name="changedArrangeDt">已经微调过的安排表</param>
        /// <param name="changedCntDt"></param>
        /// <param name="day"></param>
        /// <param name="timeIntv"></param>
        /// <returns></returns>
        /*
        public static bool CheckRepeat(DataTable changedArrangeDt,DataTable changedCntDt,out int day ,out int timeIntv)
        {
            DataLayer dl = new DataLayer();
            DataTable tmpOrigDtS =  DataLayer.DtRawStatistics;
            List<string> stafflist = dl.getStaffList();

            return false ;
        }
        */
      
        

    }
}
