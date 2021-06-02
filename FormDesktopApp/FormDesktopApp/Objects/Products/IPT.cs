using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormDesktopApp.Objects.Products
{
    class IPT
    {
        private int ipt_id;
        private int product_id;
        private int amount_sold;
        private DateTime date;
        private double profit;

        public IPT()
        {
        }

        public int Ipt_id { get => ipt_id; set => ipt_id = value; }
        public int Product_id { get => product_id; set => product_id = value; }
        public int Amount_sold { get => amount_sold; set => amount_sold = value; }
        public DateTime Date { get => date; set => date = value; }
        public double Profit { get => profit; set => profit = value; }
    }
}
