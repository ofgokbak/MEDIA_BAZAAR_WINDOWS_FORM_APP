using FormDesktopApp.Objects.Enums;
using FormDesktopApp.Objects.Persons;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using FormDesktopApp.Objects.Products;
using FormDesktopApp.Objects.Departments;
using System.Linq;

namespace FormDesktopApp.Functionality.Administration
{
    class DataAccess
    {


        // Connection for the database with its connectionstring.
        MySqlConnection conn = new MySqlConnection("Server=studmysql01.fhict.local;Uid=dbi296065;Database=dbi296065;Pwd=Admin2020;");

        // Required for execution of SQL statements.
        MySqlCommand command = new MySqlCommand();

        /// <summary>
        /// Checks the connection,whether it is open or not, if not, opens connection.
        /// </summary>
        public void CheckConnection()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        #region Person 
        /// <summary>
        /// Adds new person to the database.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="email"></param>
        /// <param name="gender"></param>
        /// <param name="birthday"></param>
        /// <param name="userName"></param>
        /// <param name="a"></param>
        /// <param name="pwd"></param>
        /// <param name="city"></param>
        /// <param name="country"></param>
        /// <param name="housenumber"></param>
        /// <param name="street"></param>
        /// <param name="zipcode"></param>
        /// <param name="hireDate"></param>
        /// <returns></returns>
        public int AddPerson(int departmentID, string firstName, string lastName, string phoneNumber, string email, string gender, DateTime birthday, string userName, string a, string pwd, string city, string country, int housenumber, string street, string zipcode, DateTime hireDate)
        {
            int personid = 0;
            try
            {

                using (conn)
                {
                    CheckConnection();
                    command = conn.CreateCommand();
                    command.CommandText = "INSERT INTO person (department_id,first_name, last_name, phone, email, gender, birthday, username, password,user_type,hire_date ) VALUES (@department_id, @firstname,@lastname, @phonenumber, @email, @gender, @birthday, @username, @password, @user_type, @hire_date)";
                    command.Parameters.AddWithValue("@username", userName);
                    command.Parameters.AddWithValue("@department_id", departmentID);
                    command.Parameters.AddWithValue("@firstname", firstName);
                    command.Parameters.AddWithValue("@lastname", lastName);
                    command.Parameters.AddWithValue("@password", pwd);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@phonenumber", phoneNumber);
                    command.Parameters.AddWithValue("@birthday", birthday);
                    command.Parameters.AddWithValue("@gender", gender);
                    command.Parameters.AddWithValue("@user_type", a);
                    command.Parameters.AddWithValue("@hire_date", hireDate);
                    command.ExecuteNonQuery();
                    personid = Convert.ToInt32(command.LastInsertedId);
                    SetAdress(personid, city, country, housenumber, street, zipcode);


                }

            }
            catch (Exception)
            {
                throw;
            }
            return personid;
        }

