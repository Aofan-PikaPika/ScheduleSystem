using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Entity;
using Schedule.ControlExtend;



namespace Schedule
{
    public class AlgorithmLayer
    {
        private Dictionary<string, Schedule.MembersInRoom> dicInfo;
        private int[] insertIndicator;
        private DataTable dtBuffer;
        private List<string> staffList;
        

        //构造器，获得所需参数
        public AlgorithmLayer(Dictionary<string, Schedule.MembersInRoom> Dic, List<string> Staff) 
        {
            dicInfo = Dic;
            staffList = Staff;
        }

        public List<int[]> GetTeacherWanted()
        {
            List<int[]> teacherWanted = new List<int[]>();
            foreach (KeyValuePair<string, Schedule.MembersInRoom> k in dicInfo)
            {
                int[] oneDayInterval = new int[4];
                oneDayInterval[0] = k.Value.MemNumInRoom1;
                oneDayInterval[1] = k.Value.MemNumInRoom2;
                oneDayInterval[2] = k.Value.MemNumInRoom3;
                oneDayInterval[3] = k.Value.MemNumInRoom4;
                teacherWanted.Add(oneDayInterval);
            }
            return teacherWanted;
        }


        //ordered = occupy 意义相同,都是预定过的老师
        //*学院不会出现在这里面
        //空数组，而不是null表示时段没有预定的老师
        public List<string[][]> GetOrderedTeacherName()
        {
            List<string[][]> orderedTeacherName = new List<string[][]>();
            foreach (KeyValuePair<string, Schedule.MembersInRoom> k in dicInfo)
            {
                string[][] oneDayOrdered = new string[4][];
                oneDayOrdered[0] = k.Value.OccupyTeacherRoom1.ToArray();
                oneDayOrdered[1] = k.Value.OccupyTeacherRoom2.ToArray();
                oneDayOrdered[2] = k.Value.OccupyTeacherRoom3.ToArray();
                oneDayOrdered[3] = k.Value.OccupyTeacherRoom4.ToArray();
                orderedTeacherName.Add(oneDayOrdered);
            }
            return orderedTeacherName;
        }


        private int range = 0;
        public int Range
        {
            get { return range; }
        }
        public List<List<List<Teacher>>> CalcTeacherArrange()
        {
            List<int[]> teacherWanted =  this.GetTeacherWanted();
            List<string[][]> orderedTeacherName = this.GetOrderedTeacherName();
            List<Teacher> totalTeacherList = this.GetTotalTeacherList();

            List<List<List<Teacher>>> outputTeacher = ArrangeTeachers(totalTeacherList, teacherWanted, orderedTeacherName);
            while (true)
            {
                //先排序，再检查监考天数极差是否符合要求
                totalTeacherList.Sort(delegate(Teacher a, Teacher b)
                {
                    return a.InvigilationCnt.CompareTo(b.InvigilationCnt);
                });
                range= (totalTeacherList[totalTeacherList.Count - 1].InvigilationCnt - totalTeacherList[0].InvigilationCnt) ;
                if (range <= 2) break;
                range = 0;
                //不符合要求则在死循环里重新计算 
                foreach (Teacher t in totalTeacherList)
                {
                    t.InvigilateClear();
                }
                outputTeacher = ArrangeTeachers(totalTeacherList, teacherWanted, orderedTeacherName);
            }
          
            return outputTeacher;
        }

        //staffList用在这里
        private List<Teacher> GetTotalTeacherList()
        {
            List<Teacher> totalTeacherList = new List<Teacher>();
            foreach (string s in staffList)
            {
                totalTeacherList.Add(new Teacher(s));
            }
            return totalTeacherList;
        }


