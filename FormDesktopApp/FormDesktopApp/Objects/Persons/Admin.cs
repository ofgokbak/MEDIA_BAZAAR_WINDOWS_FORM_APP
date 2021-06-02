
using System;
using FormDesktopApp.Objects.Enums;

namespace FormDesktopApp.Objects.Persons
{
    class Admin : Person
    {
        #region Fields
        private double _salary;

        public double Salary { get => _salary; set => _salary = value; }
        #endregion

        #region Constructor
        public Admin(int departmentID, string firstname, string familyname, string email, DateTime dob, Address address) :
            base(departmentID, firstname, familyname, email, dob, address)
        {
            _authorization = Authorization.ADMIN;
        }

        protected Admin(Person p) :
            base(p)
        {
            _authorization = Authorization.ADMIN;
        }

        public Admin(Person p, string uName, string pWord, Authorization authorization, DateTime firstWorkDay) :
            base(p, uName, pWord, authorization, firstWorkDay)
        {
            _authorization = Authorization.ADMIN;
        }

        public Admin()
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
