using FormDesktopApp.AdminGUI;
using FormDesktopApp.Functionality.Administration;
using FormDesktopApp.ManagerGUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FormDesktopApp.ShopGUI;

namespace FormDesktopApp
{
    public partial class LoginGUI : Form
    {
        AppSystem system = new AppSystem();
        public LoginGUI()
        {
            InitializeComponent();
        }

        private void LoginGUI_Load(object sender, EventArgs e)
        {

        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tbusername_Click(object sender, EventArgs e)
        {
            tbusername.Clear();
            userline.BackColor = Color.FromArgb(215, 16, 82);
            passline.BackColor = Color.FromArgb(48, 95, 147);
        }

        private void tbpassword_Click(object sender, EventArgs e)
        {
            tbpassword.Clear();
            tbpassword.PasswordChar = '*';
            passline.BackColor = Color.FromArgb(215, 16, 82);
            userline.BackColor = Color.FromArgb(48, 95, 147);
        }

        private void btnwebsite_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://i441262.hera.fhict.nl/Website/login.php");
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            
            if(string.IsNullOrEmpty(tbusername.Text) || string.IsNullOrEmpty(tbpassword.Text) || tbusername.Text == "Username" || tbpassword.Text == "Password")
            {
                MessageBox.Show("Please fill the fields!");
            }else
            {
                try
                {
                    var person = system.GetLogin(tbusername.Text, tbpassword.Text);
                    if (person != null)
                    {
                        switch (person.UserType)
                        {
                            case Objects.Enums.Authorization.ADMIN:
                                AdminEmployeeManagementGUI pg = new AdminEmployeeManagementGUI(person);
                                pg.Show();
                                this.Hide();
                                break;
                            case Objects.Enums.Authorization.MANAGER:
                                ManagerEmployeesGUI managerPg = new ManagerEmployeesGUI(person);
                                managerPg.Show();
                                this.Hide();
                                break;
                            case Objects.Enums.Authorization.EMPLOYEE:
                                ShopForm shopPg = new ShopForm(person);
                                shopPg.Show();
                                Hide();
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Your details are wrong!");
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Please connect to CISCO VPN");
                }
               
               
            }
            
        }
    }
}
