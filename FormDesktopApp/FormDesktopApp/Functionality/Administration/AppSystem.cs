using FormDesktopApp.Objects.Departments;
using FormDesktopApp.Objects.Persons;
using FormDesktopApp.Objects.Products;
using System.Collections.Generic;

namespace FormDesktopApp.Functionality.Administration
{
    public class AppSystem
    {
        DataAccess db = new DataAccess();
        private List<Person> people = new List<Person>();
        private List<Department> departments = new List<Department>();

        public List<Person> People { get => people; set => people = value; }
        public List<Department> Departments { get => departments; set => departments = value; }

        public void GetPeople()
        {
            People = db.GetPeople();
        }

        public Person GetPerson(int id)
        {
            foreach (var item in db.GetPeople())
            {
                if(item.Id == id)
                {
                    return item;
                }
            }

            return null;
        }

        public Person GetPerson(string username)
        {
            foreach (var item in db.GetPeople())
            {
                if (item.UserName == username)
                {
                    return item;
                }
            }

            return null;
        }

        public Employee GetEmployee(int id)
        {
            foreach (var item in db.GetAllEmployees())
            {
                if (item.Id == id)
                {
                    return item;
                }
            }

            return null;
        }


        public Person GetLogin(string username, string password)
        {
            GetPeople();
            var person = GetPerson(username);
            if (person == null)
            {
                return null;
            }
            else
            {
                if (person.UserName == username && person.Password == password)
                {
                    
                    return person;
                }
                else
                {
                    return null;
                }
            }

        }

        public void DeleteEmployee(int id,int weekid)
        {
            db.DeleteEmployee(id,weekid);
        }

        public void DeleteAdmin(int id)
        {
            db.DeleteAdmin(id);
        }

        public void DeleteManager(int id)
        {
            db.DeleteManager(id);
        }

        public Product GetProduct(int id)
        {
            foreach (var item in db.GetProducts())
            {
                if (item.Product_id == id)
                {
                    return item;
                }
            }

            return null;
        }

        public Product GetProductByName(string name)
        {
            foreach (var item in db.GetProducts())
            {
                if (item.Product_name == name)
                {
                    return item;
                }
            }

            return null;
        }

        public Department GetDepartment(int id)
        {
            foreach (var item in db.GetDepartments())
            {
                if (item.Department_id == id)
                {
                    return item;
                }
            }

            return null;
        }

        public Department GetDepartmentbyName(string name)
        {
            foreach (var item in db.GetDepartments())
            {
                if (item.Department_name == name)
                {
                    return item;
                }
            }

            return null;
        }

        public List<Department> GetDepartments()
        {
            this.Departments = db.GetDepartments();
            return this.Departments;
        }

        public bool AddProduct(int depid,string name,double cost,double sell,int minquan, int maxquan)
        {
            if (this.GetProductByName(name) != null)
            {
                return false;
            }
            else
            {
                db.AddProduct(depid,name,cost,sell,minquan,maxquan);
                return true;
            }
        }

        public bool AddDepartment(string name)
        {
            if(this.GetDepartmentbyName(name) != null)
            {
                return false;
            }
            else
            {
                db.AddDepartment(name);
                return true;
            }
        }

        public void UpdateDepartment(Department dep, string name)
        {
            departments = db.GetDepartments();
            var index = -1;
            Department dept = null;
            if (dep != null)
            {
                for (var i = 0; i < departments.Count; i++)
                {
                    if (dep.Department_id == departments[i].Department_id)
                    {
                        dept = departments[i];
                        index = i;
                        break;
                    }
                }
            }


            if (!string.IsNullOrWhiteSpace(name) && index >= 0 && dept != null)
            {
                dept.Department_name = name;
                departments[index] = dept;
                db.UpdateDepartment(dept);
            }
        }

        public List<Product> DepartmentProducts(int department_id)
        {
            var products = db.GetProducts().FindAll(x => x.Department_id == department_id);
            return products;
        }
    }
}
