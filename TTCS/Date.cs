using System;
using System.Collections.Generic;

namespace TTCS
{
    public class Date
    {
        public int Day;
        public int Month;
        public int Year;
        public override string ToString()
        {
            return Day.ToString() + "-" + Month.ToString() + "-" + Year.ToString();
        }

        public bool CompareTo(Date d)
        {
            if (Year > d.Year)
                return true;
            if (Year < d.Year)
                return false;
            else
            {
                if (Month > d.Month) return true;
                if (Month < d.Month) return false;
                else
                {
                    return Day > d.Day ? true : false;
                }
            }
        }
    }
}