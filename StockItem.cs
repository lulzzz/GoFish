namespace GoFish
{
    public class StockItem
    {
        private StockItem () { }
        public StockItem (string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Quantity { get; private set; }

        internal void Decrease()
        {
            Quantity--;
        }
    }
}