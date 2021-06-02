using FormDesktopApp.Functionality.Administration;
using FormDesktopApp.Objects.Enums;
using FormDesktopApp.Objects.Persons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormDesktopApp.AdminGUI
{
    public partial class AdminRemoveEmployeeGUI : Form
    {
        
        AppSystem system = new AppSystem();
        Person currentuser = new Person();
        Person person = new Person();
        DataAccess db = new DataAccess();
        public AdminRemoveEmployeeGUI(Person person)
        {
            InitializeComponent();
            currentuser = person;
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lbaddemployee_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AdminEmployeeManagementGUI pg = new AdminEmployeeManagementGUI(currentuser);
            pg.Show();
            this.Hide();
        }


        private void AdminRemoveEmployeeGUI_Load(object sender, EventArgs e)
        {
            btnEmployee.BackColor = Color.FromArgb(13, 38, 59);
          
            lbUsername.Text = currentuser.FirstName;
            LoadEmployees();
        }

        private void btnremove_Click(object sender, EventArgs e)
        {
            var person = (Person)lbemployee.SelectedItem;
            if (person == null)
            {
                MessageBox.Show("Please Select an Employee");

            }
            else if (person.Id == currentuser.Id)
            {
                MessageBox.Show("You can not delete yourself");
            }
            else if (lbemployee.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an employee to delete");
            }
            else
            {
                DateTime now = DateTime.Now;
                CultureInfo ciCurr = CultureInfo.CurrentCulture;
                int weekNum = ciCurr.Calendar.GetWeekOfYear(now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                DialogResult dialogResult = MessageBox.Show("Are you Sure to Delete the Employee?", "Delete Employee", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    switch (person.UserType)
                    {
                        case Authorization.ADMIN:
                            system.DeleteAdmin(person.Id);
                            break;
                        case Authorization.MANAGER:
                            system.DeleteManager(person.Id);
                            break;
                        case Authorization.EMPLOYEE:
                            db.DeleteEmployee(person.Id, weekNum);
                            break;
                        default:
                            break;
                    }
                    
                    MessageBox.Show("Employee Deleted");
                    LoadEmployees();




                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else
                }
            }
        }



        private void btnschedule_Click(object sender, EventArgs e)
        {
            AdminScheduleGUI pg = new AdminScheduleGUI(currentuser);
            pg.Show();
            this.Hide();
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            AdminEmployeeManagementGUI pg = new AdminEmployeeManagementGUI(currentuser);
            pg.Show();
            this.Hide();
        }

        private void btnoverview_Click(object sender, EventArgs e)
        {
            AdminDepartmentsGUI pg = new AdminDepartmentsGUI(currentuser);
            pg.Show();
            this.Hide();
        }

        private void btnproduct_Click(object sender, EventArgs e)
        {
            AdminProductGUI pg = new AdminProductGUI(currentuser);
            pg.Show();
            this.Hide();
        }

        private void btndepartments_Click(object sender, EventArgs e)
        {
            AdminDepartmentsGUI pg = new AdminDepartmentsGUI(currentuser);
            pg.Show();
            this.Hide();
        }

        private void lbLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginGUI pg = new LoginGUI();
            pg.Show();
            this.Hide();
        }

        public void LoadEmployees()
        {
            lbemployee.Items.Clear();
            foreach (var item in db.GetAllEmployees())
            {
                lbemployee.Items.Add(item);
            }
        }

        private void lbemployee_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
