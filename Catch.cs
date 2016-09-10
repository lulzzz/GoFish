namespace GoFish
{
    public class Catch
    {
        private Catch() { }
        public Catch(ProductType type, int quantity, double price, Dude caughtBy)
        {
            Type = type;
            Quantity = quantity;
            Price = price;
            CaughtBy = caughtBy;
        }

        public int Id { get; private set; }
        public ProductType Type { get; private set; }
        public int Quantity { get; private set; }
        public double Price { get; private set; }
        public Dude CaughtBy { get; private set; }
    }
}