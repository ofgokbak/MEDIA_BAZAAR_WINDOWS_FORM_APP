using System;
using System.Collections.Generic;
using System.ComponentModel;
using FormDesktopApp.Objects.Products;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormDesktopApp.ManagerGUI
{
    public partial class MProductInfoGUI : Form
    {

        Product p;

        public MProductInfoGUI(Product p)
        {
            InitializeComponent();
            this.p = p;
            lblId.Text += $"{p.Product_id}";
            lblName.Text += $"{p.Product_name}";
            lblCostPrice.Text += $"{p.Cost_price}";
            lblSellPrice.Text += $"{p.Sell_price}";
            lblDepartment.Text += $"{p.Department_id}";
        }

        private void MProductInfoGUI_Load(object sender, EventArgs e)
        {

        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
