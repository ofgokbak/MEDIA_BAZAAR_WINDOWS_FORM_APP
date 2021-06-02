using FormDesktopApp.Functionality.Administration;
using FormDesktopApp.Objects.Departments;
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
    public partial class AdminEmployeeManagementGUI : Form
    {
        DataAccess db = new DataAccess();
        Person currentuser = new Person();
        AppSystem system = new AppSystem();
        public AdminEmployeeManagementGUI(Person person)
        {
            InitializeComponent();
            lbUsername.Text = person.FirstName;
            currentuser = person;
        }
        private void AdminEmployeeManagementGUI_Load(object sender, EventArgs e)
        {
            LoadEmployees();
            tbpassword.PasswordChar = '*';
            btnEmployee.BackColor = Color.FromArgb(13, 38, 59);
            //tabControl1.Appearance = TabAppearance.FlatButtons;
            //tabControl1.ItemSize = new Size(0, 1);
            //tabControl1.SizeMode = TabSizeMode.Fixed;
            cbxcontract_type.DataSource = Enum.GetValues(typeof(Contract));
            cbxcontract_type.SelectedIndex = -1;


        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lbLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginGUI pg = new LoginGUI();
            pg.Show();
            this.Hide();

        }


        private void btnschedule_Click(object sender, EventArgs e)
        {
            AdminScheduleGUI pg = new AdminScheduleGUI(currentuser);
            pg.Show();
            this.Hide();
        }

        private void btndepartments_Click(object sender, EventArgs e)
        {
            AdminDepartmentsGUI pg = new AdminDepartmentsGUI(currentuser);
            pg.Show();
            this.Hide();
        }

        private void btnaddemployee_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime birtday = datepickerbirthday.Value;
            int i;

            if (string.IsNullOrEmpty(tbfirstname.Text) || string.IsNullOrEmpty(tblastname.Text) ||
                string.IsNullOrEmpty(tbnumber.Text) || string.IsNullOrEmpty(tbemail.Text) ||
                cbxgender.SelectedIndex == -1 || birtday == now || cbxrole.SelectedIndex == -1 ||
                string.IsNullOrEmpty(tbusername.Text) || string.IsNullOrEmpty(tbpassword.Text) ||
                string.IsNullOrEmpty(tbcity.Text) || string.IsNullOrEmpty(tbcountry.Text) ||
                string.IsNullOrEmpty(tbhousenumber.Text) || string.IsNullOrEmpty(tbstreet.Text) ||
                string.IsNullOrEmpty(tbzipcode.Text) || string.IsNullOrEmpty(tbhourly_wage.Text) || cbxnight_shift.SelectedIndex == -1 || cbxcontract_type.SelectedIndex == -1 || cbxadddepartment.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill all the fields");
            }
            else if (!int.TryParse(tbhousenumber.Text, out i))
            {
                MessageBox.Show("House number should be an integer");
            }
            else
            {
                var department = (Department)cbxadddepartment.SelectedItem;
                Authorization authorization = (Authorization)Enum.Parse(typeof(Authorization), cbxrole.SelectedItem.ToString());
                switch (authorization)
                {
                    case Authorization.ADMIN:


                        db.AddAdmin(department.Department_id, tbfirstname.Text, tblastname.Text, tbnumber.Text, tbemail.Text, cbxgender.Text, birtday, tbusername.Text, tbpassword.Text, Convert.ToDouble(tbhourly_wage.Text), tbcity.Text, tbcountry.Text, Convert.ToInt32(tbhousenumber.Text), tbstreet.Text, tbzipcode.Text, now);
                        MessageBox.Show("Admin Successfully Added");

                        break;
                    case Authorization.MANAGER:


                        db.AddManager(department.Department_id, tbfirstname.Text, tblastname.Text, tbnumber.Text, tbemail.Text, cbxgender.Text, birtday, tbusername.Text, tbpassword.Text, Convert.ToDouble(tbhourly_wage.Text), tbcity.Text, tbcountry.Text, Convert.ToInt32(tbhousenumber.Text), tbstreet.Text, tbzipcode.Text, now);
                        MessageBox.Show("Manager Successfully Added");

                        break;
                    case Authorization.EMPLOYEE:
                        
                            Contract contract = (Contract)Enum.Parse(typeof(Contract), cbxcontract_type.SelectedItem.ToString());
                            db.AddEmployee(tbfirstname.Text, tblastname.Text, department.Department_id, tbnumber.Text, tbemail.Text, cbxgender.Text, birtday, tbusername.Text, tbpassword.Text, contract, Convert.ToBoolean(cbxnight_shift.SelectedIndex), tbcity.Text, tbcountry.Text, Convert.ToInt32(tbhousenumber.Text), tbstreet.Text, tbzipcode.Text, Convert.ToInt32(tbhourly_wage.Text), now);
                            MessageBox.Show("Employee Successfully Added");
                        

                        break;
                    default:
                        break;
                }

            }



        }

        private void lbempinfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlpersonalinfo.Visible = false;
            pnlemployee.Visible = true;
        }

        private void lbpersonalinfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlpersonalinfo.Visible = true;
            pnlemployee.Visible = false;
        }

        private void btneye_MouseHover(object sender, EventArgs e)
        {
            tbpassword.PasswordChar = '\0';
        }

        private void btneye_MouseLeave(object sender, EventArgs e)
        {
            tbpassword.PasswordChar = '*';
        }

        private void lbdeleteemp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AdminRemoveEmployeeGUI pg = new AdminRemoveEmployeeGUI(currentuser);
            pg.Show();
            this.Hide();
        }

     
 

        public void EmptyFields()
        {
            
            tbfirstname.Text = "";
            tblastname.Text = "";
            tbnumber.Text = "";
            tbemail.Text = "";
            cbxgender.SelectedIndex = -1;
            cbxrole.SelectedIndex = -1;
            cbxcontract_type.SelectedIndex = -1;
            cbxnight_shift.SelectedIndex = -1;
            tbusername.Text = "";
            tbpassword.Text = "";
            tbcity.Text = "";
            tbzipcode.Text = "";
            tbcountry.Text = "";
            tbhousenumber.Text = "";
            tbstreet.Text = "";
         


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

        private void btndepartments_Click_1(object sender, EventArgs e)
        {
            AdminDepartmentsGUI pg = new AdminDepartmentsGUI(currentuser);
            pg.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cbxrole.SelectedIndex = 2;
            tbfirstname.Text = "test";
            tblastname.Text = "test";
            tbemail.Text = "test@hotmail.com";
            tbnumber.Text = "0643781259";
            cbxgender.SelectedIndex = 0;
            tbusername.Text = "test";
            tbpassword.Text = "12345678";
            tbcity.Text = "Eindhoven";
            tbcountry.Text = "Netherlands";
            tbhousenumber.Text = 10.ToString();
            tbstreet.Text = "test";
            tbzipcode.Text = "test";
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
        public void LoadEmployees()
        {
            lbemployee.Items.Clear();
            foreach (var item in db.GetAllEmployees())
            {
                lbemployee.Items.Add(item);
            }

            lbupdateemployee.Items.Clear();
            foreach (var item in db.GetAllEmployees())
            {
                lbupdateemployee.Items.Add(item);
            }

            cbxadddepartment.Items.Clear();
            foreach (var item in db.GetDepartments())
            {
                cbxadddepartment.Items.Add(item);
            }
        }

        private void lbupdateemployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            var updateemployee = (Employee)lbupdateemployee.SelectedItem;
            tbupdatewage.Text = updateemployee.HourlyWage.ToString();
            tbupdatephone.Text = updateemployee.PhoneNumber;
            tbupdateemail.Text = updateemployee.Email;
            cbxupdatedepartment.Items.Clear();
            cbxupdatedepartment.Text = system.GetDepartment(updateemployee.DepartmentID).ToString();
            foreach (var item in system.GetDepartments())
            {
                cbxupdatedepartment.Items.Add(item);
            }
            cbxupdatedepartment.Text = system.GetDepartment(updateemployee.DepartmentID).ToString();
            cbxupdatecontact.Text = updateemployee.Contract.ToString();
            Contract c = (Contract)Enum.Parse(typeof(Contract), updateemployee.Contract.ToString());
            if(c == Contract._40)
            {
                cbxupdatecontact.SelectedIndex = 0;
            }else if (c == Contract._32)
            {
                cbxupdatecontact.SelectedIndex = 1;
            }
            else if (c == Contract._0)
            {
                cbxupdatecontact.SelectedIndex = 2;
            }


        }

        private void updateemployee_Click(object sender, EventArgs e)
        {
            try
            {
                if(lbupdateemployee.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select an employee");
                }
                else
                {
                    List<Employee> allemps = db.GetAllEmployees();
                    var updateemployee = (Employee)lbupdateemployee.SelectedItem;
                    if (allemps.Exists(x => (x.Email == tbupdateemail.Text) && (updateemployee.Email != tbupdateemail.Text)))
                    {
                        MessageBox.Show("Email already exists");
                    }
                    else if (allemps.Exists(x => (x.PhoneNumber == tbupdatephone.Text) && (updateemployee.PhoneNumber != tbupdatephone.Text)))
                    {
                        MessageBox.Show("Phone number already exists");
                    }
                    else
                    {

                        db.UpdateEmployeeInfo(updateemployee.Id, ((Department)cbxupdatedepartment.SelectedItem).Department_id, tbupdatephone.Text, tbupdateemail.Text, (Contract)cbxupdatecontact.SelectedIndex, Convert.ToDouble(tbupdatewage.Text));
                        MessageBox.Show("Updated!");
                        allemps = db.GetAllEmployees();
                        lbupdateemployee.Items.Clear();
                        foreach (var item in allemps)
                        {
                            lbupdateemployee.Items.Add(item);
                        }
                    }
                }
              
                
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}
