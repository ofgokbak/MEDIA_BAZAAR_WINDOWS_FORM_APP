using System.Collections.Generic;
using FormDesktopApp.Objects.Shop;

namespace FormDesktopApp.Functionality.ManagementClasses
{
    class ShopManagement
    {
        #region Fields
        private Shop shop = new Shop();
        //private Task productsTask;
        #endregion


        #region Methods
        //?Return Product List to be implemented in ListView
        public List<ShopProduct> ReturnShopProducts()
        {
            //productsTask.Wait();
            sortList(shop.ReturnProducts());
            return shop.ReturnProducts();
        }
        //?Return Cart Items
        public List<ShopProduct> ReturnCartProducts()
        {
            return shop.ReturnCartProducts();
        }
        //?Add Item to Cart from UI to Back-End
        public void AddItemToCart(ShopProduct product, int quantity)
        {
            if (quantity >= 1)
            {
                shop.AddProductToCart(product, quantity);
            }
        }
        //?Edit Quantity of Item
        public void EditItemQuantity(ShopProduct product, int quantity)
        {
            if (quantity >= 1)
            {
                shop.EditProductInCart(product, quantity);
            }
        }
        //?Remove Item from Cart
        public void RemoveItem(ShopProduct product)
        {
            shop.removeItem(product);
        }
        //?Clear Cart
        public void ClearCart()
        {
            shop.ClearCart();
        }
        //?Create Receipt from Items in Cart
        public string CreateReceipt()
        {
            return shop.CreateReceipt();
        }
        //?Search for Items
        public List<ShopProduct> SearchProducts(string value)
        {
            return shop.SearchForItems(value);
        }
        //?Buy Products / Accept Order
        public void BuyItems()
        {
            shop.BuyItems();
        }
        //?Return Current Stock of Product Items
        public decimal ReturnCurrentStock(ShopProduct item)
        {
            return shop.ReturnCurrentStock(item);
        }
        //?Return Items in Cart and Total Price
        public void ReturnCartInfo(out int items, out double price)
        {
            shop.ReturnCartInfo(out items, out price);
        }

        #region Private Methods
        //?Sort Product List
        private void sortList(List<ShopProduct> list)
        {
            //productsTask.Wait();
            list.Sort((x, y) =>
            {
                var ret = string.CompareOrdinal(x.Department, y.Department);
                return ret != 0 ? ret : string.CompareOrdinal(x.Name, y.Name);
            });
        }
        #endregion

        #endregion
    }
}
