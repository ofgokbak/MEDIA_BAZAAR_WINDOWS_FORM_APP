using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FormDesktopApp.Functionality.ManagementClasses;
using FormDesktopApp.Objects.Persons;
using FormDesktopApp.Objects.Shop;
using static System.Convert;

namespace FormDesktopApp.ShopGUI
{
    public partial class ShopForm : Form
    {

        //TODO Implement exception handling and check for break points
        //TODO Create functionality for returning statistics (Back-End)
        private readonly ShopManagement _shop;
        public ShopForm(Person person)
        {
            InitializeComponent();
            _shop = new ShopManagement();
            updateProductList(_shop.ReturnShopProducts(), LbShopProducts);
            lbUsername.Text = person.FirstName;
            hide();
        }

        #region UI Functionality
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            var value = TxtBxSearch.Text;
            updateProductList(_shop.SearchProducts(value), LbShopProducts);
        }

        private void BtnAddToCart_Click(object sender, EventArgs e)
        {
            var product = (ShopProduct)LbShopProducts.SelectedItem;
            var quantity = ToInt32(NumUDproductQuantity.Text);
            _shop.AddItemToCart(product, quantity);
            updateProductList(_shop.ReturnCartProducts(), LbCart);
            updateProductList(_shop.ReturnShopProducts(), LbShopProducts);
            updateTextBoxes();
            hide();
        }

        private void LbCart_SelectedIndexChanged(object sender, EventArgs e)
        {
            var product = (ShopProduct)LbCart.SelectedItem;
            NumUDcart.Maximum = _shop.ReturnCurrentStock(product);
            NumUDcart.Text = product.Quantity.ToString();
            BtnApply.Visible = true;
            BtnRemove.Visible = true;
        }

        private void LbShopProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            var product = (ShopProduct)LbShopProducts.SelectedItem;
            NumUDproductQuantity.Maximum = _shop.ReturnCurrentStock(product);
            NumUDproductQuantity.Minimum = 1;
            BtnAddToCart.Visible = true;
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            if (ToInt32(NumUDcart.Text) > 0)
            {
                _shop.EditItemQuantity((ShopProduct)LbCart.SelectedItem, ToInt32(NumUDcart.Text));
                updateProductList(_shop.ReturnCartProducts(), LbCart);
                updateTextBoxes();
            }
            hide();
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            _shop.RemoveItem((ShopProduct)LbCart.SelectedItem);
            updateProductList(_shop.ReturnCartProducts(), LbCart);
            updateTextBoxes();
            hide();
        }

        private void BtnReceipt_Click(object sender, EventArgs e)
        {
            TxtBxReceipt.Clear();
            TxtBxReceipt.Text = _shop.CreateReceipt();
            hide();
            BtnPurchase.Visible = true;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            _shop.ClearCart();
            LbCart.Items.Clear();
            clearTextBoxes();
            hide();
        }

        private void BtnPurchase_Click(object sender, EventArgs e)
        {
            _shop.BuyItems();
            TxtBxReceipt.Clear();
            _shop.ClearCart();
            LbCart.Items.Clear();
            updateProductList(_shop.ReturnShopProducts(), LbShopProducts);
            clearTextBoxes();
            hide();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            TxtBxReceipt.Clear();
            hide();
        }

        private void BtnClearSearch_Click(object sender, EventArgs e)
        {
            TxtBxSearch.Clear();
            updateProductList(_shop.ReturnShopProducts(), LbShopProducts);
        }

        private void Btnclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lbLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginGUI pg = new LoginGUI();
            pg.Show();
            Hide();
        }
        #endregion

        #region Methods
        private void updateProductList(List<ShopProduct> list, ListBox listBox)
        {
            listBox.Items.Clear();
            foreach (var item in list)
            {
                listBox.Items.Add(item);
            }
        }

        private void updateTextBoxes()
        {
            _shop.ReturnCartInfo(out var items, out var price);
            TxtBxItems.Text = items.ToString();
            TxtBxTotal.Text = price.ToString("0.##");
        }
        private void clearTextBoxes()
        {
            TxtBxTotal.Clear();
            TxtBxItems.Clear();
        }

        //?Hide Button Items
        private void hide()
        {
            BtnAddToCart.Visible = false;
            BtnApply.Visible = false;
            BtnRemove.Visible = false;
            BtnPurchase.Visible = false;
            LbShopProducts.ClearSelected();
            LbCart.ClearSelected();
            NumUDproductQuantity.Minimum = 0;
            NumUDcart.Minimum = 0;
            NumUDproductQuantity.Text = "0";
            NumUDcart.Text = "0";
        }
        #endregion
    }
}
