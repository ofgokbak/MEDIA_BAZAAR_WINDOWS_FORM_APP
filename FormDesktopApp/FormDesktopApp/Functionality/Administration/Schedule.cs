using FormDesktopApp.Objects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormDesktopApp.Functionality.Administration
{
    class Schedule
    {
        private int scheduleID;
        private Days dayName;
        private Shift shiftName;
        private int workingEmployeeNumber;
        private int requiredEmployeeNumber;
        private int departmentID;
        
        public int Schedule_Week { get; set; }

        public Schedule()
        {
        }


        public int ScheduleID { get => scheduleID; set => scheduleID = value; }
        public int WorkingEmployeeNumber { get => workingEmployeeNumber; set => workingEmployeeNumber = value; }
        public int RequiredEmployeeNumber { get => requiredEmployeeNumber; set => requiredEmployeeNumber = value; }
        internal Shift ShiftName { get => shiftName; set => shiftName = value; }
        internal Days DayName { get => dayName; set => dayName = value; }
        public int DepartmentID { get => departmentID; set => departmentID = value; }
        public DateTime date { get; set; }
    }
}
