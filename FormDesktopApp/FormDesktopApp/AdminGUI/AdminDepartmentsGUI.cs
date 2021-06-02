using FormDesktopApp.Functionality.Administration;
using FormDesktopApp.Objects.Departments;
using FormDesktopApp.Objects.Persons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormDesktopApp.AdminGUI
{
    public partial class AdminDepartmentsGUI : Form
    {
        Person currentuser = new Person();
        DataAccess db = new DataAccess();
        AppSystem appSystem = new AppSystem();
        public AdminDepartmentsGUI(Person person)
        {
            InitializeComponent();
            currentuser = person;
            TxtBxDepName.ReadOnly = true;
            BtnUpdate.Visible = false;
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AdminDepartmentsGUI_Load(object sender, EventArgs e)
        {
            //btnoverview.BackColor = Color.FromArgb(13, 38, 59);
            lbUsername.Text = currentuser.FirstName;
            btndepartments.BackColor = Color.FromArgb(13, 38, 59);
        }

   

        private void btnEmployee_Click_1(object sender, EventArgs e)
        {
            AdminEmployeeManagementGUI pg = new AdminEmployeeManagementGUI(currentuser);
            pg.Show();
            this.Hide();
        }

        private void btnschedule_Click(object sender, EventArgs e)
        {
            AdminScheduleGUI pg = new AdminScheduleGUI(currentuser);
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

        private void btnadddepartment_Click(object sender, EventArgs e)
        {
            try
            {
                if (appSystem.AddDepartment(tbdepartmentname.Text))
                {
                    MessageBox.Show("Department Added");
                }
                else
                {
                    MessageBox.Show("Department name exists");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Error");
            }
            
           

           

        }

        private void btnremoveproduct_Click(object sender, EventArgs e)
        {
            if (cbxdep.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a department");
            }
            else
            {
                var d = (Department)cbxdep.SelectedItem;
                db.DeleteDepartment(d.Department_id);
                MessageBox.Show("Department Deleted");
            }
        }

        private void btnclose_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lbLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginGUI pg = new LoginGUI();
            pg.Show();
            this.Hide();
        }

        private void cbxdep_Click(object sender, EventArgs e)
        {
            cbxdep.Items.Clear();
            foreach (var item in db.GetDepartments())
            {
                cbxdep.Items.Add(item);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            var dept = (Department)CmBxDepartmentSelect.SelectedItem;
            var txt = TxtBxDepName.Text;
            if (!string.IsNullOrWhiteSpace(txt))
            {
                appSystem.UpdateDepartment(dept, txt);
                TxtBxDepName.Clear();
                CmBxDepartmentSelect.ResetText();
                BtnUpdate.Visible = false;
                MessageBox.Show("Updated!");
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        private void CmBxDepartmentSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
           

            var dept = (Department)CmBxDepartmentSelect.SelectedItem;
            if (dept != null)
            {
                TxtBxDepName.ReadOnly = false;
                TxtBxDepName.Text = dept.Department_name;
                BtnUpdate.Visible = true;
            }
        }

        private void CmBxDepartmentSelect_Click(object sender, EventArgs e)
        {
            CmBxDepartmentSelect.Items.Clear();
            foreach (var item in db.GetDepartments())
            {
                CmBxDepartmentSelect.Items.Add(item);
            }
        }
    }
}
