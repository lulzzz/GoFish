namespace GoFish.Advert
{
    public class Advertiser
    {
        private Advertiser () {}
        public Advertiser (int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }

        internal static Advertiser FromId(int advertiserId)
        {
            return new Advertiser() { Id = advertiserId};
        }
    }
}