namespace GoFish.Inventory
{
    public class ProductType
    {
        private ProductType() {}
        public ProductType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
    }
}