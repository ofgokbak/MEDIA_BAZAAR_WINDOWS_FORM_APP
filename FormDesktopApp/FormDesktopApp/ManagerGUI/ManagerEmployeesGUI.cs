using FormDesktopApp.Functionality.Administration;
using FormDesktopApp.Objects.Enums;
using FormDesktopApp.Objects.Persons;
using FormDesktopApp.Objects.Departments;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormDesktopApp.ManagerGUI
{
    public partial class ManagerEmployeesGUI : Form
    {
        DataAccess db = new DataAccess();
        Person currentuser = new Person();
        EmployeeStatisticsAdmin esa = new EmployeeStatisticsAdmin();

        public ManagerEmployeesGUI(Person person)
        {
            InitializeComponent();
            DisplayEmployees();
            SetStatistics();
            currentuser = person;
            lbUsername.Text = currentuser.FirstName;
        }

        //Closes the application after the Close button is clicked
        private void btnclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Logs out of the application after the Logout button is clicked
        private void lbLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginGUI pg = new LoginGUI();
            pg.Show();
            this.Hide();
        }

        //When switching between tabs you have to display the employee list
        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (tabControl1.SelectedTab == EmployeeList)
            {
                DisplayEmployees();
            }
        }

        //Getting the list of employees and displaying them in a listBox
        private void DisplayEmployees()
        {
            lbEmployees.Items.Clear();
            lbEmployees.Items.Add($"id\tName\tLast Name\tHourly Wage\tContract Type");
            foreach (Employee e in db.GetAllEmployees())
            {
                string info = $"{e.Id}\t{e.FirstName}\t{e.FamilyName}\t\t{e.HourlyWage}$/h\t\tcontract: {e.Contract}";
                lbEmployees.Items.Add(info);
            }
        }

        //Opening the window for a spesific employee after pressing Show Info button
        private void btnShowInfo_Click(object sender, EventArgs e)
        {
            int i = lbEmployees.SelectedIndex;
            if (i-1 >= 0)
            {
                Employee selectedEmp = db.GetAllEmployees().ElementAt(i-1);
                MEmployeeInfoGUI infoGUI = new MEmployeeInfoGUI(selectedEmp);
                infoGUI.Show();
            }
            
        }

        //Method that runs all the statiscs methods so that the labels get set every time the GUI opens
        private void SetStatistics()
        {
            GetAverageSalary();
            GetTotEmp();
            Get40hEmp();
            Get32hEmp();
            Get0hEmp();
            GetMaleEmp();
            GetFemaleEmp();
            GetAllWeeks();
            GetAllDepartments();
        }

        //Gets the average salary of the employees and displays it in a label
        private void GetAverageSalary()
        {
            lblAvgSal.Text += $" {esa.GetAverageSalary()} $/h";
        }

        //Gets the total number of employees and displays it in a label
        private void GetTotEmp()
        {
            lblTotEmp.Text += $" {esa.GetTotEmp()}";
        }

        //Gets the total number of all the 40h contract employees and displays it in a label
        private void Get40hEmp()
        {
            lblNum40hEmp.Text += $" {esa.Get40hEmp()}";
        }

        //Gets the total number of all the 32h contract employees and displays it in a label
        private void Get32hEmp()
        {
            lblNum32hEmp.Text += $" {esa.Get32hEmp()}";
        }

        //Gets the number of all the 0h cotract employees and displays it in a label
        private void Get0hEmp()
        {
            lblNum0hEmp.Text += $" {esa.Get0hEmp()}";
        }

        //Gets the total number of all the male employees and displays it in a label
        private void GetMaleEmp()
        {
            lblMale.Text += $" {esa.GetMaleEmp()}";
        }

        //Gets the total number of all the female employees and displays it in a label
        private void GetFemaleEmp()
        {
            lblFemale.Text += $" {esa.GetFemaleEmp()}";
        }

        //Changes the GUI after Stock button is clicked
        private void btnStock_Click(object sender, EventArgs e)
        {
            ManagerStockGUI pg = new ManagerStockGUI(currentuser);
            pg.Show();
            this.Hide();
        }

        //Displays all the weeks in the combobox
        private void GetAllWeeks()
        {
            cbWeek.Items.Clear();
            foreach(int weekNumber in esa.GetAllWeeks())
            {
                cbWeek.Items.Add(weekNumber);
            }
        }

        //Displays the total amount spent on salary based on the department and date of the user input
        private void GetTotSpentOnSalary()
        {
            if(cbDepartment.Text != "All" && cbWeek.SelectedItem != null)
            {
                int weekNumber = (int)cbWeek.SelectedItem;
                Department department = (Department)cbDepartment.SelectedItem;
                lblTotSpent.Text = "Total spent on employee salary:";
                lblTotSpent.Text += $" {db.GetTotalWageByChosenWeekAndDepartment(weekNumber,department.Department_id)} $";
            }
            else if (cbDepartment.Text == "All" && cbWeek.SelectedItem != null)
            {
                int weekNumber = (int)cbWeek.SelectedItem;
                lblTotSpent.Text = "Total spent on employee salary:";
                lblTotSpent.Text += $" {db.GetTotalWageForChosenWeek(weekNumber)} $";
            }

        }

        //Runs the previous method (GetTotSpentOnSalary) every time the week input from the combobox changes
        private void cbWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTotSpentOnSalary();
        }

        //Displays all the departments in the combobox
        private void GetAllDepartments()
        {
            cbDepartment.Items.Clear();
            cbDepartment.Items.Add("All");
            foreach (Department d in db.GetDepartments())
            {
                cbDepartment.Items.Add(d);
            }
        }

        //Runs the GetTotSpentOnSalary method every time the department input from the combobox changes
        private void cbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTotSpentOnSalary();
        }
    }
}
