using System;
using System.Collections.Generic;
using FormDesktopApp.Objects.Enums;
using FormDesktopApp.Objects.Persons;
using FormDesktopApp.Objects.Products;
using FormDesktopApp.Objects.Departments;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormDesktopApp.Functionality.Administration
{
    class StockStatisticsAdmin
    {
        DataAccess db = new DataAccess();
        private List<IPT> soldProducts;
        private List<Product> products;

        public StockStatisticsAdmin()
        {
            //Creates a list of all items sold from the database
            soldProducts = db.GetAllIPT();
            //Creates a list of all the products in the stock from the database
            products = db.GetProducts();
        }

        //Calculates the total profit of the store
        public double GetTotalProfit()
        {
            double profit = 0;
            foreach (IPT ipt in soldProducts)
            {
                profit += ipt.Profit;
            }
            return profit;
        }

        //Gets the highest profit product of the store
        public Product GetHighestProfitItem()
        {
            if(products != null)
            {
                double profit = -9999999999;
                Product highestProfitProduct = new Product();
                foreach (Product p in products)
                {
                    if (p.Sell_price - p.Cost_price > profit)
                    {
                        profit = p.Sell_price - p.Cost_price;
                        highestProfitProduct = p;
                    }
                }
                return highestProfitProduct; 
            }
            else
            {
                return null;
            }
        }

        //Gets the lowes profit product of the store
        public Product GetLowestProfitItem()
        {
            if (products != null)
            {
                double profit = 9999999999999999;
                Product lowestProfitProduct = new Product();
                foreach (Product p in products)
                {
                    if (p.Sell_price - p.Cost_price < profit)
                    {
                        profit = p.Sell_price - p.Cost_price;
                        lowestProfitProduct = p;
                    }
                }
                return lowestProfitProduct;
            }
            else
            {
                return null;
            }
        }

        //Calculate the total number of items in the store
        public int GetTotalNumberOfItems()
        {
            int numberOfItems = 0;
            foreach (Product p in products)
            {
                numberOfItems++;
            }
            return numberOfItems;
        }

        //Gets a list of all the dates
        public List<DateTime> GetDates()
        {
            DateTime date = new DateTime(02 / 02 / 2010);
            List<DateTime> dates = new List<DateTime>();
            foreach (IPT ipt in soldProducts)
            {
                if (ipt.Date != date)
                {
                    date = ipt.Date;
                    dates.Add(date);
                }
            }
            return dates;
        }

        //Gets a list of all the months
        public List<DateTime> GetMonths()
        {
            DateTime date = new DateTime(02 / 02 / 2010);
            List<DateTime> months = new List<DateTime>();
            foreach (IPT ipt in soldProducts)
            {
                if (ipt.Date.Month != date.Month)
                {
                    date = ipt.Date;
                    months.Add(date);
                }
            }
            return months;
        }

        //Gets a list of all the years 
        public List<DateTime> GetYears()
        {
            DateTime date = new DateTime(02 / 02 / 2010);
            List<DateTime> years = new List<DateTime>();
            foreach (IPT ipt in soldProducts)
            {
                if (ipt.Date.Year != date.Year)
                {
                    date = ipt.Date;
                    years.Add(date);
                }
            }
            return years;
        }

        //Gets a specific product base on the id
        private Product GetSpecificProduct(int id)
        {
            foreach (Product p in db.GetProducts())
            {
                if (p.Product_id == id)
                {
                    return p;
                }
            }
            return null;
        }

        //Gets the total profit based on a date
        public double GetTotalDayProfitForAllDepartemnts(DateTime date)
        {
            double profit = 0;
            foreach (IPT ipt in soldProducts)
            {
                if(ipt.Date == date)
                {
                    profit += ipt.Profit;
                }
            }
            return profit;
        }

        //Gets the total profit based on a date and department
        public double GetTotalDayProfitForOneDepartment(DateTime date,Department department)
        {
            double profit = 0;
            foreach (IPT ipt in soldProducts)
            {
                if(GetSpecificProduct(ipt.Product_id) != null)
                {
                    if (ipt.Date == date && GetSpecificProduct(ipt.Product_id).Department_id == department.Department_id)
                    {
                        profit += ipt.Profit;
                    }
                }
            }
            return profit;
        }

        //Gets the total profit based on a month
        public double GetTotalMontProfitForAllDepartments(DateTime date)
        {
            double profit = 0;
            foreach (IPT ipt in soldProducts)
            {
                if (ipt.Date.Month == date.Month)
                {
                    profit += ipt.Profit;
                }              
            }
            return profit;
        }

        //Gets the total profit based on a month and department
        public double GetTotalMonthProfitForOneDepartment(DateTime date, Department departemnt)
        {
            double profit = 0;
            foreach(IPT ipt in soldProducts)
            {
               if (GetSpecificProduct(ipt.Product_id) != null)
               {
                    if (ipt.Date.Month == date.Month && GetSpecificProduct(ipt.Product_id).Department_id == departemnt.Department_id)
                    {
                        profit += ipt.Profit;
                    }
               }
            }
            return profit;
        }

        //Gets the total profit based on a year
        public double GetTotalYearProfitForAllDepartments(DateTime date)
        {
            double profit = 0;
            foreach (IPT ipt in soldProducts)
            {
                if(ipt.Date.Year == date.Year)
                {
                    profit += ipt.Profit;
                }
            }
            return profit;
        }

        //Gets the total profit based on a year and department
        public double GetTotalYearProfitForOneDepartment(DateTime date, Department department)
        {
            double profit = 0;
            foreach (IPT ipt in soldProducts)
            {
                if(GetSpecificProduct(ipt.Product_id) != null)
                {
                    if(ipt.Date.Year == date.Year && GetSpecificProduct(ipt.Product_id).Department_id == department.Department_id)
                    {
                        profit += ipt.Profit;
                    }
                }
            }
            return profit;
        }
    }

}
