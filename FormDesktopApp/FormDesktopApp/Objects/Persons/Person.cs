using System;
using System.Drawing;
using FormDesktopApp.Objects.Enums;

namespace FormDesktopApp.Objects.Persons
{
    public class Person
    {
        #region Fileds

        //Basic Person Fields
        private int _id;
        private string _firstName;
        private string _familyName;
        private string userName;
        private string password;
        private DateTime _dateOfBirth;
        private string _email;
        private Address _address;
        private DateTime _firstWorkDay;
        private string phoneNumber;
        private string gender;
        private Authorization userType;
        private DateTime hireDate;
        private Image _photo;

        //Connectivity fields
        protected string _userName;
        protected string _password;
        protected Authorization _authorization;

        #region Properties
        public int DepartmentID { get; set; }
        public int Id { get => _id; set => _id = value; }
        public string FirstName { get => _firstName; set => _firstName = value; }
        public string FamilyName { get => _familyName; set => _familyName = value; }
        public DateTime DateOfBirth { get => _dateOfBirth; set => _dateOfBirth = value; }
        public string Email { get => _email; set => _email = value; }
        internal Address Address { get => _address; set => _address = value; }
        public DateTime FirstWorkDay { get => _firstWorkDay; set => _firstWorkDay = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string Gender { get => gender; set => gender = value; }
        public Authorization UserType { get => userType; set => userType = value; }
        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
        public DateTime HireDate { get => hireDate; set => hireDate = value; }
        #endregion

        #endregion

        #region Constructor
        public Person(int departmentID, string firstname, string familyname, string email, DateTime dob, Address addrees)
        {
            setStringValue(firstname, FirstName);
            setStringValue(familyname, FamilyName);
            setStringValue(email, Email);
            DepartmentID = departmentID;
            DateOfBirth = dob;
            Address = addrees;
        }
        protected Person(Person p)
        {
            setPerson(p);
        }

        public Person(Person p, string uName, string pWord, Authorization authorization, DateTime firstWorkDay)
        {
            setPerson(p);
            setStringValue(uName, _userName);
            setStringValue(pWord, _password);
            _authorization = authorization;
            FirstWorkDay = firstWorkDay;
        }

        public Person()
        {
        }

        public Person(int id,int departmentID, string firstName, string familyName, DateTime dateOfBirth, string email, Address address, DateTime firstWorkDay, string phoneNumber, string gender, Authorization userType, string userName, string password)
        {
            Id = id;
            DepartmentID = departmentID;
            FirstName = firstName;
            FamilyName = familyName;
            DateOfBirth = dateOfBirth;
            Email = email;
            Address = address;
            FirstWorkDay = firstWorkDay;
            PhoneNumber = phoneNumber;
            Gender = gender;
            UserType = userType;
            UserName = userName;
            Password = password;
        }
        #endregion

        #region Getters & Setters
        #region Getters

        public int GetID() { return Id; }


        public string GetFamilyName() { return FamilyName; }

        public string GetEmail() { return Email; }


        public string GetFirstName() { return FirstName; }        
        public Address GetAddress() { return Address; }

        public DateTime GetDOB() { return DateOfBirth; }

        public DateTime GetFirstWOrkDay() { return FirstWorkDay; }

        public string GetUserName() { return _userName; }

        public string GetPassWord() { return _password; }

        public Authorization GetAuthorization() { return _authorization; }

        #endregion

        #region Setters
        protected void setStringValue(string value, string item)
        {
            if (!string.IsNullOrWhiteSpace(value)) { item = value; }
        }
        private string setStringValue(string value)
        {
            if (!string.IsNullOrWhiteSpace(value)) { return value; }
            else { return null; }
        }
        private void setPerson(Person p)
        {
            setStringValue(p.FirstName, FirstName);
            setStringValue(p.FamilyName, FamilyName);
            setStringValue(p.Email, Email);
            Address = p.Address;
            DateOfBirth = p.DateOfBirth;
        }

        public void SetPassword(string value)
        {
            setStringValue(value, _password);
        }

        public void SetAddress(Address address)
        {
            if (address != null) { Address = address; }
        }

        //public void EditFirstName(string value)
        //{
        //    setStringValue(value, ref FirstName);
        //}

        //public void EditFamilyName(string value)
        //{
        //    setStringValue(value, ref FamilyName);
        //}
        #endregion
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{FirstName} {FamilyName}";
        }
        #endregion
    }
}
