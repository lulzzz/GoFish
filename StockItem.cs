namespace GoFish
{
    public class StockItem
    {
        private StockItem () { }
        public StockItem (ProductType type, int quantity, double price, Dude seller)
        {
            Type = type;
            Quantity = quantity;
            Price = price;
            Seller = seller;
        }

        public int Id { get; private set; }
        public ProductType Type { get; private set; }
        public int Quantity { get; private set; }
        public double Price { get; private set; }
        public Dude Seller { get; private set; }

        internal void Decrease()
        {
            Quantity--;
        }
    }
}