        private static List<List<List<Teacher>>> ArrangeTeachers(List<Teacher> totalTeacherLst, List<int[]> teacherWanted, List<string[][]> orderedTeacherName)
        {
            List<List<List<Teacher>>> outputTeacher = new List<List<List<Teacher>>>();
            //获取总天数
            int totalDay = teacherWanted.Count;
            if (totalDay != orderedTeacherName.Count) throw new Exception("总天数或者教师条数有误");
            for (int i = 0; i < totalDay; i++)
            {
                List<List<Teacher>> oneDayOutput = new List<List<Teacher>>();
                //按天处理，获取每天四个时段需要的人数和需要的教师
                int[] thisDayWanted = teacherWanted[i];
                string[][] thisDayOdTcherName = orderedTeacherName[i];
                //每天开始之前按干活强度排序一次
                totalTeacherLst.Sort(delegate(Teacher a, Teacher b)
                {
                    return a.InvigilationCnt.CompareTo(b.InvigilationCnt);
                });
                if (i == 0)totalTeacherLst =  UpsetTeacherList(totalTeacherLst);//开始时乱序一次
                #region 上午第一时段
                //安排上午第一时段

                //检查第一个时段预定的老师
                List<Teacher> oneIntvOdrTch = CheckOdr(totalTeacherLst, thisDayOdTcherName[0]);
                //在第一个时段选择前剔除预定的老师
                List<Teacher> oneRemoveOdrList = totalTeacherLst.Except(oneIntvOdrTch).ToList<Teacher>();
                //检查边界条件
                if (thisDayWanted[0] > totalTeacherLst.Count) throw new Exception("第" + i + "天，第1时段需要教师过多");
                //挑选出第一个时段需要写入记录上的老师
                Teacher[] oneIntvTcherArr = new Teacher[thisDayWanted[0]];
                oneRemoveOdrList.CopyTo(0, oneIntvTcherArr, 0, thisDayWanted[0]);//再复制
                //执行考试
                Invigilation(oneIntvTcherArr);
                oneDayOutput.Add(oneIntvTcherArr.ToList<Teacher>());
                /*注意，当前天在第一时段工作过的老师记录再oneIntvTcherArr,预定的老师不在其中！*/
                #endregion


                #region 上午第二时段

                List<Teacher> oneIntvTcher = oneIntvTcherArr.ToList<Teacher>();
                List<Teacher> twoIntvTcher = IntvInvigilationUseLast(totalTeacherLst, oneIntvTcher, thisDayWanted[1], thisDayOdTcherName[1]);
                oneDayOutput.Add(twoIntvTcher);
                #endregion


                //下午第一个时段
                //下午第一个时段选人，要优先选上午干过活以外的人，即总的teacherList与上午两个时段较长的一个作差
                //根据本算法的特性，
                /*这里也可以考虑取并集，碍于算法复杂度问题没法驾驭，则没有实施*/
                List<Teacher> amLongerTcherList = GetLongerList(oneIntvTcher, twoIntvTcher);
                List<Teacher> threeIntvTcher = IntvInvigilationUseLastRemain(totalTeacherLst, amLongerTcherList, thisDayWanted[2], thisDayOdTcherName[2]);
                oneDayOutput.Add(threeIntvTcher);

                //下午第二个时段
                List<Teacher> fourIntvTcher = IntvInvigilationUseLast(totalTeacherLst, threeIntvTcher, thisDayWanted[3], thisDayOdTcherName[3]);
                oneDayOutput.Add(fourIntvTcher);
                outputTeacher.Add(oneDayOutput);
            }
            return outputTeacher;
        }

