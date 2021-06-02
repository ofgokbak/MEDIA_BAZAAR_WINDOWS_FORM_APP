using System.Collections.Generic;
using FormDesktopApp.Objects.Persons;

namespace FormDesktopApp.Functionality.Administration
{
    class Records
    {
        #region Fields
        private List<Person> personList = new List<Person>();
        #endregion

        #region Getters & Setters

        #region Getter
        public List<Person> GetPeopleList() { return personList; }

        #endregion

        #region Setter

        #endregion

        #endregion

        #region Method

        public void AddItemToList(Person person)
        {
            if (person != null) { personList.Add(person); }
        }

        public void RemovePersonFromList(Person person)
        {
            int index = -1;
            int min = 0;
            int n = personList.Count;
            int max = n - 1;
            while (min <= max)
            {
                int mid = (min + max) / 2;
                if (person.GetID() > personList[mid].GetID())
                {
                    min = mid + 1;
                }
                else { max = mid - 1; }

                if (personList[mid].GetID() == person.GetID())
                {
                    index = mid;
                }
            }

            if (index >= 0)
            {
                personList.RemoveAt(index);
            }
        }

        public Person ReturnPerson(int index, int id)
        {
            for (var i = 0; i < personList.Count; i++)
            {
                if (personList[index].GetID() == id)
                {
                    return personList[index];
                }
                else if (personList[i].GetID() == id)
                {
                    return personList[i];
                }
            }
            return null;
        }
        #endregion
    }
}
