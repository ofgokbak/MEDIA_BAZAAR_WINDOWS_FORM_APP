using FormDesktopApp.Objects;
using FormDesktopApp.Objects.Persons;

namespace FormDesktopApp.Functionality.Administration
{
    class AdminAdministration : iEditor
    {
        #region Fields
        #endregion

        #region Methods
        public void EditFirstName(Person admin, string firstName)
        {
            if (CheckString(firstName))
            {
                admin.FirstName = firstName;
            }
            else
            {
                throw new BlankException("Name can't be blank");
            }
        }

        public void EditFamilyName(Person admin, string familyName)
        {
            if (CheckString(familyName))
            {
                admin.FamilyName = familyName;
            }
            else
            {
                throw new BlankException("Family name can't be blank");
            }

        }

        public void EditAddress(Person admin, Address address)
        {
            if (address != null)
            {
                admin.Address = address;
            }
            else
            {
                throw new BlankException("Address is unable to be blank");
            }
        }

        public void SetPassword(Person admin, string password)
        {
            if (CheckString(password))
            {
                admin.SetPassword(password);
            }
            else
            {
                throw new BlankException("Password can't be blank");
            }
        }

        private bool CheckString(string stringToCheck)
        {
            if (stringToCheck != "" || !string.IsNullOrWhiteSpace(stringToCheck))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

    }
}
