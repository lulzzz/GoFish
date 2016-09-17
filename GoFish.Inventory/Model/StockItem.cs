namespace GoFish.Inventory
{
    public class StockItem
    {
        private StockItem () { }

        public StockItem (ProductType type, int quantity, double price, StockOwner owner)
        {
            Type = type;
            Quantity = quantity;
            Price = price;
            Owner = owner;
        }

        public int Id { get; private set; }
        public ProductType Type { get; private set; }
        public int Quantity { get; private set; }
        public double Price { get; private set; }
        public StockOwner Owner { get; private set; }
    }
}