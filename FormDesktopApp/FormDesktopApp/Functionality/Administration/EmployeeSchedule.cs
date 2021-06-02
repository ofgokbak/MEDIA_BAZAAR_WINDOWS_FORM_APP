using FormDesktopApp.Objects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormDesktopApp.Functionality.Administration
{
    class EmployeeSchedule
    {
        private int personID;
        private int scheduleID;
        private int scheduleWeek;
        private bool attendance;
        private DateTime date_of_work;
        private Shift shift_name;
        private string Dayname;

        public EmployeeSchedule()
        {
        }

        //public EmployeeSchedule(int personID, int scheduleID, int scheduleWeek)
        //{
        //    this.personID = personID;
        //    this.scheduleID = scheduleID;
        //    this.scheduleWeek = scheduleWeek;
        //}

        public EmployeeSchedule(int personID, int scheduleID, int scheduleWeek, DateTime date_of_work, string dayname1, Shift shift_name)
        {
            PersonID = personID;
            ScheduleID = scheduleID;
            ScheduleWeek = scheduleWeek;
       
            Date_of_work = date_of_work;
            Dayname1 = dayname1;
            Shift_name = shift_name;
        }

        public int PersonID { get => personID; set => personID = value; }
        public int ScheduleID { get => scheduleID; set => scheduleID = value; }
        public int ScheduleWeek { get => scheduleWeek; set => scheduleWeek = value; }
        public bool Attendance { get => attendance; set => attendance = value; }
        public DateTime Date_of_work { get => date_of_work; set => date_of_work = value; }
        public string Dayname1 { get => Dayname; set => Dayname = value; }
        internal Shift Shift_name { get => shift_name; set => shift_name = value; }
    }
}
