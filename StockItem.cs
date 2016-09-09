namespace GoFish
{
    public class StockItem
    {
        private StockItem () { }
        public StockItem (ProductType type, int quantity)
        {
            Type = type;
            Quantity = quantity;
        }

        public int Id { get; private set; }
        public ProductType Type { get; private set; }
        public int Quantity { get; private set; }

        internal void Decrease()
        {
            Quantity--;
        }
    }
}