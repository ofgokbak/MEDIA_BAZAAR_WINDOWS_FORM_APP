using FormDesktopApp.Functionality.Administration;
using FormDesktopApp.Objects.Enums;
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

namespace FormDesktopApp.ManagerGUI
{
    public partial class MEmployeeInfoGUI : Form
    {
        Employee e;

        public MEmployeeInfoGUI(Employee e)
        {
            InitializeComponent();
            this.e = e;
            lblId.Text += $" {e.Id}";
            lblName.Text += $" {e.FirstName}";
            lblLastName.Text += $" {e.FamilyName}";
            lblHireDate.Text += $" {e.HireDate}";
            lblDepartment.Text += $" {e.DepartmentID}";
            lblContract.Text += $" {e.Contract}";
            lblWage.Text += $" {e.HourlyWage}$";
            lblHwt.Text += $" {e.WorkHours}h";
            lblHww.Text += $" {e.WeekHours}h";
            lblHwm.Text += $" {e.MonthHours}h";
            lblHwy.Text += $" {e.YearHours}h";
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
