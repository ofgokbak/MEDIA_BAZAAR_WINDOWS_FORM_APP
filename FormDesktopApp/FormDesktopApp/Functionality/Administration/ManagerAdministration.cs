using System;
using FormDesktopApp.Objects;
using FormDesktopApp.Objects.Persons;

namespace FormDesktopApp.Functionality.Administration
{
    class ManagerAdministration : iEditor
    {
        public void EditFirstName(Person mgr, string firstName)
        {
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                mgr.FirstName = firstName;
            }
            else
            {
                throw new BlankException("Name can't be blank");
            }
        }

        public void EditFamilyName(Person mgr, string familyName)
        {
            if (!string.IsNullOrWhiteSpace(familyName))
            {
                mgr.FamilyName = familyName;
            }
            else
            {
                throw new BlankException("Family name can't be blank");
            }
        }

        public void EditAddress(Person mgr, Address address)
        {
            if (address != null)
            {
                mgr.Address = address;
            }
            else
            {
                throw new BlankException("Address is unable to be blank");
            }
        }

        public void SetPassword(Person mgr, string password)
        {
            if (!string.IsNullOrWhiteSpace(password))
            {
                mgr.Password = password;
            }
            else
            {
                throw new BlankException("Password can't be blank");
            }
        }
    }
}
