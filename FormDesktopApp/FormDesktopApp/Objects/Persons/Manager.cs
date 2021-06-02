using System;
using FormDesktopApp.Objects.Enums;

namespace FormDesktopApp.Objects.Persons
{
    class Manager : Person
    {
        #region Fields
        private double _salary;

        public double Salary { get => _salary; set => _salary = value; }
        #endregion

        #region Constructor
        public Manager(int departmentID, string firstname, string familyname, string email, DateTime dob, Address address) :
            base(departmentID, firstname, familyname, email, dob, address)
        {
            _authorization = Authorization.MANAGER;
        }

        protected Manager(Person p) :
            base(p)
        {
            _authorization = Authorization.MANAGER;
        }

        public Manager(Person p, string uName, string pWord, Authorization authorization, DateTime firstWorkDay) :
            base(p, uName, pWord, authorization, firstWorkDay)
        {
            _authorization = Authorization.MANAGER;
        }

        public Manager()
        {
        }
        #endregion

        #region Getters & Setters
        #region Getter

        #endregion

        #region Setter

        #endregion
        #endregion

        #region Method

        #endregion
    }
}
