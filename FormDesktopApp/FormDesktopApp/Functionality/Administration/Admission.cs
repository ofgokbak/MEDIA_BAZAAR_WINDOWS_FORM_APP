using System;
using System.Collections.Generic;
using FormDesktopApp.Objects;
using FormDesktopApp.Objects.Enums;
using FormDesktopApp.Objects.Persons;

namespace FormDesktopApp.Functionality.Administration
{
    class Admission
    {
        #region Fields
        private readonly EmployeeAdministartion _empAdmission = new EmployeeAdministartion();
        private readonly AdminAdministration _adAdmission = new AdminAdministration();
        private readonly ManagerAdministration _mAddmission = new ManagerAdministration();
        private readonly Schedule _schedule = new Schedule();
        private readonly Records _recordsAdmission = new Records();
        private List<Person> _peopleList = null;
        #endregion

        #region Methods
        //private void CreateEmployee(int id, string firstName, string familyName, DateTime dateOfBirth, string email, Address address, DateTime firstWorkDay, string phoneNumber, string gender, Authorization userType, string userName, string password, int hourlyWage, List<Role> roles, Contract contract, bool nightShift, bool availableForWeekend)
        //{
        //    _empAdmission.CreateEmployee(id, firstName, familyName, dateOfBirth, email, address, firstWorkDay, phoneNumber, gender, userType, userName, password, hourlyWage, roles, contract, nightShift, availableForWeekend);
        //}
        #region CRUD Functionality
        #region Create & Save
        public void SaveCreated(Person person)
        {
            if (person != null) { _recordsAdmission.AddItemToList(person); }
        }
        #endregion
        #region Read
        public List<Person> ReturnPersonList()
        {
            return _recordsAdmission.GetPeopleList();
        }
        #endregion
        #region Update Information
        public void UpdatePersonalInformation(Person person, object item, string value)
        {
            switch (value)
            {
                case "address":
                    if (person.GetType() == typeof(Employee))
                    {
                        _empAdmission.EditAddress(person, (Address)item);
                    }

                    else if (person.GetType() == typeof(Admin))
                    {
                        _adAdmission.EditAddress(person, (Address)item);
                    }
                    else if (person.GetType() == typeof(Manager))
                    {
                        _mAddmission.EditAddress(person, (Address)item);
                    }
                    break;
                case "firstname":
                    if (person.GetType() == typeof(Employee))
                    {
                        _empAdmission.EditFirstName(person, item.ToString());
                    }

                    else if (person.GetType() == typeof(Admin))
                    {
                        _adAdmission.EditFirstName(person, item.ToString());
                    }
                    else if (person.GetType() == typeof(Manager))
                    {
                        _mAddmission.EditFirstName(person, item.ToString());
                    }
                    break;
                case "familyname":
                    if (person.GetType() == typeof(Employee))
                    {
                        _empAdmission.EditFamilyName(person, item.ToString());
                    }

                    else if (person.GetType() == typeof(Admin))
                    {
                        _adAdmission.EditFamilyName(person, item.ToString());
                    }
                    else if (person.GetType() == typeof(Manager))
                    {
                        _mAddmission.EditFamilyName(person, item.ToString());
                    }
                    break;
                case "password":
                    if (person.GetType() == typeof(Employee))
                    {
                        _empAdmission.SetPassword(person, item.ToString());
                    }

                    else if (person.GetType() == typeof(Admin))
                    {
                        _adAdmission.SetPassword(person, item.ToString());
                    }
                    else if (person.GetType() == typeof(Manager))
                    {
                        _mAddmission.SetPassword(person, item.ToString());
                    }
                    break;
            }
        }
        #endregion
        #region Delete Items

        #endregion
        #endregion
        #endregion

        #region Login
        public void Login(string email, string password)
        {
            var index = -1;
            var id = -1;
            var failed = false;
            _peopleList = _recordsAdmission.GetPeopleList();
            for (var i = 0; i < _peopleList.Count; i++)
            {
                if (_peopleList[i].GetEmail().Equals(email))
                {
                    index = i;
                    id = _peopleList[i].GetID();
                    break;
                }
            }

            Person tempPerson = _recordsAdmission.ReturnPerson(index, id);
            if (tempPerson.GetPassWord().Equals(password))
            {
                switch (tempPerson.GetAuthorization())
                {
                    case Authorization.MANAGER:
                        break;
                    case Authorization.ADMIN:
                        break;
                    case Authorization.EMPLOYEE:
                        break;
                }
            }
        }
        #endregion

        #region Schedule Methods
        public bool RunNightChecks(Employee employee, List<int> assignedShifts, Schedule schedule)
        {
            int n = schedule.ScheduleID;
            int maxNumOfShifts = 0;
            if (employee.Contract == Contract._40 || employee.Contract == Contract._0)
            {
                maxNumOfShifts = 5;
            }
            else if(employee.Contract == Contract._32)
            {
                maxNumOfShifts = 4;
            }
            int numOfAssignedShifts = assignedShifts.Count;
            int numOfAvailableShifts = maxNumOfShifts - numOfAssignedShifts;

            if (employee.NightShift && numOfAvailableShifts > 0 && !assignedShifts.Contains(n - 1) && !assignedShifts.Contains(n + 1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RunDayChecks(Employee employee, List<int> assignedShifts, Schedule schedule)
        {
            int n = schedule.ScheduleID;
            int maxNumOfShifts = 0;
            if (employee.Contract == Contract._40 || employee.Contract == Contract._0)
            {
                maxNumOfShifts = 5;
            }
            else if (employee.Contract == Contract._32)
            {
                maxNumOfShifts = 4;
            }
            int numOfAssignedShifts = assignedShifts.Count;
            int numOfAvailableShifts = maxNumOfShifts - numOfAssignedShifts;

            if (numOfAvailableShifts > 0 && !assignedShifts.Contains(n - 1) && !assignedShifts.Contains(n + 1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RunWeekendChecks(Employee employee, Schedule schedule)
        {
            int n = schedule.ScheduleID;
            //foreach (var item in employee.Freedays) // freedays is List of string
            //{
            //    if(item = "TUESDAY")
            //    {
            //        //todo
            //    }
            //}
            //if(n > 15 && employee.Freedays)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            return false;
        }
        #endregion

        #region Private Methods
        #region Employee Methods

        #endregion
        #endregion
    }
}
