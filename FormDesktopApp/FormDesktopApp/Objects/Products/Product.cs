using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormDesktopApp.Objects.Products
{
    public class Product
    {
        private int product_id;
        private int department_id;
        private string product_name;
        private double cost_price;
        private double sell_price;
        private int current_quantity;
        private int min_quantity;
        private int max_quantity;

        public Product(int product_id, int department_id, string product_name, double cost_price, double sell_price, int current_quantity, int min_quantity, int max_quantity)
        {
            this.product_id = product_id;
            this.department_id = department_id;
            this.product_name = product_name;
            this.cost_price = cost_price;
            this.sell_price = sell_price;
            this.current_quantity = current_quantity;
            this.min_quantity = min_quantity;
            this.max_quantity = max_quantity;
        }

        public Product()
        {

        }

        public int Product_id { get => product_id; set => product_id = value; }
        public int Department_id { get => department_id; set => department_id = value; }
        public string Product_name { get => product_name; set => product_name = value; }
        public double Cost_price { get => cost_price; set => cost_price = value; }
        public double Sell_price { get => sell_price; set => sell_price = value; }
        public int Current_quantity { get => current_quantity; set => current_quantity = value; }
        public int Min_quantity { get => min_quantity; set => min_quantity = value; }
        public int Max_quantity { get => max_quantity; set => max_quantity = value; }

        public override string ToString()
        {
            return $"ID: {this.Product_id} - Name: {this.Product_name}    Current Quantity: {this.Current_quantity}";
        }
    }

    
}
