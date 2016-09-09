namespace GoFish
{
    public class Catch
    {
        private Catch() { }
        public Catch(CatchType type)
        {
            Type = type;
        }

        public int Id { get; private set; }
        public CatchType Type { get; private set; }
    }
}