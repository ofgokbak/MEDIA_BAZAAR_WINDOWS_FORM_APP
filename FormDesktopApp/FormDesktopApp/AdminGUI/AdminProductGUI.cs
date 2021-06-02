using FormDesktopApp.Functionality.Administration;
using FormDesktopApp.Objects.Departments;
using FormDesktopApp.Objects.Persons;
using FormDesktopApp.Objects.Products;
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
    public partial class AdminProductGUI : Form
    {
        DataAccess db = new DataAccess();
        AppSystem system = new AppSystem();
        Person currentuser = new Person();
        public AdminProductGUI(Person person)
        {
            InitializeComponent();

            lbUsername.Text = person.FirstName;
            currentuser = person;


        }
        private void AdminProductGUI_Load(object sender, EventArgs e)
        {
            btnproduct.BackColor = Color.FromArgb(13, 38, 59);
            cbxproduct.Enabled = false;
            cbxpro.Enabled = false;
            foreach (var item in db.GetDepartments())
            {
                cbxdepartments2.Items.Add(item);
            }
            foreach (var item in db.GetDepartments())
            {
                cbxdep.Items.Add(item);
            }
            foreach (var item in db.GetDepartments())
            {
                cbxdepartments.Items.Add(item);
            }
            tbcostprice2.ReadOnly = true;
            tbsellprice2.ReadOnly = true;
            tbminquantity.ReadOnly = true;
            tbmaxquantity.ReadOnly = true;
            tbcurrentquantity.ReadOnly = true;

            tbcostprice2.BackColor = Color.Silver;
            tbsellprice2.BackColor = Color.Silver;
            tbminquantity.BackColor = Color.Silver;
            tbcurrentquantity.BackColor = Color.Silver;
            tbmaxquantity.BackColor = Color.Silver;


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

        private void btnaddproduct_Click(object sender, EventArgs e)
        {
            double du;
            var d = (Department)cbxdepartments.SelectedItem;
            if (cbxdepartments.SelectedIndex == -1 || string.IsNullOrEmpty(tbcostprice.Text) || string.IsNullOrEmpty(tbsellprice.Text) || string.IsNullOrEmpty(tbproductname.Text))
            {
                MessageBox.Show("Please fill all the fields");
            }
            else if(!double.TryParse(tbcostprice.Text, out du) || !double.TryParse(tbsellprice.Text, out du))
            {
                MessageBox.Show("Please enter a digit!");
            }
            else if(system.AddProduct(d.Department_id, tbproductname.Text, Convert.ToDouble(tbcostprice.Text), Convert.ToDouble(tbsellprice.Text), 5, 50))
            {
                MessageBox.Show("Product Added");
            }
            else
            {
                MessageBox.Show("Product name exists!");
            }
        
            
        }

        private void btnupdateproduct_Click(object sender, EventArgs e)
        {
            double du;
            int i;

            if (cbxdepartments2.SelectedIndex == -1 || cbxproduct.SelectedIndex == -1|| string.IsNullOrEmpty(tbcostprice2.Text) || string.IsNullOrEmpty(tbsellprice2.Text) || string.IsNullOrEmpty(tbminquantity.Text) || string.IsNullOrEmpty(tbmaxquantity.Text) || string.IsNullOrEmpty(tbcurrentquantity.Text))
            {
                MessageBox.Show("Please fill all the fields");
                updateProductInfo();
            }
            else if (!double.TryParse(tbcostprice2.Text, out du) || !double.TryParse(tbsellprice2.Text, out du) || !int.TryParse(tbminquantity.Text, out i) || !int.TryParse(tbmaxquantity.Text, out i) || !int.TryParse(tbcurrentquantity.Text, out i))
            {
                MessageBox.Show("Please enter a digit!");
                updateProductInfo();
            }
            else if (Convert.ToInt32(tbmaxquantity.Text) < Convert.ToInt32(tbcurrentquantity.Text))
            {
                MessageBox.Show("entered number exceeds the max quantity");
                updateProductInfo();
            }
            else if (Convert.ToInt32(tbminquantity.Text) > Convert.ToInt32(tbcurrentquantity.Text))
            {
                MessageBox.Show("entered number deceeds the min quantity");
                updateProductInfo();
            }
            else
            {
                try
                {
                    var p = (Product)cbxproduct.SelectedItem;
                    db.EditProduct(p.Product_id, Convert.ToDouble(tbcostprice2.Text), Convert.ToDouble(tbsellprice2.Text), Convert.ToInt32(tbminquantity.Text), Convert.ToInt32(tbmaxquantity.Text), Convert.ToInt32(tbcurrentquantity.Text));

                    MessageBox.Show("Product Updated");
                    updateProductInfo();
                    Resetfields();



                }
                catch (Exception)
                {

                    MessageBox.Show("Error");
                }
            }
           
           


        }

        private void cbxdepartments2_SelectedIndexChanged(object sender, EventArgs e)
        {

           
            if(cbxdepartments2.SelectedIndex != -1)
            {
                cbxproduct.Enabled = true;
                var d = (Department)cbxdepartments2.SelectedItem;
                cbxproduct.Items.Clear();
                foreach (var item in system.DepartmentProducts(d.Department_id))
                {
                    cbxproduct.Items.Add(item);

                }
               
            }
            

        }

        private void cbxproduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateProductInfo();
        }

        private void btnremoveproduct_Click(object sender, EventArgs e)
        {
            if (cbxdep.SelectedIndex != -1 && cbxpro.SelectedIndex != -1)
            {
                try
                {
                    var p = (Product)cbxpro.SelectedItem;
                    db.DeleteProduct(p.Product_id);
                    MessageBox.Show("Product Removed");
                    cbxpro.Items.Clear();
                    var d = (Department)cbxdep.SelectedItem;
                    foreach (var item in system.DepartmentProducts(d.Department_id))
                    {
                        cbxpro.Items.Add(item);

                    }

                }
                catch (Exception)
                {

                    MessageBox.Show("Error");
                }
            }
            else
            {
                MessageBox.Show("Please select department or product");
            }

        }

        private void cbxdep_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbxdep.SelectedIndex != -1)
            {
                cbxpro.Enabled = true;
                var d = (Department)cbxdep.SelectedItem;
                cbxpro.Items.Clear();
                foreach (var item in system.DepartmentProducts(d.Department_id))
                {
                    cbxpro.Items.Add(item);

                }

            }
        }

        private void cbxpro_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void updateProductInfo()
        {
         
            
            var p = (Product)cbxproduct.SelectedItem;
            p = system.GetProduct(p.Product_id);
            tbcostprice2.ReadOnly = true;
            tbsellprice2.ReadOnly = true;
            tbminquantity.ReadOnly = true;
            tbmaxquantity.ReadOnly = true;
            tbcurrentquantity.ReadOnly = true;

            tbcostprice2.BackColor = Color.Silver;
            tbsellprice2.BackColor = Color.Silver;
            tbminquantity.BackColor = Color.Silver;
            tbcurrentquantity.BackColor = Color.Silver;
            tbmaxquantity.BackColor = Color.Silver;
            checkboxcost.Checked = false;
            checkboxcurrent.Checked = false;
            checkboxmax.Checked = false;
            checkboxmin.Checked = false;
            checkboxsell.Checked = false;

            tbcostprice2.Text = p.Cost_price.ToString();
            tbsellprice2.Text = p.Sell_price.ToString();
            tbminquantity.Text = p.Min_quantity.ToString();
            tbmaxquantity.Text = p.Max_quantity.ToString();
            tbcurrentquantity.Text = p.Current_quantity.ToString();
            if (Convert.ToInt32(tbcurrentquantity.Text) < 10)
            {
                tbcurrentquantity.BackColor = Color.Red;
            }
        }

        public void Resetfields()
        {
            var p = (Product)cbxproduct.SelectedItem;
            p = system.GetProduct(p.Product_id);
            tbcostprice2.Text = p.Cost_price.ToString();
            tbsellprice2.Text = p.Sell_price.ToString();
            tbminquantity.Text = p.Min_quantity.ToString();
            tbmaxquantity.Text = p.Max_quantity.ToString();
            tbcurrentquantity.Text = p.Current_quantity.ToString();
        }

        private void checkboxcost_CheckedChanged(object sender, EventArgs e)
        {
            if (checkboxcost.Checked)
            {
                tbcostprice2.ReadOnly = false;
                tbcostprice2.BackColor = Color.FromArgb(240, 240, 240);
            }
            else
            {
                tbcostprice2.ReadOnly = true;
                tbcostprice2.BackColor = Color.Silver;
            }
         
        }

        private void checkboxsell_CheckedChanged(object sender, EventArgs e)
        {
            if (checkboxsell.Checked)
            {
                tbsellprice2.ReadOnly = false;
                tbsellprice2.BackColor = Color.FromArgb(240, 240, 240);
            }
            else
            {
                tbsellprice2.ReadOnly = true;
                tbsellprice2.BackColor = Color.Silver;
            }
        }

        private void checkboxmin_CheckedChanged(object sender, EventArgs e)
        {
            if (checkboxmin.Checked)
            {
                tbminquantity.ReadOnly = false;
                tbminquantity.BackColor = Color.FromArgb(240, 240, 240);
            }
            else
            {
                tbminquantity.ReadOnly = true;
                tbminquantity.BackColor = Color.Silver;
            }
        }

        private void checkboxmax_CheckedChanged(object sender, EventArgs e)
        {
            if (checkboxmax.Checked)
            {
                tbmaxquantity.ReadOnly = false;
                tbmaxquantity.BackColor = Color.FromArgb(240, 240, 240);
            }
            else
            {
                tbmaxquantity.ReadOnly = true;
                tbmaxquantity.BackColor = Color.Silver;
            }

        }

        private void checkboxcurrent_CheckedChanged(object sender, EventArgs e)
        {
            if (checkboxcurrent.Checked)
            {
                tbcurrentquantity.ReadOnly = false;
                tbcurrentquantity.BackColor = Color.FromArgb(240, 240, 240);
            }
            else
            {
                tbcurrentquantity.ReadOnly = true;
                tbcurrentquantity.BackColor = Color.Silver;
            }
        }

        private void btnproduct_Click(object sender, EventArgs e)
        {
            AdminProductGUI pg = new AdminProductGUI(currentuser);
            pg.Show();
            this.Hide();
        }

        private void btnEmployee_Click(object sender, EventArgs e)
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

        private void btndepartments_Click(object sender, EventArgs e)
        {
            AdminDepartmentsGUI pg = new AdminDepartmentsGUI(currentuser);
            pg.Show();
            this.Hide();
        }

        private void cbdepstock_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var d = (Department)cbdepstock.SelectedItem;
                lbstocks.Items.Clear();
                foreach (var item in system.DepartmentProducts(d.Department_id))
                {
                    lbstocks.Items.Add(item);

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Error");
            }
           
        }

        private void cbdepstock_Click(object sender, EventArgs e)
        {
            cbdepstock.Items.Clear();

            foreach (var item in db.GetDepartments())
            {
                cbdepstock.Items.Add(item);

            }
        }

        private void cbxlowinstock_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                var d = (Department)cbdepstock.SelectedItem;
                lbstocks.Items.Clear();
                if (cbxlowinstock.Checked == true)
                {

                    lbstocks.ForeColor = Color.Red;
                    foreach (var item in system.DepartmentProducts(d.Department_id))
                    {
                        if (item.Current_quantity < 10)
                        {
                            lbstocks.Items.Add(item);

                        }


                    }
                }
                else
                {
                    lbstocks.ForeColor = Color.Black;
                    foreach (var item in system.DepartmentProducts(d.Department_id))
                    {
                        lbstocks.Items.Add(item);

                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Please select a department");
            }
            
         

          
        }
    }
    }

