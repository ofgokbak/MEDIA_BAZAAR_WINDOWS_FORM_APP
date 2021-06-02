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
    public partial class ManagerStatisticsGUI : Form
    {
        Person user;
        public ManagerStatisticsGUI(Person person)
        {
            InitializeComponent();
            user = person;
        }

        private void btnEmployees_Click(object sender, EventArgs e)
        {
            ManagerEmployeesGUI pg = new ManagerEmployeesGUI(user);
            pg.Show();
            this.Hide();
        }
    }
}
