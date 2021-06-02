using System;
using System.Collections.Generic;
using FormDesktopApp.Objects.Enums;

namespace FormDesktopApp.Objects.Persons
{
     public class Employee : Person
    {
        #region Fields

        private int _employeeId;

        private double _hourlyWage;
        private List<Role> _roleList = new List<Role>();
        private Contract _contract;
        private int _workHours;
        private int _weekHours;
        private int _monthHours;
        private int _yearHours;
        private List<string> freedays = new List<string>();

        private bool _nightShift;
        private bool _status;

        public int EmployeeId { get => _employeeId; set => _employeeId = value; }
        public double HourlyWage { get => _hourlyWage; set => _hourlyWage = value; }
        internal List<Role> RoleList { get => _roleList; set => _roleList = value; }
        internal Contract Contract { get => _contract; set => _contract = value; }
        public int WorkHours { get => _workHours; set => _workHours = value; }
        public int WeekHours { get => _weekHours; set => _weekHours = value; }
        public int MonthHours { get => _monthHours; set => _monthHours = value; }
        public int YearHours { get => _yearHours; set => _yearHours = value; }
        public bool NightShift { get => _nightShift; set => _nightShift = value; }
        public bool Status { get => _status; set => _status = value; }
        public List<string> Freedays { get => freedays; set => freedays = value; }
        #endregion

        #region Constructors
        public Employee() { }

        public Employee(int id,int departmentID, string firstName, string familyName, DateTime dateOfBirth, string email, Address address, DateTime firstWorkDay, string phoneNumber, string gender, Authorization userType, string userName, string password, int hourlyWage, List<Role> roles, Contract contract, bool nightShift) :
            base(id,departmentID, firstName, familyName, dateOfBirth, email, address, firstWorkDay, phoneNumber, gender, userType, userName, password)
        {
            EmployeeId = id;
            HourlyWage = hourlyWage;
            RoleList = roles;
            Contract = contract;
            NightShift = nightShift;
        }

        public Employee(int departmentID,string firstname, string familyname, string email, DateTime dob, Address address) :
            base(departmentID, firstname, familyname, email, dob, address)
        {
            _authorization = Authorization.EMPLOYEE;
        }

        protected Employee(Person p, int hourlyWage, Contract contract, bool nightShift) :
            base(p)
        {
            _authorization = Authorization.EMPLOYEE;
            setIntValue(hourlyWage, ref hourlyWage);
            _contract = contract;
            _nightShift = nightShift;
        }

        public Employee(Person p, string uName, string pWord, Authorization authorization, DateTime firstWorkDay) :
            base(p, uName, pWord, authorization, firstWorkDay)
        { _authorization = Authorization.EMPLOYEE; }
        #endregion

        #region Getters & Setters
        #region Getters

        public double GetHourlyWage() { return HourlyWage; }

        public List<Role> GetRoles() { return RoleList; }

        public int GetWorkHours() { return WorkHours; }

        public int GetWekHours() { return WeekHours; }

        public int GetMonthHours() { return MonthHours; }

        #endregion

        #region Setters
        private void setIntValue(int value, ref int item)
        {
            if (value >= 0) { item = value; }
        }

        public void SetContract(Contract contract)
        {
            Contract = contract;
        }

        public void SetHourlyWage(int newWage)
        {
            if (newWage >= 0) { HourlyWage = newWage; }
        }
        #endregion
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"ID:{this.Id} Name: {this.FirstName} {this.FamilyName} - Contract type: {this.Contract}";
        }
        #endregion
    }
}
