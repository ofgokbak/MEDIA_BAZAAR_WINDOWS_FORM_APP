using FormDesktopApp.Functionality.Administration;
using FormDesktopApp.Objects.Enums;
using FormDesktopApp.Objects.Persons;
using FormDesktopApp.Objects.Products;
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
    public partial class ManagerStockGUI : Form
    {
        DataAccess db = new DataAccess();
        Person currentuser = new Person();
        StockStatisticsAdmin ssa = new StockStatisticsAdmin();

        public ManagerStockGUI(Person person)
        {
            InitializeComponent();
            currentuser = person;
            lbUsername.Text = currentuser.FirstName;
            DisplayItems();
            SetStatistics();
        }

        //Changes the GUI after Employees button is clicked
        private void btnEmployees_Click(object sender, EventArgs e)
        {
            ManagerEmployeesGUI pg = new ManagerEmployeesGUI(currentuser);
            pg.Show();
            this.Hide();
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

        //Gets the list of products and displays them in a combobox
        private void DisplayItems()
        {
            lbItems.Items.Clear();
            lbItems.Items.Add($"id\tName");
            foreach (Product p in db.GetProducts())
            {
                string info = $"{p.Product_id}\t{p.Product_name}";
                lbItems.Items.Add(info);
            }
        }

        //Method that runs all the statiscs methods so that the labels get set every time the GUI opens
        private void SetStatistics()
        {
            GetTotalProfit();
            GetHighestProfitItem();
            GetLowestProfitItem();
            GetTotalNumberOfItems();
            GetDates();
            GetMonths();
            GetYears();
            GetAllDepartments();
        }

        //Displays the total profit of the store in a label
        private void GetTotalProfit()
        {
            lblTotProfit.Text = "Total profit:";
            lblTotProfit.Text += $" {ssa.GetTotalProfit()}$";
        }

        //Displays the highes profit product of the store in a label
        private void GetHighestProfitItem()
        {
            if(ssa.GetHighestProfitItem() != null)
            {
                Product highestProfitProduct = ssa.GetHighestProfitItem();
                double profit = highestProfitProduct.Sell_price - highestProfitProduct.Cost_price;
                double roundVal = Math.Round(profit, 2);
                lblHighestProfit.Text += $" {highestProfitProduct.Product_name}: {roundVal}$";
            }
            else
            {
                MessageBox.Show("Stock is empty, could't get statistics");
            }

        }

        //Displays the lowest profit product of the store in a llbel
        private void GetLowestProfitItem()
        {
            if(ssa.GetLowestProfitItem() != null)
            {
                Product lowestProfitProduct = ssa.GetLowestProfitItem();
                double profit = lowestProfitProduct.Sell_price - lowestProfitProduct.Cost_price;
                double roundVal = Math.Round(profit, 2);
                lblLowestProfit.Text += $" {lowestProfitProduct.Product_name}: {roundVal}$";
            }
            else
            {
                MessageBox.Show("Stock is empty, could't get statistics");
            }

        }

        //Displays the total number of items in a label
        private void GetTotalNumberOfItems()
        {
            lblNumberOfItems.Text += $" {ssa.GetTotalNumberOfItems()}";
        }

        //Displays all the dates in a combobox
        private void GetDates()
        {
            cbDates.Items.Clear();
            foreach (DateTime date in ssa.GetDates())
            {
                cbDates.Items.Add(date);
            }
        }

        //Displays all the months in a combobox
        private void GetMonths()
        {
            cbMonths.Items.Clear();
            foreach (DateTime date in ssa.GetMonths())
            {
                cbMonths.Items.Add(date);
            }
        }

        //Displays all the years in a combobox
        private void GetYears()
        {
            cbYears.Items.Clear();
            foreach(DateTime date in ssa.GetYears())
            {
                cbYears.Items.Add(date);
            }
        }

        //Displays the total profit of the month based on the user input
        private void GetTotalMontProfit()
        {
            lblTotalMonthProfit.Text = "Total profit:";
            if (cbDepartmentsMonth.Text != "All" && cbMonths.SelectedItem != null)
            {
                DateTime date = (DateTime)cbMonths.SelectedItem;
                Department d = (Department)cbDepartmentsMonth.SelectedItem;
                lblTotalMonthProfit.Text += $" {ssa.GetTotalMonthProfitForOneDepartment(date, d)}$";
            }
            else if (cbMonths.SelectedItem != null)
            {
                DateTime date = (DateTime)cbMonths.SelectedItem;
                lblTotalMonthProfit.Text += $" {ssa.GetTotalMontProfitForAllDepartments(date)}$";
            }

        }

        //Displays the total profit of the year based on the user input
        private void GetTotalYearProfit()
        {
            lblTotalYearProfit.Text = "Total profit:";
            if (cbDepartmentsYear.Text != "All" && cbYears.SelectedItem != null)
            {
                DateTime date = (DateTime)cbYears.SelectedItem;
                Department d = (Department)cbDepartmentsYear.SelectedItem;
                lblTotalYearProfit.Text += $" {ssa.GetTotalYearProfitForOneDepartment(date,d)}$";
            }
            else if (cbYears.SelectedItem != null)
            {
                DateTime date = (DateTime)cbYears.SelectedItem;
                lblTotalYearProfit.Text += $" {ssa.GetTotalYearProfitForAllDepartments(date)}$";
            }

        }

        //Displays the total profit of the day based on the user input
        private void GetTotalDayProfit()
        {
            lblTotalProfit.Text = "Total profit:";
            if(cbDepartmentsDay.Text != "All" && cbDates.SelectedItem != null)  
            {
                Department d = (Department)cbDepartmentsDay.SelectedItem;
                DateTime date = (DateTime)cbDates.SelectedItem;
                lblTotalProfit.Text += $" {ssa.GetTotalDayProfitForOneDepartment(date,d)}$";
            }
            else if (cbDates.SelectedItem != null)
            {
                DateTime date = (DateTime)cbDates.SelectedItem;
                lblTotalProfit.Text += $" {ssa.GetTotalDayProfitForAllDepartemnts(date)}$";
            }

        }

        //Runs the GetTotalDayProfit method every time the date input is changed
        private void cbDates_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTotalDayProfit();
        }

        //Runs the GetTotalMontProfit method every time the month input is changed
        private void cbMonths_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTotalMontProfit();
        }

        //Runs the GetTotalYearProfit method every time the year input is changed
        private void cbYears_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTotalYearProfit();
        }

        //Displays all the departments in the comboboxes
        private void GetAllDepartments()
        {
            cbDepartmentsDay.Items.Clear();
            cbDepartmentsMonth.Items.Clear();
            cbDepartmentsYear.Items.Clear();
            cbDepartmentsDay.Items.Add("All");
            cbDepartmentsMonth.Items.Add("All");
            cbDepartmentsYear.Items.Add("All");
            foreach (Department d in db.GetDepartments())
            {
                cbDepartmentsDay.Items.Add(d);
                cbDepartmentsMonth.Items.Add(d);
                cbDepartmentsYear.Items.Add(d);
            }
        }

        //Runs the GetTotalDayProfit method every time the department input is changed
        private void cbDepartmentsDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTotalDayProfit();
        }

        //Runs the GetTotalMontProfit method every time the department input is changed
        private void cbDepartmentsMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTotalMontProfit();
        }

        //Runs the GetTotalYearProfit method every time the department input is changed
        private void cbDepartmentsYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTotalYearProfit();
        }

        private void btnShowInfo_Click(object sender, EventArgs e)
        {
            int i = lbItems.SelectedIndex;
            if (i - 1 >= 0)
            {
                Product selectedProduct = db.GetProducts().ElementAt(i - 1);
                MProductInfoGUI infoGUI = new MProductInfoGUI(selectedProduct);
                infoGUI.Show();
            }
        }
    }
}
