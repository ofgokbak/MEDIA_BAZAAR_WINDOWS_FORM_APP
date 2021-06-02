using FormDesktopApp.Objects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormDesktopApp.Objects.Schedule
{
    class Schedule
    {
        private int scheduleID;
        private DateTime scheduleDay;
        private Shift shiftName;
        private int workingEmployeeNumber;
        private int requiredEmployeeNumber;

        public Schedule()
        {
        }

        public Schedule(DateTime scheduleDay, Shift shiftName, int requiredEmployeeNumber)
        {
            this.scheduleDay = scheduleDay;
            this.shiftName = shiftName;
            this.workingEmployeeNumber = 0;
            this.requiredEmployeeNumber = requiredEmployeeNumber;
        }

        public int ScheduleID { get => scheduleID; set => scheduleID = value; }
        public DateTime ScheduleDay { get => scheduleDay; set => scheduleDay = value; }
        public int WorkingEmployeeNumber { get => workingEmployeeNumber; set => workingEmployeeNumber = value; }
        public int RequiredEmployeeNumber { get => requiredEmployeeNumber; set => requiredEmployeeNumber = value; }
        internal Shift ShiftName { get => shiftName; set => shiftName = value; }
    }
}
