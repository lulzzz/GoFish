namespace GoFish.Inventory
{
    public class ProductType
    {
        private ProductType() {}
        internal ProductType(int id)
        {
            Id = id;
        }

        public ProductType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
    }
}