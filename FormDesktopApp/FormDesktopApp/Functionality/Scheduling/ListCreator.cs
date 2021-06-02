using System;
using System.Collections.Generic;
using System.Globalization;
using FormDesktopApp.Functionality.Administration;
using FormDesktopApp.Objects.Departments;
using FormDesktopApp.Objects.Enums;
using FormDesktopApp.Objects.Persons;

namespace FormDesktopApp.Functionality.Scheduling
{
    class ListCreator
    {
        #region Fields

        private readonly DataAccess _dbAccess;
        private List<Employee> employees;
        private readonly Admission _admission;

        private List<Employee> freedaysEmps;
        private List<EmployeeSchedule> employeeSchedule;
        private List<Schedule> scheduleList;

        private CultureInfo curCultInfo = CultureInfo.CurrentCulture;
        #endregion

        #region Constructor
        public ListCreator()
        {
            _dbAccess = new DataAccess();
            employees = _dbAccess.GetAllEmployees();
            employeeSchedule = _dbAccess.GetAssignedEmployeeSchedules();
            _admission = new Admission();
            freedaysEmps = _dbAccess.GetEmployeeFreeDays();
        }
        #endregion

        #region Create Employee Lists
        //Full Restrictions Employees
        #region Free Employees
        public List<Employee> ReturnContractedEmployees(Department dept, DateTime date, Shift shift,Schedule currentShift) //String Shift
        {
            var tempDepartmentEmployeeList = _returnDepartmentEmployees(dept, date, shift, out var selected, out var weekNum);
            
            var counter = tempDepartmentEmployeeList.Count;
            for (var i = 0; i < counter; i++)
            {
                var emp = tempDepartmentEmployeeList[i];
                List<int> empscheduleid = _dbAccess.GetAssignedScheduleIDsForChosenEmployee(emp.Id, weekNum);
                if (emp.Contract == Contract._0)
                {
                    tempDepartmentEmployeeList.RemoveAt(i);
                    CounterReset(ref counter, ref i, tempDepartmentEmployeeList.Count);
                }
                else if (shift == Shift.NIGHT && tempDepartmentEmployeeList[i].NightShift == false) //!Night Shift Check
                {
                    tempDepartmentEmployeeList.RemoveAt(i);
                    CounterReset(ref counter, ref i, tempDepartmentEmployeeList.Count);
                }
                else if (empscheduleid.Contains(currentShift.ScheduleID)) //Assigned Shift Check
                {
                    tempDepartmentEmployeeList.RemoveAt(i);
                        CounterReset(ref counter, ref i, tempDepartmentEmployeeList.Count);
                }
                else if (!_admission.RunDayChecks(emp,
                    _dbAccess.GetAssignedScheduleIDsForChosenEmployee(emp.EmployeeId, weekNum), currentShift)) //Consecutive Shift CHeck
                {
                    tempDepartmentEmployeeList.RemoveAt(i);
                    CounterReset(ref counter, ref i, tempDepartmentEmployeeList.Count);
                }
                else if (freedaysEmps.Exists(x => x.Id == emp.EmployeeId)) //Check to see if its an employee's free day
                {
                    if (emp.Freedays.Contains(date.DayOfWeek.ToString().ToUpper()))
                    {
                        tempDepartmentEmployeeList.RemoveAt(i);
                        CounterReset(ref counter, ref i, tempDepartmentEmployeeList.Count);
                    }
                }
                else if (emp.Contract == Contract._40 && empscheduleid.Count == 5)
                {
                    tempDepartmentEmployeeList.RemoveAt(i);
                        CounterReset(ref counter, ref i, tempDepartmentEmployeeList.Count);
                }
                else if (emp.Contract == Contract._32 && empscheduleid.Count == 4)
                {
                    tempDepartmentEmployeeList.RemoveAt(i);
                    CounterReset(ref counter, ref i, tempDepartmentEmployeeList.Count);
                }
                
            }

            return tempDepartmentEmployeeList;
        }
        #endregion
        //Some of the Restrictions Removed
        #region Forced Employees
        public List<Employee> ReturnForcedContractedEmployees(Department dept, DateTime date, Shift shift, Schedule currentShift)
        {
            var tempDepartmentEmployeeList = _returnDepartmentEmployees(dept, date, shift, out var selected, out var weekNum);
            var counter = tempDepartmentEmployeeList.Count;
            for (var i = 0; i < counter; i++)
            {
                var emp = tempDepartmentEmployeeList[i];
                List<int> empscheduleid = _dbAccess.GetAssignedScheduleIDsForChosenEmployee(emp.Id, weekNum);
                if (emp.Contract == Contract._0)
                {
                    tempDepartmentEmployeeList.RemoveAt(i);
                    CounterReset(ref counter, ref i, tempDepartmentEmployeeList.Count);
                }
                else if (empscheduleid.Contains(currentShift.ScheduleID)) //Assigned Shift Check
                {
                    tempDepartmentEmployeeList.RemoveAt(i);
                    CounterReset(ref counter, ref i, tempDepartmentEmployeeList.Count);
                }
                else if (!_admission.RunDayChecks(emp,
                    _dbAccess.GetAssignedScheduleIDsForChosenEmployee(emp.EmployeeId, weekNum), currentShift)) //Consecutive Shift CHeck
                {
                    tempDepartmentEmployeeList.RemoveAt(i);
                    CounterReset(ref counter, ref i, tempDepartmentEmployeeList.Count);
                }
                else if (emp.Contract == Contract._40 && empscheduleid.Count == 5)
                {
                    tempDepartmentEmployeeList.RemoveAt(i);
                    CounterReset(ref counter, ref i, tempDepartmentEmployeeList.Count);

                }
                else if (emp.Contract == Contract._32 && empscheduleid.Count == 4)
                {
                    tempDepartmentEmployeeList.RemoveAt(i);
                    CounterReset(ref counter, ref i, tempDepartmentEmployeeList.Count);
                }
            }

            return tempDepartmentEmployeeList;
        }
        #endregion
        //Create 0Hr Employee List
        #region Zero Hour Employees
        public List<Employee> ReturnZeroHourEmployees(Department dept, DateTime date, Shift shift, Schedule currentShift)
        {
            var tempDepartmentEmployeeList = _returnDepartmentEmployees(dept, date, shift, out var selected, out var weekNum);

            var counter = tempDepartmentEmployeeList.Count;
           
            for (var i = 0; i < counter; i++)
            {
                var emp = tempDepartmentEmployeeList[i];
                List<int> empscheduleid = _dbAccess.GetAssignedScheduleIDsForChosenEmployee(emp.Id, weekNum);
                if (emp.Contract != Contract._0) //Checks if Employee is 0 Hour
                {
                    tempDepartmentEmployeeList.RemoveAt(i);
                    CounterReset(ref counter, ref i, tempDepartmentEmployeeList.Count);
                }
                else if (empscheduleid.Contains(currentShift.ScheduleID)) //Assigned Shift Check
                {
                    tempDepartmentEmployeeList.RemoveAt(i);
                    CounterReset(ref counter, ref i, tempDepartmentEmployeeList.Count);
                }
                else if (!_admission.RunDayChecks(emp,
                    _dbAccess.GetAssignedScheduleIDsForChosenEmployee(emp.EmployeeId, weekNum), currentShift)) //Consecutive Shift CHeck
                {
                    tempDepartmentEmployeeList.RemoveAt(i);
                    CounterReset(ref counter, ref i, tempDepartmentEmployeeList.Count);
                }
                else if (emp.Contract == Contract._0 && empscheduleid.Count == 5) //Check Shift amount
                {
                    tempDepartmentEmployeeList.RemoveAt(i);
                    CounterReset(ref counter, ref i, tempDepartmentEmployeeList.Count);

                }
            }
            return tempDepartmentEmployeeList;
        }
        #endregion
        //Create Temporary Department Employees List
        #region TempDeptEmpList
        private List<Employee> _returnDepartmentEmployees(Department dept, DateTime date, Shift shift, out Schedule selected, out int weekNum)
        {
            weekNum = curCultInfo.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            scheduleList = _dbAccess.GetSchedulesByWeek(weekNum, dept.Department_id);
            List<Employee> tempDepartmentEmployeeList = new List<Employee>();
            selected = scheduleList.Find(x =>
                (x.ShiftName == shift) && (x.DayName.ToString() == date.DayOfWeek.ToString()));

            foreach (var emp in employees)
            {
                if (emp.DepartmentID == dept.Department_id) //Department Check
                {
                    tempDepartmentEmployeeList.Add(emp);
                }
            }
            return tempDepartmentEmployeeList;
        }

        #endregion
        #endregion

        //Create a Shift List by Week Number
        #region Shift List
        public List<Schedule> ReturnShiftList(Department dept, DateTime date)
        {
            var weekNum = curCultInfo.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            var list = _dbAccess.GetSchedulesByWeek(weekNum, dept.Department_id);
            if (list.Count != 0) return list;
            if(list.Count == 0)
            {
                _dbAccess.AddSchedulesOfNewWeek(weekNum, dept.Department_id, date);
            }
            
            list = _dbAccess.GetSchedulesByWeek(weekNum, dept.Department_id);
            return list;
        }
        #endregion
        //Counter Reset is used for resetting the counter in for loops
        //Further it resets the index to the previous number to not skip over items
        #region Counter Reset
        private static void CounterReset(ref int counter, ref int index, int newCountValue)
        {
            if (counter < 0) throw new ArgumentOutOfRangeException(nameof(counter));
            if (index <= 0)
            {
                index = -1;
            }
            else
            {
                index -= 2;
            }
            counter = newCountValue;
        }
        #endregion

        
    }
}
