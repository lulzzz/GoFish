namespace GoFish
{
    public class StockItem
    {
        public StockItem (string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}