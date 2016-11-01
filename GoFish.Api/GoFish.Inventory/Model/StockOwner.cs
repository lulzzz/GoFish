namespace GoFish.Inventory
{
    public class StockOwner : LookupItem
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
    }
}