        private static List<Teacher> IntvInvigilationUseLastRemain(List<Teacher> totalTeacherLst, List<Teacher> lastIntvTcher, int thisIntvWanted, string[] thisIntvOdTcherName)
        {
            List<Teacher> lastIntvExOdr;
            List<Teacher> lastIntvRemainExOdr;
            GetTwoList(totalTeacherLst, lastIntvTcher, thisIntvOdTcherName, out lastIntvExOdr, out lastIntvRemainExOdr);
            return ChooseAndAdd(thisIntvWanted,lastIntvRemainExOdr, lastIntvExOdr);
        }
        private static List<Teacher> IntvInvigilationUseLast(List<Teacher> totalTeacherLst, List<Teacher> lastIntvTcher, int thisIntvWanted, string[] thisIntvOdTcherName)
        {
            List<Teacher> lastIntvExOdr;
            List<Teacher> lastIntvRemainExOdr;
            GetTwoList(totalTeacherLst, lastIntvTcher, thisIntvOdTcherName, out lastIntvExOdr, out lastIntvRemainExOdr);
            return ChooseAndAdd(thisIntvWanted, lastIntvExOdr, lastIntvRemainExOdr);
        }
        private static void GetTwoList(List<Teacher> totalTeacherLst, List<Teacher> lastIntvTcher, string[] thisIntvOdTcherName, out List<Teacher> lastIntvExOdr, out List<Teacher> lastIntvRemainExOdr)
        {
            //计算上一场刨除本场预定的老师
            List<Teacher> thisIntvOdrFromLast = CheckOdr(lastIntvTcher, thisIntvOdTcherName);
            lastIntvExOdr = lastIntvTcher.Except(thisIntvOdrFromLast).ToList<Teacher>();
            //计算上一场没干活的刨除本场预定的老师
            List<Teacher> lastRemain = totalTeacherLst.Except(lastIntvTcher).ToList<Teacher>();
            List<Teacher> thisIntvOdrFromLastRemain = CheckOdr(lastRemain, thisIntvOdTcherName);
            lastIntvRemainExOdr = lastRemain.Except(thisIntvOdrFromLastRemain).ToList<Teacher>();
        }

        private static List<Teacher> ChooseAndAdd(int thisIntvWanted, List<Teacher> firstAdd, List<Teacher> SecondAdd)
        {
            //开始选本时段的人,
            Teacher[] thisIntvTcherArr = new Teacher[thisIntvWanted];
            AddTchFromListToArr(thisIntvWanted, firstAdd, thisIntvTcherArr,0);
            //如果监考老师不够
            if (firstAdd.Count < thisIntvWanted)
            {
                //计算不够的人数
                int needNum = thisIntvWanted - firstAdd.Count;
                //计算上一场或者上午已经监考过，但在优先选择的集合中没有出现，且没在这一场预定主监的老师
                if (needNum > SecondAdd.Count) throw new Exception("考试安排不合理");//这里不抛异常，人数过多时下面也会抛异常
                AddTchFromListToArr(needNum, SecondAdd, thisIntvTcherArr,firstAdd.Count);
            }
            //执行考试
            Invigilation(thisIntvTcherArr);
            //输出本时段工作过且没有预定本时段主监的老师，供下一时段或者下午的算法使用
            return thisIntvTcherArr.ToList<Teacher>();
            //字符串输出用于记录
        }
     

        private static void AddTchFromListToArr(int want, List<Teacher> have, Teacher[] thisIntvTcherArr,int originalNum)
        {
            #region  选人前不用每次都排序
            /*
            //每次干活数量从大到小排序,干活少的先上
            have.Sort(delegate(Teacher a, Teacher b)
            {
                return a.InvigilationCnt.CompareTo(b.InvigilationCnt);
            });
             */
            #endregion

            //添加监考老师
            have.CopyTo(0, thisIntvTcherArr, originalNum, Math.Min(have.Count, want));
        }



        //同时有两种行为：剔除掉已预约的老师，给已预约的老师+1
        private static List<Teacher> CheckOdr(List<Teacher> teacherLst, string[] thisIntvOdrName)
        {
            //检查有无预定
            List<Teacher> odrTch = new List<Teacher>();//第一个时段预定过的老师
            if (thisIntvOdrName != null)
            {
                for (int tchCnt = 0; tchCnt < thisIntvOdrName.Length; tchCnt++)
                {
                    foreach (Teacher t in teacherLst)
                    {
                        if (t.Name.Equals(thisIntvOdrName[tchCnt]))
                        {
                            t.Invigilate();//老师监考
                            odrTch.Add(t);//将预定的老师筛选
                        }
                    }
                }
            }
            return odrTch;
        }

        //乱序教师列表的函数
        public static List<Teacher> UpsetTeacherList(List<Teacher>  t)
        {
            #region 这个是老式的乱序方法，能实现轻微的乱序
            /*
            for (int i = 0; i < t.Count; i++)
            {
                Random rnd = new Random();
                int j = i + rnd.Next(t.Count - i);//这里不太懂，为什么不是生成一个最大的随机数就可以了？
                Teacher set = t[j];
                t[j] = t[i];
                t[i] = set;
            }
             */
            #endregion 
            int[] index = GetRndIntArr(t.Count);
            List<Teacher> newList = new List<Teacher>();
            for (int i = 0; i < index.Length; i++)
            {
                newList.Add(t[index[i]]);
            }

            return newList;
        }