        /// <summary>
        /// Sets chosen person's adress.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="city"></param>
        /// <param name="country"></param>
        /// <param name="housenumber"></param>
        /// <param name="street"></param>
        /// <param name="zipcode"></param>
        public void SetAdress(int id, string city, string country, int housenumber, string street, string zipcode)
        {
            try
            {
                CheckConnection();
                using (conn)
                {
                    command = conn.CreateCommand();
                    command.CommandText = "INSERT INTO address (person_id, city, country, house_number, street, zip_code) VALUES (@id, @city, @country, @houseNumber, @street, @zipcode )";
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@city", city);
                    command.Parameters.AddWithValue("@country", country);
                    command.Parameters.AddWithValue("@houseNumber", housenumber);
                    command.Parameters.AddWithValue("@street", street);
                    command.Parameters.AddWithValue("@zipcode", zipcode);
                    CheckConnection();
                    command.ExecuteNonQuery();
                }

            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Gets all people information fromperson table on the database.
        /// </summary>
        /// <returns></returns>
        public List<Person> GetPeople()
        {
            try
            {
                List<Person> people = new List<Person>();
                command = conn.CreateCommand();
                command.CommandText = "SELECT * FROM person";
                CheckConnection();
                using (conn)
                {

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var person = new Person();
                            person.Id = Convert.ToInt32(reader["person_id"]);
                            person.FirstName = reader["first_name"].ToString();
                            person.FamilyName = reader["last_name"].ToString();
                            person.PhoneNumber = reader["phone"].ToString();
                            person.Email = reader["email"].ToString();
                            person.Gender = reader["gender"].ToString();
                            person.DateOfBirth = Convert.ToDateTime(reader["birthday"]);
                            person.UserName = reader["username"].ToString();
                            person.HireDate = Convert.ToDateTime(reader["hire_date"]);
                            person.Password = reader["password"].ToString();
                            person.UserType = (Authorization)Enum.Parse(typeof(Authorization), reader["user_type"].ToString());

                            people.Add(person);

                        }
                    }
                    return people;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Removes adress by specific id from the database.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteAdress(int id)
        {
            try
            {

                using (conn)
                {


                    command = conn.CreateCommand();
                    command.CommandText = "DELETE FROM address WHERE person_id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    CheckConnection();
                    command.ExecuteNonQuery();


                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Removes person by specific id from the database.
        /// </summary>
        /// <param name="id"></param>
        public void DeletePerson(int id)
        {
            try
            {

                using (conn)
                {


                    command = conn.CreateCommand();
                    command.CommandText = "DELETE FROM person WHERE person_id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    CheckConnection();
                    command.ExecuteNonQuery();


                }

            }
            catch (Exception)
            {
                throw;

            }

        }

        #endregion

        #region Admin 
        /// <summary>
        /// Adds new admin to the database, inherits from person table.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="email"></param>
        /// <param name="gender"></param>
        /// <param name="birthday"></param>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <param name="salary"></param>
        /// <param name="city"></param>
        /// <param name="country"></param>
        /// <param name="housenumber"></param>
        /// <param name="street"></param>
        /// <param name="zipcode"></param>
        /// <param name="hireDate"></param>
        public void AddAdmin(int departmentID, string firstName, string lastName, string phoneNumber, string email, string gender, DateTime birthday, string userName, string pwd, double salary, string city, string country, int housenumber, string street, string zipcode, DateTime hireDate)
        {
            int id;
            try
            {

                id = AddPerson(departmentID, firstName, lastName, phoneNumber, email, gender, birthday, userName, Authorization.ADMIN.ToString(), pwd, city, country, housenumber, street, zipcode, hireDate);
                CheckConnection();
                using (conn)
                {
                    command = conn.CreateCommand();
                    command.CommandText = "INSERT INTO admin (person_id, salary) VALUES (@id, @salary)";
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@salary", salary);
                    command.ExecuteNonQuery();
                }

            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Gets specific admin's information by personID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Admin GetChosenAdmin(int id)
        {
            try
            {
                command = conn.CreateCommand();
                command.CommandText = "SELECT p.person_id, p.first_name, p.last_name, p.phone, p.e-mail, p.gender, p.birthday, p.username, p.password, p.user_type,p.hire_date" +
                    " a.salary" +
                    " FROM people p " +
                      "JOIN admin a a.person_id = p.person_id " +
                      "WHERE p.person_id= '" + id + "'" +
                      " ORDER BY p.person_id";

                CheckConnection();
                using (conn)
                {
                    var admin = new Admin();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            admin.Id = Convert.ToInt32(reader["person_id"]);
                            admin.FirstName = reader["first_name"].ToString();
                            admin.FamilyName = reader["last_name"].ToString();
                            admin.PhoneNumber = reader["phone"].ToString();
                            admin.Email = reader["e-mail"].ToString();
                            admin.Gender = reader["gender"].ToString();
                            admin.DateOfBirth = Convert.ToDateTime(reader["birthday"]);
                            admin.UserName = reader["username"].ToString();
                            admin.Password = reader["password"].ToString();
                            admin.UserType = (Authorization)Enum.Parse(typeof(Authorization), reader["user_type"].ToString());
                            admin.Salary = Convert.ToDouble(reader["salary"]);


                        }
                    }
                    return admin;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets all admins by a list.
        /// </summary>
        /// <returns></returns>
        public List<Admin> GetAllAdmins()
        {
            try
            {
                List<Admin> admins = new List<Admin>();
                command = conn.CreateCommand();
                command.CommandText = "SELECT p.person_id, p.first_name, p.last_name, p.phone, p.e-mail, p.gender, p.birthday, p.username, p.password, p.user_type,p.hire_date" +
                    " a.salary" +
                    " FROM people p " +
                      "JOIN admin a a.person_id = p.person_id " +
                      "ORDER BY p.person_id";
                CheckConnection();
                using (conn)
                {

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var admin = new Admin();
                            admin.Id = Convert.ToInt32(reader["person_id"]);
                            admin.FirstName = reader["first_name"].ToString();
                            admin.FamilyName = reader["last_name"].ToString();
                            admin.PhoneNumber = reader["phone"].ToString();
                            admin.Email = reader["e-mail"].ToString();
                            admin.Gender = reader["gender"].ToString();
                            admin.DateOfBirth = Convert.ToDateTime(reader["birthday"]);
                            admin.UserName = reader["username"].ToString();
                            admin.Password = reader["password"].ToString();
                            admin.UserType = (Authorization)Enum.Parse(typeof(Authorization), reader["user_type"].ToString());
                            admin.Salary = Convert.ToDouble(reader["salary"]);

                            admins.Add(admin);

                        }
                    }
                }
                return admins;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Removes admin by specific id from the database, inherits person table.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteAdmin(int id)
        {
            try
            {
                DeleteAdress(id);
                using (conn)
                {


                    command = conn.CreateCommand();
                    command.CommandText = "DELETE FROM admin WHERE person_id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    CheckConnection();
                    command.ExecuteNonQuery();
                    DeletePerson(id);



                }


            }
            catch (Exception)
            {
                throw;

            }

        }

        #endregion

        #region Manager
        /// <summary>
        /// Adds new manager to the database, ihnerits person table.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="email"></param>
        /// <param name="gender"></param>
        /// <param name="birthday"></param>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <param name="salary"></param>
        /// <param name="city"></param>
        /// <param name="country"></param>
        /// <param name="housenumber"></param>
        /// <param name="street"></param>
        /// <param name="zipcode"></param>
        /// <param name="hireDate"></param>
        public void AddManager(int department_id, string firstName, string lastName, string phoneNumber, string email, string gender, DateTime birthday, string userName, string pwd, double salary, string city, string country, int housenumber, string street, string zipcode, DateTime hireDate)
        {
            int id;
            try
            {


                id = AddPerson(department_id, firstName, lastName, phoneNumber, email, gender, birthday, userName, Authorization.MANAGER.ToString(), pwd, city, country, housenumber, street, zipcode, hireDate);
                CheckConnection();
                using (conn)
                {
                    command = conn.CreateCommand();
                    command.CommandText = "INSERT INTO manager (person_id, salary) VALUES (@id, @salary)";
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@salary", salary);
                    command.ExecuteNonQuery();
                }

            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Gets specific manager by personID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Manager GetChosenManager(int id)
        {
            try
            {
                command = conn.CreateCommand();
                command.CommandText = "SELECT p.person_id, p.first_name, p.last_name, p.phone, p.e-mail, p.gender, p.birthday, p.username, p.password, p.user_type,p.hire_date" +
                    " m.salary" +
                    " FROM people p " +
                      "JOIN manager m m.person_id = p.person_id " +
                      "WHERE p.person_id= '" + id + "'" +
                      " ORDER BY p.person_id";


                CheckConnection();
                using (conn)
                {
                    var manager = new Manager();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            manager.Id = Convert.ToInt32(reader["person_id"]);
                            manager.FirstName = reader["first_name"].ToString();
                            manager.FamilyName = reader["last_name"].ToString();
                            manager.PhoneNumber = reader["phone"].ToString();
                            manager.Email = reader["e-mail"].ToString();
                            manager.Gender = reader["gender"].ToString();
                            manager.DateOfBirth = Convert.ToDateTime(reader["birthday"]);
                            manager.UserName = reader["username"].ToString();
                            manager.Password = reader["password"].ToString();
                            manager.UserType = (Authorization)Enum.Parse(typeof(Authorization), reader["user_type"].ToString());
                            manager.Salary = Convert.ToDouble(reader["salary"]);


                        }
                    }
                    return manager;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets all managers by a list. 
        /// </summary>
        /// <returns></returns>
        public List<Manager> GetAllManagers()
        {
            try
            {
                List<Manager> managers = new List<Manager>();
                command = conn.CreateCommand();
                command.CommandText = "SELECT p.person_id, p.first_name, p.last_name, p.phone, p.e-mail, p.gender, p.birthday, p.username, p.password, p.user_type,p.hire_date" +
                    " m.salary" +
                    " FROM people p " +
                      "JOIN manager m m.person_id = p.person_id " +
                      "ORDER BY p.person_id";
                CheckConnection();
                using (conn)
                {

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var manager = new Manager();
                            manager.Id = Convert.ToInt32(reader["person_id"]);
                            manager.FirstName = reader["first_name"].ToString();
                            manager.FamilyName = reader["last_name"].ToString();
                            manager.PhoneNumber = reader["phone"].ToString();
                            manager.Email = reader["e-mail"].ToString();
                            manager.Gender = reader["gender"].ToString();
                            manager.DateOfBirth = Convert.ToDateTime(reader["birthday"]);
                            manager.UserName = reader["username"].ToString();
                            manager.Password = reader["password"].ToString();
                            manager.UserType = (Authorization)Enum.Parse(typeof(Authorization), reader["user_type"].ToString());
                            manager.Salary = Convert.ToDouble(reader["salary"]);

                            managers.Add(manager);
                        }
                    }
                }
                return managers;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Removes Manager by specific id from the database, inherits person table.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteManager(int id)
        {
            try
            {
                DeleteAdress(id);
                using (conn)
                {


                    command = conn.CreateCommand();
                    command.CommandText = "DELETE FROM manager WHERE person_id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    CheckConnection();
                    command.ExecuteNonQuery();
                    DeletePerson(id);

                }

            }
            catch (Exception)
            {
                throw;

            }

        }

        #endregion

        #region Employee
        /// <summary>
        /// Adds new employee to the database, inherits person table.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="email"></param>
        /// <param name="gender"></param>
        /// <param name="birthday"></param>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <param name="contractType"></param>
        /// <param name="nightShift"></param>
        /// <param name="city"></param>
        /// <param name="country"></param>
        /// <param name="housenumber"></param>
        /// <param name="street"></param>
        /// <param name="zipcode"></param>
        /// <param name="availableForWeekend"></param>
        /// <param name="hourlyWage"></param>
        /// <param name="hireDate"></param>
        public void AddEmployee(string firstName, string lastName, int departmentId, string phoneNumber, string email, string gender, DateTime birthday, string userName, string pwd, Contract contractType, bool nightShift, string city, string country, int housenumber, string street, string zipcode, double hourlyWage, DateTime hireDate)
        {
            try
            {

                int id = 0;
                id = AddPerson(departmentId, firstName, lastName, phoneNumber, email, gender, birthday, userName, Authorization.EMPLOYEE.ToString(), pwd, city, country, housenumber, street, zipcode, hireDate);
                CheckConnection();
                using (conn)
                {
                    command = conn.CreateCommand();
                    command.CommandText = "INSERT INTO employee (person_id, contract_type, night_shift, hourly_wage) VALUES (@id, @contractType, @nightShift, @hourlyWage)";
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@contractType", contractType.ToString());
                    command.Parameters.AddWithValue("@nightShift", nightShift);
                    command.Parameters.AddWithValue("@hourlyWage", hourlyWage);
                    command.ExecuteNonQuery();
                }

            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Gets specific employee by personID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Employee GetChosenEmployee(int id)
        {
            try
            {
                command = conn.CreateCommand();
                command.CommandText = "SELECT p.person_id, p.department_id, p.first_name, p.last_name, p.phone, p.email, p.gender, p.birthday, p.username, p.password, p.user_type," +
                    " e.contract_type,e.night_shift, e.hourly_wage, e.hours_worked_total, e.hours_worked_weekly, e.hours_worked_monthly, e.hours_worked_year," +
                    " p.hire_date, e.status  FROM employee e " +
                      "JOIN person p on e.person_id = p.person_id " +
                       "WHERE e.person_id= '" + id + "' ";

                CheckConnection();
                using (conn)
                {
                    var employee = new Employee();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            employee.Id = Convert.ToInt32(reader["person_id"]);
                            employee.DepartmentID = Convert.ToInt32(reader["department_id"]);
                            employee.FirstName = reader["first_name"].ToString();
                            employee.FamilyName = reader["last_name"].ToString();
                            employee.PhoneNumber = reader["phone"].ToString();
                            employee.Email = reader["email"].ToString();
                            employee.Gender = reader["gender"].ToString();
                            employee.DateOfBirth = Convert.ToDateTime(reader["birthday"]);
                            employee.UserName = reader["username"].ToString();
                            employee.Password = reader["password"].ToString();
                            employee.UserType = (Authorization)Enum.Parse(typeof(Authorization), reader["user_type"].ToString());
                            employee.EmployeeId = Convert.ToInt32(reader["person_id"]);
                            employee.Contract = (Contract)Enum.Parse(typeof(Contract), reader["contract_type"].ToString());
                            employee.NightShift = Convert.ToBoolean(reader["night_shift"]);
                            employee.HourlyWage = Convert.ToDouble(reader["hourly_wage"]);
                            employee.WorkHours = Convert.ToInt32(reader["hours_worked_total"]);
                            employee.WeekHours = Convert.ToInt32(reader["hours_worked_weekly"]);
                            employee.MonthHours = Convert.ToInt32(reader["hours_worked_monthly"]);
                            employee.YearHours = Convert.ToInt32(reader["hours_worked_year"]);
                            employee.HireDate = Convert.ToDateTime(reader["hire_date"]);
                            employee.Status = Convert.ToBoolean(reader["status"]);


                        }
                    }
                    return employee;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///  Gets all employees' by a list
        /// </summary>
        /// <returns></returns>
        public List<Employee> GetAllEmployees()
        {
            try
            {
                List<Employee> employees = new List<Employee>();
                command = conn.CreateCommand();
                command.CommandText = "SELECT p.person_id, p.first_name, p.last_name, p.phone, p.email, p.gender, p.birthday, p.username, p.password, p.user_type, e.person_id," +
                    " e.contract_type,e.night_shift, e.hourly_wage,p.department_id, e.hours_worked_total, e.hours_worked_weekly, e.hours_worked_monthly, e.hours_worked_year," +
                    " p.hire_date, e.status  FROM employee e " +
                      "JOIN person p ON e.person_id = p.person_id " +
                      "ORDER BY e.person_id";
                CheckConnection();
                using (conn)
                {

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var employee = new Employee();
                            employee.Id = Convert.ToInt32(reader["person_id"]);
                            employee.DepartmentID = Convert.ToInt32(reader["department_id"]);
                            employee.FirstName = reader["first_name"].ToString();
                            employee.FamilyName = reader["last_name"].ToString();
                            employee.PhoneNumber = reader["phone"].ToString();
                            employee.Email = reader["email"].ToString();
                            employee.Gender = reader["gender"].ToString();
                            employee.DateOfBirth = Convert.ToDateTime(reader["birthday"]);
                            employee.UserName = reader["username"].ToString();
                            employee.Password = reader["password"].ToString();
                            employee.UserType = (Authorization)Enum.Parse(typeof(Authorization), reader["user_type"].ToString());
                            employee.EmployeeId = Convert.ToInt32(reader["person_id"]);
                            employee.Contract = (Contract)Enum.Parse(typeof(Contract), reader["contract_type"].ToString());
                            employee.NightShift = Convert.ToBoolean(reader["night_shift"]);
                            employee.HourlyWage = Convert.ToDouble(reader["hourly_wage"]);
                            employee.WorkHours = Convert.ToInt32(reader["hours_worked_total"]);
                            employee.WeekHours = Convert.ToInt32(reader["hours_worked_weekly"]);
                            employee.MonthHours = Convert.ToInt32(reader["hours_worked_monthly"]);
                            employee.YearHours = Convert.ToInt32(reader["hours_worked_year"]);
                            employee.HireDate = Convert.ToDateTime(reader["hire_date"]);
                            employee.Status = Convert.ToBoolean(reader["status"]);


                            employees.Add(employee);

                        }
                    }
                    return employees;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Employee> GetEmployeeFreeDays()
        {
            try
            {
                List<Employee> employees = new List<Employee>();
                command = conn.CreateCommand();
                command.CommandText = "SELECT f.first_day, f.second_day, f.third_day,p.person_id, p.first_name, p.last_name, p.phone, p.email, p.gender, p.birthday, p.username, p.password, p.user_type, e.person_id," +
                    " e.contract_type,e.night_shift, e.hourly_wage,p.department_id, e.hours_worked_total, e.hours_worked_weekly, e.hours_worked_monthly, e.hours_worked_year," +
                    " p.hire_date, e.status  FROM employee e " +
                      "JOIN person p ON e.person_id = p.person_id " +
                      "JOIN employee_free_days f ON e.person_id = f.person_id " +
                      "ORDER BY e.person_id";
                CheckConnection();
                using (conn)
                {

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var employee = new Employee();
                            employee.Id = Convert.ToInt32(reader["person_id"]);
                            employee.DepartmentID = Convert.ToInt32(reader["department_id"]);
                            employee.FirstName = reader["first_name"].ToString();
                            employee.FamilyName = reader["last_name"].ToString();
                            employee.PhoneNumber = reader["phone"].ToString();
                            employee.Email = reader["email"].ToString();
                            employee.Gender = reader["gender"].ToString();
                            employee.DateOfBirth = Convert.ToDateTime(reader["birthday"]);
                            employee.UserName = reader["username"].ToString();
                            employee.Password = reader["password"].ToString();
                            employee.UserType = (Authorization)Enum.Parse(typeof(Authorization), reader["user_type"].ToString());
                            employee.EmployeeId = Convert.ToInt32(reader["person_id"]);
                            employee.Contract = (Contract)Enum.Parse(typeof(Contract), reader["contract_type"].ToString());
                            employee.NightShift = Convert.ToBoolean(reader["night_shift"]);
                            employee.HourlyWage = Convert.ToDouble(reader["hourly_wage"]);
                            employee.WorkHours = Convert.ToInt32(reader["hours_worked_total"]);
                            employee.WeekHours = Convert.ToInt32(reader["hours_worked_weekly"]);
                            employee.MonthHours = Convert.ToInt32(reader["hours_worked_monthly"]);
                            employee.YearHours = Convert.ToInt32(reader["hours_worked_year"]);
                            employee.HireDate = Convert.ToDateTime(reader["hire_date"]);
                            employee.Status = Convert.ToBoolean(reader["status"]);
                            employee.Freedays.Add(reader["first_day"].ToString());
                            employee.Freedays.Add(reader["second_day"].ToString());
                            employee.Freedays.Add(reader["third_day"].ToString());


                            employees.Add(employee);

                        }
                    }
                    return employees;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Removes Employee by specific id from the database, inherits person table.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteEmployee(int id, int week)
        {
            try
            {
                DeleteAdress(id);
                foreach (var item in GetAllSchedulesIDsForChosenEmployee(id))
                {
                    DecreaseWorkingEmployeeByScheduleID(item);
                }

                DeleteEmployeeSchedule(id);
                DeleteEmployeeFreeDays(id);
               

                using (conn)
                {
                    command = conn.CreateCommand();
                    command.CommandText = "DELETE FROM employee WHERE person_id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    CheckConnection();
                    command.ExecuteNonQuery();
                    DeletePerson(id);
                }

            }
            catch (Exception)
            {
                throw;

            }

        }

        public void DeleteEmployeeFreeDays(int id)
        {
            try
            {

                using (conn)
                {


                    command = conn.CreateCommand();
                    command.CommandText = "DELETE FROM employee_free_days WHERE person_id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    CheckConnection();
                    command.ExecuteNonQuery();


                }

            }
            catch (Exception)
            {

                throw;
            }
        }



        public void UpdatePersonTableForEmployee(int id, int departmentID, string phone, string email)
        {
            try
            {
                CheckConnection();
                using (conn)
                {
                    command = conn.CreateCommand();
                    command.CommandText = "UPDATE person SET phone = @phone, email = @email, department_id = @department_id WHERE person_id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@department_id", departmentID);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@email", email);
                    command.ExecuteNonQuery();

                }

            }
            catch (Exception)
            {
                throw;
            }

        }


        public void UpdateEmployeeInfo(int id, int departmentID, string phone, string email, Contract contractType, double hourlywage)
        {
            try
            {
                UpdatePersonTableForEmployee(id, departmentID, phone, email);
                CheckConnection();
                using (conn)
                {
                    command = conn.CreateCommand();
                    command.CommandText = "UPDATE employee SET contract_type = @contract_type, hourly_wage = @hourly_wage  WHERE person_id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@contract_type", contractType.ToString());
                    command.Parameters.AddWithValue("@hourly_wage", hourlywage);


                    command.ExecuteNonQuery();

                }

            }
            catch (Exception)
            {
                throw;
            }

        }



        #endregion

        #region Schedule

        public void AddSchedulesOfNewWeek(int weekID, int departmentID, DateTime today)
        {
            int currentDayOfWeek = (int)today.DayOfWeek;
            DateTime sunday = today.AddDays(-currentDayOfWeek);
            DateTime monday = sunday.AddDays(1);
            // If we started on Sunday, we should actually have gone back
            // 6 days instead of forward 1...
            if (currentDayOfWeek == 0)
            {
                monday = monday.AddDays(-7);
            }
            var dates = Enumerable.Range(0, 7).Select(days => monday.AddDays(days)).ToList();

            int indexOfDates = 0;
            string dayName;
            string shiftName;
            int requiredEmployeeNumber;
            List<Schedule> schedules = GetSchedules();
            for (int i = 0; i < 21; i++)
            {
                dayName = schedules[i].DayName.ToString();
                shiftName = schedules[i].ShiftName.ToString();
                requiredEmployeeNumber = schedules[i].RequiredEmployeeNumber;
                if(i % 3 == 0 && i != 0)
                {
                    indexOfDates++;
                }
                DateTime date = dates[indexOfDates];

                using (conn)
                {
                    command = conn.CreateCommand();
                    command.CommandText = "INSERT INTO schedule (day_name, shift_name, required_employee_number, schedule_week, department_id,schedule_date) VALUES ( @day_name, @shift_name, @required_employee_number, @schedule_week, @department_id, @schedule_date)";
                    command.Parameters.AddWithValue("@day_name", dayName);
                    command.Parameters.AddWithValue("@shift_name", shiftName);
                    command.Parameters.AddWithValue("@required_employee_number", requiredEmployeeNumber);
                    command.Parameters.AddWithValue("@schedule_week", weekID);
                    command.Parameters.AddWithValue("@department_id", departmentID);
                    command.Parameters.AddWithValue("@schedule_date", date);
                    CheckConnection();
                    command.ExecuteNonQuery();

                }

            }

        }

        public Schedule GetSchedule(int weeknum, string dayname, Shift shiftname, int departmentID)
        {
            try
            {
                command = conn.CreateCommand();
                command.CommandText = "SELECT * FROM schedule " +

                       "WHERE schedule_week= '" + weeknum + "' AND shift_name= '" + shiftname + "' AND day_name= '" + dayname + "' AND department_id= '" + departmentID + "'";

                CheckConnection();
                using (conn)
                {
                    var schedule = new Schedule();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            schedule.ScheduleID = Convert.ToInt32(reader["schedule_id"]);
                            schedule.Schedule_Week = Convert.ToInt32(reader["schedule_week"]);
                            schedule.DepartmentID = Convert.ToInt32(reader["department_id"]);
                            schedule.ShiftName = (Shift)Enum.Parse(typeof(Shift), reader["shift_name"].ToString());
                            schedule.DayName = (Days)Enum.Parse(typeof(Days), reader["day_name"].ToString());
                            schedule.WorkingEmployeeNumber = Convert.ToInt32(reader["working_employee_number"]);
                            schedule.RequiredEmployeeNumber = Convert.ToInt32(reader["required_employee_number"]);
                            schedule.date = Convert.ToDateTime(reader["schedule_date"]);
                        }
                    }
                    return schedule;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }




        /// <summary>
        /// Edits chosen schedule's number of working employees.
        /// </summary>
        /// <param name="dayName"></param>
        /// <param name="shiftname"></param>
        /// <param name="workingEmployeeNumber"></param>
        public void EditChosenSchedule(string dayName, Shift shiftname, int schedule_week, int departmentID)
        {
            try
            {
                
                CheckConnection();
                using (conn)
                {

                    command = conn.CreateCommand();
                    command.CommandText = "Update schedule SET working_employee_number = working_employee_number + 1 " +
                                          "WHERE day_name = @day_name AND shift_name = @shift_name AND schedule_week = @schedule_week AND department_id =@department_id";

                    command.Parameters.AddWithValue("@day_name", dayName);
                    command.Parameters.AddWithValue("@department_id", departmentID);
                    command.Parameters.AddWithValue("@shift_name", shiftname.ToString());
                    command.Parameters.AddWithValue("@schedule_week", schedule_week);

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DecreaseWorkingEmployeeByScheduleID(int scheduleID)
        {
            try
            {
                CheckConnection();
                using (conn)
                {
                    command = conn.CreateCommand();
                    command.CommandText = "Update schedule SET working_employee_number = working_employee_number - 1 " +
                                          "WHERE schedule_id = @schedule_id";
                    command.Parameters.AddWithValue("@schedule_id", scheduleID);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets all schedules for the week.
        /// </summary>
        /// <returns></returns>
        public List<Schedule> GetSchedules()
        {
            try
            {
                List<Schedule> schedules = new List<Schedule>();
                command = conn.CreateCommand();
                command.CommandText = "SELECT schedule_id, schedule_date, day_name, shift_name, working_employee_number, required_employee_number, schedule_week, department_id " +
                    "FROM schedule " +
                      "ORDER BY schedule_id";
                CheckConnection();
                using (conn)
                {

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var schedule = new Schedule();
                            schedule.ScheduleID = Convert.ToInt32(reader["schedule_id"]);
                            schedule.DepartmentID = Convert.ToInt32(reader["department_id"]);
                            schedule.Schedule_Week = Convert.ToInt32(reader["schedule_week"]);
                            schedule.ShiftName = (Shift)Enum.Parse(typeof(Shift), reader["shift_name"].ToString());
                            schedule.DayName = (Days)Enum.Parse(typeof(Days), reader["day_name"].ToString());
                            schedule.WorkingEmployeeNumber = Convert.ToInt32(reader["working_employee_number"]);
                            schedule.RequiredEmployeeNumber = Convert.ToInt32(reader["required_employee_number"]);
                            schedule.date = Convert.ToDateTime(reader["schedule_date"]);

                            schedules.Add(schedule);

                        }

                    }
                }
                return schedules;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<Schedule> GetSchedulesByWeek(int weekNumber, int departmentID)
        {
            try
            {
                List<Schedule> schedules = new List<Schedule>();
                command = conn.CreateCommand();
                command.CommandText = "SELECT schedule_id, schedule_date, day_name, department_id, shift_name, working_employee_number, required_employee_number, schedule_week " +
                    "FROM schedule WHERE schedule_week = @weeknumber AND department_id = @department_id " +
                      "ORDER BY schedule_id";
                CheckConnection();
                using (conn)
                {
                    command.Parameters.AddWithValue("@weeknumber", weekNumber);
                    command.Parameters.AddWithValue("@department_id", departmentID);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var schedule = new Schedule();
                            schedule.ScheduleID = Convert.ToInt32(reader["schedule_id"]);
                            schedule.DepartmentID = Convert.ToInt32(reader["department_id"]);
                            schedule.Schedule_Week = Convert.ToInt32(reader["schedule_week"]);
                            schedule.ShiftName = (Shift)Enum.Parse(typeof(Shift), reader["shift_name"].ToString());
                            schedule.DayName = (Days)Enum.Parse(typeof(Days), reader["day_name"].ToString());
                            schedule.WorkingEmployeeNumber = Convert.ToInt32(reader["working_employee_number"]);
                            schedule.RequiredEmployeeNumber = Convert.ToInt32(reader["required_employee_number"]);
                            schedule.date = Convert.ToDateTime(reader["schedule_date"]);

                            schedules.Add(schedule);

                        }

                    }
                }
                return schedules;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void EditRequiredEmployeeNumberOfShift(int departmentID, string shiftName, string dayName, int weekNumber, int requiredNumber)
        {
            try
            {
                CheckConnection();
                using (conn)
                {
                    command = conn.CreateCommand();
                    command.CommandText = "UPDATE schedule SET required_employee_number = @required_employee_number WHERE department_id = @department_id AND day_name = @day_name AND shift_name = @shift_name AND schedule_week = @schedule_week";
                    command.Parameters.AddWithValue("@schedule_week", weekNumber);
                    command.Parameters.AddWithValue("@department_id", departmentID);
                    command.Parameters.AddWithValue("@shift_name", shiftName);
                    command.Parameters.AddWithValue("@day_name", dayName);
                    command.Parameters.AddWithValue("@required_employee_number", requiredNumber);
                    command.ExecuteNonQuery();
                }

            }
            catch (Exception)
            {
                throw;
            }

        }
        public int GetScheduleWorkingEmployees(int scheduleID, int departmentID)
        {
            var returnNumber = -1;
            try
            {
                command = conn.CreateCommand();
                command.CommandText = "SELECT working_employee_number FROM schedule WHERE schedule_id = @scheduleID AND department_id = @departmentID";
                CheckConnection();
                using (conn)
                {
                    command.Parameters.AddWithValue("@scheduleID", scheduleID);
                    command.Parameters.AddWithValue("@departmentID", departmentID);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnNumber = Convert.ToInt32(reader["working_employee_number"]);
                        }
                    }
                }
                return returnNumber;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetScheduleRequiredEmployees(int scheduleID, int departmentID)
        {
            var returnNumber = -1;
            try
            {
                command = conn.CreateCommand();
                command.CommandText = "SELECT required_employee_number FROM schedule WHERE schedule_id = @scheduleID AND department_id = @departmentID";
                CheckConnection();
                using (conn)
                {
                    command.Parameters.AddWithValue("@scheduleID", scheduleID);
                    command.Parameters.AddWithValue("@departmentID", departmentID);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            returnNumber = Convert.ToInt32(reader["required_employee_number"]);
                        }
                    }
                }
                return returnNumber;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Employee_Schedule
        /// <summary>
        /// Chosen employee by personID is assigned to specific shift with scheduleID.
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="scheduleId"></param>
        /// <param name="weekOfSchedule"></param>
        /// <param name="dateOfWork"></param>
        public void AssignShiftToEmployee(int personId, int scheduleId, int departmentID, int weekOfSchedule, DateTime dateOfWork, Shift shiftname, string dayname)
        {

            try
            {
                CheckConnection();
                using (conn)
                {
                    EditChosenSchedule(dayname, shiftname, weekOfSchedule, departmentID);

                    command = conn.CreateCommand();
                    command.CommandText = "INSERT INTO employee_schedule (person_id, schedule_id, shift_name, schedule_week, date_of_work) VALUES (@person_id, @schedule_id, @shift_name, @schedule_week, @date_of_work)";
                    command.Parameters.AddWithValue("@person_id", personId);
                    command.Parameters.AddWithValue("@schedule_id", scheduleId);
                    command.Parameters.AddWithValue("@shift_name", shiftname.ToString());
                    command.Parameters.AddWithValue("@schedule_week", weekOfSchedule);
                    command.Parameters.AddWithValue("@date_of_work", dateOfWork);
                    CheckConnection();
                    command.ExecuteNonQuery();
                    UpdateTotalHoursWorked(personId);


                }

            }
            catch (Exception)
            {
                throw;
            }
        }



        public double GetTotalWageForChosenWeek(int weeknumber)
        {
            double totalWage = 0;
            List<EmployeeSchedule> weeklySchedule = GetAssignedEmployeeSchedulesByWeek(weeknumber);
            List<Employee> employees = GetAllEmployees();
            foreach (var item in weeklySchedule)
            {
                Employee employee = employees.Find(x => x.EmployeeId == item.PersonID);
                totalWage += employee.HourlyWage * 8;
            }

            return totalWage;
        }

        public double GetTotalWageByChosenWeekAndDepartment(int weeknumber, int departmentID)
        {
            double totalWage = 0;
            List<EmployeeSchedule> weeklySchedule = GetAssignedEmployeeSchedulesByWeek(weeknumber);
            List<Employee> employees = GetAllEmployees();
            foreach (var item in weeklySchedule)
            {
                Employee employee = employees.Find(x => x.EmployeeId == item.PersonID && x.DepartmentID == departmentID);
                if(employee != null)
                {
                    totalWage += employee.HourlyWage * 8;
                }
                
            }

            return totalWage;
        }

        /// <summary>
        /// Gets employee's schedule ids for chosen week. 
        /// </summary>
        /// <param name="personID"> chosen person's id</param>
        /// <param week="week"> chosen week</param>
        /// <returns></returns>
        public List<int> GetAssignedScheduleIDsForChosenEmployee(int personID, int week)
        {
            try
            {
                List<int> scheduleIds = new List<int>();
                command = conn.CreateCommand();
                command.CommandText = "SELECT  person_id, schedule_id " +
                    "FROM employee_schedule " +
                    "WHERE person_id = '" + personID + "' AND schedule_week = '" + week + "'";

                CheckConnection();
                using (conn)
                {

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            scheduleIds.Add(Convert.ToInt32(reader["schedule_id"]));
                        }
                    }
                }
                return scheduleIds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<int> GetAllSchedulesIDsForChosenEmployee(int personID)
        {
            try
            {
                List<int> scheduleIds = new List<int>();
                command = conn.CreateCommand();
                command.CommandText = "SELECT  schedule_id " +
                    "FROM employee_schedule " +
                    "WHERE person_id = '" + personID + "' ";

                CheckConnection();
                using (conn)
                {

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            scheduleIds.Add(Convert.ToInt32(reader["schedule_id"]));
                        }
                    }
                }
                return scheduleIds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets all schedules for employees as a list.
        /// </summary>
        /// <returns></returns>
        public List<EmployeeSchedule> GetAssignedEmployeeSchedulesOrderByWeek()
        {
            try
            {
                List<EmployeeSchedule> schedules = new List<EmployeeSchedule>();
                command = conn.CreateCommand();
                command.CommandText = "SELECT person_id, schedule_id, schedule_week, attendance, date_of_work " +
                    "FROM employee_schedule " +
                      "ORDER BY schedule_week";
                CheckConnection();
                using (conn)
                {

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var schedule = new EmployeeSchedule();
                            schedule.ScheduleID = Convert.ToInt32(reader["schedule_id"]);
                            schedule.PersonID = Convert.ToInt32(reader["person_id"]);
                            schedule.ScheduleWeek = Convert.ToInt32(reader["schedule_week"]);
                            schedule.Attendance = Convert.ToBoolean(reader["attendance"]);
                            schedule.Date_of_work = Convert.ToDateTime(reader["date_of_work"]);
                            schedules.Add(schedule);

                        }

                    }
                }
                return schedules;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<EmployeeSchedule> GetAssignedEmployeeSchedules()
        {
            try
            {
                List<EmployeeSchedule> schedules = new List<EmployeeSchedule>();
                command = conn.CreateCommand();
                command.CommandText = "SELECT person_id, schedule_id, schedule_week, attendance, date_of_work " +
                    "FROM employee_schedule " +
                      "ORDER BY person_id";
                CheckConnection();
                using (conn)
                {

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var schedule = new EmployeeSchedule();
                            schedule.ScheduleID = Convert.ToInt32(reader["schedule_id"]);
                            schedule.PersonID = Convert.ToInt32(reader["person_id"]);
                            schedule.ScheduleWeek = Convert.ToInt32(reader["schedule_week"]);
                            schedule.Attendance = Convert.ToBoolean(reader["attendance"]);
                            schedule.Date_of_work = Convert.ToDateTime(reader["date_of_work"]);
                            schedules.Add(schedule);

                        }

                    }
                }
                return schedules;
            }
            catch (Exception)
            {

                throw;
            }
        }





        public List<EmployeeSchedule> GetAssignedEmployeeSchedulesByWeek(int weekNumber)
        {
            try
            {
                List<EmployeeSchedule> schedules = new List<EmployeeSchedule>();
                command = conn.CreateCommand();
                command.CommandText = "SELECT person_id, schedule_id, shift_name, schedule_week, attendance, date_of_work " +
                    "FROM employee_schedule WHERE schedule_week = @schedule_week " +
                      "ORDER BY person_id";

                CheckConnection();
                using (conn)
                {
                    command.Parameters.AddWithValue("@schedule_week", weekNumber);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var schedule = new EmployeeSchedule();
                            schedule.ScheduleID = Convert.ToInt32(reader["schedule_id"]);
                            schedule.PersonID = Convert.ToInt32(reader["person_id"]);
                            schedule.ScheduleWeek = Convert.ToInt32(reader["schedule_week"]);
                            schedule.Attendance = Convert.ToBoolean(reader["attendance"]);
                            schedule.Date_of_work = Convert.ToDateTime(reader["date_of_work"]);
                            schedule.Shift_name = (Shift)Enum.Parse(typeof(Shift), reader["shift_name"].ToString());
                            schedules.Add(schedule);

                        }

                    }
                }
                return schedules;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Sets total worked hours of chosen employee
        /// </summary>
        /// <param name="id">chosen employee's personID</param>
        /// <param name="hoursWorkedTotal">total worked hours</param>
        public void UpdateTotalHoursWorked(int id)
        {
            try
            {
                CheckConnection();
                using (conn)
                {
                    command = conn.CreateCommand();
                    command.CommandText = "UPDATE employee SET hours_worked_total = hours_worked_total + 8 WHERE person_id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }

            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Sets worked hours for weekly of chosen employee
        /// </summary>
        /// <param name="id"></param>
        /// <param name="hoursWorkedWeekly"></param>
        public int GetTotalHoursWorkedWeekly(int personId, int weekId)
        {
            List<EmployeeSchedule> employeeSchedules = GetAssignedEmployeeSchedulesByWeek(weekId);
            int count = 0;
            foreach (var item in employeeSchedules)
            {
                if (item.ScheduleWeek == weekId && item.PersonID == personId)
                {
                    count += 8;
                }

            }
            return count;

        }



        /// <summary>
        /// Sets whether employee's status is available to work or not.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        public void SetEmployeeStatus(int id, bool status)
        {
            try
            {
                CheckConnection();
                using (conn)
                {
                    command = conn.CreateCommand();
                    command.CommandText = "UPDATE employee SET status = @status WHERE person_id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@status", status);
                    command.ExecuteNonQuery();
                }

            }
            catch (Exception)
            {
                throw;
            }

        }

        public void DeleteEmployeeSchedule(int id)
        {
            try
            {
                using (conn)
                {
                    command = conn.CreateCommand();
                    command.CommandText = "DELETE FROM employee_schedule WHERE person_id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    CheckConnection();
                    command.ExecuteNonQuery();

                }

            }
            catch (Exception)
            {
                throw;

            }

        }
        public void DeleteEmployeeSchedule2(int id, int schedule_id)
        {
            try
            {
                using (conn)
                {
                    DecreaseWorkingEmployeeByScheduleID(schedule_id);
                    command = conn.CreateCommand();
                    command.CommandText = "DELETE FROM employee_schedule WHERE person_id = @id AND schedule_id = @s_id";
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@s_id", schedule_id);
                    CheckConnection();
                    command.ExecuteNonQuery();

                }

            }
            catch (Exception)
            {
                throw;

            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"> schedule id</param>
        public void DeleteSpecificEmployeeScheduleById(int scheduleID)
        {
            try
            {
                DecreaseWorkingEmployeeByScheduleID(scheduleID);
                using (conn)
                {
                    command = conn.CreateCommand();
                    command.CommandText = "DELETE FROM employee_schedule WHERE schedule_id = @id";
                    command.Parameters.AddWithValue("@id", scheduleID);
                    CheckConnection();
                    command.ExecuteNonQuery();

                }

            }
            catch (Exception)
            {
                throw;

            }

        }

        #endregion

        #region Product

        public void AddProduct(int departmentId, string productName, double costPrice, double sellPrice, int minQantity, int maxQuantity)
        {
            try
            {

                using (conn)
                {
                    CheckConnection();
                    command = conn.CreateCommand();
                    command.CommandText = "INSERT INTO product (department_id, product_name, cost_price, sell_price, min_quantity, max_quantity) " +
                                          "VALUES (@departmentid,@productname, @costprice, @sellprice, @minquantity, @maxquantity)";
                    command.Parameters.AddWithValue("@departmentid", departmentId);
                    command.Parameters.AddWithValue("@productname", productName);
                    command.Parameters.AddWithValue("@costprice", costPrice);
                    command.Parameters.AddWithValue("@sellprice", sellPrice);
                    command.Parameters.AddWithValue("@minquantity", minQantity);
                    command.Parameters.AddWithValue("@maxquantity", maxQuantity);
                    command.ExecuteNonQuery();

                }

            }
            catch (Exception)
            {
                throw;
            }
        }



        public List<Product> GetProducts()
        {
            try
            {
                List<Product> products = new List<Product>();
                command = conn.CreateCommand();
                command.CommandText = "SELECT * FROM product";
                CheckConnection();
                using (conn)
                {

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var product = new Product();
                            product.Product_id = Convert.ToInt32(reader["product_id"]);
                            product.Department_id = Convert.ToInt32(reader["department_id"]);
                            product.Product_name = reader["product_name"].ToString();
                            product.Cost_price = Convert.ToDouble(reader["cost_price"]);
                            product.Sell_price = Convert.ToDouble(reader["sell_price"]);
                            product.Min_quantity = Convert.ToInt32(reader["min_quantity"]);
                            product.Max_quantity = Convert.ToInt32(reader["max_quantity"]);
                            product.Current_quantity = Convert.ToInt32(reader["current_quantity"]);

                            products.Add(product);

                        }
                    }
                    return products;
                }

            }
            catch (Exception)
            {

                throw;
            }

        }




        public void EditProduct(int productId, double costPrice, double sellPrice, int minQuantity, int maxQuantity, int currentQuantity)
        {
            try
            {
                CheckConnection();
                using (conn)
                {

                    command = conn.CreateCommand();
                    command.CommandText = "Update product SET cost_price = @costPrice, sell_price = @sellPrice, current_quantity = @quantity, min_quantity = @minquantity, max_quantity = @maxquantity " +
                                          " WHERE product_id =@productid";

                    command.Parameters.AddWithValue("@productid", productId);
                    command.Parameters.AddWithValue("@costPrice", costPrice);
                    command.Parameters.AddWithValue("@sellPrice", sellPrice);
                    command.Parameters.AddWithValue("@minquantity", minQuantity);
                    command.Parameters.AddWithValue("@maxquantity", maxQuantity);
                    command.Parameters.AddWithValue("@quantity", currentQuantity);


                    command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteProduct(int id)
        {
            try
            {
                using (conn)
                {
                    command = conn.CreateCommand();
                    command.CommandText = "DELETE FROM product WHERE product_id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    CheckConnection();
                    command.ExecuteNonQuery();

                }

            }
            catch (Exception)
            {
                throw;

            }

        }

        public void DeleteProductsOfDepartment(int id)
        {
            try
            {
                using (conn)
                {
                    command = conn.CreateCommand();
                    command.CommandText = "DELETE FROM product WHERE department_id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    CheckConnection();
                    command.ExecuteNonQuery();

                }

            }
            catch (Exception)
            {
                throw;

            }

        }


        public void UpdateProductQuantity(int productID, int quantity)
        {
            try
            {
                CheckConnection();
                using (conn)
                {
                    command = conn.CreateCommand();
                    command.CommandText = "UPDATE product SET current_quantity = @quantity WHERE product_id = @productID";
                    command.Parameters.AddWithValue("@productID", productID);
                    command.Parameters.AddWithValue("@quantity", quantity);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Department

        public void AddDepartment(string departmentName)
        {
            try
            {
                CheckConnection();
                using (conn)
                {
                    command = conn.CreateCommand();
                    command.CommandText = "INSERT INTO department (department_name) VALUES (@name)";
                    command.Parameters.AddWithValue("@name", departmentName);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<Department> GetDepartments()
        {
            try
            {
                List<Department> departments = new List<Department>();
                command = conn.CreateCommand();
                command.CommandText = "SELECT * FROM department";
                CheckConnection();
                using (conn)
                {

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var department = new Department();
                            department.Department_id = Convert.ToInt32(reader["department_id"]);
                            department.Department_name = reader["department_name"].ToString();

                            departments.Add(department);

                        }
                    }
                    return departments;
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        public void DeleteDepartment(int id)
        {
            DeleteProductsOfDepartment(id);
            try
            {
                using (conn)
                {
                    command = conn.CreateCommand();
                    command.CommandText = "DELETE FROM department WHERE department_id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    CheckConnection();
                    command.ExecuteNonQuery();

                }

            }
            catch (Exception)
            {
                throw;

            }

        }

        public void UpdateDepartment(Department dept)
        {
            try
            {
                CheckConnection();
                using (conn)
                {
                    command = conn.CreateCommand();
                    command.CommandText = "UPDATE department SET department_name = @deptName WHERE department_id = @deptID;";
                    command.Parameters.AddWithValue("@deptID", dept.Department_id);
                    command.Parameters.AddWithValue("@deptName", dept.Department_name);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Intersection_Product_Table
        public void AddNewIPT(int productid, int amountSold, DateTime date, double profit)
        {
            try
            {

                using (conn)
                {
                    CheckConnection();
                    command = conn.CreateCommand();
                    command.CommandText = "INSERT INTO intersection_product_table (product_id, amount_sold, date, profit) VALUES (@productid, @amount_sold, @date, @profit)";
                    command.Parameters.AddWithValue("@productid", productid);
                    command.Parameters.AddWithValue("@amount_sold", amountSold);
                    command.Parameters.AddWithValue("@date", date);
                    command.Parameters.AddWithValue("@profit", profit);
                    command.ExecuteNonQuery();

                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<IPT> GetAllIPT()
        {
            try
            {
                List<IPT> allIPT = new List<IPT>();
                command = conn.CreateCommand();
                command.CommandText = "SELECT * FROM intersection_product_table";
                CheckConnection();
                using (conn)
                {

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var ipt = new IPT();
                            ipt.Ipt_id = Convert.ToInt32(reader["ipt_id"]);
                            ipt.Product_id = Convert.ToInt32(reader["product_id"]);
                            ipt.Date = Convert.ToDateTime(reader["date"]);
                            ipt.Profit = Convert.ToDouble(reader["profit"]);
                            ipt.Amount_sold = Convert.ToInt32(reader["amount_sold"]);

                            allIPT.Add(ipt);

                        }
                    }
                    return allIPT;
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        #endregion













    }
}
