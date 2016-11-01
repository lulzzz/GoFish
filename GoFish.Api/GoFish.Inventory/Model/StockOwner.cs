namespace GoFish.Inventory
{
    public class StockOwner
    {
        private StockOwner() { }

        internal StockOwner(int id)
        {
            Id = id;
        }

        public StockOwner(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }

        internal static StockOwner FromId(int stockOwnerId)
        {
            return new StockOwner() { Id = stockOwnerId };
        }
    }
}