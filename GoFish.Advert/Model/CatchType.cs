namespace GoFish.Advert
{
    public class CatchType
    {
        private CatchType() {}
        public CatchType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
    }
}