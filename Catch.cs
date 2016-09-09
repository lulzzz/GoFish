namespace GoFish
{
    public class Catch
    {
        private Catch() { }
        public Catch(CatchType type, int quantity)
        {
            Type = type;
            Quantity = quantity;
        }

        public int Id { get; private set; }
        public CatchType Type { get; private set; }
        public int Quantity { get; private set; }
    }
}