
using System.Security.AccessControl;

namespace FormDesktopApp.Objects
{
    public class Address
    {
        #region Fields
        private string _street;
        private string _number;
        private string _zipCode;
        private string _city;
        private string _country;
        #endregion

        #region Cosntructors

        public Address(string street, string number, string zipCode, string city, string country)
        {
            setStringvalue(street, ref _street);
            setStringvalue(number, ref _number);
            setStringvalue(zipCode, ref _zipCode);
            setStringvalue(city, ref _city);
            setStringvalue(country, ref _country);
        }
        #endregion

        #region Getters & Setters
        #region Getters

        public string GetStreet() { return _street; }

        public string GetNumber() { return _number; }

        public string GetZipCode() { return _zipCode; }

        public string GetCity() { return _city; }

        public string GetCountry() { return _country; }
        #endregion
        #region Setters

        #endregion
        #endregion

        #region Methods
        private void setStringvalue(string value, ref string item)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                item = value;
            }
        }
        #endregion
    }
}
