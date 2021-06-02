using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FormDesktopApp.Functionality.Administration;
using FormDesktopApp.Objects.Departments;
using FormDesktopApp.Objects.Products;
using static System.Convert;

namespace FormDesktopApp.Objects.Shop
{
    class Shop
    {
        #region Fields
        private List<Product> products;
        private List<ShopProduct> cart = new List<ShopProduct>();
        private List<Department> departments;
        readonly DataAccess data = new DataAccess();

        #endregion

        #region Constructor

        public Shop()
        {
            products = data.GetProducts();
            departments = data.GetDepartments();
            //products = returnProductsDB().Result;
            //productsTask = Task.Factory.StartNew(() => { products = data.GetProducts(); });
            //departmentsTask = Task.Factory.StartNew(() => { departments = data.GetDepartments(); });

            //Task.Factory.StartNew(() => products = returnProductsDB().Result);
        }
        #endregion

        #region Methods
        #region Cart Product Methods
        //?Add Items to Cart
        public void AddProductToCart(ShopProduct item, int quantity)
        {
            if (item != null)
            {
                var getDuplicateCheckValueTask = Task.Factory.StartNew(() => duplicateItemCheck(item, quantity));
                //var getProductTask = Task.Factory.StartNew(() => returnProduct(item));
                var isDuplicate = getDuplicateCheckValueTask.Result;
                if (!isDuplicate)
                {
                    var product = returnProduct(item);
                    //var product = getProductTask.Result;
                    if (product.Current_quantity - quantity >= 0)
                    {
                        item.Quantity = quantity;
                        cart.Add(item);
                    }
                    //!else statement unable to buy more products of this type (Stock ran out)
                }
            }
        }
        //?Edit Quantity of Product
        public void EditProductInCart(ShopProduct item, int quantity)
        {
            editQuantity(item, quantity);
        }

        //?Remove item from Cart
        public void removeItem(ShopProduct item)
        {
            if (item != null)
            {
                for (var i = 0; i < cart.Count; i++)
                {
                    if (cart[i].ID.Equals(item.ID))
                    {
                        cart.RemoveAt(i);
                        break;
                    }
                }
            }

        }
        //?Clear Cart
        public void ClearCart()
        {
            cart.Clear();
        }

        //?Return Items in Cart & Price of Items in Cart
        public void ReturnCartInfo(out int items, out double price)
        {
            items = 0;
            price = 0;
            foreach (var item in cart)
            {
                items += item.Quantity;
                price += (item.Quantity * item.Price);
            }
        }

        //?Create Receipt 
        public string CreateReceipt()
        {
            return createReceipt();
        }

        //?Update Stock Quantity
        //?Buy Items Accept Purchase
        public void BuyItems()
        {
            if (cart.Count > 0)
            {
                products = data.GetProducts();
                //products = returnProductsDB().Result;
                //products = data.GetProducts();
                foreach (var item in cart)
                {
                    foreach (var product in products)
                    {
                        if (item.ID.Equals(product.Product_id))
                        {
                            if (product.Current_quantity - item.Quantity >= 0)
                            {
                                product.Current_quantity -=
                                    item.Quantity;
                                data.UpdateProductQuantity(product.Product_id,
                                    product.Current_quantity);
                                data.AddNewIPT(product.Product_id, item.Quantity, DateTime.Now,
                                    calcProfit(product, item.Quantity));
                            }
                            break;
                            /*else
                            {
                                //!Message Display unable to complete purchase as stock 
                                break;
                            }*/
                        }
                    }
                }
                //!Message show purchase successful
            }
        }
        #endregion

