namespace FormDesktopApp.Objects.Shop
{
    class ShopProduct
    {
        public int ID { get; protected internal set; }
        public int Quantity { get; protected internal set; }
        public string Name { get; protected internal set; }
        public double Price { get; protected internal set; }
        public string Department { get; protected internal set; }

        public override string ToString()
        {
            //return string.Format("{0,-5} | {1,-20} | {2,-10} | {3,-10}", Quantity, Name, Department, Price);
            return string.Format($"{Quantity,-5}{Name,-15}{Department,-10}{Price,-10}");
        }
    }
}