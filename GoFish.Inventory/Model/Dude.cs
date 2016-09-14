namespace GoFish
{
    public class Dude
    {
        private Dude () {}
        public Dude (int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
    }
}