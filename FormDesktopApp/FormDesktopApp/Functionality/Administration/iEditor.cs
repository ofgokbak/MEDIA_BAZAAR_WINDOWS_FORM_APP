
using FormDesktopApp.Objects;
using FormDesktopApp.Objects.Persons;

namespace FormDesktopApp.Functionality.Administration
{
    interface iEditor
    {
        void EditFirstName(Person person, string firstName);
        void EditFamilyName(Person person, string familyName);
        void EditAddress(Person person, Address address);
        void SetPassword(Person person, string password);
    }
}
