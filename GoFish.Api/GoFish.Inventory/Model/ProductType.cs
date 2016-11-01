namespace GoFish.Inventory
{
    public class ProductType : LookupItem
    {
        private ProductType() { }

        internal ProductType(int id)
        {
            Id = id;
        }

        public ProductType(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}