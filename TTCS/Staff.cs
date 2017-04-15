using System;

namespace TTCS
{
    public class Staff
    {
        public string Name;
        public Pair Level;
        public Date DayOfBirth;
        public double Salary;
        /// <summary>
        /// 0 - SortByName
        /// 1 - SortByLevel
        /// 2 - SortByDayOfBirth
        /// 3 - SortBySalary
        /// </summary>
        /// <param name="staff"></param>
        /// <param name="tos"></param>
        /// <returns></returns>

        public bool CompareTo(Staff staff, int tos)
        {
            bool result = false;
            switch (tos)
            {
                case 0: result = Name.CompareTo(staff.Name) > 0 ? true : false;
                    break;
                case 1: result = Level.Value < staff.Level.Value ? true : false;
                    break;
                case 2: result = DayOfBirth.CompareTo(staff.DayOfBirth) ? true : false;
                    break;
                case 3: result = Salary > staff.Salary ? true : false;
                    break;
            }
            return result;
           
        }

        
    }

   
}
