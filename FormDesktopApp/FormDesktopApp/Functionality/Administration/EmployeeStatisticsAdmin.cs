using System;
using System.Collections.Generic;
using FormDesktopApp.Objects.Enums;
using FormDesktopApp.Objects.Persons;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormDesktopApp.Functionality.Administration
{
    class EmployeeStatisticsAdmin
    {
        DataAccess db = new DataAccess();
        private List<Employee> emps;

        public EmployeeStatisticsAdmin()
        {
            //Creates the list of employees from the database
            emps = db.GetAllEmployees();
        }

        //Calculates the average salary
        public double GetAverageSalary()
        {
            int numberOfEmp = 0;
            double salarySum = 0;
            foreach (Employee e in emps)
            {
                salarySum += e.HourlyWage;
            }
            foreach (Employee emp in emps)
            {
                numberOfEmp++;
            }
            double avgSal = salarySum / numberOfEmp;
            double roundedVal = Math.Round(avgSal, 2);
            return roundedVal;
        }

        //Calculates the total number of employees
        public int GetTotEmp()
        {
            int numberOfEmp = 0;
            foreach (Employee emp in db.GetAllEmployees())
            {
                numberOfEmp++;
            }
            return numberOfEmp;
        }

        //Calculates the total number of 40h employees
        public int Get40hEmp()
        {
            int numberOf40hEmp = 0;
            foreach (Employee emp in db.GetAllEmployees())
            {
                if (emp.Contract == Contract._40)
                {
                    numberOf40hEmp++;
                }
            }
            return numberOf40hEmp;
        }

        //Calculates the total number of 32h employees
        public int Get32hEmp()
        {
            int numberOf32hEmp = 0;
            foreach (Employee emp in db.GetAllEmployees())
            {
                if (emp.Contract == Contract._32)
                {
                    numberOf32hEmp++;
                }
            }
            return numberOf32hEmp;
        }

        //Calculates the total number of 0h employees
        public int Get0hEmp()
        {
            int numberOf0hEmp = 0;
            foreach (Employee emp in db.GetAllEmployees())
            {
                if (emp.Contract == Contract._0)
                {
                    numberOf0hEmp++;
                }
            }
            return numberOf0hEmp;
        }

        //Calculates the total number of male employees
        public int GetMaleEmp()
        {
            int numberOfMaleEmp = 0;
            foreach (Employee emp in db.GetAllEmployees())
            {
                if (emp.Gender == "MALE")
                {
                    numberOfMaleEmp++;
                }
            }
            return numberOfMaleEmp;
        }

        //Calculates the total number of female employees
        public int GetFemaleEmp()
        {
            int numberOfFemaleEmp = 0;
            foreach (Employee emp in db.GetAllEmployees())
            {
                if (emp.Gender == "FEMALE")
                {
                    numberOfFemaleEmp++;
                }
            }
            return numberOfFemaleEmp;
        }

        //Gets a list of all the weeks from the list of all employees scheduled for a shift
        public List<int> GetAllWeeks()
        {
            List<int> weeks = new List<int>();
            int weekNumber = 0;
            foreach (EmployeeSchedule es in db.GetAssignedEmployeeSchedulesOrderByWeek())
            {
                if (weekNumber != es.ScheduleWeek)
                {
                    weekNumber = es.ScheduleWeek;
                    weeks.Add(weekNumber);
                }
            }
            return weeks;
        }
    }
}