        #region Product Methods
        public List<ShopProduct> ReturnProducts()
        {
            products = returnProductsDB().Result;
            //products = data.GetProducts();
            //productsTask.Wait();
            List<ShopProduct> productList = new List<ShopProduct>();
            foreach (var item in products)
            {
                ShopProduct p = new ShopProduct();
                p.ID = item.Product_id;
                p.Name = item.Product_name;
                p.Department = returnDepartmentName(item);
                p.Price = item.Sell_price;
                p.Quantity = item.Current_quantity;
                productList.Add(p);
            }
            return productList;
        }
        //?Return Items in Cart
        public List<ShopProduct> ReturnCartProducts()
        {
            List<ShopProduct> list = new List<ShopProduct>();
            foreach (var item in cart)
            {
                list.Add(item);
            }
            return list;
        }
        //?Search for specific Products
        public List<ShopProduct> SearchForItems(string value)
        {
            List<ShopProduct> list = new List<ShopProduct>();
            if (!string.IsNullOrWhiteSpace(value))
            {
                foreach (var item in ReturnProducts())
                {
                    if (item.Name.ToLower().Contains(value.ToLower()) && !string.IsNullOrWhiteSpace(value))
                    {
                        list.Add(item);
                    }
                }
                return list;
            }
            else
            {
                return null;
            }
        }

        //?Return Current Stock of products left (Maximum Amount)
        //?Returned number is the amount Left in Stock
        public decimal ReturnCurrentStock(ShopProduct item)
        {
            var returnProductTask = Task.Factory.StartNew(() => returnProduct(item));
            var product = returnProductTask.Result;
            return ToDecimal(product.Current_quantity);
        }
        #endregion

        #region Private Methods
        #region Cart Methods
        //?Check for Duplicated Items
        private bool duplicateItemCheck(ShopProduct item, int quantity)
        {
            Task<Product> getProductTask = Task.Factory.StartNew(() => returnProduct(item));
            var product = getProductTask.Result;
            for (var i = 0; i < cart.Count; i++)
            {
                if (item.Name.Equals(cart[i].Name) && item.Department.Equals(cart[i].Department))
                {
                    if (product.Current_quantity - (cart[i].Quantity + quantity) >= 0)
                    {
                        cart[i].Quantity += quantity;
                        return true;
                    }
                }
            }
            return false;
        }
        //?Edit Quantity
        private void editQuantity(ShopProduct item, int quantity)
        {
            Task<Product> getProductTask = Task.Factory.StartNew(() => returnProduct(item));
            var product = getProductTask.Result;
            for (var i = 0; i < cart.Count; i++)
            {
                if (item.Name.Equals(cart[i].Name) && item.Department.Equals(cart[i].Department))
                {
                    if (product.Current_quantity - quantity >= 0)
                    {
                        cart[i].Quantity = quantity;
                        break;
                    }
                }
            }
        }
        //?Create Receipt
        private string createReceipt()
        {
            double price = 0.00;
            string receipt = $"Receipt:\t{DateTime.Now.ToShortDateString()}\r\n" +
                             $"\nItems=========================================\r\n";
            foreach (var item in cart)
            {
                receipt += "\r\n";
                receipt += string.Format(
                    $"{item.Quantity,-10}{item.Name,-30}{item.Department,-20}{item.Price,-10:0.##}");
                receipt += "\r\n";
            }
            foreach (var item in cart)
            {
                price += (item.Quantity * item.Price);
            }

            receipt += "\r\n";
            receipt += $"Total====================================={price:0.##}";
            return receipt;
        }
        #endregion

        #region Product Methods
        //?Return product
        private Product returnProduct(ShopProduct item)
        {
            products = returnProductsDB().Result;
            //products = data.GetProducts();
            Product product = null;
            for (var i = 0; i < products.Count; i++)
            {
                if (products[i].Product_id.Equals(item.ID))
                {
                    product = products[i];
                }
            }
            return product;
        }
        //?Return Department Name
        private string returnDepartmentName(Product item)
        {
            //departmentsTask.Wait();
            foreach (var department in departments)
            {
                if (department.Department_id.Equals(item.Department_id))
                {
                    return department.Department_name;
                }
            }
            return null;
        }

        private double calcProfit(Product item, int quantity)
        {
            var profit = (item.Sell_price - item.Cost_price);
            profit *= quantity;
            return profit;
        }

        private Task<List<Product>> returnProductsDB()
        {
            return Task.Run(() => data.GetProducts());
        }


        #endregion

        #endregion

        #endregion
    }
}
