using System;
using FormDesktopApp.Objects;
using FormDesktopApp.Objects.Enums;
using FormDesktopApp.Objects.Persons;
using System.Collections.Generic;

namespace FormDesktopApp.Functionality.Administration
{
    class EmployeeAdministartion
    {
        #region Fileds
        #endregion

        #region Edit Employee Information
        public void EditFirstName(Person emp, string firstName)
        {
            emp.FirstName = firstName;
        }

        public void EditFamilyName(Person emp, string familyName)
        {
            emp.FamilyName = familyName;
        }

        public void SetPassword(Person emp, string value)
        {
            emp.SetPassword(value);
        }

        public void EditAddress(Person emp, Address address)
        {
            emp.SetAddress(address);
        }

        public void EditContract(Employee emp, Contract contract, int hrWage)
        {
            emp.SetContract(contract);
            emp.SetHourlyWage(hrWage);
        }

        #endregion

        #region General Methods

        private void setStringValue(string value, ref string item)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                item = value;
            }
        }

        //public void CreateEmployee(int id, string firstName, string familyName, DateTime dateOfBirth, string email, Address address, DateTime firstWorkDay, string phoneNumber, string gender, Authorization userType, string userName, string password, int hourlyWage, List<Role> roles, Contract contract, bool nightShift, bool availabeShifts)
        //{
        //    Employee employee = new Employee(id,departmen firstName, familyName, dateOfBirth, email, address, firstWorkDay, phoneNumber, gender, userType, userName, password, hourlyWage, roles, contract, nightShift);
        //}
        #endregion
    }
}
