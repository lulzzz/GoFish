namespace GoFish.Advert
{
    public class Advertiser : LookupItem
    {
        private Advertiser () {}
        public Advertiser (int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}