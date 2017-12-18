using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Entity
{
    public class Teacher
    {
        string _name = "";
        public string Name
        {
            get { return _name; }   
        }
        int _invigilationCnt = 0;
        public int InvigilationCnt
        {
            get { return _invigilationCnt; }
        }

        //为了支撑主副均的算法，在这里添加主监考数和副监考数的属性
        //主监考次数
        private int _majorInvigilationCnt = 0;
        public int MajorInvilgilationCnt
        {
            get { return _majorInvigilationCnt; }
        }
        //副监考次数
        private int _minorInvigilationCnt = 0;
        public int MinorInvigilationCnt
        {
            get { return _minorInvigilationCnt; }
        }

        public void DoMajorInv()
        {
            this._majorInvigilationCnt++;
        }
        public void DoMinorInv()
        {
            this._minorInvigilationCnt++;
        }


        public Teacher(string name)
        {
            _name = name;
            _invigilationCnt = 0;
        }
        public void Invigilate()
        {
            _invigilationCnt++;//监考次数只能加，不能减
        }

        public override string ToString()
        {
            return this._name + "共监考" + _invigilationCnt + "次";
        }
        public void InvigilateClear()
        {
            _invigilationCnt = 0;
        }
     
    }
}
