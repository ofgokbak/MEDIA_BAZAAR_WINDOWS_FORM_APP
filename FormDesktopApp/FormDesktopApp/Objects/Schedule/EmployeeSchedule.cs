using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormDesktopApp.Objects.Schedule
{
    class EmployeeSchedule
    {
        private int personID;
        private int scheduleID;
        private int scheduleWeekInYear;
        private bool attendance;

        public EmployeeSchedule()
        {
        }

        public EmployeeSchedule(int personID, int scheduleID, int scheduleWeekInYear, bool attendance)
        {
            this.personID = personID;
            this.scheduleID = scheduleID;
            this.scheduleWeekInYear = scheduleWeekInYear;
            this.attendance = attendance;
        }
    }
}