        //循环执行考试的函数,同时有两种行为:循环执行方法，动态向数组中添加第一个时段老师的名字
        private static void Invigilation( Teacher[] intvTcherArr)
        {
            foreach (Teacher t in intvTcherArr)
            {
                t.Invigilate();
                //动态添加第一个时段老师的名字
            }
        }

        //比较两个列表哪个长的算法
        private static List<Teacher> GetLongerList(List<Teacher> oneIntvTcher, List<Teacher> twoIntvTcher)
        {
            int a = oneIntvTcher.Count;
            int b = twoIntvTcher.Count;
            return (a > b) ? oneIntvTcher : twoIntvTcher;
        }

        private static int[] GetRndIntArr(int length)
        {
            int[] index = new int[length];
            for (int i = 0; i < index.Length; i++)
                index[i] = i;
            int[] result = new int[length];
            int site = length;
            Random r = new Random();
            for (int i = 0; i < result.Length; i++)
            {
                int id = r.Next(0, site - 1);
                result[i] = index[id];
                index[id] = index[site - 1];
                site--;
            }
            return result;
        }


        public static void ForceCalc(DataTable origionRawDtArrange, DataTable origionRawDtStatistics, out DataTable finalDt, out DataTable finalStatistic, out int range)
        {
            List<List<List<Teacher>>> _teacherArrangeSolve = null;
            //执行让数据层处理数据的步骤
            DataLayer dl = new DataLayer();
            //给其中全局静态变量赋初值
            DataLayer.DtRawTchArrange = origionRawDtArrange;
            DataLayer.DtRawStatistics = origionRawDtStatistics;
            //处理数据变成字典形式
            dl.GetInfoFromRaw();
            //首次赋值并计算
            AlgorithmLayer al = new AlgorithmLayer(dl.getDictFromStatistics(), dl.getStaffList());
            _teacherArrangeSolve = al.CalcTeacherArrange();
            
            DataTable thisTimeDtArrange = null;
            DataTable thisTimeDtStatistic= null;
            ////外层循环控制主副均
            //while (true)
            //{
                //使理论折腾率与实际折腾率相等
                while (true)
                {
                    DataTable dt = dl.CreateTchArrangeDt(_teacherArrangeSolve,dl.getDictFromStatistics());
                    DataTable dtS = dl.Total(dt);
                    DataChecker dc = new DataChecker(dl.getDictFromStatistics(), dl.getStaffList(), dt, dl.totalIndex);
                    if (dc.Check()) break;
                    al = new AlgorithmLayer(dl.getDictFromStatistics(),dl.getStaffList());
                    _teacherArrangeSolve = al.CalcTeacherArrange();
                }
                //暴力主副均
                thisTimeDtArrange = dl.CreateTchArrangeDt(_teacherArrangeSolve,dl.getDictFromStatistics());
                thisTimeDtStatistic = dl.Total(thisTimeDtArrange);
                bool majorMinorBalance = true;
                int majorMinorThreshold = 3;//主副间最多差3
                DataTable dtMajorMinor = thisTimeDtStatistic.GetCols(8, 2);//一列是主，另一列是副
                foreach (DataRow dr in dtMajorMinor.Rows)
                {
                    int thisTchMajorCnt = int.Parse(dr.ItemArray[0].ToString());
                    int thisTcnMinorCnt = int.Parse(dr.ItemArray[1].ToString());
                    int threshold =Math.Abs( thisTchMajorCnt- thisTcnMinorCnt );
                    if(threshold > majorMinorThreshold) 
                        majorMinorBalance = false;//只要有一位老师的主副不均就达不到目标 继续进行循环
                }
            //    if(majorMinorBalance) break;
            //}
            //赋值退出
            finalDt = thisTimeDtArrange;
            finalStatistic = thisTimeDtStatistic;
            range = al.Range;

        }
    }
}
