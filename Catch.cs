namespace GoFish
{
    public class Catch
    {
        private Catch() { }
        public Catch(ProductType type, int quantity)
        {
            Type = type;
            Quantity = quantity;
        }

        public int Id { get; private set; }
        public ProductType Type { get; private set; }
        public int Quantity { get; private set; }
    }
}